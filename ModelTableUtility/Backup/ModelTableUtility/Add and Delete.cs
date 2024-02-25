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
    public partial class Add_and_Delete : Form
    {
        public Add_and_Delete(string tab_name, int key_num,string [] key,string strConnection,bool add)
        {
            InitializeComponent();
            InitializeDialog(tab_name,key_num, key, strConnection, add);
        }

        private void button1_Click(object sender, EventArgs e)  //ok
        {
            string buffer="";
            if (!thisadd)
            {
                if ((this.listBox1.SelectedItem == null) || (this.listBox1.SelectedItem.ToString().Trim() == ""))
                {
                    MessageBox.Show("The list isn't selected , please select one!");
                    return;

                }
                else
                {
                    select_num = this.listBox1.SelectedItems.Count;
                    for (int i = 0; i < select_num; i++)
                    {
                        select_key[i] = this.listBox1.SelectedItems[i].ToString();
                        buffer = buffer + " \"" + select_key[i] + "\"";
                    }
                }
                DialogResult result = MessageBox.Show("Do you really want to delete" + buffer + " records?", "ModelTables", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            else
            {
                if ((textBox1.Text.Trim() == "") || (textBox1.Text == null))
                {
                    MessageBox.Show("Text is null !");
                    return;
                }
                else
                {
                    from = textBox1.Text.Trim();
                }
                if ((textBox2.Text.Trim() != "") && (Convert.ToInt32(textBox2.Text.Trim()) < Convert.ToInt32(from)))
                {
                    MessageBox.Show(" To can't less than From !");
                    return;
                }
                to = textBox2.Text;

                DialogResult result = MessageBox.Show("Do you really want to Add " + from + " "+to+" record?", "ModelTables", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            ok = true;

            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)  //cancle
        {
            this.Close();
        }
    }
}
