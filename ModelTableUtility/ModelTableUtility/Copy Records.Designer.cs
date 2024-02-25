using System.IO;
using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
//using Oracle.ManagedDataAccess.Client;
namespace ModelTableUtility
{
    partial class Copy_Records
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public string[,] repeat = new string[10, 70000];
        public int key0 = 0;
        public int key1 = 0;
        public int key2 = 0;
        public bool ok = false;
        public int count = 0;

        public string [] key_from = new string [3];
        public string[,] key_to = new string[3,400];

        public int select0_num ;
        public int select1_num ;
        public int select2_num;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Copy_Records));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.listBox5 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox6 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(43, 32);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(149, 139);
            this.listBox1.TabIndex = 0;
            // 
            // listBox2
            // 
            this.listBox2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 15;
            this.listBox2.Location = new System.Drawing.Point(349, 32);
            this.listBox2.Name = "listBox2";
            this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox2.Size = new System.Drawing.Size(149, 139);
            this.listBox2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(43, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(346, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "To:";
            // 
            // listBox3
            // 
            this.listBox3.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 15;
            this.listBox3.Location = new System.Drawing.Point(43, 195);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(149, 79);
            this.listBox3.TabIndex = 4;
            this.listBox3.Visible = false;
            // 
            // listBox4
            // 
            this.listBox4.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox4.FormattingEnabled = true;
            this.listBox4.ItemHeight = 15;
            this.listBox4.Location = new System.Drawing.Point(349, 195);
            this.listBox4.Name = "listBox4";
            this.listBox4.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox4.Size = new System.Drawing.Size(149, 79);
            this.listBox4.TabIndex = 5;
            this.listBox4.Visible = false;
            // 
            // listBox5
            // 
            this.listBox5.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox5.FormattingEnabled = true;
            this.listBox5.ItemHeight = 15;
            this.listBox5.Location = new System.Drawing.Point(43, 297);
            this.listBox5.Name = "listBox5";
            this.listBox5.Size = new System.Drawing.Size(149, 64);
            this.listBox5.TabIndex = 6;
            this.listBox5.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.Location = new System.Drawing.Point(279, 494);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 28);
            this.button1.TabIndex = 8;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Silver;
            this.button2.Location = new System.Drawing.Point(397, 494);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 28);
            this.button2.TabIndex = 9;
            this.button2.Text = "CANCLE";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox6
            // 
            this.listBox6.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox6.FormattingEnabled = true;
            this.listBox6.ItemHeight = 15;
            this.listBox6.Location = new System.Drawing.Point(349, 297);
            this.listBox6.Name = "listBox6";
            this.listBox6.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox6.Size = new System.Drawing.Size(149, 64);
            this.listBox6.TabIndex = 10;
            this.listBox6.Visible = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(202, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 37);
            this.label3.TabIndex = 11;
            this.label3.Text = "grade_name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(222, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 45);
            this.label4.TabIndex = 12;
            this.label4.Text = "label4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(225, 318);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 33);
            this.label5.TabIndex = 13;
            this.label5.Text = "label5";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Visible = false;
            // 
            // Copy_Records
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(538, 546);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox5);
            this.Controls.Add(this.listBox4);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Copy_Records";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copy From Record to Multiple Records";
            this.Load += new System.EventHandler(this.Copy_Records_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void InitializeDialog(string tab_name, int key_num, string[] key, string strConnection)
        {
            string select = "SELECT * FROM " + tab_name;
            string buffer;

            this.label3.Text = key[0];
            if (key_num == 1)
            {
            }
            else if (key_num == 2)
            {
                
                this.label4.Text = key[1];
                            
                this.label4.Visible = true;              
                this.listBox3.Visible = true;
                this.listBox4.Visible = true;

            }
            else if (key_num == 3)
            {
                this.label4.Text = key[1];
                this.label4.Visible = true;
                this.listBox3.Visible = true;
                this.listBox4.Visible = true;

                this.label5.Text = key[2];
                this.label5.Visible = true;
                this.listBox5.Visible = true;
                this.listBox6.Visible = true;
            }
            else
            {
                MessageBox.Show("the number of key is 0 or more than 3 !");
                count = -1;
                return;
            }

            //SqlConnection thisConnection = new SqlConnection(conn.strconn);
            //try
            //{
            //    thisConnection.Open();
            //}
            //catch (Exception a)
            //{
            //    MessageBox.Show(a.Message);
            //    //fail = true;
            //    return;
            //}
            //SqlCommand thisCommand = thisConnection.CreateCommand();

            //thisCommand.CommandText = select;
            //SqlDataReader thisReader = thisCommand.ExecuteReader();
            int t_i0 = 0;
            int t_i1 = 0;
            int t_i2 = 0;
            int c_i0 = 0;
            int c_i1 = 0;
            int c_i2 = 0;
            string[] temp_i0 = new string[350];
            string[] temp_i1 = new string[350];
            string[] temp_i2 = new string[350];
            
            //while (thisReader.Read())// 读取符合索引要求得所以得行数。如2行  
            //{
            //    for (int i = 0; i < key_num; i++)
            //    {
            //        buffer = thisReader[key[i]].ToString().Trim();
                    
            //        if ((i==0)&&SearchRepeat(buffer, i, key0, repeat))
            //        {
            //            listBox1.Items.Add(buffer) ;
            //            listBox2.Items.Add(buffer);
            //            repeat[i, key0] = buffer;
            //            key0++;
            //        }
            //        else if (i == 1)
            //        {
            //            if (SearchRepeat(buffer, i, key1, repeat))
            //            {
            //                listBox3.Items.Add(buffer);
            //                listBox4.Items.Add(buffer);
            //                repeat[i, key1] = buffer;
            //                key1++;
            //            }
            //        }
            //        else if (i == 2)
            //        {
            //            if (SearchRepeat(buffer, i, key2, repeat))
            //            {
            //                listBox5.Items.Add(buffer);
            //                listBox6.Items.Add(buffer);
            //                repeat[i, key2] = buffer;
            //                key2++;
            //            }
            //        }
            //    }
            //}
            //thisReader.Close();
            //thisConnection.Close();
            ///////////////////////////////
            count = 0;
            int m = 0;
            buffer = null;
            for (int i = 0; i < key_num; i++)
            {
                
                if (tab_name != "CAGP" && tab_name != "SGP" && tab_name != "RAGP" && tab_name != "FAGP")
                {
                    int num_i = 0;
                    int[,] tepm_n = new int[5, 50];
                    int[] stepm_n = new int[50];
                    string[] tttt = new string[50];
                    SqlConnection thisConnection1 = new SqlConnection(conn.strconn);
                    try
                    {
                        thisConnection1.Open();
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message);
                        //fail = true;
                        return;
                    }
                    SqlCommand thisCommand1 = thisConnection1.CreateCommand();

                    thisCommand1.CommandText = select;
                    SqlDataReader thisReader1 = thisCommand1.ExecuteReader();
                    m = 0;
                    while (thisReader1.Read())
                    {
                        count++;
                        
                        buffer = thisReader1[key[i]].ToString();
                        if (SearchRepeat(buffer, i, m, repeat))
                        {
                            repeat[i, m] = buffer;//
                            m++;
                        }
                    }
                    thisReader1.Close();
                    thisConnection1.Close();
                    //排序
                    //先将字符串转成数字
                    for (int nn_i = 0; nn_i < m; nn_i++)
                    {
                        tttt[nn_i] = repeat[i, nn_i];
                    }

                    for (int nn_i = 0; nn_i < m; nn_i++)
                    {
                        if (repeat[i, nn_i] == null)
                        {
                            break;
                        }
                        tepm_n[i, nn_i] = int.Parse(tttt[nn_i]);
                    }
                    //排序
                    for (int m_i = 0; m_i < m; m_i++)
                    {
                        for (int t_i = 0; t_i < m - m_i; t_i++)
                        {
                            if (tepm_n[i, t_i] > tepm_n[i, t_i + 1])
                            {
                                num_i = tepm_n[i, t_i];
                                tepm_n[i, t_i] = tepm_n[i, t_i + 1];
                                tepm_n[i, t_i + 1] = num_i;
                            }
                        }
                    }
                    //数字转成字符串
                    for (int nn_i = 0; nn_i < m; nn_i++)
                    {
                        //combcontent[i, m] = Convert.ToString(tepm_n[i, m]);
                        repeat[i, nn_i] = tepm_n[i, nn_i + 1].ToString();
                        if(i==0)
                        {
                            listBox1.Items.Add(repeat[i, nn_i]);
                            listBox2.Items.Add(repeat[i, nn_i]);
                        }else if(i==1)
                        {
                            listBox3.Items.Add(repeat[i, nn_i]);
                            listBox4.Items.Add(repeat[i, nn_i]);
                        }else if(i==2)
                        {
                            listBox5.Items.Add(repeat[i, nn_i]);
                            listBox6.Items.Add(repeat[i, nn_i]);
                        }
                        
                    }
                }
                //////=-----------------------------------------------------------------------------------------------------
                else
                {
                    buffer = null;
                    SqlConnection thisConnection2 = new SqlConnection(conn.strconn);
                    try
                    {
                        thisConnection2.Open();
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message);
                        //fail = true;
                        return;
                    }
                    SqlCommand thisCommand2 = thisConnection2.CreateCommand();

                    thisCommand2.CommandText = select;
                    SqlDataReader thisReader2 = thisCommand2.ExecuteReader();
                    m = 0;
                    count = 0;
                    while (thisReader2.Read())// 读取符合索引要求得所以得行数。如2行
                    {
                        count++;
                        // Output ID and name columns
                        buffer = thisReader2[key[i]].ToString();
                        if (SearchRepeat(buffer, i, m, repeat))
                        {
                            repeat[i, m] = buffer;//
                            m++;
                        }
                    }
                    
                    thisReader2.Close();
                    thisConnection2.Close();
                    //字符串排序
                    string[] tttt = new string[m];
                    for (int n_i = 0; n_i < m; n_i++)
                    {
                        tttt[n_i] = repeat[i, n_i];
                    }
                    Array.Sort(tttt);
                    for (int n_i = 0; n_i < m; n_i++)
                    {
                        repeat[i, n_i] = tttt[n_i]; 
                        if (i == 0)
                        {
                            listBox1.Items.Add(repeat[i, n_i]);
                            listBox2.Items.Add(repeat[i, n_i]);
                        }
                        else if (i == 1)
                        {
                            listBox3.Items.Add(repeat[i, n_i]);
                            listBox4.Items.Add(repeat[i, n_i]);
                        }
                        else if (i == 2)
                        {
                            listBox5.Items.Add(repeat[i, n_i]);
                            listBox6.Items.Add(repeat[i, n_i]);
                        }
                    }
                }

            }
            ////////////////////////////////
            SqlConnection thisConnection = new SqlConnection(conn.strconn);
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
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            while (thisReader.Read())// 读取符合索引要求得所以得行数。如2行  
            {
                for (int i = 0; i < key_num; i++)
                {
                    buffer = thisReader[key[i]].ToString().Trim();

                    if ((i == 0) && SearchRepeat(buffer, i, key0, repeat))
                    {
                        key0++;
                    }
                    else if ((i == 1) && SearchRepeat(buffer, i, key1, repeat))
                    {
                            key1++;
                        
                    }
                    else if ((i == 2) && SearchRepeat(buffer, i, key2, repeat))
                    {
                            key2++;
                    }
                }
            }
            thisReader.Close();
            thisConnection.Close();
            
        }

        private bool SearchRepeat(string buffer, int stubon, int num, string[,] buffer2)
        {
            for (int i = 0; i < num; i++)
            {
                if (buffer2[stubon, i] == buffer)
                {
                    return false;
                }
            }
            return true;
        }
        

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.ListBox listBox4;
        private System.Windows.Forms.ListBox listBox5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}