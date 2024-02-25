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
    public partial class Initialize_table : Form
    {
        public Initialize_table(string tab_name, int key_num, string[] key, string strConnection)
        {
            InitializeComponent();
            InitializeDialog(tab_name, key_num, key);
        }

        private void button2_Click(object sender, EventArgs e) //add1
        {
            if (this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("text is null! Please Input again ");
                return;
            }
            else
            {
                this.listBox1.Items.Add(this.textBox2.Text.Trim());
                this.textBox2.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e) //delete1
        {
            if (this.listBox1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please chioce a item!");
                return;
            }
            for (int i=0; i < this.listBox1.SelectedItems.Count; i++)
            {
                this.listBox1.Items.Remove(this.listBox1.SelectedItems[i]);
            }
        }

        private void button3_Click(object sender, EventArgs e) //add2
        {
            if (this.textBox3.Text.Trim() == "")
            {
                MessageBox.Show("text is null! Please input new value!");
                return;
            }
            else
            {
                this.listBox2.Items.Add(this.textBox3.Text.Trim());
                this.textBox3.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e) //delete1
        {
            if (this.listBox2.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please chioce a item!");
                return;
            }
            for (int i=0; i < this.listBox2.SelectedItems.Count; i++)
            {
                this.listBox2.Items.Remove(this.listBox2.SelectedItems[i]);
            }
        }

        private void button6_Click(object sender, EventArgs e) //ok
        {
            int i;
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Text is null! Please input new value!");
                return;
            }
            else if ((this.groupBox2.Visible == true) && (listBox1.Items.Count == 0))
            {
                MessageBox.Show("Text is null! Please input new value!");
                return;
            }
            else if ((this.groupBox3.Visible == true) && (listBox2.Items.Count == 0))
            {
                MessageBox.Show("Text is null! Please input new value!");
                return;
            }

            this.text1_content = this.textBox1.Text;

            listbox1_num = listBox1.Items.Count;
            listbox2_num = listBox2.Items.Count;

            for (i = 0; i < listbox1_num; i++)
            {
                this.listbox1_content[i] = this.listBox1.Items[i].ToString();
            }

            for (i = 0; i < listbox2_num; i++)
            {
                this.listbox2_content[i] = this.listBox2.Items[i].ToString();
            }

            ok = true;
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)//cancle
        {
            this.Close();
        }
    }
}
