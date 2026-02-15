using System;
using System.Windows.Forms;
using PosPizza.Models;
using PosPizza.Services;

namespace PosPizza.Views
{
    public partial class PanelForm : Form
    {
        private readonly ClienteService _clienteService;
        private readonly ProductoService _productoService;
        private readonly DireccionService _direccionService;
        private readonly PedidoService _pedidoService;
        private readonly UsuarioService _usuarioService;

        public PanelForm()
        {
            InitializeComponent();
            _clienteService = new ClienteService();
            _productoService = new ProductoService();
            _direccionService = new DireccionService();
            _pedidoService = new PedidoService();
            _usuarioService = new UsuarioService();

            CargarTodosLosDatos();
        }

        private async void CargarTodosLosDatos()
        {
            await CargarClientes();
            await CargarProductos();
            await CargarDirecciones();
            await CargarPedidos();
        }

        private async Task CargarClientes()
        {
            try
            {
                var clientes = await _clienteService.ObtenerTodos();
                dgvClientes.DataSource = clientes;

                // Configurar columnas
                if (dgvClientes.Columns.Count > 0)
                {
                    dgvClientes.Columns["Id"].HeaderText = "ID";
                    dgvClientes.Columns["Nombre"].HeaderText = "Nombre";
                    dgvClientes.Columns["Telefono"].HeaderText = "Teléfono";
                    dgvClientes.Columns["Email"].HeaderText = "Email";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarProductos()
        {
            try
            {
                var productos = await _productoService.ObtenerTodos();
                dgvProductos.DataSource = productos;

                // Configurar columnas
                if (dgvProductos.Columns.Count > 0)
                {
                    dgvProductos.Columns["Id"].HeaderText = "ID";
                    dgvProductos.Columns["Nombre"].HeaderText = "Nombre";
                    dgvProductos.Columns["Precio"].HeaderText = "Precio";
                    dgvProductos.Columns["Stock"].HeaderText = "Stock";
                    dgvProductos.Columns["CategoriaId"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarDirecciones()
        {
            try
            {
                var direcciones = await _direccionService.ObtenerTodas();
                dgvDirecciones.DataSource = direcciones;
                dgvDirecciones.Columns["ClienteId"].Visible = false;

                // Configurar columnas
                if (dgvDirecciones.Columns.Count > 0)
                {
                    dgvDirecciones.Columns["Id"].HeaderText = "ID";
                    dgvDirecciones.Columns["Calle"].HeaderText = "Calle";
                    dgvDirecciones.Columns["Ciudad"].HeaderText = "Ciudad";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar direcciones: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarPedidos()
        {
            try
            {
                var pedidos = await _pedidoService.ObtenerTodos();
                dgvPedidos.DataSource = pedidos;

                // Configurar columnas
                if (dgvPedidos.Columns.Count > 0)
                {
                    dgvPedidos.Columns["Id"].HeaderText = "ID";
                    dgvPedidos.Columns["ClienteId"].Visible = false;
                    dgvPedidos.Columns["DireccionId"].Visible = false;
                    dgvPedidos.Columns["UsuarioId"].Visible = false;
                    dgvPedidos.Columns["Total"].HeaderText = "Total";
                    dgvPedidos.Columns["Estado"].HeaderText = "Estado";
                    dgvPedidos.Columns["Fecha"].HeaderText = "Fecha";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar pedidos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Eventos de botones
        private void btnClientes_Click(object sender, EventArgs e)
        {
            using (var form = new ClienteForm(_clienteService))
            {
                form.ShowDialog();
                CargarClientes();
            }
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            using (var form = new ProductoForm(_productoService))
            {
                form.ShowDialog();
                CargarProductos();
            }
        }

        private void btnDirecciones_Click(object sender, EventArgs e)
        {
            using (var form = new DireccionForm(_direccionService, _clienteService))
            {
                form.ShowDialog();
                CargarDirecciones();
            }
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            using (var form = new PedidoForm(_pedidoService, _clienteService, _direccionService, _productoService))
            {
                form.ShowDialog();
                CargarPedidos();
            }
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            using (var form = new Usuarioform(_usuarioService))
            {
                form.ShowDialog();
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarTodosLosDatos();
        }

        // Eventos de doble clic en DataGridViews para ver detalles
        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnClientes_Click(sender, e);
            }
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnProductos_Click(sender, e);
            }
        }

        private void dgvDirecciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnDirecciones_Click(sender, e);
            }
        }

        private void dgvPedidos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnPedidos_Click(sender, e);
            }
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}