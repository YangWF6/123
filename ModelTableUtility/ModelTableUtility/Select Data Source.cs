using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using CSPComm;

namespace ModelTableUtility
{
    public partial class Select_Data_Source : Form
    {
        //internal PComm pca = new PComm();
        //string MdbUpdate;
        public Select_Data_Source(string strConn)
        {
            InitializeComponent();
            if (gl.tabname != "smainframe")//枚举
            {
                InitializeDialog(strConn);
            }
            comboBox1.Text = "MODTBL";
            //string IPAddress = null;
            //string Name = null;
            //int Port;
            //IPAddress = "192.168.10.1";//conn.IP_Port[0];
            //Port = 9090;
            ////int.Parse(conn.IP_Port[1]);
            ////Name = conn.IP_Port[2];
            ////MdbUpdate = "MdbUpdate";
            //MdbUpdate = System.Environment.MachineName;
            ////oPCommApp.PCommConnect(IPAddress, (ushort)Port, "PCommServer", MdbUpdate, conn.FilePath + "\\log");//model连接PComm服务器
            //pca.PCommConnect(IPAddress, (ushort)Port, "PCommServer", MdbUpdate, "D:\\PCSLog\\PComm");//连接PComm服务器
        }
        protected override void OnClosing(CancelEventArgs e)
        {
             System.Environment.Exit(0);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //this.Close();
            System.Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ok = true;
            gl.tabname ="smainframe";
            if (comboBox1.Text != "MODTBL")
            {
                MessageBox.Show("Table CAGP is not in Model Tables database");
            }
            else 
            {
                //strConnection = SearchDataBase(strConnection);//20181029暂时注释
                int number = 1;
                int num = 1;
                //string[] lab = new string[]{};
                string[] lab = { "grade_name" };

                string[,] combcontent = new string[5, 5];
               // combcontent[0, 0] = "";//下拉框显示钢种
                Select_SGP_Table form = new Select_SGP_Table(number, lab, combcontent, num);
                //InitializeDialog(conn.strconn);
                this.Hide();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    //  this.Close();
                    System.Environment.Exit(0);
                }
            }      
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //PCommLib.Message msg = new PCommLib.Message();
            //msg.To = "zzzz";
            //msg.From = "aaaa";
            //msg.Selector = "select";
            //msg.SendTime = DateTime.Now;


            //byte[] bb = new byte[2];

            //bb[0] = 1;

            //PCommLib.PCommApp pca = new PCommLib.PCommApp("192.168.11.3", 6666, "zanp");

            //pca.Connect();
            //pca.Post(msg.To, "", "", msg.ToBytes());

            //pca.Close();
        }

        private void Select_Data_Source_Load(object sender, EventArgs e)
        {

        }
    }
}
