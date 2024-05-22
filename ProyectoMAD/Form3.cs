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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProyectoMAD
{
    public partial class Form3 : Form
    {
        private string busquedaActual;
        private static Form3 instance;
        public Form3()
        {

            InitializeComponent();
            CargarIdiomas();
            ConfigurarDataGridView();

        }


        private void ConfigurarDataGridView()
        {
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            if (dataGridView1.Columns["Versiculo"] == null)
            {
                dataGridView1.AutoGenerateColumns = false;

                DataGridViewTextBoxColumn textBoxColumn = new DataGridViewTextBoxColumn();
                textBoxColumn.Name = "Versiculo";
                textBoxColumn.HeaderText = "Versiculo";
                textBoxColumn.DataPropertyName = "Versiculo";
                textBoxColumn.Width = 800;
                textBoxColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.Columns.Add(textBoxColumn);
            }
            else
            {
                DataGridViewColumn versiculoColumn = dataGridView1.Columns["Versiculo"];
                versiculoColumn.Width = 800;
                versiculoColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
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



        private void CargarIdiomas()
        {
            EnlaceDB enlaceDB = new EnlaceDB();

            DataTable idiomas = enlaceDB.ObtenerIdiomas();
            cbIdioma.DataSource = idiomas;
            cbIdioma.DisplayMember = "Nombre";
            cbIdioma.ValueMember = "Id_Idioma";
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


        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

            string nombreTestamento = cbTestamento.Text;
            EnlaceDB enlaceDB = new EnlaceDB();
            DataTable libros = enlaceDB.ObtenerLibrosPorNombreTestamento(nombreTestamento);
            cbLibro.DataSource = libros;
            cbLibro.DisplayMember = "Nombre";
        }




        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
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

            // Verificar si se obtuvo algún dato
            if (tablaCapitulos != null && tablaCapitulos.Rows.Count > 0)
            {
                // Limpiar el ComboBox
                cb_Cap.DataSource = null;

                // Crear lista de capítulos
                List<int> capitulos = new List<int>();
                foreach (DataRow fila in tablaCapitulos.Rows)
                {
                    int numeroCapitulo = Convert.ToInt32(fila["NumeroCap"]);
                    capitulos.Add(numeroCapitulo);
                }

                // Establecer la lista de capítulos como origen de datos y refrescar el ComboBox
                cb_Cap.DataSource = capitulos;
                cb_Cap.Refresh(); // o cb_Cap.Invalidate();
            }
            else
            {
                MessageBox.Show("No se encontraron capítulos para este libro.");
            }
        }



        private void btnMostrarLibro_Click(object sender, EventArgs e)
        {
            // Verificar que los ComboBoxes no estén vacíos
            if (cbLibro.SelectedItem == null || cbIdioma.SelectedItem == null || cbVersion.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona todos los campos requeridos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el nombre del libro seleccionado
            string nombreLibro = cbLibro.SelectedItem.ToString();

            try
            {
                EnlaceDB enlaceDB = new EnlaceDB();
                DataTable versiculos = enlaceDB.ObtenerVersiculosPorNombreLibro(nombreLibro);

                // Limpiar ComboBoxes antes de asignar datos al DataGridView
                LimpiarComboBoxes();

                // Configurar el DataGridView
                ConfigurarDataGridView();

                // Asignar datos a la columna "Versiculo"
                dataGridView1.DataSource = versiculos;
                dataGridView1.Columns["Versiculo"].DataPropertyName = "Versiculo"; // Asegurar que se asigne a la columna correcta
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar el libro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void LimpiarComboBoxes()
        {
            cbLibro.SelectedIndex = -1;
            cbIdioma.SelectedIndex = -1;
            cbVersion.SelectedIndex = -1;
            cbTestamento.SelectedIndex = -1;
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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
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

            cbLibro.DataSource = null;

            cbLibro.DataSource = libros;
            cbLibro.DisplayMember = "Nombre";
            cbLibro.ValueMember = "Id_Libro";

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtener la palabra o frase a buscar
            string palabraBuscar = textBox1.Text;
            if (string.IsNullOrEmpty(palabraBuscar))
            {
                MessageBox.Show("Por favor, ingresa una palabra o frase para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EnlaceDB enlaceDB = new EnlaceDB();
            DataTable versiculos = enlaceDB.BuscarVersiculos(palabraBuscar);

            // Configurar el DataGridView
            ConfigurarDataGridView();

            // Asignar datos a la columna "Versiculos"
            dataGridView1.DataSource = versiculos;
            dataGridView1.Columns["Versiculo"].DataPropertyName = "Versiculo";
        }


        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Versiculo"].Index && e.RowIndex >= 0)
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



        private void btnGuardarFav_Click(object sender, EventArgs e)
        {
            // Verificar que los ComboBoxes no estén vacíos
            if (cbLibro.SelectedItem == null || cb_Cap.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona todos los campos requeridos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el nombre del libro seleccionado y el número del capítulo
            string nombreLibro = cbLibro.SelectedItem.ToString();
            int numeroCapitulo = Convert.ToInt32(cb_Cap.SelectedItem);

            try
            {
                EnlaceDB enlaceDB = new EnlaceDB();
                DataTable versiculos = enlaceDB.ObtenerVersiculosPorNombreLibroYNumeroCap(nombreLibro, numeroCapitulo);

                // Limpiar ComboBoxes antes de asignar datos al DataGridView
                LimpiarComboBoxes();

                // Configurar el DataGridView
                ConfigurarDataGridView();

                // Asignar datos a la columna "Versiculo"
                dataGridView1.DataSource = versiculos;
                dataGridView1.Columns["Versiculo"].DataPropertyName = "Versiculo"; // Asegurar que se asigne a la columna correcta
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar el capítulo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cb_Cap_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscarEnUnTestemento_Click(object sender, EventArgs e)
        {
            // Obtener la palabra o frase a buscar
            string palabraBuscar = textBox1.Text;
            if (string.IsNullOrEmpty(palabraBuscar))
            {
                MessageBox.Show("Por favor, ingresa una palabra o frase para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el testamento seleccionado
            string testamento = cbTestamento.SelectedItem?.ToString();

            // Obtener la versión seleccionada
            string version = cbVersion.SelectedItem?.ToString();

            // Verificar si el testamento o la versión son NULL
            if (string.IsNullOrEmpty(testamento) || string.IsNullOrEmpty(version))
            {
                MessageBox.Show("Por favor, selecciona un testamento y una versión.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EnlaceDB enlaceDB = new EnlaceDB();
            DataTable versiculos = enlaceDB.BuscarVersiculosPorTestamento(palabraBuscar, testamento, version);

            // Configurar el DataGridView
            ConfigurarDataGridView();

            // Asignar datos a la columna "Versiculos"
            dataGridView1.DataSource = versiculos;
            dataGridView1.Columns["Versiculo"].DataPropertyName = "Versiculo";
        }

        private void BtnBuscarEnUnLibro_Click(object sender, EventArgs e)
        {

            // Obtener la palabra o frase a buscar
            string palabraBuscar = textBox1.Text;
            if (string.IsNullOrEmpty(palabraBuscar))
            {
                MessageBox.Show("Por favor, ingresa una palabra o frase para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el testamento seleccionado
            string testamento = cbTestamento.SelectedItem?.ToString();

            // Obtener la versión seleccionada
            string version = cbVersion.SelectedItem?.ToString();

            string Libro = cbLibro.SelectedItem?.ToString();

            // Verificar si el testamento o la versión son NULL
            if (string.IsNullOrEmpty(testamento) || string.IsNullOrEmpty(version) || string.IsNullOrEmpty(Libro))
            {
                MessageBox.Show("Por favor, selecciona un testamento y una versión.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EnlaceDB enlaceDB = new EnlaceDB();
            DataTable versiculos = enlaceDB.BuscarVersiculosPorTestamentoYLibro(palabraBuscar, testamento, version, Libro);

            // Configurar el DataGridView
            ConfigurarDataGridView();

            // Asignar datos a la columna "Versiculos"
            dataGridView1.DataSource = versiculos;
            dataGridView1.Columns["Versiculo"].DataPropertyName = "Versiculo";

        }
    }
}
