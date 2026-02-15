using Newtonsoft.Json;
using PosPizza.Models;
using PosPizza.Services;
using System;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace PosPizza.Views
{
    public partial class PedidoForm : Form
    {
        private readonly PedidoService _pedidoService;
        private readonly ClienteService _clienteService;
        private readonly DireccionService _direccionService;
        private readonly ProductoService _productoService;
        private DataGridView dgvPedidos;

        public PedidoForm(PedidoService pedidoService, ClienteService clienteService, DireccionService direccionService, ProductoService productoService)
        {
            _pedidoService = pedidoService;
            _clienteService = clienteService;
            _direccionService = direccionService;
            _productoService = productoService;
            InitializeComponent();
            CargarPedidos();
        }

        private void InitializeComponent()
        {
            this.Text = "Gestión de Pedidos";
            this.Size = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            var lblTitle = new Label { Text = "Gestión de Pedidos", Font = new Font("Segoe UI", 18, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };
            var btnCrear = new Button { Text = "Crear Pedido", Location = new Point(20, 60), Size = new Size(160, 35), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnCrear.Click += BtnCrear_Click;
            var btnVer = new Button { Text = "Ver Detalles", Location = new Point(190, 60), Size = new Size(140, 35), BackColor = Color.FromArgb(52, 152, 219), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnVer.Click += BtnVer_Click;
            var btnEliminar = new Button { Text = "Eliminar", Location = new Point(340, 60), Size = new Size(120, 35), BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnEliminar.Click += BtnEliminar_Click;
            var btnRefresh = new Button { Text = "Actualizar", Location = new Point(470, 60), Size = new Size(120, 35), BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnRefresh.Click += (s, e) => CargarPedidos();
            var btnCerrar = new Button { Text = "Cerrar", Location = new Point(940, 60), Size = new Size(120, 35), BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnCerrar.Click += (s, e) => this.Close();
            dgvPedidos = new DataGridView { Location = new Point(20, 110), Size = new Size(1040, 430), AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

            this.Controls.AddRange(new Control[] { lblTitle, btnCrear, btnVer, btnEliminar, btnRefresh, btnCerrar, dgvPedidos });
        }

        private async void CargarPedidos()
        {
            try
            {
                var pedidos = await _pedidoService.ObtenerTodos();
                dgvPedidos.DataSource = null;
                dgvPedidos.DataSource = pedidos;
                dgvPedidos.Columns["ClienteId"].Visible = false;
                dgvPedidos.Columns["DireccionId"].Visible = false;
                dgvPedidos.Columns["UsuarioId"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }

        private void BtnCrear_Click(object sender, EventArgs e)
        {
            using (var form = new PedidoCreateForm(_pedidoService, _clienteService, _direccionService, _productoService))
            {
                if (form.ShowDialog() == DialogResult.OK) CargarPedidos();
            }
        }

        private async void BtnVer_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.SelectedRows.Count > 0)
            {
                var selected = dgvPedidos.SelectedRows[0].DataBoundItem as PedidoDTO;
                if (selected != null)
                {
                    try
                    {
                        var pedido = await _pedidoService.ObtenerPorId(selected.Id);
                        if (pedido != null) new PedidoDetailForm(pedido).ShowDialog();
                    }
                    catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
                }
            }
        }

        private async void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.SelectedRows.Count > 0)
            {
                var selected = dgvPedidos.SelectedRows[0].DataBoundItem as PedidoDTO;
                if (selected != null && MessageBox.Show($"¿Eliminar pedido #{selected.Id}?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try { await _pedidoService.Eliminar(selected.Id); MessageBox.Show("Eliminado"); CargarPedidos(); }
                    catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
                }
            }
        }
    }

    public partial class PedidoCreateForm : Form
    {
        private readonly PedidoService _pedidoService;
        private readonly ClienteService _clienteService;
        private readonly DireccionService _direccionService;
        private readonly ProductoService _productoService;
        private List<ClienteDTO> clientes = new();
        private List<ProductoDTO> productos = new();
        private List<PedidoDetalleCreateUpdateDTO> detalles = new();
        private DataGridView dgvDetalles;
        private ComboBox cbClienteId;
        private ComboBox cbDireecionId;

        public PedidoCreateForm(PedidoService pedidoService, ClienteService clienteService, DireccionService direccionService, ProductoService productoService)
        {
            _pedidoService = pedidoService;
            _clienteService = clienteService;
            _direccionService = direccionService;
            _productoService = productoService;
            InitializeComponent();
            CargarDatos();
        }

        private void InitializeComponent()
        {
            this.Text = "Crear Nuevo Pedido";
            this.Size = new Size(800, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            //dgvPedidos = new DataGridView { Location = new Point(20, 110), Size = new Size(1040, 430), AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

            //numClienteId.Items.AddRange(new object[] { _clienteService.ObtenerTodos()});
            cbClienteId = new ComboBox { Name = "cbClienteId", Location = new Point(140, 30), Size = new Size(300, 25), };
            cbDireecionId = new ComboBox { Name = "cbDireecionId", Location = new Point(140, 70), Size = new Size(300, 25), };
            CargarClientes();
            CargarDireccion();
            //List<ClienteDTO> listacliente = _clienteService.ObtenerTodos();
            //DataTable dt = _clienteService.ObtenerTodos();
            //numClienteId.DataSource = _clienteService.ObtenerTodos(); ;
            //numClienteId.DisplayMember = "Nombre";
            //numClienteId.ValueMember = "Id";
            //var numDireccionId = new NumericUpDown { Name = "numDireccionId", Location = new Point(140, 70), Size = new Size(300, 25), Minimum = 1, Maximum = 9999, Value = 1 };
            var cmbEstado = new ComboBox { Name = "cmbEstado", Location = new Point(140, 110), Size = new Size(300, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbEstado.Items.AddRange(new object[] { "Pendiente", "En Preparación", "En Camino", "Entregado", "Cancelado" });
            cmbEstado.SelectedIndex = 0;

            var lblDetalles = new Label { Text = "Detalles del Pedido:", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(20, 160), AutoSize = true };
            var numProductoId = new NumericUpDown { Name = "numProductoId", Location = new Point(20, 190), Size = new Size(100, 25), Minimum = 1, Maximum = 9999, Value = 1 };
            var numCantidad = new NumericUpDown { Name = "numCantidad", Location = new Point(130, 190), Size = new Size(80, 25), Minimum = 1, Maximum = 9999, Value = 1 };
            var numPrecio = new NumericUpDown { Name = "numPrecio", Location = new Point(220, 190), Size = new Size(120, 25), DecimalPlaces = 2, Minimum = 0, Maximum = 99999, Value = 0 };
            var btnAddDetalle = new Button { Text = "Agregar", Location = new Point(350, 188), Size = new Size(100, 28), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnAddDetalle.Click += BtnAddDetalle_Click;

            dgvDetalles = new DataGridView { Location = new Point(20, 230), Size = new Size(740, 300), AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductoId", HeaderText = "Producto ID" });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", HeaderText = "Cantidad" });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrecioUnitario", HeaderText = "Precio Unit." });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { Name = "SubTotal", HeaderText = "SubTotal" });

            var btnRemoveDetalle = new Button { Text = "Quitar", Location = new Point(20, 540), Size = new Size(100, 30), BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnRemoveDetalle.Click += BtnRemoveDetalle_Click;

            var lblTotal = new Label { Name = "lblTotal", Text = "Total: L. 0.00", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(550, 545), AutoSize = true };

            var btnSave = new Button { Text = "Crear Pedido", Location = new Point(420, 600), Size = new Size(150, 40), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSave.Click += async (s, e) => await SaveAsync();
            var btnCancel = new Button { Text = "Cancelar", Location = new Point(580, 600), Size = new Size(150, 40), BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DialogResult = DialogResult.Cancel };

            this.Controls.AddRange(new Control[] {
                new Label { Text = "Cliente:", Location = new Point(30, 30), Size = new Size(100, 20) }, cbClienteId,
                new Label { Text = "Dirección:", Location = new Point(30, 70), Size = new Size(100, 20) }, cbDireecionId,
                new Label { Text = "Estado:", Location = new Point(30, 110), Size = new Size(100, 20) }, cmbEstado,
                lblDetalles,
                new Label { Text = "Producto:", Location = new Point(20, 170), Size = new Size(100, 20) }, numProductoId,
                new Label { Text = "Cant:", Location = new Point(130, 170), Size = new Size(50, 20) }, numCantidad,
                new Label { Text = "Precio:", Location = new Point(220, 170), Size = new Size(100, 20) }, numPrecio,
                btnAddDetalle, dgvDetalles, btnRemoveDetalle, lblTotal, btnSave, btnCancel
            });
        }


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

        private async void CargarDireccion()
        {
            try
            {
                List<DireccionDTO> direccion = await _direccionService.ObtenerTodas();
                //var DataSource = clientes.Select(x => new ClienteDTO({Nombre = x.Nombre, Id = x.Id, Email = x.Email, Telefono = x.Telefono}));
                cbDireecionId.DataSource = direccion;
                cbDireecionId.DisplayMember = "Referencia";
                cbDireecionId.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private async void CargarDatos()
        {
            try
            {
                clientes = await _clienteService.ObtenerTodos();
                productos = await _productoService.ObtenerTodos();
            }
            catch { }
        }

        private void BtnAddDetalle_Click(object sender, EventArgs e)
        {
            var numProductoId = Controls.Find("numProductoId", true).First() as NumericUpDown;
            var numCantidad = Controls.Find("numCantidad", true).First() as NumericUpDown;
            var numPrecio = Controls.Find("numPrecio", true).First() as NumericUpDown;

            if (numProductoId != null && numCantidad != null && numPrecio != null)
            {
                int productoId = (int)numProductoId.Value;
                int cantidad = (int)numCantidad.Value;
                decimal precio = numPrecio.Value;
                decimal subtotal = cantidad * precio;

                detalles.Add(new PedidoDetalleCreateUpdateDTO { ProductoId = productoId, Cantidad = cantidad, PrecioUnitario = precio });
                dgvDetalles.Rows.Add(productoId, cantidad, precio, subtotal);
                UpdateTotal();
            }
        }

        private void BtnRemoveDetalle_Click(object sender, EventArgs e)
        {
            if (dgvDetalles.SelectedRows.Count > 0)
            {
                var index = dgvDetalles.SelectedRows[0].Index;
                dgvDetalles.Rows.RemoveAt(index);
                detalles.RemoveAt(index);
                UpdateTotal();
            }
        }

        private void UpdateTotal()
        {
            var total = detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
            var lblTotal = Controls.Find("lblTotal", true).First() as Label;
            if (lblTotal != null) lblTotal.Text = $"Total: L. {total:N2}";
        }

        private async Task SaveAsync()
        {
            try
            {
                var cbClienteId = Controls.Find("cbClienteId", true).First() as ComboBox;
                var cbDireecionId = Controls.Find("cbDireecionId", true).First() as ComboBox;
                var cmbEstado = Controls.Find("cmbEstado", true).First() as ComboBox;

                if (cbClienteId == null || cbDireecionId == null || !detalles.Any())
                {
                    MessageBox.Show("Complete todos los campos y agregue al menos un producto");
                    return;
                }

                var dto = new PedidoCreateUpdateDTO
                {
                    ClienteId = Convert.ToInt32(cbClienteId.SelectedValue),
                    DireccionId = Convert.ToInt32(cbDireecionId.SelectedValue),
                    UsuarioId = SessionManager.CurrentUser?.Id ?? 1,
                    Estado = cmbEstado?.SelectedItem?.ToString(),
                    Detalles = detalles
                };

                await _pedidoService.Crear(dto);
                MessageBox.Show("Pedido creado exitosamente");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }
    }

    public partial class PedidoDetailForm : Form
    {
        private readonly PedidoDetailDTO _pedido;

        public PedidoDetailForm(PedidoDetailDTO pedido)
        {
            _pedido = pedido;
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = $"Detalles del Pedido #{_pedido.Id}";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            var lblTitle = new Label { Text = $"Pedido #{_pedido.Id}", Font = new Font("Segoe UI", 18, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };
            var txtInfo = new TextBox { Name = "txtInfo", Location = new Point(20, 70), Size = new Size(740, 120), Multiline = true, ReadOnly = true, BackColor = Color.White };
            var dgvDetalles = new DataGridView { Name = "dgvDetalles", Location = new Point(20, 210), Size = new Size(740, 280), AllowUserToAddRows = false, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            var lblTotal = new Label { Name = "lblTotal", Text = $"Total: L. {_pedido.Total:N2}", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(550, 500), AutoSize = true };
            var btnClose = new Button { Text = "Cerrar", Location = new Point(650, 520), Size = new Size(110, 35), BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnClose.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblTitle, txtInfo, new Label { Text = "Productos:", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(20, 185), AutoSize = true }, dgvDetalles, lblTotal, btnClose });
        }

        private void LoadData()
        {
            var info = $"Cliente: {_pedido.ClienteId}\n";
            info += $"Dirección: {_pedido.DireccionId}\n";
            info += $"Usuario: {_pedido.UsuarioId}\n";
            info += $"Fecha: {_pedido.Fecha:dd/MM/yyyy HH:mm}\n";
            info += $"Estado: {_pedido.Estado}";

            var txtInfo = Controls.Find("txtInfo", true).First() as TextBox;
            if (txtInfo != null) txtInfo.Text = info;

            var dgv = Controls.Find("dgvDetalles", true).First() as DataGridView;
            if (dgv != null)
            {
                dgv.DataSource = _pedido.Detalles.Select(d => new
                {
                    ProductoId = d.ProductoId,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    SubTotal = d.SubTotal
                }).ToList();
            }
        }
    }
}
