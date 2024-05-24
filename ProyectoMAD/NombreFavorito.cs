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
    public partial class NombreFavorito : Form
    {
        public static string nombre { get; set; }
        public static bool esTodoElCapitulo { get; set; }
        public static bool cancelado { get; set; }

        public NombreFavorito()
        {
            InitializeComponent();
            nombre = string.Empty;
            esTodoElCapitulo = false;
            cancelado = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("El campo está vacío", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                nombre = txtName.Text;
                esTodoElCapitulo = checkAll.Checked;
                cancelado = false;
                this.Close();
            }
        }
    }
}
