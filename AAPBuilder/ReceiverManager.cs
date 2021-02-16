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
    public partial class ReceiverManager : Form
    {
        public ReceiverManager()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && Array.IndexOf(receiverList, textBox1.Text) == -1) return;
            if (listBox1.Items.Count == 4) bunifuFlatButton1.Enabled = false;
            listBox1.Items.Add(textBox1.Text);
        }

        public string[] receiverList { get { return listBox1.Items.OfType<string>().ToArray(); } }
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(listBox1);
            selectedItems = listBox1.SelectedItems;
            if (selectedItems.Count == 0) return;
            if (listBox1.SelectedIndex != -1) for (int i = selectedItems.Count - 1; i >= 0; i--) listBox1.Items.Remove(selectedItems[i]);
            if (!bunifuFlatButton1.Enabled) bunifuFlatButton1.Enabled = true;
        }
    }
}
