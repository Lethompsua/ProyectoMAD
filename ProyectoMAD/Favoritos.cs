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


    }
}
