using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
        public static int userID {  get; set; }
        public frmLogin()
        {
            InitializeComponent();
            string lastID = ConfigurationManager.AppSettings["RememberUserId"];

            if (int.TryParse(lastID, out int id) == true)
            {
                userID = id;
            }
            else
            {
                userID = 0;
            }

            if (userID != 0)
            {
                EnlaceDB enlaceDB = new EnlaceDB();
                DataTable lastUser = new DataTable();
                lastUser = enlaceDB.lastUser(userID);

                if (lastUser.Rows.Count > 0)
                {
                    DataRow row = lastUser.Rows[0];
                    txtEmail.Text = row["email"].ToString();
                    txtPassword.Text = row["password"].ToString();
                    checkboxRemember.Checked = true;
                }
                else
                {
                    MessageBox.Show("No se encontró el usuario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
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

            int loginExitoso = enlaceDB.Login(email, password);

            if (loginExitoso == 1)
            {
                if (checkboxRemember.Checked == true)
                {
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None); //Abre el archivo app.config
                    config.AppSettings.Settings["RememberUserId"].Value = userID.ToString();
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings"); //Actualiza los cambios
                }
                else
                {
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None); //Abre el archivo app.config
                    config.AppSettings.Settings["RememberUserId"].Value = "0";
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings"); //Actualiza los cambios
                }

                Homepage homepage = new Homepage();
                homepage.Show();
                this.Hide();
            }
            else if (loginExitoso == 2)
            {
                MessageBox.Show("Se ha equivocado muchas veces seguidas. Su usuario ha sido desactivado", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
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