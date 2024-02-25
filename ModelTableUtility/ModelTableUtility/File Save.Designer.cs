using System.IO;
using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
//using Oracle.ManagedDataAccess.Client;
namespace ModelTableUtility
{
    partial class File_Save
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public string[,] repeat = new string[10, 70000];
        public int key0 = 0;
        public int key1 = 0;
        public int key2 = 0;
        public int count = 0;
        public bool ok = false;
       // public string select_key;
        public string [,] select_key = new string [3,350];
        public int [] Select_num= new int [3];

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(File_Save));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(28, 44);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(149, 139);
            this.listBox1.TabIndex = 1;
            // 
            // listBox2
            // 
            this.listBox2.BackColor = System.Drawing.Color.Silver;
            this.listBox2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 15;
            this.listBox2.Location = new System.Drawing.Point(198, 44);
            this.listBox2.Name = "listBox2";
            this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox2.Size = new System.Drawing.Size(149, 139);
            this.listBox2.TabIndex = 2;
            this.listBox2.Visible = false;
            // 
            // listBox3
            // 
            this.listBox3.BackColor = System.Drawing.Color.Silver;
            this.listBox3.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 15;
            this.listBox3.Location = new System.Drawing.Point(371, 44);
            this.listBox3.Name = "listBox3";
            this.listBox3.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox3.Size = new System.Drawing.Size(149, 139);
            this.listBox3.TabIndex = 3;
            this.listBox3.Visible = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Silver;
            this.button2.Location = new System.Drawing.Point(445, 376);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 28);
            this.button2.TabIndex = 11;
            this.button2.Text = "CANCLE";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.Location = new System.Drawing.Point(327, 376);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 28);
            this.button1.TabIndex = 10;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(25, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(195, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(368, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "label3";
            this.label3.Visible = false;
            // 
            // File_Save
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(544, 416);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "File_Save";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Records";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void InitializeDialog(string tab_name, int key_num, string[] key, string strConnection, bool save, bool copy)
        {
            string select = "SELECT * FROM " + tab_name;
            string buffer;

            this.label1.Text = key[0];
            if (key_num == 1)
            {
            }
            else if (key_num == 2)
            {

                this.label2.Text = key[1];

                this.label2.Visible = true;
                this.listBox2.Visible = true;


            }
            else if (key_num == 3)
            {
                this.label2.Text = key[1];

                this.label2.Visible = true;
                this.listBox2.Visible = true;

                this.label3.Text = key[1];
                this.label3.Visible = true;
                this.listBox3.Visible = true;

            }
            else
            {
                MessageBox.Show("the number of key is 0 or more than 3 !");
                count = -1;
                return;
            }
            if (!save)
            {
                this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            }
            if (copy)
            {
                this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
                this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
                this.listBox3.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
                this.listBox2.BackColor = System.Drawing.Color.White;
                this.listBox3.BackColor = System.Drawing.Color.White;
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
                for (int i = 0; i < key_num; i++)
                {
                    buffer = thisReader[key[i]].ToString().Trim();

                    if ((i == 0) && SearchRepeat(buffer, i, key0, repeat))
                    {
                        listBox1.Items.Add(buffer);
                        repeat[i, key0] = buffer;
                        key0++;
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
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}