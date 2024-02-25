using System.IO;
using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
//using Oracle.ManagedDataAccess.Client;
namespace ModelTableUtility
{
    partial class Add_and_Delete
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private bool thisadd;
        public string [] select_key = new string [100];
        public int select_num = 0;
        public string from;
        public string to;
        public string[,] repeat = new string[10, 70000];
        public int key0 = 0;
        public int key1 = 0;
        public int key2 = 0;
        public bool ok = false;
        public int count = 0;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add_and_Delete));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.Location = new System.Drawing.Point(382, 371);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Silver;
            this.button2.Location = new System.Drawing.Point(487, 371);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 28);
            this.button2.TabIndex = 1;
            this.button2.Text = "CANCLE";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(24, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "From";
            this.label1.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(76, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(114, 24);
            this.textBox1.TabIndex = 3;
            this.textBox1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(74, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 15);
            this.label2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(232, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 15);
            this.label3.TabIndex = 6;
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(403, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 15);
            this.label4.TabIndex = 8;
            this.label4.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(76, 106);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(114, 24);
            this.textBox2.TabIndex = 9;
            this.textBox2.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(24, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "To";
            this.label5.Visible = false;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(77, 48);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(143, 169);
            this.listBox1.TabIndex = 12;
            this.listBox1.Visible = false;
            // 
            // listBox2
            // 
            this.listBox2.BackColor = System.Drawing.Color.Silver;
            this.listBox2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 15;
            this.listBox2.Location = new System.Drawing.Point(235, 48);
            this.listBox2.Name = "listBox2";
            this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox2.Size = new System.Drawing.Size(143, 169);
            this.listBox2.TabIndex = 13;
            this.listBox2.Visible = false;
            // 
            // listBox3
            // 
            this.listBox3.BackColor = System.Drawing.Color.Silver;
            this.listBox3.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 15;
            this.listBox3.Location = new System.Drawing.Point(406, 48);
            this.listBox3.Name = "listBox3";
            this.listBox3.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox3.Size = new System.Drawing.Size(143, 169);
            this.listBox3.TabIndex = 14;
            this.listBox3.Visible = false;
            // 
            // Add_and_Delete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(572, 414);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Add_and_Delete";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void InitializeDialog(string tab_name,int key_num,string [] key,string strConnection,bool add)
        {
            string select = "SELECT * FROM " + tab_name;
            string buffer;
            int maxnum = 0;


            thisadd = add;
            if (add)
            {
                this.textBox1.Visible = true;
                this.Text = "Add " + tab_name + " Table";
            }
            else
            {
                this.listBox1.Visible = true;
                this.Text = "Select Record From " + tab_name;
            }
            if (key_num == 1)
            {
                this.label2.Text = key[0];
                this.label2.Visible = true;

            }
            else if (key_num == 2)
            {
                this.label2.Text = key[0];
                this.label3.Text = key[1];
                            
                this.label2.Visible = true;
                this.label3.Visible = true;              
                this.listBox2.Visible = true;
                this.textBox2.Visible = true;

            }
            else if (key_num == 3)
            {
                this.label2.Text = key[0];
                this.label3.Text = key[1];
                this.label4.Text = key[2];

                this.label3.Visible = true;
                this.label2.Visible = true;
                this.label4.Visible = true;
                this.listBox2.Visible = true;
                this.listBox3.Visible = true;
            }
            else
            {
                MessageBox.Show("the number of key is 0 or more than 3 !");
                count = -1;
                return;
            }
            if ((key[0] != "grade_name")&&(add))
            {
                this.textBox2.Visible = true;
                this.label5.Visible = true;
                this.label1.Visible = true;
            }

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
                count++;
                for (int i = 0; i < key_num; i++)
                {
                    buffer = thisReader[key[i]].ToString().Trim();

                    if ((i==0)&&SearchRepeat(buffer, i, key0, repeat))
                    {
                        listBox1.Items.Add(buffer) ;
                        repeat[i, key0] = buffer;
                        key0++;
                    }
                    if ((key[0] != "grade_name")&&(i==0))
                    {
                        if (maxnum < Convert.ToInt32(buffer))
                        {
                            maxnum = Convert.ToInt32(buffer);
                        }
                    }
                    else if (i == 1)
                    {
                        if (SearchRepeat(buffer, i, key1, repeat))
                        {
                            listBox2.Items.Add(buffer);
                            repeat[i, key1] = buffer;
                            key1++;
                        }
                    }
                    else if (i == 2)
                    {
                        if (SearchRepeat(buffer, i, key2, repeat))
                        {
                            listBox3.Items.Add(buffer);
                            repeat[i, key2] = buffer;
                            key2++;
                        }
                    }
                }
            }
            if (key[0] != "grade_name")
            {
                this.textBox1.Text = Convert.ToString(maxnum+1);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private ListBox listBox1;
        private ListBox listBox2;
        private ListBox listBox3;
    }
}