using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
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

        public static int userID {  get; set; }
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

            int loginExitoso = enlaceDB.Login(email, password);

            if (loginExitoso == 1)
            {
                Homepage homepage = new Homepage();
                homepage.Show();
                this.Hide();
            }
            else if (loginExitoso == 2)
            {
                MessageBox.Show("Se ha equivocado 3 veces seguidas. Su usuario ha sido desactivado", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ContraseñaTemporal contraseñaTemporal = new ContraseñaTemporal();
                contraseñaTemporal.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 IngresarUsu = new Form2();
            IngresarUsu.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}