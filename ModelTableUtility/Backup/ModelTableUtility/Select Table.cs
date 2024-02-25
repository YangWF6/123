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
    public partial class Select_SGP_Table : Form
    {
        public Select_SGP_Table(int number, string[] lab, string[,] combcontent, int num)
        {
            InitializeComponent();
            IntialDialog(number, lab, combcontent, num);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool just = true;
            for(int i=0;i<number;i++)
            {
                if ((comboBox[i].SelectedItem == null)|| (comboBox[i].SelectedItem.ToString().Trim() == ""))
                {
                    MessageBox.Show("Options can't be null!");
                    just = false;
                    break;
                }
                combstring[i] = comboBox[i].SelectedItem.ToString();
            }
            if (just)
            {
                this.Close();
            }
            enterok = true;
        }
    }
}
