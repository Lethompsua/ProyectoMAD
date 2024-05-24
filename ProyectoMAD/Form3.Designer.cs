namespace ProyectoMAD
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbVersion = new System.Windows.Forms.ComboBox();
            this.cbLibro = new System.Windows.Forms.ComboBox();
            this.cbIdioma = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbTestamento = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Favorito = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnShowChap = new System.Windows.Forms.Button();
            this.btnMostrarLibro = new System.Windows.Forms.Button();
            this.btnBuscarTodLibro = new System.Windows.Forms.Button();
            this.cbCap = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnBuscarEnUnLibro = new System.Windows.Forms.Button();
            this.btnBuscarEnUnTestemento = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Palabras clave:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(640, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Version:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1249, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Libro:";
            // 
            // cbVersion
            // 
            this.cbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVersion.FormattingEnabled = true;
            this.cbVersion.Location = new System.Drawing.Point(623, 67);
            this.cbVersion.Name = "cbVersion";
            this.cbVersion.Size = new System.Drawing.Size(346, 29);
            this.cbVersion.TabIndex = 4;
            this.cbVersion.SelectedIndexChanged += new System.EventHandler(this.cbVersion_SelectedIndexChanged);
            // 
            // cbLibro
            // 
            this.cbLibro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLibro.FormattingEnabled = true;
            this.cbLibro.Location = new System.Drawing.Point(1254, 68);
            this.cbLibro.Name = "cbLibro";
            this.cbLibro.Size = new System.Drawing.Size(173, 29);
            this.cbLibro.TabIndex = 5;
            this.cbLibro.SelectedIndexChanged += new System.EventHandler(this.cbLibro_SelectedIndexChanged);
            // 
            // cbIdioma
            // 
            this.cbIdioma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIdioma.FormattingEnabled = true;
            this.cbIdioma.Location = new System.Drawing.Point(444, 67);
            this.cbIdioma.Name = "cbIdioma";
            this.cbIdioma.Size = new System.Drawing.Size(173, 29);
            this.cbIdioma.TabIndex = 9;
            this.cbIdioma.SelectedIndexChanged += new System.EventHandler(this.cbIdioma_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(440, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "Idioma:";
            // 
            // cbTestamento
            // 
            this.cbTestamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTestamento.FormattingEnabled = true;
            this.cbTestamento.Location = new System.Drawing.Point(975, 68);
            this.cbTestamento.Name = "cbTestamento";
            this.cbTestamento.Size = new System.Drawing.Size(273, 29);
            this.cbTestamento.TabIndex = 11;
            this.cbTestamento.SelectedIndexChanged += new System.EventHandler(this.cbTestamento_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(971, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 21);
            this.label6.TabIndex = 10;
            this.label6.Text = "Testamento";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1530, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(11, 67);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(427, 29);
            this.textBox1.TabIndex = 16;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Favorito});
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.Location = new System.Drawing.Point(12, 107);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 80;
            this.dataGridView1.Size = new System.Drawing.Size(1494, 578);
            this.dataGridView1.TabIndex = 36;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Favorito
            // 
            this.Favorito.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Favorito.HeaderText = "Favorito:";
            this.Favorito.MinimumWidth = 6;
            this.Favorito.Name = "Favorito";
            this.Favorito.ReadOnly = true;
            this.Favorito.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Favorito.Text = "ADD";
            this.Favorito.ToolTipText = "ADD";
            this.Favorito.UseColumnTextForButtonValue = true;
            this.Favorito.Width = 125;
            // 
            // btnShowChap
            // 
            this.btnShowChap.Location = new System.Drawing.Point(238, 691);
            this.btnShowChap.Name = "btnShowChap";
            this.btnShowChap.Size = new System.Drawing.Size(212, 73);
            this.btnShowChap.TabIndex = 28;
            this.btnShowChap.Text = "Mostrar Capítulo";
            this.btnShowChap.UseVisualStyleBackColor = true;
            this.btnShowChap.Click += new System.EventHandler(this.btnShowCap_Click);
            // 
            // btnMostrarLibro
            // 
            this.btnMostrarLibro.Location = new System.Drawing.Point(13, 691);
            this.btnMostrarLibro.Name = "btnMostrarLibro";
            this.btnMostrarLibro.Size = new System.Drawing.Size(219, 73);
            this.btnMostrarLibro.TabIndex = 29;
            this.btnMostrarLibro.Text = "Mostrar Libro";
            this.btnMostrarLibro.UseVisualStyleBackColor = true;
            this.btnMostrarLibro.Click += new System.EventHandler(this.btnMostrarLibro_Click);
            // 
            // btnBuscarTodLibro
            // 
            this.btnBuscarTodLibro.Location = new System.Drawing.Point(456, 691);
            this.btnBuscarTodLibro.Name = "btnBuscarTodLibro";
            this.btnBuscarTodLibro.Size = new System.Drawing.Size(219, 73);
            this.btnBuscarTodLibro.TabIndex = 37;
            this.btnBuscarTodLibro.Text = "Buscar en toda la biblia";
            this.btnBuscarTodLibro.UseVisualStyleBackColor = true;
            this.btnBuscarTodLibro.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cbCap
            // 
            this.cbCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCap.FormattingEnabled = true;
            this.cbCap.Location = new System.Drawing.Point(1433, 67);
            this.cbCap.Name = "cbCap";
            this.cbCap.Size = new System.Drawing.Size(73, 29);
            this.cbCap.TabIndex = 39;
            this.cbCap.SelectedIndexChanged += new System.EventHandler(this.cbCap_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1428, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 21);
            this.label5.TabIndex = 38;
            this.label5.Text = "Capitulo:";
            // 
            // BtnBuscarEnUnLibro
            // 
            this.BtnBuscarEnUnLibro.Location = new System.Drawing.Point(906, 691);
            this.BtnBuscarEnUnLibro.Name = "BtnBuscarEnUnLibro";
            this.BtnBuscarEnUnLibro.Size = new System.Drawing.Size(219, 73);
            this.BtnBuscarEnUnLibro.TabIndex = 40;
            this.BtnBuscarEnUnLibro.Text = "Buscar en capitulo";
            this.BtnBuscarEnUnLibro.UseVisualStyleBackColor = true;
            this.BtnBuscarEnUnLibro.Click += new System.EventHandler(this.BtnBuscarEnUnLibro_Click);
            // 
            // btnBuscarEnUnTestemento
            // 
            this.btnBuscarEnUnTestemento.Location = new System.Drawing.Point(681, 691);
            this.btnBuscarEnUnTestemento.Name = "btnBuscarEnUnTestemento";
            this.btnBuscarEnUnTestemento.Size = new System.Drawing.Size(219, 73);
            this.btnBuscarEnUnTestemento.TabIndex = 41;
            this.btnBuscarEnUnTestemento.Text = "Buscar en testamento";
            this.btnBuscarEnUnTestemento.UseVisualStyleBackColor = true;
            this.btnBuscarEnUnTestemento.Click += new System.EventHandler(this.btnBuscarEnUnTestemento_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.Location = new System.Drawing.Point(1438, 691);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(68, 58);
            this.btnCopy.TabIndex = 43;
            this.btnCopy.Text = "📝Copiar";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1530, 1055);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnBuscarEnUnTestemento);
            this.Controls.Add(this.BtnBuscarEnUnLibro);
            this.Controls.Add(this.cbCap);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnBuscarTodLibro);
            this.Controls.Add(this.btnMostrarLibro);
            this.Controls.Add(this.btnShowChap);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbTestamento);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbIdioma);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbLibro);
            this.Controls.Add(this.cbVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form3";
            this.Text = "Búsquedas";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbVersion;
        private System.Windows.Forms.ComboBox cbLibro;
        private System.Windows.Forms.ComboBox cbIdioma;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbTestamento;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnShowChap;
        private System.Windows.Forms.Button btnMostrarLibro;
        private System.Windows.Forms.Button btnBuscarTodLibro;
        private System.Windows.Forms.DataGridViewButtonColumn Favorito;
        private System.Windows.Forms.ComboBox cbCap;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnBuscarEnUnLibro;
        private System.Windows.Forms.Button btnBuscarEnUnTestemento;
        private System.Windows.Forms.Button btnCopy;
    }
}