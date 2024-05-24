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
                textoColumn.Width = 800;
                textoColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.Columns.Add(textoColumn);
            }
            if (dataGridView1.Columns["Favorito"] == null)
            {
                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                buttonColumn.Name = "Favorito";
                buttonColumn.HeaderText = "Favorito";
                buttonColumn.Text = "Favorito";
                buttonColumn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(buttonColumn);
            }

            dataGridView1.CellClick += DataGridView1_CellClick;
            dataGridView1.CellPainting += DataGridView1_CellPainting;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Asegurarse de que el índice de columna es el de la columna de botón y no es un encabezado
            if (e.ColumnIndex == dataGridView1.Columns["Favorito"].Index && e.RowIndex >= 0)
            {
                // Mostrar un mensaje como prueba
                MessageBox.Show("Favorito button clicked in row " + e.RowIndex);
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
                int idLibro = Convert.ToInt32(cbLibro.SelectedValue);
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns["Favorito"] == null)
            {
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.Name = "Favorito";
                checkBoxColumn.HeaderText = "Favorito";
                dataGridView1.Columns.Add(checkBoxColumn);
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
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Texto"].Index && e.RowIndex >= 0)
            {
                e.Handled = true;
                e.PaintBackground(e.CellBounds, true);

                string cellText = e.FormattedValue?.ToString();
                string palabraBuscar = textBox1.Text;

                if (!string.IsNullOrEmpty(cellText) && !string.IsNullOrEmpty(palabraBuscar))
                {
                    int startIndex = cellText.IndexOf(palabraBuscar, StringComparison.OrdinalIgnoreCase);
                    if (startIndex >= 0)
                    {
                        // Dibujar el texto antes, durante y después de la palabra buscada
                        string beforeSearchText = cellText.Substring(0, startIndex);
                        string searchText = cellText.Substring(startIndex, palabraBuscar.Length);
                        string afterSearchText = cellText.Substring(startIndex + palabraBuscar.Length);

                        // Medir el tamaño del texto antes de la palabra buscada
                        SizeF beforeSize = e.Graphics.MeasureString(beforeSearchText, e.CellStyle.Font, e.CellBounds.Width);

                        // Dibujar el texto antes de la palabra buscada
                        RectangleF beforeRect = new RectangleF(e.CellBounds.X, e.CellBounds.Y, beforeSize.Width, e.CellBounds.Height);
                        e.Graphics.DrawString(beforeSearchText, e.CellStyle.Font, new SolidBrush(e.CellStyle.ForeColor), beforeRect);

                        // Medir el tamaño del texto de la palabra buscada
                        SizeF searchSize = e.Graphics.MeasureString(searchText, new Font(e.CellStyle.Font, FontStyle.Bold), e.CellBounds.Width);

                        // Dibujar la palabra buscada en rojo y negrita
                        RectangleF searchRect = new RectangleF(e.CellBounds.X + beforeSize.Width, e.CellBounds.Y, searchSize.Width, e.CellBounds.Height);
                        e.Graphics.DrawString(searchText, new Font(e.CellStyle.Font, FontStyle.Bold), new SolidBrush(Color.Red), searchRect);

                        // Dibujar el texto después de la palabra buscada
                        RectangleF afterRect = new RectangleF(e.CellBounds.X + beforeSize.Width + searchSize.Width, e.CellBounds.Y, e.CellBounds.Width - beforeSize.Width - searchSize.Width, e.CellBounds.Height);
                        e.Graphics.DrawString(afterSearchText, e.CellStyle.Font, new SolidBrush(e.CellStyle.ForeColor), afterRect);
                    }
                    else
                    {
                        // Si no se encuentra la palabra, dibujar el texto normalmente
                        e.Graphics.DrawString(cellText, e.CellStyle.Font, new SolidBrush(e.CellStyle.ForeColor), e.CellBounds);
                    }
                }
                else
                {
                    // Si el texto o la palabra buscada están vacíos, dibujar el texto normalmente
                    e.Graphics.DrawString(cellText, e.CellStyle.Font, new SolidBrush(e.CellStyle.ForeColor), e.CellBounds);
                }
            }
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
            DataTable versiculos = enlaceDB.BuscarVersiculosPorCapitulo(palabraBuscar, version, libro, capitulo);

            if (versiculos.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron resultados para la búsqueda.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ConfigurarDataGridView();
            dataGridView1.DataSource = versiculos;
            dataGridView1.Columns["Cita"].DataPropertyName = "Cita";
            dataGridView1.Columns["Texto"].DataPropertyName = "Texto";
        }
    }
}