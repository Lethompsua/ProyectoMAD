using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace ProyectoMAD
{
    public partial class ContraseñaTemporal : Form
    {

        private string password {  get; set; }
        public ContraseñaTemporal()
        {
            InitializeComponent();

            EnlaceDB enlaceDB = new EnlaceDB();
            string pregunta = enlaceDB.getQuestion(frmLogin.userID);
            labelQuestion.Text = pregunta;

            picCopy.Visible = false;
            picCopy.Enabled = false;

            labelCopy.Visible = false;
            labelCopy.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string answer = txtAnswer.Text;
            password = getRandomPassword();

            if (answer == "")
            {
                MessageBox.Show("Por favor, responda la pregunta", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                EnlaceDB enlaceDB = new EnlaceDB();
                if (enlaceDB.ValidarRespuesta(answer, password, frmLogin.userID) == true)
                {
                    MessageBox.Show("Respuesta correcta", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAnswer.Visible = false;

                    btnSend.Visible = false;
                    btnSend.Enabled = false;

                    labelQuestion.Text = "Tu contraseña temporal es: " + password;
                    labelQuestion.Font = new Font(labelQuestion.Font, FontStyle.Regular);

                    btnCancel.Text = "Regresar";
                    btnCancel.BackColor = Color.LightGreen;
                    btnCancel.Left -= 50;
                    btnCancel.Top -= 30;

                    picCopy.Visible = true;
                    picCopy.Enabled = true;

                    labelCopy.Visible = true;
                    labelCopy.Enabled = true;
                }
            }
        }

        private string getRandomPassword()
        {
            const string caracteresPermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!¡?¿@#$%^\"&*()-_=+/'";
            int longitud = 12;
            Random random = new Random();
            char[] password = new char[longitud];

            for (int i = 0; i < longitud; i++)
            {
                password[i] = caracteresPermitidos[random.Next(caracteresPermitidos.Length)];
            }

            if (password.Any(char.IsLower) == false)
                password[random.Next(longitud)] = getRandomLowercase();
            if (password.Any(char.IsUpper) == false)
                password[random.Next(longitud)] = getRandomUppercase();
            if (password.Any(IsSpecialCharacter) == false)
                password[random.Next(longitud)] = getRandomSpecialChar();

            return new string(password);
        }

        private char getRandomLowercase()
        {
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            Random random = new Random();
            return lowercase[random.Next(lowercase.Length)];
        }

        private char getRandomUppercase()
        {
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            return uppercase[random.Next(uppercase.Length)];
        }

        private char getRandomSpecialChar()
        {
            const string specialChar = "!¡?¿@#$%^\"&*()-_=+/'";
            Random random = new Random();
            return specialChar[random.Next(specialChar.Length)];
        }

        private bool IsSpecialCharacter(char c)
        {
            return !char.IsLetterOrDigit(c);
        }

        private void picCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(password);
            MessageBox.Show("Contraseña copiada", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void labelCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(password);
            MessageBox.Show("Contraseña copiada", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
