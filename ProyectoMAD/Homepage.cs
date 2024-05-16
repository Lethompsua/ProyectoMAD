using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoMAD
{
    public partial class Homepage : Form
    {
        private static Homepage instance;
        public Homepage()
        {
            InitializeComponent();
            this.FormClosed += Homepage_FormClosed;
        }

        public static Homepage GetInstance ()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Homepage();
            }
            return instance;
        }
        private void Homepage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        #region Menu
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que quiere salir?", "ATENCIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Form3.InstanceExists() == true)
            {
                Form3.CloseInstance();
            }
            if (Form4.InstanceExists() == true)
            {
                Form4.CloseInstance();
            }
            if (Consultas.InstanceExists() == true)
            {
                Consultas.CloseInstance();
            }
            if (Historial.InstanceExists() == true)
            {
                Historial.CloseInstance();
            }
            if (Favoritos.InstanceExists() == true)
            {
                Favoritos.CloseInstance();
            }

            this.Hide();
            frmLogin login = frmLogin.GetInstance();
            login.Show();
        }
        private void modificarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 editarUsuario = new Form4();
            editarUsuario.Show();
        }
        #endregion

        #region Botones PictureBox
        private void picEdit_Click(object sender, EventArgs e)
        {
            Form4 editarUsuario = Form4.GetInstance();
            editarUsuario.Show();
        }
        private void picSearch_Click(object sender, EventArgs e)
        {
            Form3 buscar = Form3.GetInstance();
            buscar.Show();
        }
        private void picFavoritos_Click(object sender, EventArgs e)
        {
            Favoritos favoritos = Favoritos.GetInstance();
            favoritos.Show();
        }
        private void picHistorial_Click(object sender, EventArgs e)
        {
            Historial historial = Historial.GetInstance();
            historial.Show();
        }
        private void picConsultas_Click(object sender, EventArgs e)
        {
            Consultas consultas = Consultas.GetInstance();
            consultas.Show();
        }
        #endregion
    }
}
