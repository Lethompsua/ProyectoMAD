using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoMAD
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Al hacer clic en el botón, creamos una nueva instancia de la otra ventana
            Form2 IngresarUsu = new Form2();

            // Mostramos la nueva ventana
            IngresarUsu.Show();
        }
    }
}
