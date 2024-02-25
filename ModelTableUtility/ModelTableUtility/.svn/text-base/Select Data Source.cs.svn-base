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
    public partial class Select_Data_Source : Form
    {
        public Select_Data_Source(string strConn)
        {
            InitializeComponent();
            InitializeDialog(strConn);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ok = true;
            strConnection = SearchDataBase(strConnection);
            this.Close();
        }
    }
}
