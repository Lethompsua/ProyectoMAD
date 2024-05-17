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
    public partial class Form3 : Form
    {
        private static Form3 instance;
        public Form3()
        {
            EnlaceDB enlacedb = new EnlaceDB();
            InitializeComponent();
          
            enlacedb.MostrarTestamentosEnComboBox(comboBox5);
            
            DataTable tablaVersiculos = enlacedb.get_Versiculos();
            dataGridView1.DataSource = tablaVersiculos;

            // Ajustar automáticamente el ancho de las columnas y hacer que llenen todo el espacio disponible
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            EnlaceDB enlacedb = new EnlaceDB();

            // Obtener el testamento seleccionado del ComboBox
            string testamentoSeleccionado = comboBox5.SelectedItem.ToString();

            // Obtener los libros correspondientes al testamento seleccionado
            List<string> libros = enlacedb.ObtenerLibrosPorTestamento(testamentoSeleccionado);

            // Limpiar el ComboBox de libros
            comboBox2.Items.Clear();

            // Agregar los libros al ComboBox
            foreach (string libro in libros)
            {
                comboBox2.Items.Add(libro);
            }

        }

        public void CargarVersiculosEnDataGridView(DataTable dataTable)
        {
            dataGridView1.DataSource = dataTable;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }


        private void btnMostrarLibro_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
