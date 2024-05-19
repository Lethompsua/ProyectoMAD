﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace ProyectoMAD
{
    public partial class Favoritos : Form
    {
        private static Favoritos instance;
        private int id_fav {  get; set; }
        public Favoritos()
        {
            InitializeComponent();

            EnlaceDB enlaceDB = new EnlaceDB();

            DataTable favs = enlaceDB.getFavs(frmLogin.userID);
            gridFavoritos.DataSource = favs;
            gridFavoritos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            gridFavoritos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public static Favoritos GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Favoritos();
            }

            return instance;
        }

        #region Métodos para el manejo de ventanas
        public static bool InstanceExists()
        {
            return (instance != null && !instance.IsDisposed);
        }
        public static void CloseInstance()
        {
            if (instance != null && !instance.IsDisposed)
            {
                instance.Close();
            }
        }
        #endregion

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridFavoritos.Rows.Count == 0)
            {
                MessageBox.Show("Su lista de favoritos se encuentra vacía", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (gridFavoritos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("No ha seleccionado ningún favorito", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("¿Está seguro de que desea eliminar este favorito?", "ATENCIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                        == DialogResult.Yes)
                    {
                        EnlaceDB enlaceDB = new EnlaceDB();
                        if (enlaceDB.deleteFav(id_fav) == true)
                        {
                            gridFavoritos.Rows.RemoveAt(gridFavoritos.CurrentRow.Index);
                            MessageBox.Show("Se ha eliminado el favorito", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }
    }
}
