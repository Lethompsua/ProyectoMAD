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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            EnlaceDB enlaceDB = new EnlaceDB();

            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, llene los campos faltantes", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool loginExitoso = enlaceDB.Login(email, password);

            if (loginExitoso)
            {
                Homepage homepage = new Homepage();
                homepage.Show();
            }
            else
            {
                MessageBox.Show("Error al iniciar sesión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Al hacer clic en el botón, creamos una nueva instancia de la otra ventana
            Form2 IngresarUsu = new Form2();

            // Mostramos la nueva ventana
            IngresarUsu.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
