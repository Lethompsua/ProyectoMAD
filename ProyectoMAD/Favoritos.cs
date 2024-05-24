using System;
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
        private bool gridChapterOn { get; set; }

        public Favoritos()
        {
            InitializeComponent();

            EnlaceDB enlaceDB = new EnlaceDB();

            DataTable favs = enlaceDB.getFavs(frmLogin.userID);
            gridFavoritos.DataSource = favs;

            actualizarGrid();

            picReturn.Visible = false;
            picReturn.Enabled = false;

            gridChapterOn = false;
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

        public void actualizarGrid()
        {
            if (gridChapterOn == true)
            {
                gridFavoritos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                gridFavoritos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridFavoritos.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
            else
            {
                gridFavoritos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                gridFavoritos.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                gridFavoritos.Columns["#"].Width = 50;
                gridFavoritos.Columns["Nombre"].Width = 100;
                gridFavoritos.Columns["Libro"].Width = 100;
                gridFavoritos.Columns["Capitulo"].Width = 100;
                gridFavoritos.Columns["Texto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridChapterOn == false)
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
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Form3 consultas = Form3.GetInstance();
            consultas.Show();
            this.Close();
        }
        private void gridFavoritos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridChapterOn == false)
            {
                gridChapterOn = true;

                EnlaceDB enlaceDB = new EnlaceDB();
                int capitulo = Convert.ToInt32(gridFavoritos.CurrentRow.Cells["Capitulo"].Value);
                string libro = gridFavoritos.CurrentRow.Cells["Libro"].Value.ToString();

                DataTable chapter = enlaceDB.getChapter(libro, capitulo);
                gridFavoritos.DataSource = chapter;
                actualizarGrid();

                picReturn.Visible = true;
                picReturn.Enabled = true;
            }

        }
        private void gridFavoritos_SelectionChanged(object sender, EventArgs e)
        {
            if (gridFavoritos.CurrentRow != null && gridChapterOn == false)
            {
                id_fav = Convert.ToInt32(gridFavoritos.CurrentRow.Cells["#"].Value);
            }
        }
        private void picReturn_Click(object sender, EventArgs e)
        {
            EnlaceDB enlaceDB = new EnlaceDB();
            DataTable favs = enlaceDB.getFavs(frmLogin.userID);

            gridFavoritos.DataSource = null; //Primero vacío el datagrid para que se acomode bien
            gridFavoritos.DataSource = favs;

            picReturn.Visible = false;
            picReturn.Enabled = false;

            gridChapterOn = false;
            actualizarGrid();
        }
    }
}
