using System;
using System.Drawing;
using System.Windows.Forms;
using System.Security;

namespace MetroLoader
{
    public partial class EnterPassword : Form
    {
        SecureString pwd = new SecureString();
        public EnterPassword()
        {
            InitializeComponent();
            this.CenterToScreen();
        }


        void Button2Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        void Button1Click(object sender, System.EventArgs e)
        {
            checkForm();
        }

        void EnterPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkForm();
            }
        }

        void checkForm()
        {
            if (textBox1.Text.Equals(textBox2.Text))
            {
                if (textBox1.Text.Equals(""))
                {
                    MessageBox.Show("Your password cannot be blank, please try again");
                    textBox1.Focus();
                }
                else
                {
                    foreach (char c in textBox1.Text)
                    {
                        pwd.AppendChar(c);
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Passwords do not match, please try again");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
            }
        }

        public SecureString getPassword()
        {
            return pwd;
        }


        private void EnterPassword_Load(object sender, EventArgs e)
        {
            textBox1.Text = "dankmeme";
            textBox2.Text = "dankmeme";
            checkForm();
        }
    }
}
