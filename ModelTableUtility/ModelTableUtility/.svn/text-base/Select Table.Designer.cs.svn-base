namespace ModelTableUtility
{
    partial class Select_SGP_Table
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        public bool enterok = false;
        public int number;
        public string [] combstring = new string [10];
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Select_SGP_Table));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.Location = new System.Drawing.Point(332, 350);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Silver;
            this.button2.Location = new System.Drawing.Point(332, 401);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 28);
            this.button2.TabIndex = 3;
            this.button2.Text = "CANCLE";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Select_SGP_Table
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(414, 441);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Select_SGP_Table";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select_SGP_Table";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label[] label = new System.Windows.Forms.Label[10];
        private System.Windows.Forms.ComboBox[] comboBox = new System.Windows.Forms.ComboBox[10];
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;

        private void IntialDialog(int number, string[] lab, string[,] combcontent, int num)   // lab和comb的个数，lab的内容，所有comb的内容，
        {
            int j = 0;
            this.number = number;
            for (int i = 0; i < this.number; i++)
            {
                this.label[i] = new System.Windows.Forms.Label();
                this.comboBox[i] = new System.Windows.Forms.ComboBox();

                // label
                // 
                this.label[i].AutoSize = true;
                this.label[i].Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.label[i].Location = new System.Drawing.Point(32, 28 + i * 50);
                this.label[i].Name = lab[i];
                this.label[i].Size = new System.Drawing.Size(41, 12);
                this.label[i].TabIndex = 0;
                this.label[i].Text = lab[i];
                // 
                // comboBox
                // 
                this.comboBox[i].Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.comboBox[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
                this.comboBox[i].FormattingEnabled = true;
                this.comboBox[i].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                for (j = 0; j < num; j++)
                {
                    if(combcontent[i, j]!=null)
                    this.comboBox[i].Items.Add(combcontent[i, j].Trim());
                }
                this.comboBox[i].Location = new System.Drawing.Point(145, 28 + i * 50);
                this.comboBox[i].Name = "comboBox1";
                this.comboBox[i].Size = new System.Drawing.Size(187, 20);
               // this.comboBox[i].TabIndex = 1;
                // 
                this.Controls.Add(this.comboBox[i]);
                this.Controls.Add(this.label[i]);

            }
            this.ResumeLayout(false);
            this.PerformLayout();


        }
    }

}