using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using PCommLib;
using CSPComm;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Data;
using System.Threading;

namespace ModelTableUtility
{
    
    public partial class sMainframe : Form
    {
        DataGridView current_grid;
        int current_row;
        int current_col;
        PComm oPCommApp = new PComm();
        string MdbUpdate = null;

        internal PComm pca = new PComm();
        internal PCommLib.Message oMessage = new PCommLib.Message();
        public DataTable SQLConne(string constr)
        {
            try
            {
                string connstring = "";
                SqlConnection myconn = new SqlConnection(connstring);
                myconn.Open();
                SqlDataAdapter da = new SqlDataAdapter(constr, myconn);
                DataTable dt = new DataTable();
                dt.Clear();
                da.Fill(dt);
                myconn.Dispose();
                myconn.Close();
                return dt;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
            
        }
        public sMainframe()
        {try
            {
                if (!Config())
                {
                    return;
                }
                if (gl.tabname != "smainframe")
                {
                    //if (DataSource())
                    //{
                    //    return;
                    //}
                }
                if (InitializeFile() == 3)
                {
                    return;
                }
                if (!ReadAllCfg())
                {
                    MessageBox.Show("Fail to read " + ".cfg file! ");
                }
                else if (ReadAllDat())
                {
                    InitializeComponent();
                    Initalcompent();

                }
                else
                {
                    MessageBox.Show("Fail to read " + ".dat file! ");
                    return;
                }

                cmb_cell.Visible = false;

                //将所选钢种信息在主画面弹出的时候就显示的主画面
                if (gl.tabname == "smainframe")
                {
                    //cmb_cell.Visible = false;
                    //string tabname = tabControl1.SelectedTab.Name;
                    string tabname = "CAGP";
                    tabControl1.SelectedTab.Name = "CAGP";
                    if (!tablestruct[SearchGridNum(tabname)].live)
                    {
                        Select_Table();
                        gl.tabname = "change";
                    }
                }
                else
                {
                    string tabname = tabControl1.SelectedTab.Name;
                    if (!tablestruct[SearchGridNum(tabname)].live)
                    {
                        Select_Table();
                        //gl.tabname = "change";
                    }
                }
                string IPAddress = null;
                string Name = null;
                int Port;
                IPAddress = "172.21.10.201";//conn.IP_Port[0];
                Port = 9090;//int.Parse(conn.IP_Port[1]);
                            //Name = conn.IP_Port[2];
                            //MdbUpdate = "MdbUpdate";
                MdbUpdate = System.Environment.MachineName;
                byte[] temp = new byte[1024];
            
                pca.PCommConnect(IPAddress, (ushort)Port, "PCommServer", MdbUpdate, "D:\\PCSLog\\PComm");//连接PComm服务器 暂时注释20211119武凯
                //20231214 添加连接PCommConnect报警
                MessageBox.Show("连接PCommConnect");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
               // pca.PCommExit(0);
                 System.Environment.Exit(0);
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            string aaa;
            int firstline=0;
            int columnline = 0;
            int left=0;
            int i;
            int left_hide = 0;
            DataGridView grid = sender as DataGridView;
            if (grid.CurrentCell.Value != null)
            {
                aaa = grid.CurrentCell.Value.ToString();
                if (e.ColumnIndex > -1)
                {

                    if ((aaa == "TRUE") || (aaa == "FALSE"))
                    {
                        //3更新下拉框的值
                        current_grid = grid;
                        current_row = e.RowIndex;
                        current_col = e.ColumnIndex;
                        firstline = grid.FirstDisplayedCell.RowIndex;
                        columnline = grid.FirstDisplayedCell.ColumnIndex;

                        int top = 0;
                        //1找位置
                        top = tabControl1.Top + grid.Top + (21 * (e.RowIndex - firstline)) + 5;//grid.ColumnHeadersHeight  
                        for (i = 0; i < columnline; i++)
                        {
                            left_hide = left_hide + grid.Columns[i].Width;
                        }
                        left_hide = tablestruct[SearchGridNum(tabControl1.SelectedTab.Name)].horizon_long - left_hide;
                        for (i = columnline; i < e.ColumnIndex; i++)
                        {
                            left = left + grid.Columns[i].Width;
                        }
                        left = left - left_hide;
                        //2定位下拉款
                        cmb_cell.Top = top;
                        cmb_cell.Left = tabControl1.Left + grid.Left + left + grid.RowHeadersWidth + 5;
                        cmb_cell.Width = grid.Columns[e.ColumnIndex].Width;  //120
                        cmb_cell.Height = 30;
                        cmb_cell.Text = aaa;

                        cmb_cell.Visible = true;
                    }
                    else
                    {
                        firstline = grid.FirstDisplayedCell.RowIndex;
                        columnline = grid.FirstDisplayedCell.ColumnIndex;
                        //grid[firstline,columnline].;
                        grid.BeginEdit(true);

                    }
                }
            }
        }

        private void SelectChange(object sender, EventArgs e)
        {
            cmb_cell.Visible = false;
        }
        private void ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cmb_cell.Visible = false;
        }
        private void RowHeadersWidthChanged(object sender, EventArgs e)
        {
            cmb_cell.Visible = false;
        }
        private void cmb_cell_SelectedValueChanged(object sender, EventArgs e)
        {
            if (current_grid.Rows[current_row].Cells[current_col].Value.ToString() != cmb_cell.Text)
            {
                AddToList();
                current_grid.Rows[current_row].Cells[current_col].Value = cmb_cell.Text;
            }

        }
        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            cmb_cell.Visible = false;
            if (e.ScrollOrientation.ToString() == "HorizontalScroll")
            {
                tablestruct[SearchGridNum(tabControl1.SelectedTab.Name)].horizon_long = e.NewValue;               
            }
            
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)   
        {           
            cmb_cell.Visible = false;
            string tabname = tabControl1.SelectedTab.Name;
            gl.subtabname = tabname;
            if (!tablestruct[SearchGridNum(tabname)].live)
            {
                Select_Table();              
            }
        }
        
        private void dataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            this.label3.Text = "";
            this.toolStripStatusLabel1.Text = "";
            this.label4.Text = "";
            int i=0;
            int l_num = 0;
            int y = e.Y;
            string cfgname = tabControl1.SelectedTab.Name;
            int num=SearchGridNum(cfgname);
            System.Windows.Forms.DataGridViewRow gridrow1;
            if (this.dataGridView[num].Rows.Count < 1)
            {
                return;
            }

            if( (e.X < this.dataGridView[num].RowHeadersWidth)&&(tablestruct[num].live) )
            {
                while(true)
                {
                    y = y - 21;
                    if (y < 0)
                    {
                        break;
                    }
                    l_num++;
                }
                if ((this.dataGridView[num].Rows.Count > l_num + this.dataGridView[num].FirstDisplayedCell.RowIndex))
                {

                    gridrow1 = this.dataGridView[num].Rows[l_num + this.dataGridView[num].FirstDisplayedCell.RowIndex];
                    if (gridrow1.HeaderCell.Value != null) 
                    {
                        for(i=0;i<this.dataGridView[num].Rows.Count;i++)
                        {
                            if (cfgstruct[num, i].explant == gridrow1.HeaderCell.Value.ToString())
                            {
                                this.toolStripStatusLabel1.Text = tablestruct[num].name+"."+cfgstruct[num, i].name;
                                this.label1.Text = cfgstruct[num, i].explant;
                                this.label3.Text = "Unit: "+cfgstruct[num, i].unit;
                                this.label4.Text = "DataType: " + cfgstruct[num, i].datatype;
                                break;
                            }
                        }
                    }
                }
            }           
        }

        private void button1_Click(object sender, EventArgs e)  //select Record
        {
            string cfgname1 = tabControl1.SelectedTab.Name;
            gl.subtabname = cfgname1;
            int slabnum1 = SearchGridNum(cfgname1);
            if (slabnum1==0)
            {
                Select_Table();
                return;
            }
            int numb=0;
            int size;
            size = contant.MaxSize;
            for (int i = 0; i < size; i++)
            {
                if (cfgstruct[slabnum1, i].key == 1)
                {
                    numb++;
                }
            }
            if (gl.TData_Same[slabnum1]==1)
            {
                for (int i = 0; i < numb; i++)
                {
                    if (this.dataGridView[slabnum1].RowCount > 0)
                    {
                        gl.TempName[i] = this.dataGridView[slabnum1].Rows[i].Cells[0].Value.ToString();
                        gl.TempName[4] = "same_data";
                    }
                    
                }
            }
            
            if (slabnum1 > 50)
            {
                return;
            }
            list[slabnum1].Destroy();
            Select_Table();
        }
        private void Select_Table()
        {
            string select;
            int m;
            string cfgname = tabControl1.SelectedTab.Name;
            int numb = 0;
            string[] lab = new string[10];
            int slabnum = SearchGridNum(cfgname);
            int count=0;
            if (slabnum != 51)
            {
                tablestruct[SearchGridNum(cfgname)].horizon_long = 0;
                int size;
                size = contant.MaxSize;
                for (int i = 0; i < size; i++)
                {
                    if (cfgstruct[slabnum, i].key == 1)
                    {
                        lab[numb] = cfgstruct[slabnum, i].name;
                        numb++;
                    }
                }
                string[,] combcontent = new string[numb, 70000];

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
                
                    for (int i = 0; i < numb; i++)
                    {
                        m = 0;
                        select = "SELECT " + lab[i] + " FROM " + cfgname;
                        thisCommand.CommandText = select;
                        // Execute DataReader for specified command
                        SqlDataReader thisReader = thisCommand.ExecuteReader();
                        ///=-----------------------------------------------------------------------------------------------------
                        ///
                        if (cfgname != "CAGP" && cfgname != "SGP" && cfgname != "RAGP" && cfgname != "FAGP")
                        {
                            int num_i = 0;
                            int[,] tepm_n = new int[5, 50];
                            int[] stepm_n = new int[50];
                            string[] tttt = new string[50];
                            while (thisReader.Read())
                            {
                                count++;
                                string buffer = thisReader[lab[i]].ToString();
                                if (duplicate(buffer, i, m, combcontent))
                                {
                                    combcontent[i, m] = buffer;//
                                    m++;
                                }
                            }
                            //排序
                            //先将字符串转成数字
                            for(int nn_i=0;nn_i < m; nn_i++)
                            {
                                tttt[nn_i] = combcontent[i, nn_i];
                            }
                             
                            for (int nn_i = 0; nn_i < m; nn_i++)
                            {
                                if (combcontent[i, nn_i] == null)
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
                                combcontent[i, nn_i] = tepm_n[i, nn_i+1].ToString();
                            }
                        }
                        //////=-----------------------------------------------------------------------------------------------------
                        else 
                        { 
                            while (thisReader.Read())// 读取符合索引要求得所以得行数。如2行
                            {
                                count++;
                                // Output ID and name columns
                                string buffer = thisReader[lab[i]].ToString();
                                if (duplicate(buffer, i, m, combcontent))
                                {
                                    combcontent[i, m] = buffer;//
                                    m++;
                                }
                            }
                            //字符串排序
                            string[] tttt = new string[m];
                            for (int n_i = 0; n_i < m; n_i++)
                            {
                                tttt[n_i] = combcontent[i, n_i];
                            }
                            Array.Sort(tttt);
                            for (int n_i = 0; n_i < m; n_i++)
                            {
                                combcontent[i, n_i]=tttt[n_i];
                            }
                        }
                        thisReader.Close();
                    }
                    thisConnection.Close();

                    if (count == 0)
                    {
                        DialogResult result = MessageBox.Show("Database table " + cfgname + " is empty.\nDo you want to initialize the table?", "ModelTables", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Cancel || result == DialogResult.OK)
                        {
                            return;
                        }

                        Initial_table();
                        return;
                    }
                    Select_SGP_Table dlg = new Select_SGP_Table(numb, lab, combcontent, 70000);

                    if (gl.tabname != "smainframe")
                    {
                        dlg.Text = cfgname;
                        dlg.ShowDialog();
                        dlg.Close();
                    }
                    else
                    {
                        dlg.combstring[0] = gl.subtabname;
                        dlg.enterok = true;
                    }
                if(gl.win_shut == "shut")
                {
                    dlg.enterok = false;
                    gl.win_shut = "open";
                }
                    
                    if (dlg.enterok)
                    {
                        Data_Grid_View(lab, dlg.combstring, numb);
                    }
            }
        }

