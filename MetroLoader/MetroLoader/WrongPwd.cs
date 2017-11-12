using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetroLoader
{
    public partial class WrongPwd : Form
    {
        public WrongPwd()
        {
            InitializeComponent();
            this.CenterToScreen();
            System.Reflection.Assembly assem = this.GetType().Assembly;
            ResourceManager rm = new System.Resources.ResourceManager("EncryptIt.Resources", assem);
        }

        private void WrongPwd_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Wrong password ?");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Hide();
            timer1.Stop();
        }
    }
}
