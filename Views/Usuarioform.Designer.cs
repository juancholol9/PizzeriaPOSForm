namespace PosPizza.Views
{
    partial class Usuarioform
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox InputNombre;
        private TextBox InputEmail;
        private TextBox InputPassword;
        private TextBox InputRol;
        private CheckBox chkActivo;
        private Button btnGuardar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            InputNombre = new TextBox();
            InputEmail = new TextBox();
            InputPassword = new TextBox();
            InputRol = new TextBox();
            chkActivo = new CheckBox();
            btnGuardar = new Button();
            SuspendLayout();
            // 
            // InputNombre
            // 
            InputNombre.Location = new Point(20, 60);
            InputNombre.Name = "InputNombre";
            InputNombre.PlaceholderText = "Nombre de Usuario";
            InputNombre.Size = new Size(200, 23);
            InputNombre.TabIndex = 1;
            // 
            // InputEmail
            // 
            InputEmail.Location = new Point(20, 102);
            InputEmail.Name = "InputEmail";
            InputEmail.PlaceholderText = "Correo";
            InputEmail.Size = new Size(200, 23);
            InputEmail.TabIndex = 3;
            // 
            // InputPassword
            // 
            InputPassword.Location = new Point(20, 153);
            InputPassword.Name = "InputPassword";
            InputPassword.PlaceholderText = "Contraseña";
            InputPassword.Size = new Size(200, 23);
            InputPassword.TabIndex = 5;
            InputPassword.PasswordChar = '•';
            // 
            // InputRol
            // 
            InputRol.Location = new Point(20, 203);
            InputRol.Name = "InputRol";
            InputRol.PlaceholderText = "Rol";
            InputRol.Size = new Size(200, 23);
            InputRol.TabIndex = 6;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Location = new Point(20, 250);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(61, 19);
            chkActivo.TabIndex = 7;
            chkActivo.Text = "Activo";
            chkActivo.UseVisualStyleBackColor = true;
            chkActivo.Checked = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(20, 290);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(200, 35);
            btnGuardar.TabIndex = 8;
            btnGuardar.Text = "Guardar";
            btnGuardar.BackColor = Color.FromArgb(46, 204, 113);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // CrearUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(250, 350);
            Controls.Add(InputNombre);
            Controls.Add(InputEmail);
            Controls.Add(InputPassword);
            Controls.Add(InputRol);
            Controls.Add(chkActivo);
            Controls.Add(btnGuardar);
            Name = "CrearUsuario";
            Text = "Crear Usuario";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}