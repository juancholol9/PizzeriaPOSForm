using System;
using System.Windows.Forms;
using PosPizza.Models;
using PosPizza.Services;

namespace PosPizza.Views
{
    public partial class ClienteForm : Form
    {
        private readonly ClienteService _clienteService;
        private DataGridView dgvClientes;
        private List<ClienteDTO> clientes = new();

        public ClienteForm(ClienteService clienteService)
        {
            _clienteService = clienteService;
            InitializeComponent();
            CargarClientes();
        }

        private void InitializeComponent()
        {
            this.Text = "Gestión de Clientes";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            // Title
            Label lblTitle = new Label
            {
                Text = "Gestión de Clientes",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };

            // Buttons
            Button btnAgregar = new Button
            {
                Text = "Agregar Cliente",
                Location = new Point(20, 60),
                Size = new Size(150, 35),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAgregar.Click += BtnAgregar_Click;

            Button btnEditar = new Button
            {
                Text = "Editar",
                Location = new Point(180, 60),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnEditar.Click += BtnEditar_Click;

            Button btnEliminar = new Button
            {
                Text = "Eliminar",
                Location = new Point(310, 60),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnEliminar.Click += BtnEliminar_Click;

            Button btnRefresh = new Button
            {
                Text = "Actualizar",
                Location = new Point(440, 60),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRefresh.Click += (s, e) => CargarClientes();

            Button btnCerrar = new Button
            {
                Text = "Cerrar",
                Location = new Point(740, 60),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCerrar.Click += (s, e) => this.Close();

            // DataGridView
            dgvClientes = new DataGridView
            {
                Location = new Point(20, 110),
                Size = new Size(840, 430),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            this.Controls.AddRange(new Control[] { lblTitle, btnAgregar, btnEditar, btnEliminar, btnRefresh, btnCerrar, dgvClientes });
        }

        private async void CargarClientes()
        {
            try
            {
                clientes = await _clienteService.ObtenerTodos();
                dgvClientes.DataSource = null;
                dgvClientes.DataSource = clientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            using (var form = new ClienteEditForm(_clienteService))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    CargarClientes();
                }
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                var selected = dgvClientes.SelectedRows[0].DataBoundItem as ClienteDTO;
                if (selected != null)
                {
                    using (var form = new ClienteEditForm(_clienteService, selected))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            CargarClientes();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un cliente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                var selected = dgvClientes.SelectedRows[0].DataBoundItem as ClienteDTO;
                if (selected != null)
                {
                    var result = MessageBox.Show($"¿Está seguro que desea eliminar al cliente '{selected.Nombre}'?",
                        "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            await _clienteService.Eliminar(selected.Id);
                            MessageBox.Show("Cliente eliminado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarClientes();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }

    public partial class ClienteEditForm : Form
    {
        private readonly ClienteService _clienteService;
        private readonly ClienteDTO? _cliente;
        private readonly bool _isEditMode;

        public ClienteEditForm(ClienteService clienteService, ClienteDTO? cliente = null)
        {
            _clienteService = clienteService;
            _cliente = cliente;
            _isEditMode = cliente != null;
            InitializeComponent();
            if (_isEditMode && _cliente != null)
            {
                LoadClienteData();
            }
        }

        private void InitializeComponent()
        {
            this.Text = _isEditMode ? "Editar Cliente" : "Agregar Cliente";
            this.Size = new Size(450, 300);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Nombre
            Label lblNombre = new Label
            {
                Text = "Nombre:",
                Location = new Point(30, 30),
                Size = new Size(100, 20)
            };

            TextBox txtNombre = new TextBox
            {
                Name = "txtNombre",
                Location = new Point(140, 30),
                Size = new Size(250, 25)
            };

            // Telefono
            Label lblTelefono = new Label
            {
                Text = "Teléfono:",
                Location = new Point(30, 70),
                Size = new Size(100, 20)
            };

            TextBox txtTelefono = new TextBox
            {
                Name = "txtTelefono",
                Location = new Point(140, 70),
                Size = new Size(250, 25)
            };

            // Email
            Label lblEmail = new Label
            {
                Text = "Email:",
                Location = new Point(30, 110),
                Size = new Size(100, 20)
            };

            TextBox txtEmail = new TextBox
            {
                Name = "txtEmail",
                Location = new Point(140, 110),
                Size = new Size(250, 25)
            };

            // Save Button
            Button btnSave = new Button
            {
                Text = "Guardar",
                Location = new Point(140, 170),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.Click += async (s, e) => await BtnSave_Click(txtNombre.Text, txtTelefono.Text, txtEmail.Text);

            // Cancel Button
            Button btnCancel = new Button
            {
                Text = "Cancelar",
                Location = new Point(270, 170),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange(new Control[] {
                lblNombre, txtNombre,
                lblTelefono, txtTelefono,
                lblEmail, txtEmail,
                btnSave, btnCancel
            });
        }

        private void LoadClienteData()
        {
            if (_cliente != null)
            {
                (this.Controls.Find("txtNombre", true).FirstOrDefault() as TextBox)!.Text = _cliente.Nombre;
                (this.Controls.Find("txtTelefono", true).FirstOrDefault() as TextBox)!.Text = _cliente.Telefono ?? "";
                (this.Controls.Find("txtEmail", true).FirstOrDefault() as TextBox)!.Text = _cliente.Email ?? "";
            }
        }

        private async Task BtnSave_Click(string nombre, string telefono, string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MessageBox.Show("El nombre es requerido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dto = new ClienteCreateUpdateDTO
                {
                    Nombre = nombre,
                    Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono,
                    Email = string.IsNullOrWhiteSpace(email) ? null : email
                };

                if (_isEditMode && _cliente != null)
                {
                    await _clienteService.Actualizar(_cliente.Id, dto);
                    MessageBox.Show("Cliente actualizado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await _clienteService.Crear(dto);
                    MessageBox.Show("Cliente creado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}