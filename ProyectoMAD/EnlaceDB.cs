/*
Autor: Alejandro Villarreal

LMAD

PARA EL PROYECTO ES OBLIGATORIO EL USO DE ESTA CLASE, 
EN EL SENTIDO DE QUE LOS DATOS DE CONEXION AL SERVIDOR ESTAN DEFINIDOS EN EL App.Config
Y NO TENER ESOS DATOS EN CODIGO DURO DEL PROYECTO.

NO SE PERMITE HARDCODE.

LOS MÉTODOS QUE SE DEFINEN EN ESTA CLASE SON EJEMPLOS, PARA QUE SE BASEN Y USTEDES HAGAN LOS SUYOS PROPIOS
Y DEFINAN Y PROGRAMEN TODOS LOS MÉTODOS QUE SEAN NECESARIOS PARA SU PROYECTO.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using ProyectoMAD;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Runtime.Remoting.Messaging;


/*
Se tiene que cambiar el namespace para el que usen en su proyecto
*/
namespace WindowsFormsApplication1
{
    public class EnlaceDB
    {
        static private string _aux { set; get; }
        static private SqlConnection _conexion;
        static private SqlDataAdapter _adaptador = new SqlDataAdapter();
        static private SqlCommand _comandosql = new SqlCommand();
        static private DataTable _tabla = new DataTable();
        static private DataSet _DS = new DataSet();

        public DataTable obtenertabla
        {
            get
            {
                return _tabla;
            }
        }

        private static void conectar()
        {
            /*
			Para que funcione el ConfigurationManager
			en la sección de "Referencias" de su proyecto, en el "Solution Explorer"
			dar clic al botón derecho del mouse y dar clic a "Add Reference"
			Luego elegir la opción System.Configuration
			
			tal como lo vimos en clase.
			*/
            string cnn = ConfigurationManager.ConnectionStrings["ProyectoMAD"].ToString(); 
			// Cambiar Grupo01 por el que ustedes hayan definido en el App.Confif
            _conexion = new SqlConnection(cnn);
            _conexion.Open();
        }
        private static void desconectar()
        {
            _conexion.Close();
        }

        public bool Autentificar(string us, string ps)
        {
            bool isValid = false;
            try
            {
                conectar();
                string qry = "SP_ValidaUser";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@u", SqlDbType.Char, 20);
                parametro1.Value = us;
                var parametro2 = _comandosql.Parameters.Add("@p", SqlDbType.Char, 20);
                parametro2.Value = ps;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(_tabla);

                if(_tabla.Rows.Count > 0)
                {
                    isValid = true;
                }

            }
            catch(SqlException e)
            {
                isValid = false;
            }
            finally
            {
                desconectar();
            }

            return isValid;
        }

