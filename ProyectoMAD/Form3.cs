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
    }
}
