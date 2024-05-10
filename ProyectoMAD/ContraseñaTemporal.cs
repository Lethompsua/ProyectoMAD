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

            string password = getRandomPassword();
            labelQuestion.Text = pregunta;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            
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
    }
}