        public DataTable get_Users()
        {
            var msg = "";
            DataTable tabla = new DataTable();
            try
            {
                conectar();
				// Ejemplo de cómo ejecutar un query, 
				// PERO lo correcto es siempre usar SP para cualquier consulta a la base de datos
                string qry = "Select Nombre, email, Fecha_modif from Usuarios where Activo = 0;";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.Text;
						// Esta opción solo la podrían utilizar si hacen un EXEC al SP concatenando los parámetros.
                _comandosql.CommandTimeout = 1200;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return tabla;
        }

		// Ejemplo de método para recibir una consulta en forma de tabla
		// Cuando el SP ejecutará un SELECT
        public DataTable get_Deptos(string opc)
        {
            var msg = "";
            DataTable tabla = new DataTable();
            try
            {
                conectar();
                string qry = "sp_Gestiona_Deptos";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Opc", SqlDbType.Char, 1);
                parametro1.Value = opc;


                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla); 
				// la ejecución del SP espera que regrese datos en formato tabla

            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return tabla;
        }
		
		// Ejemplo de método para ejecutar un SP que no se espera que regrese información, 
		// solo que ejecute ya sea un INSERT, UPDATE o DELETE
        public bool Add_Deptos(string opc, string depto)
        {
            var msg = "";
            var add = true;
            try
            {
                conectar();
                string qry = "sp_Gestiona_Deptos";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Opc", SqlDbType.Char, 1);
                parametro1.Value = opc;
                var parametro2 = _comandosql.Parameters.Add("@Nombre", SqlDbType.VarChar, 20);
                parametro2.Value = depto;

                _adaptador.InsertCommand = _comandosql;
				// También se tienen las propiedades del adaptador: UpdateCommand  y DeleteCommand
                
                _comandosql.ExecuteNonQuery();

            }
            catch (SqlException e)
            {
                add = false;
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();                
            }

            return add;
        }

        public bool AgregarUsuario(string email, string password, string nombreCompleto, DateTime fechaNacimiento, string genero, string preguntaSeguridad, string respuestaSeguridad)
        {
            bool agregado = true;
            try
            {
                conectar();
                string qry = "InsertarUsuario"; // Solo el nombre del procedimiento almacenado

                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                _comandosql.Parameters.AddWithValue("@Email", email);
                _comandosql.Parameters.AddWithValue("@Password", password);
                _comandosql.Parameters.AddWithValue("@nombre_completo", nombreCompleto);
                _comandosql.Parameters.AddWithValue("@fecha_nacimiento", fechaNacimiento);
                _comandosql.Parameters.AddWithValue("@genero", genero);
                _comandosql.Parameters.AddWithValue("@fecha_registro", DateTime.Now);
                _comandosql.Parameters.AddWithValue("@pregunta_seguridad", preguntaSeguridad);
                _comandosql.Parameters.AddWithValue("@respuesta_seguridad", respuestaSeguridad);

                _comandosql.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                string msg = e.Message;
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                agregado = false;
            }
            finally
            {
                desconectar();
            }
            return agregado;
        }

        public int Login (string email, string password)
        {
            int loginExitoso = 1;

            try
            {
                conectar();
                string qry = "VerificarLogin"; //Nombre del sp
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;

                _comandosql.Parameters.AddWithValue("@Email", email);
                _comandosql.Parameters.AddWithValue("@Password", password);

                SqlParameter id = new SqlParameter("@id", SqlDbType.SmallInt);
                id.Direction = ParameterDirection.Output;
                _comandosql.Parameters.Add(id);

                SqlParameter usuarioDesactivado = new SqlParameter("@usuarioDesactivado", SqlDbType.Bit);
                usuarioDesactivado.Direction = ParameterDirection.Output;
                _comandosql.Parameters.Add(usuarioDesactivado);

                SqlParameter estatus = new SqlParameter("@usuarioActivo", SqlDbType.Bit);
                estatus.Direction = ParameterDirection.Output;
                _comandosql.Parameters.Add(estatus);

                _comandosql.ExecuteNonQuery();

                frmLogin.userID = Convert.ToInt32(id.Value);
                bool desactivado = Convert.ToBoolean(usuarioDesactivado.Value);
                bool usuarioActivo = Convert.ToBoolean(estatus.Value);

                if (desactivado == true)
                {
                    loginExitoso = 2;
                }
                else if (usuarioActivo == false)
                {
                    loginExitoso = 3;
                }
            }
            catch (SqlException e)
            {
                //string msg = "Atención: \n";
                string msg = e.Message;
                MessageBox.Show(msg, "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                loginExitoso = 0;
            }
            finally
            {
                desconectar();
            }

            return loginExitoso;
        }

        public string getQuestion(int id)
        {
            string result = "No se encontró la pregunta";

            try
            {
                conectar();
                string qry = "SELECT dbo.GetQuestion(@id)";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.Parameters.AddWithValue("@id", id);

                result = _comandosql.ExecuteScalar().ToString();
            }
            catch (SqlException e)
            {
                string msg = e.Message;
                MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            { 
                desconectar(); 
            }

            return result;
        }

        public bool ValidarRespuesta (string respuesta, string contraseña, int id)
        {
            bool result = false;
            try
            {
                conectar();
                string qry = "spValidarRespuesta";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;

                _comandosql.Parameters.AddWithValue("@Respuesta", respuesta);
                _comandosql.Parameters.AddWithValue("@Contraseña", contraseña);
                _comandosql.Parameters.AddWithValue("@ID", id);

                _comandosql.ExecuteNonQuery();
                result = true;
            }
            catch (SqlException e)
            {
                string msg = e.Message;
                MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                desconectar();
            }

            return result;
        }

        public DataTable lastUser(int id)
        {
            DataTable tabla = new DataTable();

            try
            {
                conectar();
                string qry = "spGetEmailAndPassword";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;

                _comandosql.Parameters.AddWithValue("@id", id);

                SqlParameter email = new SqlParameter("@email", SqlDbType.VarChar, 50);
                email.Direction = ParameterDirection.Output;
                _comandosql.Parameters.Add(email);

                SqlParameter password = new SqlParameter("@password", SqlDbType.VarChar, 50);
                password.Direction = ParameterDirection.Output;
                _comandosql.Parameters.Add(password);

                _comandosql.ExecuteNonQuery();

                tabla.Columns.Add("email", typeof(string));
                tabla.Columns.Add("password", typeof(string));

                DataRow row = tabla.NewRow();
                row["email"] = _comandosql.Parameters["@email"].Value.ToString();
                row["password"] = _comandosql.Parameters["@password"].Value.ToString();
                tabla.Rows.Add(row);
            }
            catch (SqlException e)
            {
                string msg = e.Message;
                MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                desconectar();
            }

            return tabla;
        }

        public DataTable showUser(int id)
        {
            DataTable tabla = new DataTable();

            try
            {
                conectar();
                string qry = "spShowUser";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;

                _comandosql.Parameters.AddWithValue("@id", id);

                SqlParameter nombre = new SqlParameter("@nombre", SqlDbType.VarChar, 50);
                nombre.Direction = ParameterDirection.Output;
                _comandosql.Parameters.Add(nombre);

                SqlParameter email = new SqlParameter("@email", SqlDbType.VarChar, 50);
                email.Direction = ParameterDirection.Output;
                _comandosql.Parameters.Add(email);

                SqlParameter password = new SqlParameter("@password", SqlDbType.VarChar, 50);
                password.Direction = ParameterDirection.Output;
                _comandosql.Parameters.Add(password);

                SqlParameter genero = new SqlParameter("@genero", SqlDbType.VarChar, 15);
                genero.Direction = ParameterDirection.Output;
                _comandosql.Parameters.Add(genero);

                SqlParameter idioma = new SqlParameter("@idioma", SqlDbType.VarChar, 10);
                idioma.Direction = ParameterDirection.Output;
                _comandosql.Parameters.Add(idioma);

                SqlParameter tamaño = new SqlParameter("@tamaño", SqlDbType.SmallInt);
                tamaño.Direction = ParameterDirection.Output;
                _comandosql.Parameters.Add(tamaño);

                _comandosql.ExecuteNonQuery();

                tabla.Columns.Add("nombre", typeof(string));
                tabla.Columns.Add("email", typeof(string));
                tabla.Columns.Add("password", typeof(string));
                tabla.Columns.Add("genero", typeof(string));
                tabla.Columns.Add("idioma", typeof(string));
                tabla.Columns.Add("tamaño", typeof(string));

                DataRow row = tabla.NewRow();
                row["nombre"] = _comandosql.Parameters["@nombre"].Value.ToString();
                row["email"] = _comandosql.Parameters["@email"].Value.ToString();
                row["password"] = _comandosql.Parameters["@password"].Value.ToString();
                row["genero"] = _comandosql.Parameters["@genero"].Value.ToString();
                row["idioma"] = _comandosql.Parameters["@idioma"].Value.ToString();
                row["tamaño"] = _comandosql.Parameters["@tamaño"].Value.ToString();
                tabla.Rows.Add(row);
            }
            catch (SqlException e)
            {
                string msg = e.Message;
                MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                desconectar();
            }

            return tabla;
        }

        public bool deleteUser (int id)
        {
            bool result = true;
            try
            {
                conectar();
                string qry = "spDeleteUser";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;

                _comandosql.Parameters.AddWithValue("@id", id);

                _comandosql.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                string msg = e.Message;
                MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                result = false;
            }
            finally
            {
                desconectar();
            }

            return result;
        }

        public bool updateUser(int id, string nombre, string email, string password, string genero, string idioma, int tamaño)
        {
            bool result = true;

            try
            {
                conectar();
                string qry = "spUpdateUser";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;

                _comandosql.Parameters.AddWithValue("@id", id);
                _comandosql.Parameters.AddWithValue("@nombre", nombre);
                _comandosql.Parameters.AddWithValue("@email", email);
                _comandosql.Parameters.AddWithValue("@newPassword", password);
                _comandosql.Parameters.AddWithValue("@genero", genero);
                _comandosql.Parameters.AddWithValue("@idioma", idioma);
                _comandosql.Parameters.AddWithValue("@tamaño", tamaño);

                _comandosql.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                string msg = e.Message;
                MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                result = false;
            }
            finally
            {
                desconectar();
            }

            return result;
        }

        public bool altaUsuario (int id)
        {
            bool result = true;
            try
            {
                conectar();
                string qry = "spAltaUsuario";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;

                _comandosql.Parameters.AddWithValue("@id", id);
                _comandosql.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                string msg = e.Message;
                MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                result = false;
            }
            finally
            {
                desconectar();
            }

            return result;
        }
    }
}
