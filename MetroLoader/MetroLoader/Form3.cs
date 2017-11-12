using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManualMapInjection.Injection;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace MetroLoader
{
    public partial class Form2 : MetroForm
    {

        bool csgof;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = "Starting injection";
            Thread.Sleep(200);

            var name = "csgo";
            var target = Process.GetProcessesByName(name).FirstOrDefault();

            if (target == null)
            {
                csgof = false;
            }
            else if (target != null)
            {
                //MessageBox.Show("Error: CS:GO is open!");
                //Application.Restart();
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (csgof == false)
            {
                label1.Text = "Waiting for CSGO.exe";

                var name = "csgo";
                var target = Process.GetProcessesByName(name).FirstOrDefault();

                if (target != null)
                {
                    var path = "C:\\Temp\\cheat.dll";
                    var file = File.ReadAllBytes(path);

                    if (!File.Exists(path))
                    {
                        label1.Text = "DLL not found";
                        return;
                    }

                    //Thread.Sleep(10000);
                    var injector = new ManualMapInjector(target) { AsyncInjection = true };
                    label1.Text = $"hmodule = 0x{injector.Inject(file).ToInt64():x8}";
                    label1.Text = "Successfully injected";
                    timer1.Stop();
                    timer2.Start();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            File.Delete("C:\\Temp\\cheat.dll");
            Application.Exit();
            timer2.Stop();
        }
    }
}