        private void button9_Click(object sender, EventArgs e)  // updata sql //DB Save
        {
            string tab_name = tabControl1.SelectedTab.Name;
            int grid_num = SearchGridNum(tab_name);
            string buffer;
            int x=0; //row
            int y=0; //column
            int param_num=0;
            int j=0;
            string Updata_data="";
            bool succes = true;
            

            if (!tablestruct[grid_num].live)
            {
               // oPCommApp.PCommExit(1);//model
                return;
            }

            list[grid_num].MoveFrist();
            if (list[grid_num].IsNull())
            {
                return;
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
            //-------------------------------------------------------------------------------
            int h = 0;
            
            //-------------------------------------------------------------------------------
            
            for (int i = 0; i < list[grid_num].ListCount;i++ )
            {
                x = list[grid_num].GetCurrentValueX();
                y = list[grid_num].GetCurrentValueY();

                buffer = this.dataGridView[grid_num].Rows[x].Cells[y].Value.ToString();

                

                if (y == 0)   //_1d
                {
                    //param_num = x;
                    System.Windows.Forms.DataGridViewRow gridrow11;
                    gridrow11 = this.dataGridView[grid_num].Rows[x];
                    if (gridrow11.HeaderCell.Value != null)
                    {
                        for (j = 0; j < tablestruct[grid_num].number; j++)
                        {
                            if (cfgstruct[grid_num, j].explant == gridrow11.HeaderCell.Value.ToString())
                            {
                                h = j;
                            }
                        }
                    }
                    param_num = h;
                    if (cfgstruct[grid_num, param_num].datatype == "BOOLEAN8")
                    {
                        if (buffer == "false")
                        {
                            buffer = "0";
                        }
                        else
                        {
                            buffer = "1";
                        }
                    }
                    Updata_data = "UPDATE " + tablestruct[grid_num].name + " SET " +
                                    cfgstruct[grid_num, param_num].name + " = '" + buffer + "' WHERE ";

                }
                else if (dataGridView[grid_num].Rows[x].Cells[1].Value == null)  // _2d
                {
                    System.Windows.Forms.DataGridViewRow gridrow1;
                    gridrow1 = this.dataGridView[grid_num].Rows[x];
                    if (gridrow1.HeaderCell.Value != null)
                    {
                        for (j = 0; j < tablestruct[grid_num].number; j++)
                        {
                            if (cfgstruct[grid_num, j].explant == gridrow1.HeaderCell.Value.ToString())
                            {

                                break;
                            }
                        }
                    }
                    if (j < tablestruct[grid_num].number)
                    {
                        param_num = j;
                    }
                    else
                    {
                        MessageBox.Show("Error,Can't search _2d explant!");
                        return;
                    }
                    if (cfgstruct[grid_num, param_num].datatype == "BOOLEAN8")
                    {
                        if (buffer == "false")
                        {
                            buffer = "0";
                        }
                        else
                        {
                            buffer = "1";
                        }
                    }
                    Updata_data = "UPDATE " + tablestruct[grid_num].name + "_1d SET " +
                                    cfgstruct[grid_num, param_num].name + " = '" + buffer + "' WHERE ";


                    Updata_data = Updata_data + "idx1 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[x - 1].Cells[y].Value.ToString()) - 1) + "' AND ";
                }
                else if (dataGridView[grid_num].Rows[x].Cells[1].Value.ToString() == "1")                                // _3d_1
                {
                    System.Windows.Forms.DataGridViewRow gridrow1;
                    gridrow1 = this.dataGridView[grid_num].Rows[x];
                    if (gridrow1.HeaderCell.Value != null)
                    {
                        for (j = 0; j < tablestruct[grid_num].number; j++)
                        {
                            if (cfgstruct[grid_num, j].explant == gridrow1.HeaderCell.Value.ToString())
                            {

                                break;
                            }
                        }
                    }
                    if (j < tablestruct[grid_num].number)
                    {
                        param_num = j;
                    }
                    else
                    {
                        MessageBox.Show("Error,Can't search _3d_1 explant!");
                        return;
                    }
                    if (cfgstruct[grid_num, param_num].datatype == "BOOLEAN8")
                    {
                        if (buffer == "false")
                        {
                            buffer = "0";
                        }
                        else
                        {
                            buffer = "1";
                        }
                    }
                    Updata_data = "UPDATE " + tablestruct[grid_num].name + "_2d SET " +
                                   cfgstruct[grid_num, param_num].name + " = '" + buffer + "' WHERE ";


                    Updata_data = Updata_data + "idx1 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[x - 1].Cells[y].Value.ToString()) - 1) + "' AND ";

                    Updata_data = Updata_data + "idx2 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[x].Cells[1].Value.ToString()) - 1) + "' AND ";

                }
                else if (dataGridView[grid_num].Rows[x].Cells[1].Value.ToString() == "2" || dataGridView[grid_num].Rows[x].Cells[1].Value.ToString() == "3" || dataGridView[grid_num].Rows[x].Cells[1].Value.ToString() == "4")                                // _3d_2
                {
                    int tt = 0;
                    System.Windows.Forms.DataGridViewRow gridrow1;
                    gridrow1 = this.dataGridView[grid_num].Rows[x - 1]; 
                    if (dataGridView[grid_num].Rows[x].Cells[1].Value.ToString() == "2")
                    { 
                        gridrow1 = this.dataGridView[grid_num].Rows[x - 1];
                        tt = x - 1;
                    }
                    if (dataGridView[grid_num].Rows[x].Cells[1].Value.ToString() == "3")
                    { 
                        gridrow1 = this.dataGridView[grid_num].Rows[x - 2];
                        tt = x - 2;
                    }
                    if (dataGridView[grid_num].Rows[x].Cells[1].Value.ToString() == "4")
                    {
                        gridrow1 = this.dataGridView[grid_num].Rows[x - 3];
                        tt = x - 3;
                    }
                   // gridrow1 = this.dataGridView[grid_num].Rows[x - 1];
                    if (gridrow1.HeaderCell.Value != null)
                    {
                        for (j = 0; j < tablestruct[grid_num].number; j++)
                        {
                            if (cfgstruct[grid_num, j].explant == gridrow1.HeaderCell.Value.ToString())
                            {

                                break;
                            }
                        }
                    }
                    if (j < tablestruct[grid_num].number)
                    {
                        param_num = j;
                    }
                    else
                    {
                        MessageBox.Show("Error,Can't search _3d_2 explant!");
                        return;
                    }
                    if (cfgstruct[grid_num, param_num].datatype == "BOOLEAN8")
                    {
                        if (buffer == "false")
                        {
                            buffer = "0";
                        }
                        else
                        {
                            buffer = "1";
                        }
                    }
                    Updata_data = "UPDATE " + tablestruct[grid_num].name + "_2d SET " +
                                   cfgstruct[grid_num, param_num].name + " = '" + buffer + "' WHERE ";


                    Updata_data = Updata_data + "idx1 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[tt - 1].Cells[y].Value.ToString()) - 1) + "' AND ";

                    Updata_data = Updata_data + "idx2 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[x].Cells[1].Value.ToString()) - 1) + "' AND ";

                }
                else
                {
                    MessageBox.Show(x.ToString() + "," + y.ToString() + " can't save !");
                    continue;
                }

                Updata_data = Updata_data + cfgstruct[grid_num, 0].name + " = '" + this.dataGridView[grid_num].Rows[0].Cells[0].Value.ToString() + "'";

                for (j = 1; j < tablestruct[grid_num].number; j++)
                {
                    if (cfgstruct[grid_num, j].key == 1)
                    {
                        Updata_data = Updata_data + " AND " + cfgstruct[grid_num, j].name + " = '" + this.dataGridView[grid_num].Rows[j].Cells[0].Value.ToString() + "'";
                    }
                }

                thisCommand.CommandText = Updata_data;
                try
                {
                    thisCommand.ExecuteNonQuery();
                    this.dataGridView[grid_num].Rows[x].Cells[y].Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                    this.dataGridView[grid_num].Rows[x].Cells[y].Style.ForeColor = System.Drawing.Color.Navy;
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                    this.dataGridView[grid_num].Rows[x].Cells[y].Style.ForeColor = System.Drawing.Color.White;
                    this.dataGridView[grid_num].Rows[x].Cells[y].Style.BackColor = System.Drawing.Color.Red;
                    succes = false;
                    continue;

                }
                list[grid_num].MoveNext();
            }

            thisConnection.Close();
            
            if (!succes)
            {
                MessageBox.Show("Error! Please check red data!");
            }
            else
            {
                //==================================================================
                //发送报文
                string temp = null;
                string[] stemp = new string[10];
                string ctemp;
                int length = 0;
                int k = 0;
                for (int i = 0; i < 30; i++)
                {
                    if (tablestruct[i].name == tab_name)
                    {
                        temp = tablestruct[i].Job;
                        while (temp != null)
                        {
                            if (temp.Trim() == "")
                            {
                                break;
                            }
                            temp.Trim();
                            if (temp.IndexOf("!") == -1)
                            {
                                temp.Trim();
                                length = temp.IndexOf(":");
                                if (length > 0)
                                {
                                    stemp[k] = temp.Substring(0, length);
                                    k++;
                                }


                                if (temp.Length > length + 1)
                                {
                                    temp = temp.Substring(length + 1, temp.Trim().Length - length-1);

                                    if (temp != null)
                                        length = temp.IndexOf(":");
                                    if (length > 0)
                                    {
                                        stemp[k] = temp.Substring(0, length);
                                        k++;
                                    }


                                    if (temp.Length > length + 1)
                                    {
                                        temp = temp.Substring(length + 1, (temp.Trim().Length - length - 1));

                                    }
                                    if (temp.Length > 5 && temp.Length < 10)
                                    {
                                        stemp[k] = temp;
                                        temp = null;
                                        break;
                                    }
                                }
                                //i++;
                            }
                        }

                        break;
                    }
                }


                if (PComm.pcommExit == 0 && list[grid_num].ListCount > 0)
                {
                    PCommLib.Message message = new PCommLib.Message();
                    message.Selector = tab_name;
                    message.SendTime = DateTime.Now;
                    message.From = MdbUpdate;
                    int t = 0;
                    ctemp = stemp[t];
                    Form formmain = new Form();
                    foreach (string jobname in stemp)
                    {
                        if (jobname != null)
                        {
                            message.To = jobname;//RSU_JOB   //HTT_JOB  //FSU_JOB // FTC_JOB  //CTC_JOB //CSU_JOB
                            message.EventNo = 32;
                            byte[] bytes = message.ToBytes();
                            int iPostSzie;
                            int iArrOneIdx = 0;
                            iPostSzie = pca.Send(jobname, bytes, bytes.Length);
                        }
                    }
                    // PCommApp.PCommExit(1);
                }
                //-----------------------
            }
            list[grid_num].Destroy();

        }

        private void CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            AddToList();
        }

        private void button4_Click(object sender, EventArgs e)   //refresh
        {
            gl.ReFrash = true;
            string tab_name = tabControl1.SelectedTab.Name;
            int grid_num = SearchGridNum(tab_name);
            if (!tablestruct[grid_num].live)
            {
                return;
            }
            list[grid_num].Destroy();
            string [] lab = new string [10];
            string [] content = new string [10];
            for (int i = 0; i < tablestruct[grid_num].key_num; i++)
            {
                lab[i] = cfgstruct[grid_num,i].name;
                content[i] = this.dataGridView[grid_num].Rows[i].Cells[0].Value.ToString();

            }
            Data_Grid_View(lab, content, tablestruct[grid_num].key_num);
            gl.ReFrash = false;
        }

