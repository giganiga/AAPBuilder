using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AAPBuilder
{
    public partial class HexEditor : Form
    {
        public HexEditor(RegFixer regFixer, MainForm handle)
        {
            InitializeComponent();
            this.regFixer = regFixer;
            formHandle = handle;
        }

        private RegFixer regFixer = null;
        private MainForm formHandle;

        private void HexEditor_Shown(object sender, EventArgs e)
        {
            string[] keyNames = regFixer.ScankeyNames(Registry.CurrentUser);
            if (keyNames == null || keyNames.Length <= 0) label31.Text = string.Format("[{0}]", Registry.CurrentUser.Name + "\\" + "-");
            else label31.Text = string.Format("[{0}]", Registry.CurrentUser.Name + "\\" + string.Join(",", keyNames));
            label31.Location = new Point(Width / 2 - label31.Width / 2, label31.Location.Y);
            if (keyNames.Length >= 5) label31.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular, GraphicsUnit.Point, 0);

            string[] keyNames1 = regFixer.ScankeyNames(Registry.CurrentUser.OpenSubKey("Software\\Microsoft"));
            if (keyNames1 == null || keyNames1.Length <= 0) label14.Text = string.Format("[{0}]", Registry.CurrentUser.OpenSubKey("Software\\Microsoft") + "\\" + "-");
            else label14.Text = string.Format("[{0}]", Registry.CurrentUser.OpenSubKey("Software\\Microsoft") + "\\" + string.Join(",", keyNames1));
            label14.Location = new Point(Width / 2 - label14.Width / 2, label14.Location.Y);
            if (keyNames1.Length >= 5) label14.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        private void influenceButton3_Click(object sender, EventArgs e)
        {
            string[][] HexArray = new string[3][] { new string[2] { "", "" }, new string[4] { "", "", "", "" }, new string[1] { "" } };
            HexArray[0][0] = textBox25.Text;
            HexArray[0][1] = textBox23.Text;

            HexArray[1][0] = textBox29.Text;
            HexArray[1][1] = textBox28.Text;
            HexArray[1][2] = textBox30.Text;
            HexArray[1][3] = textBox31.Text;

            HexArray[2][0] = textBox27.Text;
            Hide();
            regFixer.PatchHexValues(HexArray);
        }

        private void HexEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            formHandle.FormDisposed = true;
        }
    }
}
