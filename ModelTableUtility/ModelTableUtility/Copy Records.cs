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
    public partial class Copy_Records : Form
    {
        public Copy_Records(string tab_name, int key_num, string[] key, string strConnection)
        {
            InitializeComponent();
            InitializeDialog(tab_name, key_num, key, strConnection);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            if ((this.listBox1.Text.Trim() == "")||(this.listBox2.Text == ""))
            {
                MessageBox.Show("Incorrect Selection! Please correct the selection !");
                return;
            }
            else if ((this.listBox3.Visible == true)&&((this.listBox3.Text == "")||(this.listBox4.Text == "")))
            {
                MessageBox.Show("Incorrect Selection! Please correct the selection !");
                return;
            }
            else if ((this.listBox5.Visible == true) && ((this.listBox5.Text == "") || (this.listBox6.Text == "")))
            {
                MessageBox.Show("Incorrect Selection! Please correct the selection !");
                return;
            }
            key_from[0] = this.listBox1.Text.Trim();
            key_from[1] = this.listBox3.Text.Trim();          
            key_from[2] = this.listBox5.Text.Trim();

            select0_num = this.listBox2.SelectedItems.Count;
            select1_num = this.listBox4.SelectedItems.Count;
            select2_num = this.listBox6.SelectedItems.Count;

            for (i = 0; i < select0_num; i++)
            {
                key_to[0,i] = this.listBox2.SelectedItems[i].ToString().Trim();
            }
            for (i = 0; i < select1_num; i++)
            {
                key_to[1, i] = this.listBox4.SelectedItems[i].ToString().Trim();
            }
            for (i = 0; i < select2_num; i++)
            {
                key_to[2, i] = this.listBox6.SelectedItems[i].ToString().Trim();
            }
            ok = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Copy_Records_Load(object sender, EventArgs e)
        {

        }
    }
}
