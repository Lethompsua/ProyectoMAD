﻿namespace ProyectoMAD
{
    partial class Historial
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
            this.checkAll = new System.Windows.Forms.CheckBox();
            this.comboMeses = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboAños = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gridHistory = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistory)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkAll
            // 
            this.checkAll.AutoSize = true;
            this.checkAll.Location = new System.Drawing.Point(449, 136);
            this.checkAll.Name = "checkAll";
            this.checkAll.Size = new System.Drawing.Size(146, 25);
            this.checkAll.TabIndex = 0;
            this.checkAll.Text = "Toda la historia";
            this.checkAll.UseVisualStyleBackColor = true;
            // 
            // comboMeses
            // 
            this.comboMeses.FormattingEnabled = true;
            this.comboMeses.Location = new System.Drawing.Point(659, 136);
            this.comboMeses.Name = "comboMeses";
            this.comboMeses.Size = new System.Drawing.Size(92, 29);
            this.comboMeses.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(655, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 21);
            this.label2.TabIndex = 18;
            this.label2.Text = "Mes";
            // 
            // comboAños
            // 
            this.comboAños.FormattingEnabled = true;
            this.comboAños.Location = new System.Drawing.Point(777, 136);
            this.comboAños.Name = "comboAños";
            this.comboAños.Size = new System.Drawing.Size(92, 29);
            this.comboAños.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(773, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 21);
            this.label1.TabIndex = 20;
            this.label1.Text = "Año";
            // 
            // gridHistory
            // 
            this.gridHistory.AllowUserToAddRows = false;
            this.gridHistory.AllowUserToDeleteRows = false;
            this.gridHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridHistory.Location = new System.Drawing.Point(12, 202);
            this.gridHistory.MultiSelect = false;
            this.gridHistory.Name = "gridHistory";
            this.gridHistory.ReadOnly = true;
            this.gridHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridHistory.Size = new System.Drawing.Size(1208, 415);
            this.gridHistory.TabIndex = 22;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(1226, 202);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(85, 60);
            this.btnDelete.TabIndex = 23;
            this.btnDelete.Text = "Eliminar registro";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightCoral;
            this.button2.Location = new System.Drawing.Point(12, 659);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 56);
            this.button2.TabIndex = 24;
            this.button2.Text = "Borrar todo";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1333, 24);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(503, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(311, 35);
            this.label3.TabIndex = 26;
            this.label3.Text = "Historial de búsquedas";
            // 
            // Historial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 727);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.gridHistory);
            this.Controls.Add(this.comboAños);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboMeses);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkAll);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Historial";
            this.Text = "Historial";
            ((System.ComponentModel.ISupportInitialize)(this.gridHistory)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkAll;
        private System.Windows.Forms.ComboBox comboMeses;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboAños;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridHistory;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.Label label3;
    }
}