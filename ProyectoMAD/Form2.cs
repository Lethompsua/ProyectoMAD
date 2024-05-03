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

            //Inicializo los textbox para no tener que estar llenándolos cada vez:
            txtNomCom.Text = "Daniel Zambrano";
            txtCorreo.Text = "danyzglez@hotmail.com";
            txtContrasena.Text = "passwoRd1#";
            txtConfContrasenaña.Text = "password1";
            txtRespuesta.Text = "dslafkjadsf";
            DateTime fechaTemp = new DateTime(1990, 5, 5, 0, 0, 0);
            DTPFechaNac.Value = fechaTemp;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegUsu_Click(object sender, EventArgs e)
        {
            // Crear una instancia de la clase EnlaceDB
            EnlaceDB enlaceDB = new EnlaceDB();

            try
            {
                // Obtener los datos del usuario desde los controles del formulario
                int idGenero;
                string email = txtCorreo.Text;
                string password = txtContrasena.Text;
                string confirmarContraseña = txtConfContrasenaña.Text;
                string nombreCompleto = txtNomCom.Text;
                string PreguntaSeguridad = cbPregunta.Text;
                string RespuestaSeguridad = txtRespuesta.Text;
                DateTime fechaNacimiento = DTPFechaNac.Value;

                //Validando nombre
                string patronNombre = @"^[\p{L}\s]+$"; //Expresión Unicode que admite todos los caracteres del español
                if (Regex.IsMatch(nombreCompleto, patronNombre) == false)
                {
                    MessageBox.Show("El nombre no puede contener números ni caracteres especiales", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Validando email
                string patronEmail = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$"; //Expresión Regex para validar email
                if (Regex.IsMatch(email, patronEmail) == false)
                {
                    MessageBox.Show("El formato del email no es correcto", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Validando contraseña
                string patronContraseña = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).{8,}$"; //Expresión Regex para validar contraseña
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

                //Validando que el usuario sea mayor de 12 años
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

                //Obteniendo Género
                if (rbMas.Checked)
                {
                    idGenero = 1; // id = 1 = Masculino
                }
                else if (rbFem.Checked)
                {
                    idGenero = 0; // id = 0 = Femenino
                }
                else
                {
                    MessageBox.Show("Por favor, selecciona un género.", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Validando que haya escogido una pregunta de seguridad
                if (PreguntaSeguridad == "Selecciona una pregunta")
                {
                    MessageBox.Show("Por favor, selecciona una pregunta.", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Validando que la respuesta no este vacía
                if (RespuestaSeguridad == "")
                {
                    MessageBox.Show("Por favor, responda la pregunta de seguridad", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Llamar al método para agregar un nuevo usuario a la base de datos
                bool registroExitoso = enlaceDB.AgregarUsuario(email, password, nombreCompleto, fechaNacimiento, idGenero, PreguntaSeguridad, RespuestaSeguridad);

                if (registroExitoso)
                {
                    MessageBox.Show("Usuario registrado exitosamente.", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al registrar usuario. Por favor, inténtalo de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}