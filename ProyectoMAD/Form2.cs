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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            cbPregunta.Items.Add("¿Cuál es el nombre de tu mascota?");
            cbPregunta.Items.Add("¿En qué ciudad naciste?");
            cbPregunta.Items.Add("¿Cuál es el nombre de tu mejor amigo de la infancia?");
            cbPregunta.Items.Add("¿Cuál es el nombre de tu abuela materna?");
            cbPregunta.Items.Add("¿Cuál es tu comida favorita?");
            cbPregunta.Items.Add("¿Cuál es tu película favorita?");

            picShow.Visible = false;

            picHide.Click += new EventHandler(picHide_Click);
            picShow.Click += new EventHandler(picShow_Click);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegUsu_Click(object sender, EventArgs e)
        {
            EnlaceDB enlaceDB = new EnlaceDB();

            try
            {
                string genero;
                string email = txtCorreo.Text;
                string password = txtContrasena.Text;
                string confirmarContraseña = txtConfContrasenaña.Text;
                string nombreCompleto = txtNomCom.Text;
                string PreguntaSeguridad = cbPregunta.Text;
                string RespuestaSeguridad = txtRespuesta.Text;
                DateTime fechaNacimiento = DTPFechaNac.Value;

                //Validaciones
                if (string.IsNullOrEmpty(email) == true || string.IsNullOrEmpty(password) == true || string.IsNullOrEmpty(confirmarContraseña) == true ||
                    string.IsNullOrEmpty(nombreCompleto) == true || string.IsNullOrEmpty(RespuestaSeguridad) == true) {
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

                DateTime fechaActual = DateTime.Now;
                int edad = fechaActual.Year - fechaNacimiento.Year; //Diferencia de años

                //Diferencia de días
                if (fechaNacimiento > fechaActual.AddYears(-edad))
                {
                    edad--; //Si es true, aún no ha cumplido años este año
                }

                if (edad <= 12) 
                {
                    MessageBox.Show("Solo pueden registrarse personas mayores de 12 años", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (rbMas.Checked)
                {
                    genero = "Masculino";
                }
                else if (rbFem.Checked)
                {
                    genero = "Femenino";
                }
                else
                {
                    MessageBox.Show("Por favor, selecciona un género.", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (PreguntaSeguridad == "Selecciona una pregunta")
                {
                    MessageBox.Show("Por favor, selecciona una pregunta.", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (RespuestaSeguridad == "")
                {
                    MessageBox.Show("Por favor, responda la pregunta de seguridad", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool registroExitoso = enlaceDB.AgregarUsuario(email, password, nombreCompleto, fechaNacimiento, genero, PreguntaSeguridad, RespuestaSeguridad);
                if (registroExitoso == true)
                {
                    MessageBox.Show("Usuario registrado exitosamente.", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error en el formulario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void picShow_Click(object sender, EventArgs e)
        {
            txtContrasena.UseSystemPasswordChar = true;
            picShow.Visible = false;
            picHide.Visible = true;
        }

        private void picHide_Click(object sender, EventArgs e)
        {
            txtContrasena.UseSystemPasswordChar = false;
            picShow.Visible = true;
            picHide.Visible = false;
        }
    }
}