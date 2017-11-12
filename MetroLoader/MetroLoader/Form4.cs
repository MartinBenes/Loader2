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

        // Get the path for decryption
        public void decryptFilePath(string path)
        {
            string[] breakup = path.Split('\\');
            int length = breakup.Length;
            string[] period = breakup[length - 1].Split('-');
            string output = period[0] + "-decrypt.txt";
            string finalpath = "";
            for (int i = 0; i < breakup.Length - 1; i++)
            {
                if (i == 0)
                {
                    finalpath = breakup[0];
                }
                else
                {
                    finalpath = finalpath + "\\" + breakup[i];
                }
            }
            outputDecrypt = "C:\\Users\\prefo\\Desktop\\test-decrypt.txt";
        }

        // Actual decryption
        public void decryptFile()
        {
            String line;
            StreamWriter sw = new StreamWriter(outputDecrypt);
            ArrayList d1 = new ArrayList();
            ArrayList d2 = new ArrayList();
            IntPtr ptr = IntPtr.Zero;
            ptr = Marshal.SecureStringToBSTR(pwd);
            string hash = getPasswordHash(Marshal.PtrToStringBSTR(ptr));
            try
            {
                StreamReader sr = new StreamReader(fileDecrypt);
                while ((line = sr.ReadLine()) != null)
                {
                    d2.Add(line);
                }

                for (int i = 0; i < numPass; i++)
                {
                    foreach (string s1 in d2)
                    {
                        d1.Add(decrypt(s1, hash));
                    }
                    d2.Clear();
                    foreach (string s2 in d1)
                    {
                        d2.Add(decrypt(s2, hash));
                    }
                    d1.Clear();
                }

                foreach (string dec in d2)
                {
                    sw.WriteLine(decrypt(dec, hash));
                }
                sw.Close();
            }
            catch (Exception f)
            {
                string error = f.ToString();
                sw.Close();
                d1.Clear();
                d2.Clear();
                WrongPwd pwdIncorrect = new WrongPwd();
                pwdIncorrect.Show();
                File.Delete(outputDecrypt);
            }
        }

        public static string decrypt(string cipherText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Padding = PaddingMode.ISO10126;
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }



        public string getPasswordHash(string pwd)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] src;
            byte[] hash;
            src = ASCIIEncoding.ASCII.GetBytes(pwd);
            hash = md5.ComputeHash(src);
            StringBuilder output = new StringBuilder(hash.Length);
            for (int i = 0; i < hash.Length; i++)
            {
                output.Append(hash[i].ToString("X2"));
            }
            return output.ToString();
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
