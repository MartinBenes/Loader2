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
using System.Threading;
using System.Net;
using System.Security;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

// 284, 261

namespace MetroLoader
{
    public partial class Form4 : MetroForm
    {

        bool admin;
        bool premium;
        public string fileEncrypt;
        public string fileDecrypt;
        public string outputEncrypt;
        public string outputDecrypt;
        public int numPass = 10;
        const int keysize = 256;
        const string initVector = "Exm4ypIEs2owh8bB";
        SecureString pwd = new SecureString();


        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://localhost/loader/group.php?username=" + Properties.Settings.Default.Username);
            metroButton1.Enabled = false;
            metroRadioButton2.Visible = false;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.DocumentText.Contains("4"))
            {
                admin = true;
                metroButton1.Enabled = true;
                metroLabel1.Text = "User status: Admin";
                metroRadioButton2.Visible = true;
            }
            else if (webBrowser1.DocumentText.Contains("8"))
            {
                premium = true;
                metroButton1.Enabled = true;
                metroLabel1.Text = "User status: Premium";
            }

            Thread.Sleep(1000);
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (admin == true)
            {
                metroRadioButton2.Visible = true;
            }
        }




        private void metroButton1_Click(object sender, EventArgs e)
        {

            if (metroRadioButton1.Checked == true) // Premium
            {
                WebClient wb = new WebClient();
                wb.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.33 Safari/537.36");
                wb.DownloadFile("http://localhost/loader/dlls/premium.dll", "C:\\Temp\\cheat.dll");
            }

            if (metroRadioButton2.Checked == true) // Developer
            {
                WebClient wb = new WebClient();
                wb.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.33 Safari/537.36");
                wb.DownloadFile("http://localhost/loader/dlls/admin.dll", "C:\\Temp\\cheat.dll");
            }

            this.Hide();
            var form2 = new Form2();
            form2.Closed += (s, args) => this.Close();
            form2.Show();

            // Decryption start

            string filename = @"‪C:\Users\prefo\Desktop\test-encrypt.txt";
            if (filename.Equals(""))
            {
                MessageBox.Show("File missing");
            }
            else
            {
                decryptFilePath(filename);
                EnterPassword enterPass = new EnterPassword();
                if (enterPass.ShowDialog() == DialogResult.OK)
                {
                    pwd = enterPass.getPassword();
                    decryptFile();
                }
            }
        }
    }
}

//-----------------------------------------------------
// Coded by /id/Thaisen! Free loader source
// https://github.com/ThaisenPM/Cheat-Loader-CSGO-2.0
// Note to the person using this, removing this
// text is in violation of the license you agreed
// to by downloading. Only you can see this so what
// does it matter anyways.
// Copyright © ThaisenPM 2017
// Licensed under a MIT license
// Read the terms of the license here
// https://github.com/ThaisenPM/Cheat-Loader-CSGO-2.0/blob/master/LICENSE
//-----------------------------------------------------
