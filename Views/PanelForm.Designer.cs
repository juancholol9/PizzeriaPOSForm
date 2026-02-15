namespace PosPizza.Views
{
    partial class PanelForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnClientes;
        private Button btnProductos;
        private Button btnDirecciones;
        private Button btnPedidos;
        private Button btnUsuarios;
        private Button btnRefrescar;
        private DataGridView dgvClientes;
        private DataGridView dgvProductos;
        private ComboBox CbCliente;
        private DataGridView dgvDirecciones;
        private DataGridView dgvPedidos;
        private Label lblClientes;
        private Label lblProductos;
        private Label lblDirecciones;
        private Label lblPedidos;
        private Panel panelClientes;
        private Panel panelProductos;
        private Panel panelDirecciones;
        private Panel panelPedidos;
        private Panel panelButtons;

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
            CbCliente = new ComboBox();
            btnClientes = new Button();
            btnProductos = new Button();
            btnDirecciones = new Button();
            btnPedidos = new Button();
            btnUsuarios = new Button();
            btnRefrescar = new Button();
            dgvClientes = new DataGridView();
            dgvProductos = new DataGridView();
            dgvDirecciones = new DataGridView();
            dgvPedidos = new DataGridView();
            lblClientes = new Label();
            lblProductos = new Label();
            lblDirecciones = new Label();
            lblPedidos = new Label();
            panelClientes = new Panel();
            panelProductos = new Panel();
            panelDirecciones = new Panel();
            panelPedidos = new Panel();
            panelButtons = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDirecciones).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).BeginInit();
            panelClientes.SuspendLayout();
            panelProductos.SuspendLayout();
            panelDirecciones.SuspendLayout();
            panelPedidos.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // CbCliente
            // 
            CbCliente.Location = new Point(0, 0);
            CbCliente.Name = "CbCliente";
            CbCliente.Size = new Size(121, 23);
            CbCliente.TabIndex = 0;
            // 
            // btnClientes
            // 
            btnClientes.BackColor = Color.FromArgb(52, 152, 219);
            btnClientes.Cursor = Cursors.Hand;
            btnClientes.FlatAppearance.BorderSize = 0;
            btnClientes.FlatStyle = FlatStyle.Flat;
            btnClientes.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnClientes.ForeColor = Color.White;
            btnClientes.Location = new Point(10, 10);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(140, 40);
            btnClientes.TabIndex = 0;
            btnClientes.Text = "Gestionar Clientes";
            btnClientes.UseVisualStyleBackColor = false;
            btnClientes.Click += btnClientes_Click;
            // 
            // btnProductos
            // 
            btnProductos.BackColor = Color.FromArgb(52, 152, 219);
            btnProductos.Cursor = Cursors.Hand;
            btnProductos.FlatAppearance.BorderSize = 0;
            btnProductos.FlatStyle = FlatStyle.Flat;
            btnProductos.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnProductos.ForeColor = Color.White;
            btnProductos.Location = new Point(160, 10);
            btnProductos.Name = "btnProductos";
            btnProductos.Size = new Size(140, 40);
            btnProductos.TabIndex = 1;
            btnProductos.Text = "Gestionar Productos";
            btnProductos.UseVisualStyleBackColor = false;
            btnProductos.Click += btnProductos_Click;
            // 
            // btnDirecciones
            // 
            btnDirecciones.BackColor = Color.FromArgb(52, 152, 219);
            btnDirecciones.Cursor = Cursors.Hand;
            btnDirecciones.FlatAppearance.BorderSize = 0;
            btnDirecciones.FlatStyle = FlatStyle.Flat;
            btnDirecciones.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnDirecciones.ForeColor = Color.White;
            btnDirecciones.Location = new Point(310, 10);
            btnDirecciones.Name = "btnDirecciones";
            btnDirecciones.Size = new Size(150, 40);
            btnDirecciones.TabIndex = 2;
            btnDirecciones.Text = "Gestionar Direcciones";
            btnDirecciones.UseVisualStyleBackColor = false;
            btnDirecciones.Click += btnDirecciones_Click;
            // 
            // btnPedidos
            // 
            btnPedidos.BackColor = Color.FromArgb(52, 152, 219);
            btnPedidos.Cursor = Cursors.Hand;
            btnPedidos.FlatAppearance.BorderSize = 0;
            btnPedidos.FlatStyle = FlatStyle.Flat;
            btnPedidos.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnPedidos.ForeColor = Color.White;
            btnPedidos.Location = new Point(470, 10);
            btnPedidos.Name = "btnPedidos";
            btnPedidos.Size = new Size(140, 40);
            btnPedidos.TabIndex = 3;
            btnPedidos.Text = "Gestionar Pedidos";
            btnPedidos.UseVisualStyleBackColor = false;
            btnPedidos.Click += btnPedidos_Click;
            // 
            // btnUsuarios
            // 
            btnUsuarios.BackColor = Color.FromArgb(155, 89, 182);
            btnUsuarios.Cursor = Cursors.Hand;
            btnUsuarios.FlatAppearance.BorderSize = 0;
            btnUsuarios.FlatStyle = FlatStyle.Flat;
            btnUsuarios.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnUsuarios.ForeColor = Color.White;
            btnUsuarios.Location = new Point(620, 10);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Size = new Size(140, 40);
            btnUsuarios.TabIndex = 4;
            btnUsuarios.Text = "Gestionar Usuarios";
            btnUsuarios.UseVisualStyleBackColor = false;
            btnUsuarios.Click += btnUsuarios_Click;
            // 
            // btnRefrescar
            // 
            btnRefrescar.BackColor = Color.FromArgb(46, 204, 113);
            btnRefrescar.Cursor = Cursors.Hand;
            btnRefrescar.FlatAppearance.BorderSize = 0;
            btnRefrescar.FlatStyle = FlatStyle.Flat;
            btnRefrescar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnRefrescar.ForeColor = Color.White;
            btnRefrescar.Location = new Point(770, 10);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(120, 40);
            btnRefrescar.TabIndex = 5;
            btnRefrescar.Text = "🔄 Refrescar";
            btnRefrescar.UseVisualStyleBackColor = false;
            btnRefrescar.Click += btnRefrescar_Click;
            // 
            // dgvClientes
            // 
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.BackgroundColor = Color.White;
            dgvClientes.BorderStyle = BorderStyle.None;
            dgvClientes.Location = new Point(5, 35);
            dgvClientes.MultiSelect = false;
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.Size = new Size(573, 298);
            dgvClientes.TabIndex = 1;
            dgvClientes.CellContentClick += dgvClientes_CellContentClick;
            dgvClientes.CellDoubleClick += dgvClientes_CellDoubleClick;
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.BackgroundColor = Color.White;
            dgvProductos.BorderStyle = BorderStyle.None;
            dgvProductos.Location = new Point(5, 35);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(573, 298);
            dgvProductos.TabIndex = 1;
            dgvProductos.CellDoubleClick += dgvProductos_CellDoubleClick;
            // 
            // dgvDirecciones
            // 
            dgvDirecciones.AllowUserToAddRows = false;
            dgvDirecciones.AllowUserToDeleteRows = false;
            dgvDirecciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDirecciones.BackgroundColor = Color.White;
            dgvDirecciones.BorderStyle = BorderStyle.None;
            dgvDirecciones.Location = new Point(5, 35);
            dgvDirecciones.MultiSelect = false;
            dgvDirecciones.Name = "dgvDirecciones";
            dgvDirecciones.ReadOnly = true;
            dgvDirecciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDirecciones.Size = new Size(573, 298);
            dgvDirecciones.TabIndex = 1;
            dgvDirecciones.CellDoubleClick += dgvDirecciones_CellDoubleClick;
            // 
            // dgvPedidos
            // 
            dgvPedidos.AllowUserToAddRows = false;
            dgvPedidos.AllowUserToDeleteRows = false;
            dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPedidos.BackgroundColor = Color.White;
            dgvPedidos.BorderStyle = BorderStyle.None;
            dgvPedidos.Location = new Point(5, 35);
            dgvPedidos.MultiSelect = false;
            dgvPedidos.Name = "dgvPedidos";
            dgvPedidos.ReadOnly = true;
            dgvPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPedidos.Size = new Size(573, 298);
            dgvPedidos.TabIndex = 1;
            dgvPedidos.CellDoubleClick += dgvPedidos_CellDoubleClick;
            // 
            // lblClientes
            // 
            lblClientes.AutoSize = true;
            lblClientes.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblClientes.ForeColor = Color.FromArgb(52, 73, 94);
            lblClientes.Location = new Point(5, 5);
            lblClientes.Name = "lblClientes";
            lblClientes.Size = new Size(98, 21);
            lblClientes.TabIndex = 0;
            lblClientes.Text = "📋 Clientes";
            // 
            // lblProductos
            // 
            lblProductos.AutoSize = true;
            lblProductos.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblProductos.ForeColor = Color.FromArgb(52, 73, 94);
            lblProductos.Location = new Point(5, 5);
            lblProductos.Name = "lblProductos";
            lblProductos.Size = new Size(114, 21);
            lblProductos.TabIndex = 0;
            lblProductos.Text = "🍕 Productos";
            // 
            // lblDirecciones
            // 
            lblDirecciones.AutoSize = true;
            lblDirecciones.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblDirecciones.ForeColor = Color.FromArgb(52, 73, 94);
            lblDirecciones.Location = new Point(5, 5);
            lblDirecciones.Name = "lblDirecciones";
            lblDirecciones.Size = new Size(126, 21);
            lblDirecciones.TabIndex = 0;
            lblDirecciones.Text = "📍 Direcciones";
            // 
            // lblPedidos
            // 
            lblPedidos.AutoSize = true;
            lblPedidos.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblPedidos.ForeColor = Color.FromArgb(52, 73, 94);
            lblPedidos.Location = new Point(5, 5);
            lblPedidos.Name = "lblPedidos";
            lblPedidos.Size = new Size(98, 21);
            lblPedidos.TabIndex = 0;
            lblPedidos.Text = "🛒 Pedidos";
            // 
            // panelClientes
            // 
            panelClientes.BorderStyle = BorderStyle.FixedSingle;
            panelClientes.Controls.Add(lblClientes);
            panelClientes.Controls.Add(dgvClientes);
            panelClientes.Location = new Point(10, 70);
            panelClientes.Name = "panelClientes";
            panelClientes.Size = new Size(585, 340);
            panelClientes.TabIndex = 1;
            // 
            // panelProductos
            // 
            panelProductos.BorderStyle = BorderStyle.FixedSingle;
            panelProductos.Controls.Add(lblProductos);
            panelProductos.Controls.Add(dgvProductos);
            panelProductos.Location = new Point(605, 70);
            panelProductos.Name = "panelProductos";
            panelProductos.Size = new Size(585, 340);
            panelProductos.TabIndex = 2;
            // 
            // panelDirecciones
            // 
            panelDirecciones.BorderStyle = BorderStyle.FixedSingle;
            panelDirecciones.Controls.Add(lblDirecciones);
            panelDirecciones.Controls.Add(dgvDirecciones);
            panelDirecciones.Location = new Point(10, 420);
            panelDirecciones.Name = "panelDirecciones";
            panelDirecciones.Size = new Size(585, 340);
            panelDirecciones.TabIndex = 3;
            // 
            // panelPedidos
            // 
            panelPedidos.BorderStyle = BorderStyle.FixedSingle;
            panelPedidos.Controls.Add(lblPedidos);
            panelPedidos.Controls.Add(dgvPedidos);
            panelPedidos.Location = new Point(605, 420);
            panelPedidos.Name = "panelPedidos";
            panelPedidos.Size = new Size(585, 340);
            panelPedidos.TabIndex = 4;
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.FromArgb(240, 240, 240);
            panelButtons.Controls.Add(btnClientes);
            panelButtons.Controls.Add(btnProductos);
            panelButtons.Controls.Add(btnDirecciones);
            panelButtons.Controls.Add(btnPedidos);
            panelButtons.Controls.Add(btnUsuarios);
            panelButtons.Controls.Add(btnRefrescar);
            panelButtons.Dock = DockStyle.Top;
            panelButtons.Location = new Point(0, 0);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new Padding(10);
            panelButtons.Size = new Size(1200, 60);
            panelButtons.TabIndex = 0;
            // 
            // PanelForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1200, 770);
            Controls.Add(panelButtons);
            Controls.Add(panelClientes);
            Controls.Add(panelProductos);
            Controls.Add(panelDirecciones);
            Controls.Add(panelPedidos);
            Name = "PanelForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Panel Principal - POS Pizza";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDirecciones).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).EndInit();
            panelClientes.ResumeLayout(false);
            panelClientes.PerformLayout();
            panelProductos.ResumeLayout(false);
            panelProductos.PerformLayout();
            panelDirecciones.ResumeLayout(false);
            panelDirecciones.PerformLayout();
            panelPedidos.ResumeLayout(false);
            panelPedidos.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}