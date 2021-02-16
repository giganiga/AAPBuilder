using Microsoft.Win32;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AAPBuilder
{
    public partial class Disclaimer : Form
    {
        public Disclaimer()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string resourceName = new AssemblyName(args.Name).Name + ".dll";
                string resource = Array.Find(GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };
            InitializeComponent();
        }

        private bool accepted = false;
        private void ınfluenceButton1_Click(object sender, EventArgs e)
        {
            Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AAPBuilder\DisclaimerApproval", true).SetValue("Accepted", "True");
            accepted = true;
            this.Close();
        }

        private void LoginPanel_Load(object sender, EventArgs e)
        {

        }

        private void ınfluenceCheckBox2_CheckedChanged(object sender)
        {
            ınfluenceButton1.Enabled = ınfluenceCheckBox2.Checked;
        }

        private void LoginPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!accepted) Application.Exit(); 
        }

        private void influenceButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
