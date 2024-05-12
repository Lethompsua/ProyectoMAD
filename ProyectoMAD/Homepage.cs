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
    public partial class Homepage : Form
    {
        public Homepage()
        {
            InitializeComponent();
            this.FormClosed += Homepage_FormClosed;
        }
        private void Homepage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
