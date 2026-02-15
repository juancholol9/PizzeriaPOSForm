using PosPizza.Controllers;
using PosPizza.Models;
using PosPizza.Views;

namespace PosPizza
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService;

        public LoginForm()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Por favor ingrese usuario y contraseña", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var response = await _authService.Login(txtUsuario.Text, txtPassword.Text);

                SessionManager.Token = response.Token;
                SessionManager.CurrentUser = response.Usuario;

                var productosForm = new PanelForm();
                this.Hide();
                productosForm.FormClosed += (s, args) => this.Close();
                productosForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
