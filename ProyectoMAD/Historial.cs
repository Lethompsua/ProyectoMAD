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
    public partial class Historial : Form
    {
        private static Historial instance;
        public Historial()
        {
            InitializeComponent();
            checkAll.Checked = true;
            
            comboMeses.Items.Add("Sin filtro");
            comboMeses.Items.Add("Enero");
            comboMeses.Items.Add("Febrero");
            comboMeses.Items.Add("Marzo");
            comboMeses.Items.Add("Abril");
            comboMeses.Items.Add("Mayo");
            comboMeses.Items.Add("Junio");
            comboMeses.Items.Add("Agosto");
            comboMeses.Items.Add("Septiembre");
            comboMeses.Items.Add("Octubre");
            comboMeses.Items.Add("Noviembre");
            comboMeses.Items.Add("Diciembre");
            comboMeses.SelectedIndex = 0;

            comboAños.Items.Add("Sin filtro");
            comboAños.Items.Add("2021");
            comboAños.Items.Add("2022");
            comboAños.Items.Add("2023");
            comboAños.Items.Add("2024");
            comboAños.Items.Add("2025");
            comboAños.Items.Add("2026");
            comboAños.Items.Add("2027");
            comboAños.Items.Add("2028");
            comboAños.Items.Add("2029");
            comboAños.Items.Add("2030");
            comboAños.SelectedIndex = 0;

            EnlaceDB enlaceDB = new EnlaceDB();

            DataTable history = enlaceDB.getHistory(frmLogin.userID);
            gridHistory.DataSource = history;
            gridHistory.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            gridHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public static Historial GetInstance() //Singleton
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Historial();
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

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
