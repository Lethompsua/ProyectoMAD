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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            comboIdioma.Items.Add("Español");
            comboIdioma.Items.Add("Inglés");

            comboTamaño.Items.Add("8");
            comboTamaño.Items.Add("9");
            comboTamaño.Items.Add("10");
            comboTamaño.Items.Add("11");
            comboTamaño.Items.Add("12");
            comboTamaño.Items.Add("14");
            comboTamaño.Items.Add("16");
            comboTamaño.Items.Add("18");
            comboTamaño.Items.Add("20");
            comboTamaño.Items.Add("22");
            comboTamaño.Items.Add("24");
            comboTamaño.Items.Add("26");
            comboTamaño.Items.Add("28");
            comboTamaño.Items.Add("36");
            comboTamaño.Items.Add("48");
            comboTamaño.Items.Add("72");

            picShow.Visible = false;

            EnlaceDB enlaceDB = new EnlaceDB();
            DataTable usuarioActual = new DataTable();
            usuarioActual = enlaceDB.showUser(frmLogin.userID);

            if (usuarioActual.Rows.Count > 0)
            {
                DataRow row = usuarioActual.Rows[0];
                txtName.Text = row["nombre"].ToString();
                txtEmail.Text = row["email"].ToString();
                txtPassword.Text = row["password"].ToString();

                string genero = row["genero"].ToString();

                if (genero == "Masculino") 
                { 
                    radioMasculino.Checked = true;
                }
                else if (genero == "Femenino")
                {
                    radioFemenino.Checked = true;
                }

                comboIdioma.SelectedItem = row["idioma"].ToString();
                comboTamaño.SelectedItem = row["tamaño"].ToString();
            }
            else
            {
                MessageBox.Show("No se encontró el usuario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picHide_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
            picShow.Visible = true;
            picHide.Visible = false;
        }

        private void picShow_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            picShow.Visible = false;
            picHide.Visible = true;
        }
    }
}
