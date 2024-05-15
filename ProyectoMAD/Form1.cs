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
            picShow.Visible = false;

            try
            {
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
            catch (Exception e)
            {
                string msg = e.Message;
                MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (Form4.userWasDisabled == true) //Si el usuario estaba deshabilitado, tiene que cambiar su contraseña
                {
                    Form4 editUser = new Form4();
                    editUser.Show();
                }
            }
            else if (loginExitoso == 2)
            {
                MessageBox.Show("Se ha equivocado muchas veces seguidas. Su usuario ha sido desactivado", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ContraseñaTemporal contraseñaTemporal = new ContraseñaTemporal();
                contraseñaTemporal.Show();
            }
            else if (loginExitoso == 3)
            {
                if (MessageBox.Show("Su usuario se encuentra dado de baja. ¿Desea darse de alta nuevamente?", "ATENCIÓN", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (enlaceDB.altaUsuario(userID) == true)
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

                        MessageBox.Show("Su usuario se ha dado de alta nuevamente", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Homepage homepage = new Homepage();
                        homepage.Show();
                        this.Hide();
                    }
                }
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