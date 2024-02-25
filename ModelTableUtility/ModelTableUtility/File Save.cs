using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModelTableUtility
{
    public partial class File_Save : Form
    {
        public File_Save(string tab_name, int key_num, string[] key, string strConnection, bool save, bool copy)
        {
            InitializeComponent();
            InitializeDialog(tab_name, key_num, key, strConnection,save,copy);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.listBox1.Text == "")
            {
                MessageBox.Show("Selection is null! please check !");
                return;
            }
            Select_num[0]=listBox1.SelectedItems.Count;
            Select_num[1] = listBox2.SelectedItems.Count;
            Select_num[2] = listBox3.SelectedItems.Count;

            for (int i = 0; i < Select_num[0]; i++)
            {
                select_key[0,i] = this.listBox1.SelectedItems[i].ToString();
            }
            for (int i = 0; i < Select_num[1]; i++)
            {
                select_key[1, i] = this.listBox2.SelectedItems[i].ToString();
            }
            for (int i = 0; i < Select_num[2]; i++)
            {
                select_key[2, i] = this.listBox3.SelectedItems[i].ToString();
            }
            ok = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
