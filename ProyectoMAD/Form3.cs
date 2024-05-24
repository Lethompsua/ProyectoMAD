using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;

namespace ProyectoMAD
{
    public partial class Form3 : Form
    {
        private static Form3 instance;
        public Form3()
        {

            InitializeComponent();
            CargarIdiomas();
            ConfigurarDataGridView();
        }
        public static Form3 GetInstance() //Singleton
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Form3();
            }
            return instance;
        }

        #region Métodos para el manejo de ventanas
        public static bool InstanceExists()
        {
            return (instance != null && !instance.IsDisposed);
        }
        public static void CloseInstance()
        {
            if (instance != null && !instance.IsDisposed)
            {
                instance.Close();
            }
        }
        #endregion

        private void ConfigurarDataGridView()
        {
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            if (dataGridView1.Columns["Cita"] == null)
            {
                DataGridViewTextBoxColumn citaColumn = new DataGridViewTextBoxColumn();
                citaColumn.Name = "Cita";
                citaColumn.HeaderText = "Cita";
                citaColumn.DataPropertyName = "Cita";
                citaColumn.Width = 200;
                dataGridView1.Columns.Add(citaColumn);
            }
            if (dataGridView1.Columns["Texto"] == null)
            {
                DataGridViewTextBoxColumn textoColumn = new DataGridViewTextBoxColumn();
                textoColumn.Name = "Texto";
                textoColumn.HeaderText = "Texto";
                textoColumn.DataPropertyName = "Texto";
                textoColumn.Width = dataGridView1.Width - 200;
                textoColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.Columns.Add(textoColumn);
            }
        }

        #region Comboboxes
        private void CargarIdiomas()
        {
            EnlaceDB enlaceDB = new EnlaceDB();
            DataTable idiomas = enlaceDB.ObtenerIdiomas();

            if (idiomas.Rows.Count == 0)
            {
                MessageBox.Show("Lo sentimos. No se ha encontrado ningún idioma", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cbIdioma.DataSource = idiomas;
            cbIdioma.DisplayMember = "Nombre";
            cbIdioma.ValueMember = "Id_Idioma";
        }
        private void cbIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombreIdioma = cbIdioma.Text;

            if (string.IsNullOrEmpty(nombreIdioma))
            {
                cbVersion.DataSource = null;
                cbTestamento.DataSource = null;
                cbLibro.DataSource = null;
                return;
            }

            EnlaceDB enlaceDB = new EnlaceDB();
            DataTable versiones = enlaceDB.ObtenerVersionesPorNombreIdioma(nombreIdioma);

            if (versiones.Rows.Count == 0 && nombreIdioma != "System.Data.DataRowView")
            {
                MessageBox.Show("Lo sentimos. No se ha encontrado ninguna versión para este idioma", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            cbVersion.DataSource = null;
            cbTestamento.DataSource = null;
            cbLibro.DataSource = null;

            cbVersion.DataSource = versiones;
            cbVersion.DisplayMember = "NombreVersion";
            cbVersion.ValueMember = "Id_Version";
        }
        private void cbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombreIdioma = cbIdioma.Text;
            string nombreVersion = cbVersion.Text;

            if (string.IsNullOrEmpty(nombreVersion))
            {
                cbTestamento.DataSource = null;
                cbLibro.DataSource = null;
                return;
            }

            EnlaceDB enlaceDB = new EnlaceDB();
            DataTable testamentos = enlaceDB.ObtenerTestamentosPorNombreVersion(nombreIdioma, nombreVersion);

            if (testamentos.Rows.Count == 0 && nombreVersion != "System.Data.DataRowView")
            {
                MessageBox.Show("Lo sentimos. No se ha encontrado ningún testamento para esta versión", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            cbTestamento.DataSource = null;
            cbLibro.DataSource = null;

            cbTestamento.DataSource = testamentos;
            cbTestamento.DisplayMember = "Nombre";
            cbTestamento.ValueMember = "Id_Testamento";
        }
        private void cbTestamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombreTestamento = cbTestamento.Text;

            if (string.IsNullOrEmpty(nombreTestamento))
            {
                cbLibro.DataSource = null;
                return;
            }

            EnlaceDB enlaceDB = new EnlaceDB();
            DataTable libros = enlaceDB.ObtenerLibrosPorNombreTestamento(nombreTestamento);

            if (libros.Rows.Count == 0 && nombreTestamento != "System.Data.DataRowView")
            {
                MessageBox.Show("Lo sentimos. No se ha encontrado ningún libro para este testamento", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            cbLibro.DataSource = libros;
            cbLibro.DisplayMember = "Nombre";
            cbLibro.ValueMember = "Id_Libro";
        }
        private void cbLibro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLibro.SelectedValue != null)
            {
                DataRowView selectedRow = (DataRowView)cbLibro.SelectedItem;
                int idLibro = Convert.ToInt32(selectedRow["Id_Libro"]);

                try
                {
                    CargarCapitulos(idLibro);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los capítulos: " + ex.Message);
                }
            }
        }
        private void CargarCapitulos(int idLibro)
        {
            EnlaceDB enlaceDB = new EnlaceDB();
            DataTable tablaCapitulos = enlaceDB.ObtenerCapitulosPorLibro(idLibro);

            if (tablaCapitulos != null && tablaCapitulos.Rows.Count > 0)
            {
                cbCap.DataSource = null;

                List<int> capitulos = new List<int>();
                foreach (DataRow fila in tablaCapitulos.Rows)
                {
                    int numeroCapitulo = Convert.ToInt32(fila["NumeroCap"]);
                    capitulos.Add(numeroCapitulo);
                }

                // Establecer la lista de capítulos como origen de datos y refrescar el ComboBox
                cbCap.DataSource = capitulos;
                cbCap.Refresh(); // o cb_Cap.Invalidate();
            }
            else
            {
                MessageBox.Show("No se encontraron capítulos para este libro.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCap.DataSource = null;
                return;
            }
        }
        private void cbCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void LimpiarComboBoxes()
        {
            cbLibro.SelectedIndex = -1;
            cbIdioma.SelectedIndex = -1;
            cbVersion.SelectedIndex = -1;
            cbTestamento.SelectedIndex = -1;
        }
        #endregion

        private void btnMostrarLibro_Click(object sender, EventArgs e)
        {
            // Verificar que los ComboBoxes no estén vacíos
            if (cbLibro.SelectedItem == null || cbIdioma.SelectedItem == null || cbVersion.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona todos los campos requeridos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el nombre del libro seleccionado
            string nombreLibro = cbLibro.Text;
            string version = cbVersion.Text;

            try
            {
                EnlaceDB enlaceDB = new EnlaceDB();
                DataTable versiculos = enlaceDB.ObtenerVersiculosPorNombreLibro(nombreLibro, version);

                if (versiculos.Rows.Count == 0)
                {
                    MessageBox.Show("Lo sentimos. No se encontraron versículos para este libro", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //LimpiarComboBoxes();

                ConfigurarDataGridView();

                dataGridView1.DataSource = versiculos;
                dataGridView1.Columns["Cita"].DataPropertyName = "Cita";
                dataGridView1.Columns["Texto"].DataPropertyName = "Texto";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar el libro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string palabraBuscar = textBox1.Text;
            string version = cbVersion.Text;

            if (string.IsNullOrEmpty(palabraBuscar))
            {
                MessageBox.Show("Por favor, ingresa una palabra o frase para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(version))
            {
                MessageBox.Show("Por favor, selecciona la versión en la que quieres buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EnlaceDB enlaceDB = new EnlaceDB();
            DataTable versiculos = enlaceDB.BuscarVersiculos(palabraBuscar, version);

            if (versiculos.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron resultados para la búsqueda.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //// Limpiar ComboBoxes antes de asignar datos al DataGridView
            //LimpiarComboBoxes();

            // Configurar el DataGridView
            ConfigurarDataGridView();

            // Asignar datos a la columna "Versiculos"
            dataGridView1.DataSource = versiculos;
            dataGridView1.Columns["Cita"].DataPropertyName = "Cita";
            dataGridView1.Columns["Texto"].DataPropertyName = "Texto";

            enlaceDB.registrarBusqueda(frmLogin.userID, textBox1.Text, "N/A", "N/A", cbVersion.Text);
        }

        private void btnShowCap_Click(object sender, EventArgs e)
        {
            if (cbLibro.SelectedItem == null || cbCap.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona todos los campos requeridos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView libroSeleccionado = (DataRowView)cbLibro.SelectedItem;
            DataRowView versionSeleccionada = (DataRowView)cbVersion.SelectedItem;

            string nombreLibro = libroSeleccionado["Nombre"].ToString();
            int numeroCapitulo = Convert.ToInt32(cbCap.SelectedItem);
            int version = Convert.ToInt32(versionSeleccionada["id_version"]);

            try
            {
                EnlaceDB enlaceDB = new EnlaceDB();
                DataTable versiculos = enlaceDB.ObtenerVersiculosPorNombreLibroYNumeroCap(nombreLibro, numeroCapitulo, version);

                if (versiculos.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron versículos para este capítulo.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //LimpiarComboBoxes();
                ConfigurarDataGridView();

                dataGridView1.DataSource = versiculos;
                dataGridView1.Columns["Cita"].DataPropertyName = "Cita";
                dataGridView1.Columns["Texto"].DataPropertyName = "Texto";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar el capítulo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarEnUnTestemento_Click(object sender, EventArgs e)
        {
            string palabraBuscar = textBox1.Text;
            if (string.IsNullOrEmpty(palabraBuscar))
            {
                MessageBox.Show("Por favor, ingresa una palabra o frase para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string testamento = cbTestamento.Text;
            string version = cbVersion.Text;
            
            if (string.IsNullOrEmpty(testamento) || string.IsNullOrEmpty(version))
            {
                MessageBox.Show("Por favor, selecciona un testamento y una versión.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EnlaceDB enlaceDB = new EnlaceDB();
            DataTable versiculos = enlaceDB.BuscarVersiculosPorTestamento(palabraBuscar, testamento, version);

            if (versiculos.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron resultados para la búsqueda.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Configurar el DataGridView
            ConfigurarDataGridView();

            dataGridView1.DataSource = versiculos;
            dataGridView1.Columns["Cita"].DataPropertyName = "Cita";
            dataGridView1.Columns["Texto"].DataPropertyName = "Texto";

            enlaceDB.registrarBusqueda(frmLogin.userID, textBox1.Text, cbTestamento.Text, "N/A", cbVersion.Text);
        }

        private void BtnBuscarEnUnLibro_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Por favor, ingresa una palabra o frase para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(cbCap.Text))
            {
                MessageBox.Show("Por favor, selecciona un capítulo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string palabraBuscar = textBox1.Text;
            string libro = cbLibro.Text;
            string version = cbVersion.Text;
            int capitulo = Convert.ToInt32(cbCap.Text);

            EnlaceDB enlaceDB = new EnlaceDB();
            DataTable versiculos = enlaceDB.BuscarVersiculosPorLibro(palabraBuscar, version, libro);

            if (versiculos.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron resultados para la búsqueda.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ConfigurarDataGridView();
            dataGridView1.DataSource = versiculos;
            dataGridView1.Columns["Cita"].DataPropertyName = "Cita";
            dataGridView1.Columns["Texto"].DataPropertyName = "Texto";

            enlaceDB.registrarBusqueda(frmLogin.userID, textBox1.Text, cbTestamento.Text, cbLibro.Text, cbVersion.Text);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                var cellValue = dataGridView1.SelectedCells[0].Value;

                if (cellValue != null)
                {
                    string textToCopy = cellValue.ToString();
                    Clipboard.SetText(textToCopy);
                    MessageBox.Show("El texto se ha copiado al portapapeles.", "Copiado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No hay ningún texto seleccionado para copiar.", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una celda que contenga texto para copiar.", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            EnlaceDB enlaceDB = new EnlaceDB();
            int tamanoFuente = enlaceDB.getSize(frmLogin.userID);

            Font font = new Font(e.CellStyle.Font.FontFamily, tamanoFuente, e.CellStyle.Font.Style);
            e.CellStyle.Font = font;
        }

        private void btnFav_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;

                EnlaceDB enlaceDB = new EnlaceDB();
                string texto = dataGridView1.Rows[rowIndex].Cells["Texto"].Value.ToString();

                NombreFavorito nombreFavorito = new NombreFavorito();
                nombreFavorito.ShowDialog();

                if (NombreFavorito.cancelado == false)
                {
                    string nombre = NombreFavorito.nombre;

                    int idVers = enlaceDB.getIdVersiculo(texto);

                    if (NombreFavorito.esTodoElCapitulo == true)
                    {
                        enlaceDB.registrarFavorito(nombre, cbLibro.Text, Convert.ToInt32(cbCap.Text), cbVersion.Text, 0, frmLogin.userID);
                    }
                    else
                    {
                        enlaceDB.registrarFavorito(nombre, cbLibro.Text, Convert.ToInt32(cbCap.Text), cbVersion.Text, idVers, frmLogin.userID);
                    }

                    MessageBox.Show("Tu favorito se ha registrado", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una celda primero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}