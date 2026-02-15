using System;
using System.Windows.Forms;
using PosPizza.Models;
using PosPizza.Services;

namespace PosPizza.Views
{
    public partial class ProductoForm : Form
    {
        private readonly ProductoService _productoService;
        private DataGridView dgvProductos;

        public ProductoForm(ProductoService productoService)
        {
            _productoService = productoService;
            InitializeComponent();
            CargarProductos();
        }

        private void InitializeComponent()
        {
            this.Text = "Gestión de Productos";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            var lblTitle = new Label { Text = "Gestión de Productos", Font = new Font("Segoe UI", 18, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };

            var btnAgregar = new Button { Text = "Agregar Producto", Location = new Point(20, 60), Size = new Size(160, 35), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnAgregar.Click += BtnAgregar_Click;

            var btnEditar = new Button { Text = "Editar", Location = new Point(190, 60), Size = new Size(120, 35), BackColor = Color.FromArgb(52, 152, 219), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnEditar.Click += BtnEditar_Click;

            var btnEliminar = new Button { Text = "Eliminar", Location = new Point(320, 60), Size = new Size(120, 35), BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnEliminar.Click += BtnEliminar_Click;

            var btnRefresh = new Button { Text = "Actualizar", Location = new Point(450, 60), Size = new Size(120, 35), BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnRefresh.Click += (s, e) => CargarProductos();

            var btnCerrar = new Button { Text = "Cerrar", Location = new Point(840, 60), Size = new Size(120, 35), BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnCerrar.Click += (s, e) => this.Close();

            dgvProductos = new DataGridView { Location = new Point(20, 110), Size = new Size(940, 430), AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

            this.Controls.AddRange(new Control[] { lblTitle, btnAgregar, btnEditar, btnEliminar, btnRefresh, btnCerrar, dgvProductos });
        }

        private async void CargarProductos()
        {
            try
            {
                var productos = await _productoService.ObtenerTodos();
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = productos;
                dgvProductos.Columns["CategoriaId"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            using (var form = new ProductoEditForm(_productoService))
            {
                if (form.ShowDialog() == DialogResult.OK) CargarProductos();
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                var selected = dgvProductos.SelectedRows[0].DataBoundItem as ProductoDTO;
                if (selected != null && new ProductoEditForm(_productoService, selected).ShowDialog() == DialogResult.OK)
                    CargarProductos();
            }
        }

        private async void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                var selected = dgvProductos.SelectedRows[0].DataBoundItem as ProductoDTO;
                if (selected != null && MessageBox.Show($"¿Eliminar '{selected.Nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try { await _productoService.Eliminar(selected.Id); MessageBox.Show("Eliminado"); CargarProductos(); }
                    catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
                }
            }
        }
    }

    public partial class ProductoEditForm : Form
    {
        private readonly ProductoService _productoService;
        private readonly ProductoDTO? _producto;
        private readonly bool _isEditMode;
        private ComboBox cbCategoria;

        public ProductoEditForm(ProductoService productoService, ProductoDTO? producto = null)
        {
            _productoService = productoService;
            _producto = producto;
            _isEditMode = producto != null;
            InitializeComponent();
            if (_isEditMode && _producto != null) LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = _isEditMode ? "Editar Producto" : "Agregar Producto";
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            var txtNombre = new TextBox { Name = "txtNombre", Location = new Point(140, 30), Size = new Size(300, 25) };
            var txtDescripcion = new TextBox { Name = "txtDescripcion", Location = new Point(140, 70), Size = new Size(300, 60), Multiline = true };
            //var numCategoriaId = new NumericUpDown { Name = "numCategoriaId", Location = new Point(140, 140), Size = new Size(150, 25), Minimum = 1, Maximum = 9999, Value = 1 };
            cbCategoria = new ComboBox { Name = "cbCategoria", Location = new Point(140, 140), Size = new Size(300, 25), };
            CargarCategoria();
            var numPrecio = new NumericUpDown { Name = "numPrecio", Location = new Point(140, 180), Size = new Size(150, 25), DecimalPlaces = 2, Maximum = 999999, Minimum = 0 };
            var numStock = new NumericUpDown { Name = "numStock", Location = new Point(140, 220), Size = new Size(150, 25), Maximum = 999999, Minimum = 0 };
            var chkActivo = new CheckBox { Name = "chkActivo", Text = "Activo", Location = new Point(140, 260), Checked = true };

            var btnSave = new Button { Text = "Guardar", Location = new Point(140, 310), Size = new Size(120, 35), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSave.Click += async (s, e) => await SaveAsync(txtNombre.Text, txtDescripcion.Text, (int)cbCategoria.SelectedValue, numPrecio.Value, (int)numStock.Value, chkActivo.Checked);

            var btnCancel = new Button { Text = "Cancelar", Location = new Point(270, 310), Size = new Size(120, 35), BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DialogResult = DialogResult.Cancel };

            this.Controls.AddRange(new Control[] {
                new Label { Text = "Nombre:", Location = new Point(30, 30), Size = new Size(100, 20) }, txtNombre,
                new Label { Text = "Descripción:", Location = new Point(30, 70), Size = new Size(100, 20) }, txtDescripcion,
                new Label { Text = "Categoría:", Location = new Point(30, 140), Size = new Size(100, 20) }, cbCategoria,
                new Label { Text = "Precio:", Location = new Point(30, 180), Size = new Size(100, 20) }, numPrecio,
                new Label { Text = "Stock:", Location = new Point(30, 220), Size = new Size(100, 20) }, numStock,
                chkActivo, btnSave, btnCancel
            });
        }

        private async void CargarCategoria()
        {
            try
            {
                List<CategoriaDTO> categoria = await _productoService.ObtenerCategoria();
                //var DataSource = clientes.Select(x => new ClienteDTO({Nombre = x.Nombre, Id = x.Id, Email = x.Email, Telefono = x.Telefono}));
                cbCategoria.DataSource = categoria;
                cbCategoria.DisplayMember = "Nombre";
                cbCategoria.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            if (_producto != null)
            {
                (Controls.Find("txtNombre", true).First() as TextBox)!.Text = _producto.Nombre;
                (Controls.Find("txtDescripcion", true).First() as TextBox)!.Text = _producto.Descripcion ?? "";
                (Controls.Find("cbCategoria", true).First() as ComboBox)!.SelectedValue = _producto.CategoriaId;
                (Controls.Find("numPrecio", true).First() as NumericUpDown)!.Value = _producto.Precio;
                (Controls.Find("numStock", true).First() as NumericUpDown)!.Value = _producto.Stock;
                (Controls.Find("chkActivo", true).First() as CheckBox)!.Checked = _producto.Activo ?? true;
            }
        }

        private async Task SaveAsync(string nombre, string desc, int catId, decimal precio, int stock, bool activo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MessageBox.Show("Complete los campos requeridos");
                    return;
                }

                var dto = new ProductoCreateUpdateDTO { Nombre = nombre, Descripcion = desc, CategoriaId = catId, Precio = precio, Stock = stock, Activo = activo };

                if (_isEditMode && _producto != null)
                    await _productoService.Actualizar(_producto.Id, dto);
                else
                    await _productoService.Crear(dto);

                MessageBox.Show("Guardado exitosamente");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }
    }
}