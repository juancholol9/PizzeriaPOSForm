using System;
using System.Windows.Forms;
using PosPizza.Models;
using PosPizza.Services;

namespace PosPizza.Views
{
    public partial class DireccionForm : Form
    {
        private readonly DireccionService _direccionService;
        private readonly ClienteService _clienteService;
        private DataGridView dgvDirecciones;

        public DireccionForm(DireccionService direccionService, ClienteService clienteService)
        {
            _direccionService = direccionService;
            _clienteService = clienteService;
            InitializeComponent();
            CargarDirecciones();
        }

        private void InitializeComponent()
        {
            this.Text = "Gestión de Direcciones";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            var lblTitle = new Label { Text = "Gestión de Direcciones", Font = new Font("Segoe UI", 18, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };
            var btnAgregar = new Button { Text = "Agregar", Location = new Point(20, 60), Size = new Size(140, 35), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnAgregar.Click += BtnAgregar_Click;
            var btnEditar = new Button { Text = "Editar", Location = new Point(170, 60), Size = new Size(120, 35), BackColor = Color.FromArgb(52, 152, 219), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnEditar.Click += BtnEditar_Click;
            var btnEliminar = new Button { Text = "Eliminar", Location = new Point(300, 60), Size = new Size(120, 35), BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnEliminar.Click += BtnEliminar_Click;
            var btnRefresh = new Button { Text = "Actualizar", Location = new Point(430, 60), Size = new Size(120, 35), BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnRefresh.Click += (s, e) => CargarDirecciones();
            var btnCerrar = new Button { Text = "Cerrar", Location = new Point(840, 60), Size = new Size(120, 35), BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnCerrar.Click += (s, e) => this.Close();

            dgvDirecciones = new DataGridView { Location = new Point(20, 110), Size = new Size(940, 430), AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

            this.Controls.AddRange(new Control[] { lblTitle, btnAgregar, btnEditar, btnEliminar, btnRefresh, btnCerrar, dgvDirecciones });
        }

        private async void CargarDirecciones()
        {
            try
            {
                var direcciones = await _direccionService.ObtenerTodas();
                dgvDirecciones.DataSource = null;
                dgvDirecciones.DataSource = direcciones;
                dgvDirecciones.Columns["ClienteId"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            using (var form = new DireccionEditForm(_direccionService, _clienteService))
            {
                if (form.ShowDialog() == DialogResult.OK) CargarDirecciones();
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvDirecciones.SelectedRows.Count > 0)
            {
                var selected = dgvDirecciones.SelectedRows[0].DataBoundItem as DireccionDTO;
                if (selected != null && new DireccionEditForm(_direccionService, _clienteService, selected).ShowDialog() == DialogResult.OK)
                    CargarDirecciones();
            }
        }

        private async void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDirecciones.SelectedRows.Count > 0)
            {
                var selected = dgvDirecciones.SelectedRows[0].DataBoundItem as DireccionDTO;
                if (selected != null && MessageBox.Show($"¿Eliminar dirección '{selected.Calle}'?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try { await _direccionService.Eliminar(selected.Id); MessageBox.Show("Eliminado"); CargarDirecciones(); }
                    catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
                }
            }
        }
    }

    public partial class DireccionEditForm : Form
    {
        private readonly DireccionService _direccionService;
        private readonly ClienteService _clienteService;
        private readonly DireccionDTO? _direccion;
        private readonly bool _isEditMode;
        private List<ClienteDTO> clientes = new();
        private ComboBox cbClienteId;

        public DireccionEditForm(DireccionService direccionService, ClienteService clienteService, DireccionDTO? direccion = null)
        {
            _direccionService = direccionService;
            _clienteService = clienteService;
            _direccion = direccion;
            _isEditMode = direccion != null;
            InitializeComponent();
            CargarClientes();
            if (_isEditMode && _direccion != null) LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = _isEditMode ? "Editar Dirección" : "Agregar Dirección";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            //var numClienteId = new NumericUpDown { Name = "numClienteId", Location = new Point(140, 30), Size = new Size(300, 25), Minimum = 1, Maximum = 9999, Value = 1 };
            cbClienteId = new ComboBox { Name = "cbClienteId", Location = new Point(140, 30), Size = new Size(300, 25), };
            CargarClientes();
            var txtCalle = new TextBox { Name = "txtCalle", Location = new Point(140, 70), Size = new Size(300, 25) };
            var txtCiudad = new TextBox { Name = "txtCiudad", Location = new Point(140, 110), Size = new Size(300, 25) };
            var txtReferencia = new TextBox { Name = "txtReferencia", Location = new Point(140, 150), Size = new Size(300, 60), Multiline = true };
            var chkActiva = new CheckBox { Name = "chkActiva", Text = "Activa", Location = new Point(140, 220), Checked = true };

            var btnSave = new Button { Text = "Guardar", Location = new Point(140, 260), Size = new Size(120, 35), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSave.Click += async (s, e) => await SaveAsync((int)cbClienteId.SelectedValue, txtCalle.Text, txtCiudad.Text, txtReferencia.Text, chkActiva.Checked);

            var btnCancel = new Button { Text = "Cancelar", Location = new Point(270, 260), Size = new Size(120, 35), BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DialogResult = DialogResult.Cancel };

            this.Controls.AddRange(new Control[] {
                new Label { Text = "Cliente:", Location = new Point(30, 30), Size = new Size(100, 20) } , cbClienteId,
                new Label { Text = "Calle:", Location = new Point(30, 70), Size = new Size(100, 20) }, txtCalle,
                new Label { Text = "Ciudad:", Location = new Point(30, 110), Size = new Size(100, 20) }, txtCiudad,
                new Label { Text = "Referencia:", Location = new Point(30, 150), Size = new Size(100, 20) }, txtReferencia,
                chkActiva, btnSave, btnCancel
            });
        }

        //private async void CargarClientes()
        //{
        //    try
        //    {
        //        clientes = await _clienteService.ObtenerTodos();
        //    }
        //    catch { }
        //}

        private async void CargarClientes()
        {
            try
            {
                List<ClienteDTO> clientes = await _clienteService.ObtenerTodos();
                //var DataSource = clientes.Select(x => new ClienteDTO({Nombre = x.Nombre, Id = x.Id, Email = x.Email, Telefono = x.Telefono}));
                cbClienteId.DataSource = clientes;
                cbClienteId.DisplayMember = "Nombre";
                cbClienteId.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            if (_direccion != null)
            {
                (Controls.Find("cbClienteId", true).First() as ComboBox)!.SelectedValue = _direccion.ClienteId;
                (Controls.Find("txtCalle", true).First() as TextBox)!.Text = _direccion.Calle;
                (Controls.Find("txtCiudad", true).First() as TextBox)!.Text = _direccion.Ciudad;
                (Controls.Find("txtReferencia", true).First() as TextBox)!.Text = _direccion.Referencia ?? "";
                (Controls.Find("chkActiva", true).First() as CheckBox)!.Checked = _direccion.Activa ?? true;
            }
        }

        private async Task SaveAsync(int clienteId, string calle, string ciudad, string referencia, bool activa)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(calle) || string.IsNullOrWhiteSpace(ciudad))
                {
                    MessageBox.Show("Complete los campos requeridos");
                    return;
                }

                var dto = new DireccionCreateUpdateDTO { ClienteId = clienteId, Calle = calle, Ciudad = ciudad, Referencia = referencia, Activa = activa };

                if (_isEditMode && _direccion != null)
                    await _direccionService.Actualizar(_direccion.Id, dto);
                else
                    await _direccionService.Crear(dto);

                MessageBox.Show("Guardado exitosamente");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }
    }
}
