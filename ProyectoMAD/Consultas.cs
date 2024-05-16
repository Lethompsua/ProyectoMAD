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
    public partial class Consultas : Form
    {
        private static Consultas instance;
        public Consultas()
        {
            InitializeComponent();
        }

        public static Consultas GetInstance() //Singleton
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Consultas();
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
    }
}