        private void button8_Click(object sender, EventArgs e)  // file save
        {
            string tab_name = tabControl1.SelectedTab.Name;
            int grid_num = SearchGridNum(tab_name);
            int key_num = tablestruct[grid_num].key_num;
            string[] key = new string[key_num];
            string select;
            string select1;
            int i;
            string buffer="";
            
            if (saveFileDialog1.ShowDialog().ToString() != "OK")
            {
                return;
            }
            
            if (saveFileDialog1.FileName == "")
            {
                return;
            }

            for ( i = 0; i < key_num; i++)
            {
                key[i] = cfgstruct[grid_num, i].name;
            }

            File_Save dlg = new File_Save(tab_name, key_num, key, strConnection,true,false);

            dlg.ShowDialog();

            if (!dlg.ok)
            {
                return;
            }

            StreamWriter file;
            file = File.CreateText(saveFileDialog1.FileName);

            file.WriteLine(",\""+tab_name+"\"");
            for (i = 0; i < tablestruct[grid_num].number; i++)
            {
                if (cfgstruct[grid_num, i].dim == "0")
                {
                    buffer = buffer + cfgstruct[grid_num, i].name + ",";
                }
            }
            file.WriteLine(",\"" + buffer + "\"");

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

            select1 = "SELECT * FROM " + tab_name + " WHERE ";

            select = cfgstruct[grid_num, 0].name + " = '" + dlg.select_key[0,0] + "'";

            thisCommand.CommandText = select1+select;
            SqlDataReader thisReader = thisCommand.ExecuteReader();

            
            while (thisReader.Read())
            {
                buffer = "";
                for (i = 0; i < tablestruct[grid_num].number; i++)
                {
                    if (cfgstruct[grid_num, i].dim == "0")
                    buffer = buffer + thisReader[cfgstruct[grid_num, i].name].ToString()+",";
                }
                file.WriteLine(",\"" + buffer + "\"");
            }

           

            file.WriteLine(",\"" +"/" + "\"");
            thisReader.Close();

            if (Convert.ToInt32(tablestruct[grid_num].dim) > 0)
            {
                buffer = "";
                select1 = "SELECT * FROM " + tab_name + "_1d WHERE ";
                file.WriteLine(",\"" + tab_name + "_1D\"");


                for (i = 0; i < tablestruct[grid_num].number; i++)
                {
                    if (cfgstruct[grid_num, i].key == 1)
                    {
                        buffer = buffer + cfgstruct[grid_num, i].name + ",";
                    }
                }

                buffer = buffer + "idx1,";

                for (i = 0; i < tablestruct[grid_num].number; i++)
                {
                    if (cfgstruct[grid_num, i].dim == "1")
                    {
                        buffer = buffer + cfgstruct[grid_num, i].name + ",";
                    }
                }
                file.WriteLine(",\"" + buffer + "\"");
                thisCommand.CommandText = select1+select;
                thisReader = thisCommand.ExecuteReader();

                
                while (thisReader.Read())
                {
                    buffer = "";
                    if (cfgstruct[grid_num, i].key == 1)
                    {
                        buffer = buffer + thisReader[cfgstruct[grid_num, i].name].ToString() + ",";
                    }
                    for (i = 0; i < tablestruct[grid_num].number; i++)
                    {
                        if (cfgstruct[grid_num, i].key == 1)
                        {
                            buffer = buffer + thisReader[cfgstruct[grid_num, i].name].ToString() + ",";
                        }
                        
                    }
                    buffer = buffer + thisReader["idx1"].ToString() + ",";
                    for (i = 0; i < tablestruct[grid_num].number; i++)
                    {
                        if (cfgstruct[grid_num, i].dim == "1")
                        {
                            buffer = buffer + thisReader[cfgstruct[grid_num, i].name].ToString() + ",";
                        }
                    }
                    file.WriteLine(",\"" + buffer + "\"");
                }

                file.WriteLine(",\"" + "/" + "\"");
                thisReader.Close();
            }
            if (Convert.ToInt32(tablestruct[grid_num].dim) == 2)
            {
                buffer = "";
                select1 = "SELECT * FROM " + tab_name + "_2d WHERE ";
                file.WriteLine(",\"" + tab_name + "_2D\"");


                for (i = 0; i < tablestruct[grid_num].number; i++)
                {
                    if (cfgstruct[grid_num, i].key == 1)
                    {
                        buffer = buffer + cfgstruct[grid_num, i].name + ",";
                    }
                }

                buffer = buffer + "idx1," + "idx2,";

                for (i = 0; i < tablestruct[grid_num].number; i++)
                {
                    if (cfgstruct[grid_num, i].dim == "2")
                    {
                        buffer = buffer + cfgstruct[grid_num, i].name + ",";
                    }
                }
                file.WriteLine(",\"" + buffer + "\"");
                thisCommand.CommandText = select1 + select;
                thisReader = thisCommand.ExecuteReader();


                while (thisReader.Read())
                {
                    buffer = "";
                    if (cfgstruct[grid_num, i].key == 1)
                    {
                        buffer = buffer + thisReader[cfgstruct[grid_num, i].name].ToString() + ",";
                    }
                    for (i = 0; i < tablestruct[grid_num].number; i++)
                    {
                        if (cfgstruct[grid_num, i].key == 1)
                        {
                            buffer = buffer + thisReader[cfgstruct[grid_num, i].name].ToString() + ",";
                        }

                    }
                    buffer = buffer + thisReader["idx1"].ToString() + ",";
                    buffer = buffer + thisReader["idx2"].ToString() + ",";
                    for (i = 0; i < tablestruct[grid_num].number; i++)
                    {
                        if (cfgstruct[grid_num, i].dim == "2")
                        {
                            buffer = buffer + thisReader[cfgstruct[grid_num, i].name].ToString() + ",";
                        }
                    }
                    file.WriteLine(",\"" + buffer + "\"");
                }

                file.WriteLine(",\"" + "/" + "\"");
                thisReader.Close();
            }



            thisConnection.Close();

            file.Close();

        }
        private void button7_Click(object sender, EventArgs e) //file load
        {
            string tab_name = tabControl1.SelectedTab.Name;
            int grid_num = SearchGridNum(tab_name);
            int key_num = tablestruct[grid_num].key_num;
            string[] key = new string[key_num];
            int i,n,m=0;
            string buffer;
            string update;
            char[] delimiterChars = { '\"', '$' };
            char[] delimiterChars1 = { ',', '$' };

            if (openFileDialog1.ShowDialog().ToString() != "OK")
            {
                return;
            }
            if (openFileDialog1.FileName == "")
            {
                return;
            }
            for (i = 0; i < key_num; i++)
            {
                key[i] = cfgstruct[grid_num, i].name;
            }

            File_Save dlg = new File_Save(tab_name, key_num, key, strConnection,false,false);

            dlg.ShowDialog();
            if (!dlg.ok)
            {
                return;
            }

            //

            StreamReader text;


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
            for (m = 0; m < dlg.Select_num[0]; m++)
            {

                text = File.OpenText(openFileDialog1.FileName);

                buffer = text.ReadLine();
                if (buffer == null)
                {
                    MessageBox.Show("file is null!");
                    return;
                }
                if (buffer.IndexOf(tab_name) == -1)
                {
                    MessageBox.Show("You choice the file which don't match " + tab_name + "table");
                    return;
                }

                buffer = text.ReadLine();
                if (buffer == null)
                {
                    continue;
                }
                for (i = 0; i < tablestruct[grid_num].number; i++)
                {
                    if ((cfgstruct[grid_num, i].dim == "0") && (buffer.IndexOf(cfgstruct[grid_num, i].name) == -1))
                    {
                        MessageBox.Show("can't find " + cfgstruct[grid_num, i].name + "table in the file! ");
                        return;
                    }
                }
                bool[] timeflag = new bool[20];
                bool[] timeflag0 = new bool[20];
                bool[] timeflag1 = new bool[20];
                bool[] timeflag2 = new bool[20];
                int c1 = 0;
                int c2 = 0;
                int c0 = 0;
                for (int j = Convert.ToInt32(tablestruct[grid_num].key_num); j < Convert.ToInt32(tablestruct[grid_num].number); j++)
                {
                    if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                    { timeflag[j] = true; }
                }
                //_1d

                buffer = text.ReadLine();


                while (buffer != null)
                {
                    string[] buffer1 = buffer.Split(delimiterChars);

                    buffer = buffer1[1];

                    string[] buffer2 = buffer.Split(delimiterChars1);
                    n = tablestruct[grid_num].key_num;

                    update = "UPDATE " + tab_name + " SET " + cfgstruct[grid_num, tablestruct[grid_num].key_num].name + " = '" + buffer2[n].Trim() + "' ";
                    n++;

                    for (i = tablestruct[grid_num].key_num + 1; i < tablestruct[grid_num].number; i++)
                    {
                        if (cfgstruct[grid_num, i].dim == "0")
                        {
                            if (timeflag[i] == false)
                            {
                                update = update + "," + cfgstruct[grid_num, i].name + " = '" + buffer2[n].Trim() + "' ";
                            }
                            else { }
                           // update = update + "," + cfgstruct[grid_num, i].name + " = '" + buffer2[n].Trim() + "' ";
                            n++;
                        }

                    }
                    update = update + "WHERE ";
                    update = update + cfgstruct[grid_num, 0].name + " = '" + dlg.select_key[0, m] + "'";


                    if (tablestruct[grid_num].key_num == 2)
                    {
                        update = update + " AND " + cfgstruct[grid_num, 1].name + " = '" + buffer2[1].Trim() + "'";
                    }
                    if (tablestruct[grid_num].key_num == 3)
                    {
                        update = update + " AND " + cfgstruct[grid_num, 1].name + " = '" + buffer2[2].Trim() + "'";
                    }
                    try
                    {
                        thisCommand.CommandText = update;
                        thisCommand.ExecuteNonQuery();
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message);
                        return;
                    }
                    buffer = text.ReadLine();
                    if (buffer.Trim() == ",\"/\"")
                    {
                        break;
                    }
                }

                //_1d

                buffer = text.ReadLine();
                if ((buffer == null) || (buffer.IndexOf("_1D") == -1))
                {
                    continue;
                }
                buffer = text.ReadLine();

                if (buffer == null)
                {
                    continue;
                }
                for (i = 0; i < tablestruct[grid_num].number; i++)
                {
                    if ((cfgstruct[grid_num, i].dim == "1") && (buffer.IndexOf(cfgstruct[grid_num, i].name) == -1))
                    {
                        MessageBox.Show("can't find " + cfgstruct[grid_num, i].name + "table in the file! ");
                        return;
                    }
                }

                buffer = text.ReadLine();
                while (buffer != null)
                {
                    string[] buffer1 = buffer.Split(delimiterChars);

                    buffer = buffer1[1];

                    string[] buffer2 = buffer.Split(delimiterChars1);

                    n = tablestruct[grid_num].key_num;


                    update = "UPDATE " + tab_name + "_1d SET idx1" + " = '" + buffer2[n].Trim() + "' ";

                    n++;

                    for (i = tablestruct[grid_num].key_num + 1; i < tablestruct[grid_num].number; i++)
                    {
                        if (cfgstruct[grid_num, i].dim == "1")
                        {
                            if (timeflag[i] == false)
                            {
                                update = update + "," + cfgstruct[grid_num, i].name + " = '" + buffer2[n].Trim() + "' ";
                            }
                            else { }
                            
                            n++;
                        }

                    }
                    update = update + "WHERE ";
                    update = update + cfgstruct[grid_num, 0].name + " = '" + dlg.select_key[0, m] + "'" + " AND idx1 " + " = '" + buffer2[tablestruct[grid_num].key_num].Trim() + "'";


                    if (tablestruct[grid_num].key_num == 2)
                    {
                        update = update + " AND " + cfgstruct[grid_num, 1].name + " = '" + buffer2[1].Trim() + "'";
                    }
                    if (tablestruct[grid_num].key_num == 3)
                    {
                        update = update + " AND " + cfgstruct[grid_num, 2].name + " = '" + buffer2[2].Trim() + "'";
                    }

                    thisCommand.CommandText = update;
                    thisCommand.ExecuteNonQuery();

                    buffer = text.ReadLine();
                    if (buffer.Trim() == ",\"/\"")
                    {
                        break;
                    }
                }
                //_2d
                buffer = text.ReadLine();
                if ((buffer == null) || (buffer.IndexOf("_2D") == -1))
                {
                    continue;
                }
                buffer = text.ReadLine();

                if (buffer == null)
                {
                    continue;
                }
                for (i = 0; i < tablestruct[grid_num].number; i++)
                {
                    if ((cfgstruct[grid_num, i].dim == "2") && (buffer.IndexOf(cfgstruct[grid_num, i].name) == -1))
                    {
                        MessageBox.Show("can't find " + cfgstruct[grid_num, i].name + "table in the file! ");
                        return;
                    }
                }

                buffer = text.ReadLine();
                while (buffer != null)
                {
                    string[] buffer1 = buffer.Split(delimiterChars);

                    buffer = buffer1[1];

                    string[] buffer2 = buffer.Split(delimiterChars1);

                    n = tablestruct[grid_num].key_num;


                    update = "UPDATE " + tab_name + "_2d SET idx1" + " = '" + buffer2[n].Trim() + "' ";

                    n++;
                    n++;

                    for (i = tablestruct[grid_num].key_num + 1; i < tablestruct[grid_num].number; i++)
                    {
                        if (cfgstruct[grid_num, i].dim == "2")
                        {
                            if (timeflag[i] == false)
                            {
                                update = update + "," + cfgstruct[grid_num, i].name + " = '" + buffer2[n].Trim() + "' ";
                            }
                            else { }
                            //update = update + "," + cfgstruct[grid_num, i].name + " = '" + buffer2[n].Trim() + "' ";
                            n++;
                        }

                    }
                    update = update + "WHERE ";
                    update = update + cfgstruct[grid_num, 0].name + " = '" + dlg.select_key[0, m] + "'" + " AND idx1 " + " = '" + buffer2[tablestruct[grid_num].key_num].Trim() + "'"
                        + " AND idx2 " + " = '" + buffer2[tablestruct[grid_num].key_num + 1].Trim() + "'";

                    if (tablestruct[grid_num].key_num == 2)
                    {
                        update = update + " AND " + cfgstruct[grid_num, 1].name + " = '" + buffer2[1].Trim() + "'";
                    }
                    if (tablestruct[grid_num].key_num == 3)
                    {
                        update = update + " AND " + cfgstruct[grid_num, 2].name + " = '" + buffer2[2].Trim() + "'";
                    }

                    thisCommand.CommandText = update;
                    thisCommand.ExecuteNonQuery();

                    buffer = text.ReadLine();
                    if (buffer.Trim() == ",\"/\"")
                    {
                        break;
                    }
                }

                text.Close();
            }
            thisConnection.Close();

        }

        private void button2_Click(object sender, EventArgs e)  //add
        {

            string tab_name = tabControl1.SelectedTab.Name;
            int grid_num = SearchGridNum(tab_name);
            int key_num = tablestruct[grid_num].key_num;
            string[] key = new string[key_num];
            string insert; 
            string insert_exert;
            int i,n,j,m,p,q;
            string media;
            int idx_num;
            int idx_num2;
            string insert_exert_1d="";
            string insert_exert_0d="";
            string from;

            for ( i = 0; i < key_num; i++)
            {
                key[i] = cfgstruct[grid_num, i].name;
            }

            Add_and_Delete dlg = new Add_and_Delete(tab_name,key_num, key, strConnection, true);

            if (dlg.count == 0)
            {
                DialogResult result = MessageBox.Show("Database table " + tab_name + " is empty.\nDo you want to initialize the table?", "ModelTables", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Cancel)
                {
                    return;
                }

                Initial_table();
                return;               
            }
            if (dlg.count == -1)
            {
                return;
            }
            dlg.ShowDialog();

            if (!dlg.ok)
            {
                return;
            }

            from=dlg.from;

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

            do
            {
                for (i = 0; i < Convert.ToInt32(tablestruct[grid_num].dim) + 1; i++)
                {
                    if (i == 0)
                    {
                        insert = "INSERT INTO " + tab_name + " ( ";
                        insert = insert + cfgstruct[grid_num, 0].name;

                        for (j = 1; j < tablestruct[grid_num].number; j++)
                        {
                            if (cfgstruct[grid_num, j].dim == "0")
                                insert = insert + "," + cfgstruct[grid_num, j].name;
                        }

                        insert = insert + " ) " + "VALUES ( '";
                        insert_exert = insert + from + "'";
                        for (n = 0; n < dlg.key1; n++)
                        {
                            for (m = 0; m < dlg.key2; m++)
                            {

                                insert_exert_0d = insert_exert + ",'" + dlg.repeat[1, n] + "'";

                                insert_exert_0d = insert_exert_0d + ",'" + dlg.repeat[2, m] + "'";


                                for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                                {
                                    if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                                    {
                                        media = Convert.ToString(System.DateTime.Now);
                                    }
                                    else
                                    {
                                        media = cfgstruct[grid_num, j].default_value;
                                    }
                                    if (cfgstruct[grid_num, j].dim == "0")
                                        insert_exert_0d = insert_exert_0d + ",'" + media + "'";
                                }

                                insert_exert_0d = insert_exert_0d + " ) ";


                                thisCommand.CommandText = insert_exert_0d;
                                thisCommand.ExecuteNonQuery();
                            }
                            if (m == 0)
                            {
                                insert_exert_0d = insert_exert;
                                for (j = 1; j < tablestruct[grid_num].key_num; j++)
                                {
                                    insert_exert_0d = insert_exert_0d + ",'" + dlg.repeat[j, n] + "'";
                                }
                                for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                                {
                                    if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                                    {
                                        media = Convert.ToString(System.DateTime.Now);
                                    }
                                    else
                                    {
                                        media = cfgstruct[grid_num, j].default_value;
                                    }
                                    if (cfgstruct[grid_num, j].dim == "0")
                                        insert_exert_0d = insert_exert_0d + ",'" + media + "'";
                                }

                                insert_exert_0d = insert_exert_0d + " ) ";
                                
                                thisCommand.CommandText = insert_exert_0d;
                                thisCommand.ExecuteNonQuery();
                            }
                        }
                        if (n == 0)
                        {
                            for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                            {
                                if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                                {
                                    //media = Convert.ToString(System.DateTime.Now);
                                    //media = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    media = "getdate()";
                                }
                                else
                                {
                                    media = cfgstruct[grid_num, j].default_value;
                                }
                               // if (cfgstruct[grid_num, j].dim == "0")
                                //    insert_exert = insert_exert + ",'" + media + "'";
                                if (cfgstruct[grid_num, j].dim == "0")
                                    insert_exert = insert_exert + "," + media ;
                            }

                            insert_exert = insert_exert + " ) ";


                            thisCommand.CommandText = insert_exert;
                            try
                            {
                                thisCommand.ExecuteNonQuery();
                            }
                            catch (Exception a)
                            {
                                MessageBox.Show(a.Message);
                                return;
                            }

                        }
                    }
                    else if (i == 1)
                    {
                        string buffer_str;
                        string buffer_str1;
                        int length;
                        idx_num = SearchNum(tab_name);
                        insert = "INSERT INTO " + tab_name + "_1d ( ";
                        insert = insert + cfgstruct[grid_num, 0].name;
                        for (j = 1; j < tablestruct[grid_num].key_num; j++)
                        {
                            insert = insert + "," + cfgstruct[grid_num, j].name;
                        }
                        insert = insert + ",idx1";
                        for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                        {
                            if (cfgstruct[grid_num, j].dim == "1")
                                insert = insert + "," + cfgstruct[grid_num, j].name;
                        }

                        insert = insert + " ) " + "VALUES ( '";

                        insert_exert = insert + from + "'";


                        for (n = 0; n < dlg.key1; n++)
                        {
                            for (p = 0; p < dlg.key2; p++)
                            {
                                for (m = 0; m < idx_num; m++)
                                {

                                    insert_exert_1d = insert_exert + ",'" + dlg.repeat[1, n] + "'";
                                    insert_exert_1d = insert_exert_1d + ",'" + dlg.repeat[2, p] + "'";

                                    insert_exert_1d = insert_exert_1d + ",'" + m.ToString() + "'";
                                    for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                                    {
                                        if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                                        {
                                            media = Convert.ToString(System.DateTime.Now);
                                        }
                                        else
                                        {
                                            media = cfgstruct[grid_num, j].default_value;
                                        }
                                        if ((cfgstruct[grid_num, j].dim == "1") && (cfgstruct[grid_num, j].idx1 > m))
                                        {
                                            insert_exert_1d = insert_exert_1d + ",'" + media + "'";
                                        }
                                        else if ((cfgstruct[grid_num, j].dim == "1") && ((cfgstruct[grid_num, j].idx1 < m)||(cfgstruct[grid_num, j].idx1 == m)))
                                        {
                                            insert_exert_1d = insert_exert_1d + ",'" + "'";
                                        }
                                    }

                                    insert_exert_1d = insert_exert_1d + " ) ";


                                    thisCommand.CommandText = insert_exert_1d;
                                    thisCommand.ExecuteNonQuery();
                                }

                            }
                            if (p == 0)
                            {
                                for (m = 0; m < idx_num; m++)
                                {
                                    insert_exert_1d = insert_exert;
                                    for (j = 1; j < tablestruct[grid_num].key_num; j++)
                                    {

                                        insert_exert_1d = insert_exert_1d + ",'" + dlg.repeat[j, n] + "'";
                                    }
                                    insert_exert_1d = insert_exert_1d + ",'" + m.ToString() + "'";
                                    for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                                    {
                                        if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                                        {
                                            media = Convert.ToString(System.DateTime.Now);
                                        }
                                        else
                                        {
                                            media = cfgstruct[grid_num, j].default_value;
                                        }
                                        if ((cfgstruct[grid_num, j].dim == "1") && (cfgstruct[grid_num, j].idx1 > m))
                                        {
                                            insert_exert_1d = insert_exert_1d + ",'" + media + "'";
                                        }
                                        else if ((cfgstruct[grid_num, j].dim == "1") && ((cfgstruct[grid_num, j].idx1 < m) || (cfgstruct[grid_num, j].idx1 == m)))
                                        {
                                            length=insert_exert_1d.IndexOf(cfgstruct[grid_num, j].name);
                                            buffer_str1 = insert_exert_1d.Substring(0, length);
                                            buffer_str = insert_exert_1d.Substring(length, insert_exert_1d.Length - length);
                                            length = buffer_str.IndexOf(",");
                                            buffer_str = buffer_str.Substring(length + 1, buffer_str.Length - length - 1);
                                            insert_exert_1d = buffer_str1 + buffer_str;
                                        }
                                    }

                                    insert_exert_1d = insert_exert_1d + " ) ";


                                    thisCommand.CommandText = insert_exert_1d;
                                    thisCommand.ExecuteNonQuery();
                                }
                            }
                        }
                        if (n == 0)
                        {
                            for (m = 0; m < idx_num; m++)
                            {
                                insert_exert_1d = insert_exert + ",'" + m.ToString() + "'";
                                for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                                {
                                    if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                                    {
                                        media = Convert.ToString(System.DateTime.Now);
                                    }
                                    else
                                    {
                                        media = cfgstruct[grid_num, j].default_value;
                                    }
                                    if ((cfgstruct[grid_num, j].dim == "1") && (cfgstruct[grid_num, j].idx1 > m))
                                    {
                                        insert_exert_1d = insert_exert_1d + ",'" + media + "'";
                                    }
                                    else if ((cfgstruct[grid_num, j].dim == "1") && ((cfgstruct[grid_num, j].idx1 < m) || (cfgstruct[grid_num, j].idx1 == m)))
                                    {
                                        insert_exert_1d = insert_exert_1d + ",'" + "'";
                                    }
                                }

                                insert_exert_1d = insert_exert_1d + " ) ";


                                thisCommand.CommandText = insert_exert_1d;
                                thisCommand.ExecuteNonQuery();
                            }

                        }
                    }
                    else if (i == 2)
                    {
                        idx_num = SearchNum(tab_name);
                        idx_num2 = SearchNum2(tab_name);

                        insert = "INSERT INTO " + tab_name + "_2d ( ";
                        insert = insert + cfgstruct[grid_num, 0].name;

                        for (j = 1; j < tablestruct[grid_num].key_num; j++)
                        {
                            insert = insert + "," + cfgstruct[grid_num, j].name;
                        }
                        insert = insert + ",idx1" + ",idx2";

                        for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                        {
                            if (cfgstruct[grid_num, j].dim == "2")
                                insert = insert + "," + cfgstruct[grid_num, j].name;
                        }

                        insert = insert + " ) " + "VALUES ( '";

                        insert_exert = insert + from + "'";


                        for (n = 0; n < dlg.key1; n++)
                        {
                            for (p = 0; p < dlg.key2; p++)
                            {
                                for (m = 0; m < idx_num; m++)
                                {
                                    for (q = 0; q < idx_num2; q++)
                                    {
                                        insert_exert_1d = insert_exert + ",'" + dlg.repeat[1, n] + "'";
                                        insert_exert_1d = insert_exert_1d + ",'" + dlg.repeat[2, p] + "'";
                                        insert_exert_1d = insert_exert_1d + ",'" + m.ToString() + "'";

                                        insert_exert_1d = insert_exert_1d + ",'" + q.ToString() + "'";
                                        for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                                        {
                                            if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                                            {
                                                media = Convert.ToString(System.DateTime.Now);
                                            }
                                            else
                                            {
                                                media = cfgstruct[grid_num, j].default_value;
                                            }
                                            if (cfgstruct[grid_num, j].dim == "2")
                                                insert_exert_1d = insert_exert_1d + ",'" + media + "'";
                                        }

                                        insert_exert_1d = insert_exert_1d + " ) ";


                                        thisCommand.CommandText = insert_exert_1d;

                                        thisCommand.ExecuteNonQuery();
                                    }
                                }

                            }
                            if (p == 0)
                            {
                                for (m = 0; m < idx_num; m++)
                                {
                                    for (q = 0; q < idx_num2; q++)
                                    {
                                        insert_exert_1d = insert_exert + ",'" + dlg.repeat[1, n] + "'";
                                        insert_exert_1d = insert_exert_1d + ",'" + m.ToString() + "'";

                                        insert_exert_1d = insert_exert_1d + ",'" + q.ToString() + "'";

                                        for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                                        {
                                            if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                                            {
                                                media = Convert.ToString(System.DateTime.Now);
                                            }
                                            else
                                            {
                                                media = cfgstruct[grid_num, j].default_value;
                                            }
                                            if (cfgstruct[grid_num, j].dim == "2")
                                            {
                                                insert_exert_1d = insert_exert_1d + ",'" + media + "'";
                                            }
                                        }

                                        insert_exert_1d = insert_exert_1d + " ) ";


                                        thisCommand.CommandText = insert_exert_1d;

                                        thisCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        if (n == 0)
                        {
                            for (m = 0; m < idx_num; m++)
                            {
                                for (q = 0; q < idx_num2; q++)
                                {

                                    insert_exert_1d = insert_exert_1d + ",'" + m.ToString() + "'";
                                    insert_exert_1d = insert_exert_1d + ",'" + q.ToString() + "'";
                                    for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                                    {
                                        if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                                        {
                                            media = Convert.ToString(System.DateTime.Now);
                                        }
                                        else
                                        {
                                            media = cfgstruct[grid_num, j].default_value;
                                        }
                                        if (cfgstruct[grid_num, j].dim == "2")
                                            insert_exert_1d = insert_exert_1d + ",'" + media + "'";
                                    }

                                    insert_exert_1d = insert_exert_1d + " ) ";


                                    thisCommand.CommandText = insert_exert_1d;

                                    thisCommand.ExecuteNonQuery();
                                }
                            }

                        }
                    }

                }
                if ((dlg.to == "") || (Convert.ToInt32(from) == Convert.ToInt32(dlg.to)))
                {
                    break;
                }
                else
                {
                    from = Convert.ToString(Convert.ToInt32(from) + 1);
                }

            } while (true);

            thisConnection.Close();
        }

        private void button3_Click(object sender, EventArgs e) //delete
        {
            string tab_name = tabControl1.SelectedTab.Name;
            int grid_num = SearchGridNum(tab_name);
            int key_num = tablestruct[grid_num].key_num;
            string[] key = new string[key_num];
            string delete="";
            int i,j;

            for (i = 0; i < key_num; i++)
            {
                key[i] = cfgstruct[grid_num, i].name;
            }

            Add_and_Delete dlg = new Add_and_Delete(tab_name, key_num, key, strConnection, false);
            if(dlg.count==-1)
            {
                return;
            }
            dlg.ShowDialog();
            if (!dlg.ok)
            {
                return;
            }
            if ((dlg.select_key == null)||(dlg.select_key[0] == ""))
            {
                return;
            }
                       // System.Data.OleDb.OleDbDataReader thisReader = thisCommand.ExecuteReader();
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
            for (j = 0; j < dlg.select_num; j++)
            {

                for (i = 0; i < Convert.ToInt32(tablestruct[grid_num].dim) + 1; i++)
                {
                    if (i == 0)
                    {
                        delete = "DELETE FROM " + tab_name + " WHERE " + key[0] + " = '" + dlg.select_key[j] + "'";
                    }
                    else if (i == 1)
                    {
                        delete = "DELETE FROM " + tab_name + "_1d WHERE " + key[0] + " = '" + dlg.select_key[j] + "'";
                    }
                    else if (i == 2)
                    {
                        delete = "DELETE FROM " + tab_name + "_2d WHERE " + key[0] + " = '" + dlg.select_key[j] + "'";
                    }


                    thisCommand.CommandText = delete;
                    thisCommand.ExecuteNonQuery();
                }
            }
            thisConnection.Close();
            if (!tablestruct[grid_num].live)
            {
                return;
            }
            for(i=0;i<dlg.select_num;i++)
            {

                if(this.dataGridView[grid_num].Rows[0].Cells[0].Value.ToString() == dlg.select_key[i])
                {
                    RemoveRowColum(grid_num);
                    tablestruct[grid_num].live = false;
                    break;
                }

            }           

        }

        private void button6_Click(object sender, EventArgs e) //copy records
        {
            string tab_name = tabControl1.SelectedTab.Name;
            int grid_num = SearchGridNum(tab_name);
            int key_num = tablestruct[grid_num].key_num;
            string[] key = new string[key_num];
            string update = "UPDATE ";
            string update_0d;
            string update_1d;
            string update_2d;
            int j,n,m,p;
            int [] num = new int [3];
            int idx_num;
            int idx_num_2d;
            bool C_flag = false;
            bool C1_flag = false;
            bool C2_flag = false;
            for (j = Convert.ToInt32(tablestruct[grid_num].key_num); j < Convert.ToInt32(tablestruct[grid_num].number); j++)
            {
                if (cfgstruct[grid_num, j].dim == "1")
                {
                    contant.s_idx_num = SearchNum(tab_name);
                    break;
                }
            }
            idx_num = contant.s_idx_num;
            for (j = Convert.ToInt32(tablestruct[grid_num].key_num); j < Convert.ToInt32(tablestruct[grid_num].number); j++)
            {
                if (cfgstruct[grid_num, j].dim == "2")
                {
                    contant.s_idx_num_2d = SearchNum2(tab_name);
                    break;
                }
            }
            idx_num_2d = contant.s_idx_num_2d;
            for (int i = 0; i < key_num; i++)
            {
                key[i] = cfgstruct[grid_num, i].name;
            }

            Copy_Records dlg = new Copy_Records(tab_name, key_num, key, strConnection);
            if (dlg.count == -1)
            {
                return;
            }
            dlg.ShowDialog();
            if (!dlg.ok)
            {
                return;
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
            SqlTransaction Trn;
            Trn = thisConnection.BeginTransaction();

            thisCommand.Connection = thisConnection;
            thisCommand.Transaction = Trn;
            //---------------------------------------------------------------------------------------------
            string ZeroDim= null;
            string OneDim= null;
            string TwoDim= null;
            string ZeroDimStr = null;
            string OneDimStr = null;
            string TwoDimStr = null;
            string[] ZeroDimS = new string[20];
            string[] OneDimS = new string[20];
            string[] TwoDimS = new string[20];
            string [] OneDimValue=new string[20];
            string[] ZeroDimValue = new string[20];
            string[,] tOneDimValue = new string[20, 230];
            string[, ,] tTwoDimValue = new string[20, 20, 20];
            
            bool []timeflag0 = new bool[20];
            bool []timeflag1 = new bool[20];
            bool []timeflag2 = new bool[20];
            int count0=0;
            int count1=0;
            int count2=0;
            string select=null;

                for (j = Convert.ToInt32(tablestruct[grid_num].key_num); j < Convert.ToInt32(tablestruct[grid_num].number); j++)
                {
                    if (cfgstruct[grid_num, j].dim == "0")
                    {
                        if (ZeroDim == null)
                        {
                            if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                            { timeflag0[count0] = true; }
                            ZeroDimS[count0] = cfgstruct[grid_num, j].name;
                            ZeroDimStr = cfgstruct[grid_num, j].name;
                            ZeroDim = cfgstruct[grid_num, j].name + " = ( SELECT " + cfgstruct[grid_num, j].name + " FROM " + tab_name + " WHERE ";
                            ZeroDim = ZeroDim + cfgstruct[grid_num, 0].name + " = '" + dlg.key_from[0] + "'";

                            for (n = 1; n < tablestruct[grid_num].key_num; n++)
                            {
                                ZeroDim = ZeroDim + " AND " + cfgstruct[grid_num, n].name + " = '" + dlg.key_from[n] + "'";
                            }
                            ZeroDim = ZeroDim + " )";
                        }
                        else 
                        {
                            if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                            {
                                timeflag0[count0] = true;
                            }
                            ZeroDimS[count0] = cfgstruct[grid_num, j].name;
                            ZeroDimStr = ZeroDimStr + "," + cfgstruct[grid_num, j].name;
                            ZeroDim = ZeroDim + "," + cfgstruct[grid_num, j].name + " = ( SELECT " + cfgstruct[grid_num, j].name + " FROM " + tab_name + " WHERE ";
                            ZeroDim = ZeroDim + cfgstruct[grid_num, 0].name + " = '" + dlg.key_from[0] + "'";

                            for (n = 1; n < tablestruct[grid_num].key_num; n++)
                            {
                                ZeroDim = ZeroDim + " AND " + cfgstruct[grid_num, n].name + " = '" + dlg.key_from[n] + "'";
                            }
                            ZeroDim = ZeroDim + " )";
                        }
                    count0++;
                }
                else if (cfgstruct[grid_num, j].dim == "1")
                {
                    idx_num = SearchNum(tab_name);
                    if (OneDim == null)
                    {
                        if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                        { timeflag1[count1] = true; }
                        OneDimS[count1] = cfgstruct[grid_num, j].name;
                        OneDimStr = cfgstruct[grid_num, j].name;
                        
                        {
                            OneDim = cfgstruct[grid_num, j].name + " = ( SELECT " + cfgstruct[grid_num, j].name + " FROM " + tab_name + "_1d WHERE ";
                        }
                        OneDim = OneDim + cfgstruct[grid_num, 0].name + " = '" + dlg.key_from[0] + "'";
                        
                       
                            for (n = 1; n < tablestruct[grid_num].key_num; n++)
                            {
                                OneDim = OneDim + " AND " + cfgstruct[grid_num, n].name + " = '" + dlg.key_from[n] + "'";
                            }
                        
                        OneDim = OneDim + " )";
                        //if (tab_name == "CAMP")
                        //{
                        //    OneDim = OneDim+",CID = ( SELECT CID FROM " + tab_name + "_1d WHERE "+ cfgstruct[grid_num, 0].name + " = '" + dlg.key_from[0] + "')";
                        //    OneDim = OneDim + ",MsgID = ( SELECT MsgID FROM " + tab_name + "_1d WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.key_from[0] + "')";
                        //    OneDim = OneDim + ",AliasID = ( SELECT AliasID FROM " + tab_name + "_1d WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.key_from[0] + "')";
                        //    OneDim = OneDim + ",idx1 = ( SELECT idx1 FROM " + tab_name + "_1d WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.key_from[0] + "')";
                        //}
                    }
                    else
                    {
                        if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                        { timeflag1[count1] = true; }
                        OneDimS[count1] = cfgstruct[grid_num, j].name;
                        OneDimStr = OneDimStr + "," + cfgstruct[grid_num, j].name;
                        OneDim += "," + cfgstruct[grid_num, j].name + " = ( SELECT " + cfgstruct[grid_num, j].name + " FROM " + tab_name + "_1d WHERE ";
                        OneDim = OneDim + cfgstruct[grid_num, 0].name + " = '" + dlg.key_from[0] + "'";

                        for (n = 1; n < tablestruct[grid_num].key_num; n++)
                        {
                            OneDim = OneDim + " AND " + cfgstruct[grid_num, n].name + " = '" + dlg.key_from[n] + "'";
                        }
                        OneDim = OneDim + " )";
                        // OneDim = OneDim + " AND " + "idx1 = '" + m.ToString() + "'" +" )";
                    }
                    OneDimValue[count1] = cfgstruct[grid_num, j].name;
                    //OneDimStr[count1] = cfgstruct[grid_num, j].name;
                    count1++;
                }else if (cfgstruct[grid_num, j].dim == "2")
                {
                    if (TwoDim == null)
                    {
                        if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                        { timeflag2[count2] = true; }
                        TwoDimS[count2] = cfgstruct[grid_num, j].name;
                        TwoDimStr = cfgstruct[grid_num, j].name;
                        TwoDim = cfgstruct[grid_num, j].name + " = ( SELECT " + cfgstruct[grid_num, j].name + " FROM " + tab_name + "_2d WHERE ";
                        TwoDim = TwoDim + cfgstruct[grid_num, 0].name + " = '" + dlg.key_from[0] + "'";

                        for (n = 1; n < tablestruct[grid_num].key_num; n++)
                        {
                            TwoDim = TwoDim + " AND " + cfgstruct[grid_num, n].name + " = '" + dlg.key_from[n] + "'";
                        }
                        TwoDim = TwoDim + " )";
                    }
                    else 
                    {
                        if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                        { timeflag2[count2] = true; }
                        TwoDimS[count2] = cfgstruct[grid_num, j].name;
                        TwoDimStr = TwoDimStr + "," + cfgstruct[grid_num, j].name;
                        TwoDim += "," + cfgstruct[grid_num, j].name + " = ( SELECT " + cfgstruct[grid_num, j].name + " FROM " + tab_name + "_2d WHERE ";
                        TwoDim = TwoDim + cfgstruct[grid_num, 0].name + " = '" + dlg.key_from[0] + "'";

                        for (n = 1; n < tablestruct[grid_num].key_num; n++)
                        {
                            TwoDim = TwoDim + " AND " + cfgstruct[grid_num, n].name + " = '" + dlg.key_from[n] + "'";
                        }
                        TwoDim = TwoDim + " )";
                    }
                    //TwoDimStr[count2] = cfgstruct[grid_num, j].name;
                    count2++;
                }
            }
                //----------------------------------------------------
                //将全选和单选分开
                int[] t1 = new int[4];
                for (int i = 0; i < 3; i++)
                {
                    for (int t = 0; t < 400; t++)
                    {
                        //if (dlg.repeat[i, t] != null)
                        if (dlg.key_to[i, t] != null)
                        {
                            t1[i]++;
                        }
                        else { break; }
                    }
                }
                if (t1[0] == dlg.select0_num && t1[1] == dlg.select1_num && t1[2] == dlg.select2_num)
                {
                    C_flag = true;
                }
                else if (t1[0] < dlg.select0_num && t1[1] == dlg.select1_num && t1[2] == dlg.select2_num && 10 < t1[0])
                {
                    MessageBox.Show("Do you want to choose all of them,don't you?Please choose again!");
                }
                if (t1[1] == dlg.select1_num && dlg.select1_num != 0)
                {
                    C1_flag = true;
                }
                if (t1[2] == dlg.select2_num && dlg.select2_num!= 0)
                {
                    C2_flag = true;
                }
                if (C_flag == true)
                {
                    if (count0 > 0)
                    {
                        
                        //string[] ZeroDimValue = new string[count0];
                        select = null;
                        select = "SELECT " + ZeroDimStr + " FROM " + tab_name + " WHERE ";
                        select = select + cfgstruct[grid_num, 0].name + " = '" + dlg.key_from[0] + "'";
                        for (n = 1; n < tablestruct[grid_num].key_num; n++)
                        {
                            select = select + " AND " + cfgstruct[grid_num, n].name + " = '" + dlg.key_from[n] + "'";
                        }
                        thisCommand.CommandText = select;
                        SqlDataReader thisReader = thisCommand.ExecuteReader();
                        //int t = 0;
                        while (thisReader.Read())
                        {
                            for (int t = 0; t < count0; t++)
                            {
                                ZeroDimValue[t] = thisReader[ZeroDimS[t]].ToString();
                            }
                        }
                        update_0d = update + tab_name + " SET " + ZeroDimS[0] + "=" + "'" + ZeroDimValue[0] + "'";
                        for (int t = 1; t < count0; t++)
                        {
                            if (timeflag0[t] == true)
                            {
                                //在此写入日期，须将日期进行转换
                                // update_0d = update_0d + "," + ZeroDimS[t] + "="+ ZeroDimValue[t] ;
                            }
                            else
                                update_0d = update_0d + "," + ZeroDimS[t] + "=" + "'" + ZeroDimValue[t] + "'";
                        }
                    //for (n = 1; n < tablestruct[grid_num].key_num; n++)
                    //{
                    //    update_0d = update_0d + " AND " + cfgstruct[grid_num, n].name + " = '" + dlg.key_to[n,0] + "'";
                    //}
                    update_0d = update_0d + " WHERE (";
                    //update_0d = update_0d + cfgstruct[grid_num, 0].name + " = '" + dlg.key_to[0, 0] + "'";
                    for (n = 0; n < tablestruct[grid_num].key_num; n++)
                    {
                        if (dlg.select0_num > 0 && n == 0)
                        {
                            update_0d = update_0d + cfgstruct[grid_num, 0].name + " = '" + dlg.key_to[0, 0] + "'";
                            for (int jc = 1; jc < dlg.select0_num; jc++)
                            {
                                update_0d = update_0d + " or " + cfgstruct[grid_num, n].name + " = '" + dlg.key_to[n, jc] + "'";
                            }
                            update_0d = update_0d + ")";
                        }
                           
                        if(dlg.select1_num >0 && n == 1)
                        {
                            update_0d = update_0d + " and (" + cfgstruct[grid_num, n].name + " = '" + dlg.key_to[n, 0] + "'";
                            for (int jc = 1; jc < dlg.select1_num; jc++)
                            {
                                update_0d = update_0d + " or " + cfgstruct[grid_num, n].name + " = '" + dlg.key_to[n, jc] + "'";
                            }
                            update_0d = update_0d + ")";
                        }
                        if (dlg.select2_num > 0&&n==2)
                        {
                            update_0d = update_0d + " and (" + cfgstruct[grid_num, n].name + " = '" + dlg.key_to[n, 0] + "'";
                            for (int jc = 1; jc < dlg.select2_num; jc++)
                            {
                                update_0d = update_0d + " or " + cfgstruct[grid_num, n].name + " = '" + dlg.key_to[n, jc] + "'";
                            }
                            update_0d = update_0d + ")";
                        }

                    }
                    thisCommand.CommandText = update_0d;
                    thisReader.Close();
                    thisCommand.ExecuteNonQuery();
                   
                }
                    if (count1 > 0)
                    {
                        select = null; ;
                        
                        for (int i = 0; i < idx_num; i++)
                        {
                            select = "SELECT " + OneDimStr + " FROM " + tab_name + "_1d WHERE ";
                            select = select + cfgstruct[grid_num, 0].name + " = '" + dlg.key_from[0] + "'";
                            for (n = 1; n < tablestruct[grid_num].key_num; n++)
                            {
                                select = select + " AND " + cfgstruct[grid_num, n].name + " = '" + dlg.key_from[n] + "'";
                            }
                            select = select + " AND idx1 = '" + i.ToString() + "'";
                            thisCommand.CommandText = select;
                            SqlDataReader thisReader = thisCommand.ExecuteReader();
                            //int t = 0;
                            while (thisReader.Read())
                            {
                                for (int t = 0; t < count1; t++)
                                {
                                    tOneDimValue[t, i] = thisReader[OneDimS[t]].ToString();
                                }
                            }
                            update_1d = update + tab_name + "_1d SET " + OneDimS[0] + "=" + "'" + tOneDimValue[0, i] + "'";
                            for (int t = 1; t < count1; t++)
                            {
                                if (timeflag1[t] == true)
                                {
                                    //在此写入日期，须将日期进行转换
                                    // update_0d = update_0d + "," + ZeroDimS[t] + "="+ ZeroDimValue[t] ;
                                }
                                else
                                    update_1d = update_1d + "," + OneDimS[t] + "=" + "'" + tOneDimValue[t, i] + "'";
                            }
                            update_1d = update_1d + " WHERE idx1=" + "'" + i.ToString() + "'";

                        update_1d = update_1d + " AND (";
                        //update_1d = update_1d  + cfgstruct[grid_num, 0].name + " = '" + dlg.key_to[0, 0] + "'";
                        for (n = 0; n < tablestruct[grid_num].key_num; n++)
                        {
                            if (dlg.select0_num > 0 && n == 0)
                            {
                                update_1d = update_1d + cfgstruct[grid_num, 0].name + " = '" + dlg.key_to[0, 0] + "'";
                                for (int jc = 1; jc < dlg.select0_num; jc++)
                                {
                                    update_1d = update_1d + " or " + cfgstruct[grid_num, n].name + " = '" + dlg.key_to[n, jc] + "'";
                                }
                                update_1d = update_1d + ")";
                            }
                            if (dlg.select1_num > 0 && n == 1)
                            {
                                update_1d = update_1d + " and (" + cfgstruct[grid_num, n].name + " = '" + dlg.key_to[n, 0] + "'";
                                for (int jc = 1; jc < dlg.select1_num; jc++)
                                {
                                    update_1d = update_1d + " or " + cfgstruct[grid_num, n].name + " = '" + dlg.key_to[n, jc] + "'";
                                }
                                update_1d = update_1d + ")";
                            }
                            if (dlg.select2_num > 0 && n == 2)
                            {
                                update_1d = update_1d + " and (" + cfgstruct[grid_num, n].name + " = '" + dlg.key_to[n, 0] + "'";
                                for (int jc = 1; jc < dlg.select2_num; jc++)
                                {
                                    update_1d = update_1d + " or " + cfgstruct[grid_num, n].name + " = '" + dlg.key_to[n, jc] + "'";
                                }
                                update_1d = update_1d + ")";
                            }

                        }
                        thisCommand.CommandText = update_1d;
                            thisReader.Close();
                            thisCommand.ExecuteNonQuery();
                        }
                    }
                    if (count2 > 0)
                    {
                        select = null;
                        
                        for (int i = 0; i < idx_num; i++)
                        {
                            for (int c = 0; c < idx_num_2d; c++)
                            {
                                select = "SELECT " + TwoDimStr + " FROM " + tab_name + "_2d WHERE ";
                                select = select + cfgstruct[grid_num, 0].name + " = '" + dlg.key_from[0] + "'";
                                for (n = 1; n < tablestruct[grid_num].key_num; n++)
                                {
                                    select = select + " AND " + cfgstruct[grid_num, n].name + " = '" + dlg.key_from[n] + "'";
                                }
                                select = select + " AND idx1 = '" + i.ToString() + "'" + " AND idx2 = '" + c.ToString() + "'";
                                thisCommand.CommandText = select;
                                SqlDataReader thisReader = thisCommand.ExecuteReader();
                                //int t = 0;
                                while (thisReader.Read())
                                {
                                    for (int t = 0; t < count2; t++)
                                    {
                                        tTwoDimValue[t, i, c] = thisReader[TwoDimS[t]].ToString();
                                    }
                                }
                                update_2d = update + tab_name + "_2d SET " + TwoDimS[0] + "=" + "'" + tTwoDimValue[0, i, c] + "'";
                                for (int t = 1; t < count2; t++)
                                {
                                    if (timeflag2[t] == true)
                                    {
                                        //在此写入日期，须将日期进行转换
                                        // update_0d = update_0d + "," + ZeroDimS[t] + "="+ ZeroDimValue[t] ;
                                    }
                                    else
                                        update_2d = update_2d + "," + TwoDimS[t] + "=" + "'" + tTwoDimValue[t, i, c] + "'";
                                }
                                update_2d = update_2d + " WHERE idx1=" + "'" + i.ToString() + "'" + " AND idx2 = '" + c.ToString() + "'";
                                thisCommand.CommandText = update_2d;
                                thisReader.Close();
                                thisCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
                else
                {
                    if (count0 > 0)
                    {
                        select=null;
                        
                        select = "SELECT " + ZeroDimStr + " FROM " + tab_name + " WHERE ";
                        select = select + cfgstruct[grid_num, 0].name + " = '" + dlg.key_from[0] + "'";
                        for (n = 1; n < tablestruct[grid_num].key_num; n++)
                        {
                            select = select + " AND " + cfgstruct[grid_num, n].name + " = '" + dlg.key_from[n] + "'";
                        }
                        thisCommand.CommandText = select;
                        SqlDataReader thisReader = thisCommand.ExecuteReader();
                        //int t = 0;
                        while (thisReader.Read())
                        {
                            for (int t = 0; t < count0; t++)
                            {
                                ZeroDimValue[t] = thisReader[ZeroDimS[t]].ToString();
                            }
                        }
                    }
                    if (count1 > 0)
                    {
                       select = null;
                        for (int i = 0; i < idx_num; i++)
                        {
                            select = "SELECT " + OneDimStr + " FROM " + tab_name + "_1d WHERE ";
                            select = select + cfgstruct[grid_num, 0].name + " = '" + dlg.key_from[0] + "'";
                            for (n = 1; n < tablestruct[grid_num].key_num; n++)
                            {
                                select = select + " AND " + cfgstruct[grid_num, n].name + " = '" + dlg.key_from[n] + "'";
                            }
                            select = select + " AND idx1 = '" + i.ToString() + "'";
                            thisCommand.CommandText = select;
                            SqlDataReader thisReader = thisCommand.ExecuteReader();
                            //int t = 0;
                            while (thisReader.Read())
                            {
                                for (int t = 0; t < count1; t++)
                                {
                                    tOneDimValue[t, i] = thisReader[OneDimS[t]].ToString();
                                }
                            }
                        }
                    }
                    if (count2 > 0)
                    {
                       select=null;
                        for (int i = 0; i < idx_num; i++)
                        {
                            for (int c = 0; c < idx_num_2d; c++)
                            {
                                select = "SELECT " + TwoDimStr + " FROM " + tab_name + "_2d WHERE ";
                                select = select + cfgstruct[grid_num, 0].name + " = '" + dlg.key_from[0] + "'";
                                for (n = 1; n < tablestruct[grid_num].key_num; n++)
                                {
                                    select = select + " AND " + cfgstruct[grid_num, n].name + " = '" + dlg.key_from[n] + "'";
                                }
                                select = select + " AND idx1 = '" + i.ToString() + "'" + " AND idx2 = '" + c.ToString() + "'";
                                thisCommand.CommandText = select;
                                SqlDataReader thisReader = thisCommand.ExecuteReader();
                                //int t = 0;
                                while (thisReader.Read())
                                {
                                    for (int t = 0; t < count2; t++)
                                    {
                                        tTwoDimValue[t, i, c] = thisReader[TwoDimS[t]].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
                //====================================================
            if (C_flag==false)
            {
            for (num[0] = 0; num[0] < dlg.select0_num; num[0]++)
            {
                for (num[1] = 0; num[1] < dlg.select1_num; num[1]++)
                {
                    for (num[2] = 0; num[2] < dlg.select2_num; num[2]++)
                    {
                        if (count0 > 0)
                        {
                            update_0d = update + tab_name + " SET " + ZeroDimS[0] + "=" + "'" + ZeroDimValue[0] + "'";
                            for (int t = 1; t < count0; t++)
                            {
                                if (timeflag0[t] == true)
                                {
                                    //在此写入日期，须将日期进行转换
                                    // update_0d = update_0d + "," + ZeroDimS[t] + "="+ ZeroDimValue[t] ;
                                }
                                else
                                    update_0d = update_0d + "," + ZeroDimS[t] + "=" + "'" + ZeroDimValue[t] + "'" ;
                            }

                            update_0d = update_0d + " WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.key_to[0, num[0]] + "'";

                            for (n = 1; n < tablestruct[grid_num].key_num; n++)
                            {
                                update_0d = update_0d + " AND " + cfgstruct[grid_num, n].name + " = '" + dlg.key_to[n, num[n]] + "'";
                            }
                            // thisCommand.Connection = thisConnection;
                            thisCommand.CommandText = update_0d;
                                
                                //thisCommand.Transaction = Trn;

                                thisCommand.ExecuteNonQuery();
                        }
                        for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                        {
                            if (cfgstruct[grid_num, j].dim == "1")
                            {
                                //-------------------------------
                                for (int i = 0; i < idx_num; i++)
                                {
                                    update_1d = update + tab_name + "_1d SET " + OneDimS[0] + "=" + "'" + tOneDimValue[0, i] + "'";
                                    for (int t = 1; t < count1; t++)
                                    {
                                        if (timeflag1[t] == true)
                                        {
                                            //在此写入日期，须将日期进行转换
                                            // update_0d = update_0d + "," + ZeroDimS[t] + "="+ ZeroDimValue[t] ;
                                        }
                                        else
                                            update_1d = update_1d + "," + OneDimS[t] + "=" + "'" + tOneDimValue[t, i] + "'";
                                    }
                                    update_1d = update_1d + " WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.key_to[0, num[0]] + "'";
                                    for (n = 1; n < tablestruct[grid_num].key_num; n++)
                                    {
                                        update_1d = update_1d + " AND " + cfgstruct[grid_num, n].name + " = '" + dlg.key_to[n, num[n]] + "'";
                                    }
                                    update_1d = update_1d + " AND " + "idx1 = '" + i.ToString() + "'";
                                    thisCommand.CommandText = update_1d;
                                    //thisCommand.Transaction = Trn;
                                    thisCommand.ExecuteNonQuery();
                                }
                                //-------------------------------
                            }
                        }

                        for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                        {
                            if (cfgstruct[grid_num, j].dim == "2")
                            {
                                for (m = 0; m < idx_num; m++)
                                {
                                    for (p = 0; p < idx_num_2d; p++)
                                    {
                                        /////////////
                                        update_2d = update + tab_name + "_2d SET " + TwoDimS[0] + "=" + "'" + tTwoDimValue[0, m, p] + "'";
                                        for (int t = 1; t < count2; t++)
                                        {
                                            if (timeflag2[t] == true)
                                            {
                                                //在此写入日期，须将日期进行转换
                                                // update_0d = update_0d + "," + ZeroDimS[t] + "="+ ZeroDimValue[t] ;
                                            }
                                            else
                                                update_2d = update_2d + "," + TwoDimS[t] + "=" + "'" + tTwoDimValue[t, m, p] + "'";
                                        }
                                        update_2d = update_2d + " WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.key_to[0, num[0]] + "'";
                                        for (n = 1; n < tablestruct[grid_num].key_num; n++)
                                        {
                                            update_2d = update_2d + " AND " + cfgstruct[grid_num, n].name + " = '" + dlg.key_to[n, num[n]] + "'";
                                        }
                                        update_2d = update_2d + " AND idx1=" + "'" + m.ToString() + "'" + " AND idx2 = '" + p.ToString() + "'";
                                        
                                        thisCommand.CommandText = update_2d;
                                        thisCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
            //}
            //--------------------------------------------------------------------------------------------------------------------------
            Trn.Commit();
            thisConnection.Close();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            button9_Click(sender, e);          
        }

        private void printTableOnlyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string tab_name = tabControl1.SelectedTab.Name;
            int grid_num = SearchGridNum(tab_name);
            if (!tablestruct[grid_num].live)
            {
                MessageBox.Show("Data is null!");
                return;
            }
            try
            {
                pageSetupDialog1.Document = printDocument1;

                if (pageSetupDialog1.ShowDialog().ToString() == "OK")
                {

                    printPreviewDialog1.Document = printDocument1;

                    printPreviewDialog1.Height = 600;

                    printPreviewDialog1.Width = 800;

                    printPreviewDialog1.ShowDialog();
                }
            }
            catch (Exception a)
            {

                throw new Exception(a.Message);

            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string tab_name = tabControl1.SelectedTab.Name;
            int grid_num = SearchGridNum(tab_name);
            int y = 0, x = 0;
            int rowGap = 20;
            int leftMargin = 50;
            int j;
            int i = this.linenum;

            Font font = new Font("Arial", 10);

            Font headingFont = new Font("Arial", 11, FontStyle.Underline);

            Font captionFont = new Font("Arial", 10, FontStyle.Bold);

            Brush brush = new SolidBrush(Color.Black);

            string cellValue = "";

           int rowCount = dataGridView[grid_num].Rows.Count - 1;

           int colCount = dataGridView[grid_num].ColumnCount;

            y += rowGap;

            x = leftMargin;

            //print headings 
            //for (int j = 0; j < colCount; j++)
            //{
            //    if (dataGridView[grid_num].Columns[j].Width > 0)
            //    {
            //        cellValue = dataGridView[grid_num].Columns[j].HeaderText;
            //        e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), x, y, dataGridView[grid_num].Columns[j].Width, rowGap);
            //        e.Graphics.DrawRectangle(Pens.Black, x, y, dataGridView[grid_num].Columns[j].Width, rowGap);
            //        e.Graphics.DrawString(cellValue, headingFont, brush, x, y);
            //        x += dataGridView[grid_num].Columns[j].Width-20;
            //    }
            //}

            //print headings
            System.Windows.Forms.DataGridViewRow gridrow;


            //print all rows 

            for (; i < rowCount+1; i++)
            {
                if (colCount < setnum)
                {
                    media = colCount;
                }
                else
                {
                    media = setnum;
                }

                y += rowGap;

                x = leftMargin;

                //print headings
                if ((media<8) && (!several))
                {

                    gridrow = this.dataGridView[grid_num].Rows[i];
                    if (gridrow.HeaderCell.Value == null)
                    {
                        cellValue = "";
                    }
                    else
                    {
                        cellValue = gridrow.HeaderCell.Value.ToString();
                    }
                    e.Graphics.DrawRectangle(Pens.Black, x, y, 480, rowGap);

                    e.Graphics.DrawString(cellValue, font, brush, x, y);

                    x += 480;

                }

                for (j = test; j < media; j++)
                {

                    if (dataGridView[grid_num].Columns[j].Width > 0)
                    {
                        if (dataGridView[grid_num].Rows[i].Cells[j].Value == null)
                        {
                            cellValue = "";
                        }
                        else
                        {
                            cellValue = dataGridView[grid_num].Rows[i].Cells[j].Value.ToString();
                        }

                        e.Graphics.DrawRectangle(Pens.Black, x, y, dataGridView[grid_num].Columns[j].Width, rowGap);

                        e.Graphics.DrawString(cellValue, font, brush, x, y);

                        x += dataGridView[grid_num].Columns[j].Width;

                    }

                }
                if ((j == media) && (media < colCount) && (i == rowCount))
                {
                    i = 0;
                    test = setnum;
                    setnum = setnum + 12;
                    y = y + 20;
                }

                // more than one page
                if (y >= e.PageBounds.Height - 80)
                {

                    y = 0;
                    e.HasMorePages = true;
                    several = true;
                    i++;
                    this.linenum = i;
                    return;

                }

            }

            y += rowGap;

            for (j = 0; j < colCount; j++)
            {

                e.Graphics.DrawString(" ", font, brush, x, y);

            }

            e.HasMorePages = false;
            media = 0;
            test = 0;
            linenum = 0;
            setnum = 6;
            several = false;
        }

        private void button5_Click(object sender, EventArgs e) //data cpy
        {
            string tab_name = tabControl1.SelectedTab.Name;
            int grid_num = SearchGridNum(tab_name);
            int key_num = tablestruct[grid_num].key_num;
            string[] key = new string[key_num];
            int i,j=0,m,n;
            int row, column;
            string update;
            string buffer;
            string bufferfalse="";

            for (i = 0; i < key_num; i++)
            {
                key[i] = cfgstruct[grid_num, i].name;
            }
            File_Save dlg = new File_Save(tab_name, key_num, key, strConnection, false,true);
            if (dlg.count == -1)
            {
                return;
            }
            dlg.ShowDialog();
            if (!dlg.ok)
            {
                return;
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

            for (i=0; i < this.dataGridView[grid_num].SelectedCells.Count; i++)
            {
                row = this.dataGridView[grid_num].SelectedCells[i].RowIndex;
                column = this.dataGridView[grid_num].SelectedCells[i].ColumnIndex;

                if (this.dataGridView[grid_num].Rows[row].Cells[column].Value!=null)
                {
                    bufferfalse = this.dataGridView[grid_num].Rows[row].Cells[column].Value.ToString();
                }
                if ( (this.dataGridView[grid_num].Rows[row].Cells[column].Value!=null)&&(this.dataGridView[grid_num].Rows[row].Cells[column].Value.ToString() == "false"))
                {
                    bufferfalse = "0";
                }
                else if ((this.dataGridView[grid_num].Rows[row].Cells[column].Value!=null)&&(this.dataGridView[grid_num].Rows[row].Cells[column].Value.ToString() == "true"))
                {
                    bufferfalse = "1";
                }
                

                if (this.dataGridView[grid_num].Rows[row].Cells[column].ReadOnly == true)
                {
                    continue;
                }
                else if (column == 0)// 
                {
                    System.Windows.Forms.DataGridViewRow gridrow1;
                    gridrow1 = this.dataGridView[grid_num].Rows[row];
                    if (gridrow1.HeaderCell.Value != null)
                    {
                        for (j = 0; j < tablestruct[grid_num].number; j++)
                        {
                            if (cfgstruct[grid_num, j].explant == gridrow1.HeaderCell.Value.ToString())
                            {
                                row = j;
                                break;
                            }
                        }
                    }
                   for(j=0;j<dlg.Select_num[0];j++)
                   {
                       for(m=0;m<dlg.Select_num[1];m++)
                       {
                           for (n = 0; n < dlg.Select_num[2]; n++)
                           {
                               if (cfgstruct[grid_num, row].datatype == "BOOLEAN8")
                               {
                                   if(bufferfalse == "false")
                                   {
                                       bufferfalse = "0";
                                   }
                                   else if(bufferfalse == "true")
                                   {
                                       bufferfalse = "1";
                                   }
                               }
                               if (cfgstruct[grid_num, row].datatype == "GE_TIME")
                               {
                               }
                               else 
                               {
                                   update = "UPDATE " + tab_name + " SET " + cfgstruct[grid_num, row].name + " = '" + bufferfalse
                                   + "' WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.select_key[0, j] + "' AND " + cfgstruct[grid_num, 1].name + " = '" + dlg.select_key[1, m] + "' AND "
                                   + cfgstruct[grid_num, 2].name + " = '" + dlg.select_key[2, n] + "'";

                                   thisCommand.CommandText = update;
                                   thisCommand.ExecuteNonQuery();
                               }
                           }
                           if (n == 0)
                           {
                               if (cfgstruct[grid_num, row].datatype == "BOOLEAN8")
                               {
                                   if (bufferfalse == "false")
                                   {
                                       bufferfalse = "0";
                                   }
                                   else if(bufferfalse == "true")
                                   {
                                       bufferfalse = "1";
                                   }
                               }
                               if (cfgstruct[grid_num, row].datatype == "GE_TIME")
                               {
                               }
                               else
                               {
                                   update = "UPDATE " + tab_name + " SET " + cfgstruct[grid_num, row].name + " = '" + bufferfalse
                                     + "' WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.select_key[0, j] + "' AND " + cfgstruct[grid_num, 1].name + " = '" + dlg.select_key[1, m] + "'";

                                   thisCommand.CommandText = update;
                                   thisCommand.ExecuteNonQuery();
                               }
                           }
                       }
                       if (m == 0)
                       {

                           update = "UPDATE " + tab_name + " SET " + cfgstruct[grid_num, row].name + " = '" + bufferfalse
                               + "' WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.select_key[0, j] + "'";
                           try
                           {
                               thisCommand.CommandText = update;
                               thisCommand.ExecuteNonQuery();
                           }
                           catch (Exception a)
                           {
                               MessageBox.Show(a.Message);
                           }
                           
                       }
                   }
                
                }
                else if ((this.dataGridView[grid_num].Rows[row].Cells[1].Value == null) && (this.dataGridView[grid_num].Rows[row].Cells[column].Value != null) && (this.dataGridView[grid_num].Rows[row].Cells[column].ReadOnly == false))//_1d
                {
                    System.Windows.Forms.DataGridViewRow gridrow1;
                    gridrow1 = this.dataGridView[grid_num].Rows[row];
                    if (gridrow1.HeaderCell.Value != null)
                    {
                        for (j = 0; j < tablestruct[grid_num].number; j++)
                        {
                            if (cfgstruct[grid_num, j].explant == gridrow1.HeaderCell.Value.ToString())
                            {

                                break;
                            }
                        }
                    }
                    
                    //if (j == 0)
                    //{
                    //    MessageBox.Show("error!!!!");
                    //    return;
                    //}

                    buffer = cfgstruct[grid_num, j].name;

                    for (j = 0; j < dlg.Select_num[0]; j++)
                    {
                        for (m = 0; m < dlg.Select_num[1]; m++)
                        {
                            for (n = 0; n < dlg.Select_num[2]; n++)
                            {

                                update = "UPDATE " + tab_name + "_1d SET " + buffer + " = '" + bufferfalse
                                    + "' WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.select_key[0, j] + "' AND " + cfgstruct[grid_num, 1].name + " = '" + dlg.select_key[1, m] + "' AND "
                                    + cfgstruct[grid_num, 2].name + " = '" + dlg.select_key[2, n] + "' AND idx1 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[row - 1].Cells[column].Value.ToString()) - 1) + "'";

                                thisCommand.CommandText = update;
                                thisCommand.ExecuteNonQuery();
                            }
                            if (n == 0)
                            {
                                update = "UPDATE " + tab_name + "_1d SET " + buffer + " = '" + bufferfalse
                                      + "' WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.select_key[0, j] + "' AND " + cfgstruct[grid_num, 1].name + " = '" + dlg.select_key[1, m] +
                                      "' AND idx1 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[row - 1].Cells[column].Value.ToString()) - 1) + "'";

                                thisCommand.CommandText = update;
                                thisCommand.ExecuteNonQuery();

                            }
                        }
                        if (m == 0)
                        {

                            update = "UPDATE " + tab_name + "_1d SET " + buffer + " = '" + bufferfalse
                                + "' WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.select_key[0, j] +"' AND idx1 = '" + Convert.ToString( Convert.ToInt32( this.dataGridView[grid_num].Rows[row - 1].Cells[column].Value.ToString())-1)+"'";

                            thisCommand.CommandText = update;
                            thisCommand.ExecuteNonQuery();
                        }
                    }

                }
                else if (this.dataGridView[grid_num].Rows[row].Cells[column].Value == null)
                {
                    continue;
                }
                else if(this.dataGridView[grid_num].Rows[row].Cells[1].Value.ToString() == "1")
                {
                    System.Windows.Forms.DataGridViewRow gridrow1;
                    gridrow1 = this.dataGridView[grid_num].Rows[row];
                    if (gridrow1.HeaderCell.Value != null)
                    {
                        for (j = 0; j < tablestruct[grid_num].number; j++)
                        {
                            if (cfgstruct[grid_num, j].explant == gridrow1.HeaderCell.Value.ToString())
                            {

                                break;
                            }
                        }
                    }

                    //if (j == 0)
                    //{
                    //    MessageBox.Show("error!!!!");
                    //    return;
                    //}

                    buffer = cfgstruct[grid_num, j].name;

                    for (j = 0; j < dlg.Select_num[0]; j++)
                    {
                        for (m = 0; m < dlg.Select_num[1]; m++)
                        {
                            for (n = 0; n < dlg.Select_num[2]; n++)
                            {

                                update = "UPDATE " + tab_name + "_2d SET " + buffer + " = '" + bufferfalse
                                    + "' WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.select_key[0, j] + "' AND " + cfgstruct[grid_num, 1].name + " = '" + dlg.select_key[1, m] + "' AND "
                                    + cfgstruct[grid_num, 2].name + " = '" + dlg.select_key[2, n] + "' AND idx1 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[row - 1].Cells[column].Value.ToString()) - 1) 
                                     + "' AND idx2 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[row].Cells[1].Value.ToString()) - 1) + "'";

                                thisCommand.CommandText = update;
                                thisCommand.ExecuteNonQuery();
                            }
                            if (n == 0)
                            {
                                update = "UPDATE " + tab_name + "_2d SET " + buffer + " = '" + bufferfalse
                                      + "' WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.select_key[0, j] + "' AND " + cfgstruct[grid_num, 1].name + " = '" + dlg.select_key[1, m] +
                                      "' AND idx1 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[row - 1].Cells[column].Value.ToString()) - 1) 
                                      +"' AND idx2 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[row].Cells[1].Value.ToString()) - 1) + "'";

                                thisCommand.CommandText = update;
                                thisCommand.ExecuteNonQuery();

                            }
                        }
                        if (m == 0)
                        {

                            update = "UPDATE " + tab_name + "_2d SET " + buffer + " = '" + bufferfalse
                                + "' WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.select_key[0, j] + "' AND idx1 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[row - 1].Cells[column].Value.ToString()) - 1)
                                 + "' AND idx2 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[row].Cells[1].Value.ToString()) - 1) + "'";

                            thisCommand.CommandText = update;
                            thisCommand.ExecuteNonQuery();
                        }
                    }


                }
                else if (this.dataGridView[grid_num].Rows[row].Cells[1].Value.ToString() == "2")
                {
                    System.Windows.Forms.DataGridViewRow gridrow1;
                    gridrow1 = this.dataGridView[grid_num].Rows[row-1];
                    if (gridrow1.HeaderCell.Value != null)
                    {
                        for (j = 0; j < tablestruct[grid_num].number; j++)
                        {
                            if (cfgstruct[grid_num, j].explant == gridrow1.HeaderCell.Value.ToString())
                            {

                                break;
                            }
                        }
                    }

                    //if (j == 0)
                    //{
                    //    MessageBox.Show("error!!!!");
                    //    return;
                    //}

                    buffer = cfgstruct[grid_num, j].name;

                    for (j = 0; j < dlg.Select_num[0]; j++)
                    {
                        for (m = 0; m < dlg.Select_num[1]; m++)
                        {
                            for (n = 0; n < dlg.Select_num[2]; n++)
                            {

                                update = "UPDATE " + tab_name + "_2d SET " + buffer + " = '" + bufferfalse
                                    + "' WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.select_key[0, j] + "' AND " + cfgstruct[grid_num, 1].name + " = '" + dlg.select_key[1, m] + "' AND "
                                    + cfgstruct[grid_num, 2].name + " = '" + dlg.select_key[2, n] + "' AND idx1 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[row - 2].Cells[column].Value.ToString()) - 1)
                                     + "' AND idx2 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[row].Cells[1].Value.ToString()) - 1) + "'";

                                thisCommand.CommandText = update;
                                thisCommand.ExecuteNonQuery();
                            }
                            if (n == 0)
                            {
                                update = "UPDATE " + tab_name + "_2d SET " + buffer + " = '" + bufferfalse
                                      + "' WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.select_key[0, j] + "' AND " + cfgstruct[grid_num, 1].name + " = '" + dlg.select_key[1, m] +
                                      "' AND idx1 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[row - 2].Cells[column].Value.ToString()) - 1)
                                      + "' AND idx2 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[row].Cells[1].Value.ToString()) - 1) + "'";

                                thisCommand.CommandText = update;
                                thisCommand.ExecuteNonQuery();

                            }
                        }
                        if (m == 0)
                        {

                            update = "UPDATE " + tab_name + "_2d SET " + buffer + " = '" + bufferfalse
                                + "' WHERE " + cfgstruct[grid_num, 0].name + " = '" + dlg.select_key[0, j] + "' AND idx1 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[row - 2].Cells[column].Value.ToString()) - 1)
                                 + "' AND idx2 = '" + Convert.ToString(Convert.ToInt32(this.dataGridView[grid_num].Rows[row].Cells[1].Value.ToString()) - 1) + "'";

                            thisCommand.CommandText = update;
                            thisCommand.ExecuteNonQuery();
                        }
                    }


                }
                else
                {
                    MessageBox.Show("The selection can't find !");
                }

            }
        }

        private void saveDBRecordsInFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button8_Click(sender, e); 
        }

        private void loadDBRecordsFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button7_Click(sender, e);
        }

        private void cmb_cell_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Mainframe_Load(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

    }
}
