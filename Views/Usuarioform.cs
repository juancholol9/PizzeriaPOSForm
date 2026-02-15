using System;
using System.Windows.Forms;
using PosPizza.Models;
using PosPizza.Services;

namespace PosPizza.Views
{
    public partial class Usuarioform : Form
    {
        private readonly UsuarioService _usuarioService;
        private readonly UsuarioDTO _usuario;

        public Usuarioform(UsuarioService usuarioService, UsuarioDTO usuario = null)
        {
            InitializeComponent();
            _usuarioService = usuarioService;
            _usuario = usuario;

            if (_usuario != null)
            {
                InputNombre.Text = _usuario.NombreUsuario;
                InputEmail.Text = _usuario.Email;
                InputPassword.Text = ""; // No mostrar password
                InputRol.Text = _usuario.Rol;
                chkActivo.Checked = _usuario.Activo ?? true;
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(InputNombre.Text) || string.IsNullOrWhiteSpace(InputEmail.Text))
                {
                    MessageBox.Show("Usuario y Email son requeridos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_usuario == null && string.IsNullOrWhiteSpace(InputPassword.Text))
                {
                    MessageBox.Show("La contraseña es requerida para nuevos usuarios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dto = new UsuarioCreateUpdateDTO
                {
                    NombreUsuario = InputNombre.Text,
                    Email = InputEmail.Text,
                    Password = InputPassword.Text,
                    Rol = InputRol.Text,
                    Activo = chkActivo.Checked
                };

                bool result;
                if (_usuario == null)
                {
                    result = await _usuarioService.Crear(dto);
                }
                else
                {
                    result = await _usuarioService.Actualizar(_usuario.Id, dto);
                }

                if (result)
                {
                    MessageBox.Show("Usuario guardado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al guardar usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}