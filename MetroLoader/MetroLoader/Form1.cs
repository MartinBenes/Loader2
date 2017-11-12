using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using HWIDGrabber;
using System.Diagnostics;

/*
 * Size: 355, 237
 * Style: Red
 * Theme: Light
 */

namespace MetroLoader
{
    public partial class Form1 : MetroForm
    {
        bool username;
        bool usergroup;
        bool hwid;
        string hwidstring;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            var steam = "steam";
            var starget = Process.GetProcessesByName(steam).FirstOrDefault();

            hwidstring = HWDI.GetMachineGuid();

            if (Properties.Settings.Default.Checked == true)
            {
                metroTextBox1.Text = Properties.Settings.Default.Username;
                metroTextBox2.Text = Properties.Settings.Default.Password;
                metroCheckBox1.Checked = Properties.Settings.Default.Checked;
            }
            else if (Properties.Settings.Default.Checked == false)
            {
                metroTextBox1.Text = String.Empty;
                metroTextBox2.Text = String.Empty;
                metroCheckBox1.Checked = false;
            }

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://localhost/loader/check.php?username=" + metroTextBox1.Text + "&password=" + metroTextBox2.Text);
            username = true;

            if (metroCheckBox1.Checked == true)
            {
                Properties.Settings.Default.Username = metroTextBox1.Text;
                Properties.Settings.Default.Password = metroTextBox2.Text;
                Properties.Settings.Default.Checked = metroCheckBox1.Checked;
                Properties.Settings.Default.Save();
            }
            // Checks if it is not checked to re-write the information that was saved
            else if (metroCheckBox1.Checked == false)
            {
                Properties.Settings.Default.Checked = false;
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (username == true)
            {
                if (webBrowser1.DocumentText.Contains("0"))
                {
                    username = false;
                    MessageBox.Show("Password incorrect");
                }
                else if (webBrowser1.DocumentText.Contains("1"))
                {
                    usergroup = true;
                    username = false;
                    webBrowser2.Navigate("http://localhost/loader/group.php?username=" + metroTextBox1.Text);
                }
                else if (webBrowser2.DocumentText.Contains("2"))
                {
                    username = false;
                    MessageBox.Show("No user with that name");
                }
            }
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (usergroup == true)
            {
                if (webBrowser2.DocumentText.Contains("4") || webBrowser2.DocumentText.Contains("8"))
                {
                    usergroup = false;
                    hwid = true;
                    webBrowser3.Navigate("http://localhost/loader/hwid.php?username=" + metroTextBox1.Text + "&hwid=" + hwidstring);
                }
                // General statment, if the group isn't in the list, they get thrown this
                else
                {
                    MessageBox.Show("Group incorrect");
                    usergroup = false;
                }
            }
        }

        private void webBrowser3_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            if (hwid == true)
            {
                // HWID is incorrect
                if (webBrowser3.DocumentText.Contains("0"))
                {
                    MessageBox.Show("HWID Incorrect");
                    hwid = false;
                    Application.Exit();
                }
                // HWID is correct, opens the new form
                else if (webBrowser3.DocumentText.Contains("1"))
                {
                    this.Hide();
                    var form2 = new Form4();
                    form2.Closed += (s, args) => this.Close();
                    form2.Show();
                }
                // HWID value is empty for some reason
                else if (webBrowser3.DocumentText.Contains("2"))
                {
                    MessageBox.Show("HWID Value Empty?");
                    hwid = false;
                }
                // HWID has been set, opens the new form
                else if (webBrowser3.DocumentText.Contains("3"))
                {
                    DialogResult dialogResult = MessageBox.Show("HWID Has Been Set!" + Environment.NewLine + "HWID: " + hwidstring + Environment.NewLine + "You will need your current HWID to change it later!" + Environment.NewLine + "Press yes to copy the text to your clipboard, no to not.", "HWID Set", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Clipboard.SetText(hwidstring);
                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }
                    hwid = false;

                    this.Hide();
                    var form2 = new Form4();
                    form2.Closed += (s, args) => this.Close();
                    form2.Show();
                }
                // Any SQL error (normally comes with INSERT INTO functions glitching out
                else if (webBrowser3.DocumentText.Contains("4"))
                {
                    MessageBox.Show("SQL error with HWID setting");
                    hwid = false;
                }
            }
        }

        private void webBrowser4_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
