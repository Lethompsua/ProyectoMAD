using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que quiere darse de baja?", "ATENCIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                EnlaceDB enlaceDB = new EnlaceDB();
                if (enlaceDB.deleteUser(frmLogin.userID) == true)
                {
                    MessageBox.Show("El usuario ha sido dado de baja", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Application.Exit();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            EnlaceDB enlaceDB = new EnlaceDB();

            try
            {
                string genero;
                string email = txtEmail.Text;
                string password = txtPassword.Text;
                string confirmarContraseña = txtConfirmar.Text;
                string nombreCompleto = txtName.Text;
                string idioma = comboIdioma.Text;
                string tamaño = comboTamaño.Text;

                //Validaciones
                if (string.IsNullOrEmpty(email) == true || string.IsNullOrEmpty(password) == true || string.IsNullOrEmpty(confirmarContraseña) == true ||
                    string.IsNullOrEmpty(nombreCompleto) == true)
                {
                    MessageBox.Show("Por favor, llene los campos faltantes", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string patronNombre = @"^[\p{L}\s]+$"; //Expresión Unicode que admite todos los caracteres del español
                if (Regex.IsMatch(nombreCompleto, patronNombre) == false)
                {
                    MessageBox.Show("El nombre no puede contener números ni caracteres especiales", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string patronEmail = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
                if (Regex.IsMatch(email, patronEmail) == false)
                {
                    MessageBox.Show("El formato del email no es correcto", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string patronContraseña = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).{8,}$";
                // 8 caracteres, una mayúscula, una minúscula y un caracter especial
                if (Regex.IsMatch(password, patronContraseña) == false)
                {
                    MessageBox.Show("La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula" +
                        " y un caracter especial", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (confirmarContraseña != password)
                {
                    MessageBox.Show("Las contraseñas no coinciden", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (radioMasculino.Checked)
                {
                    genero = "Masculino";
                }
                else if (radioFemenino.Checked)
                {
                    genero = "Femenino";
                }
                else
                {
                    MessageBox.Show("Por favor, selecciona un género.", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (enlaceDB.updateUser(frmLogin.userID, nombreCompleto, email, password, genero, idioma, Convert.ToInt32(tamaño)) == true)
                {
                    MessageBox.Show("El usuario ha sido actualizado", "Actualización Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error en el formulario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
