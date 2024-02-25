using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
//using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;

namespace ModelTableUtility
{
    public partial class Select_SGP_Table : Form
    {
        public Select_SGP_Table(int number, string[] lab, string[,] combcontent, int num)
        {
            InitializeComponent();
            IntialDialog(number, lab, combcontent, num);
                
            String strConn = conn.strconn;
            string select=null;
            if (gl.tabname == "smainframe")
            {
                 select = "SELECT GRADE_NAME FROM CAGP";
            }
            else
            {
                select = "SELECT " + lab[0];
                for (int i = 1; i < number;i++ )
                {
                    select = select + "," + lab[i];
                }
                select = select + " FROM " + gl.subtabname;
            }
            if (gl.tabname != "smainframe")
            {
                return;
            }
            SqlConnection thisConnection = new SqlConnection(strConn);
            try
            {
                thisConnection.Open();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                //fail = true;
                return;
            }
            SqlCommand thisCommand = thisConnection.CreateCommand();
            thisCommand.CommandText = select;
            try
            {
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                if ((gl.tabname == "smainframe"))
                {
                    int t_i = 0;
                    int c_i = 0;
                    string[] temp_i = new string[350];
                    while (thisReader.Read())
                    {
                        temp_i[c_i] = thisReader[lab[0]].ToString().Trim();
                        c_i++;
                    }
                    string[] stemp_i = new string[c_i];
                    //字符串排序
                    for (int j = 0; j < c_i;j++ )
                    {
                        stemp_i[j] = temp_i[j];
                    }
                    Array.Sort(stemp_i);

                    for (int i = 0; i < c_i; i++)
                    {
                        comboBox[0].Items.Add(stemp_i[i]);
                       gl.Gradeidx[i]=stemp_i[i];
                    }
                    comboBox[0].Text = stemp_i[0];
                }
                else
                {
                    while (thisReader.Read())
                    {
                        for (int i = 0; i < number; i++)
                        {
                            comboBox[i].Items.Add(thisReader[lab[i]].ToString().Trim());

                        }
                    }
                    comboBox[0].Text = thisReader[lab[0]].ToString().Trim();
                }
                //字符串排序
                thisConnection.Close();
                comboBox[0].Text = "ModelTables";
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                return;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // gl.win_shut = "shut";
            /// this.Close(); 
            System.Environment.Exit(0);
            return;
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
                enterok = true;
                this.Hide();
                if (gl.tabname == "smainframe")
                {
                    gl.subtabname = combstring[0];
                    sMainframe dlg = new sMainframe();
                    dlg.ShowDialog();
                }
                this.Close();
            }
        }
        private void Select_SGP_Table_FormClosing(object sender, FormClosingEventArgs e)
        {
           // DialogResult RESULT;
            e.Cancel = false;
        }
        private void Select_SGP_Table_Load(object sender, EventArgs e)
        {
        }
    }
}
