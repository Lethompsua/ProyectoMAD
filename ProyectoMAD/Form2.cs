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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
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
                string email = txtCorreo.Text; // Suponiendo que tienes un TextBox llamado txtEmail para ingresar el email
                string password = txtContrasena.Text; // Suponiendo que tienes un TextBox llamado txtPassword para ingresar la contraseña
                string nombreCompleto = txtNomCom.Text; // Suponiendo que tienes un TextBox llamado txtNombreCompleto para ingresar el nombre completo
                DateTime fechaNacimiento = DTPFechaNac.Value; // Suponiendo que tienes un DateTimePicker llamado dateTimePickerFechaNacimiento para ingresar la fecha de nacimiento
                


                int idGenero;

                if (rbMas.Checked)
                {
                    idGenero = 1; // Supongamos que el ID para género masculino es 1
                }
                else if (rbFem.Checked)
                {

                    idGenero = 0; // Supongamos que el ID para género femenino es 2

                    idGenero = 2; // Supongamos que el ID para género femenino es 2

                }
                else
                {
                    // En caso de que ningún RadioButton esté seleccionado o algo vaya mal
                    // Puedes manejarlo de acuerdo a tu lógica, por ejemplo, mostrar un mensaje de error
                    MessageBox.Show("Por favor, selecciona un género.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir del método sin continuar con el registro
                }

                string PreguntaSeguridad = cbPregunta.Text;
                string RespuestaSeguridad = txtRespuesta.Text;

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
    }
}
