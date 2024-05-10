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
    public partial class ContraseñaTemporal : Form
    {
        public ContraseñaTemporal()
        {
            InitializeComponent();

            EnlaceDB enlaceDB = new EnlaceDB();
            string pregunta = enlaceDB.getQuestion(frmLogin.userID);

            labelQuestion.Text = pregunta;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            
        }
    }
}
