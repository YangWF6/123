using System.IO;
using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ModelTableUtility
{
    partial class Mainframe
    {
        //private string strConnection = @"server=172.17.1.10;database=ModelTables;uid=sa;pwd=sa;Connect Timeout=1;";
        private string strConnection;
        private struct FileContext
        {
            public string name ;  //配置文件中某一行 中第一个字符串的名字
            public string explant;  
            public string Job;
            public int key;
            public int key_num; //tablestruct
  
            public string datatype;
            public int idx1;
            public int idx2;
            public string dim;
            public string default_value;
            public string min;
            public string max;
            public string unit;
            public string boolen;

            public int number;
            public int horizon_long;

            public bool live;  // tablestruct 使用
        }

        // print
        private int media = 0;
        private int test = 0;
        private int linenum = 0;
        private int setnum = 6;
        private bool several = false;

        //
        private int TotalNum=0;
        private FileContext[] tablestruct = new FileContext[50]; //modtbls;
        private FileContext[,] cfgstruct = new FileContext[50,200]; //cfg;
       // private string data_source;
        private bool DataSource()
        {
            Select_Data_Source dlg = new Select_Data_Source(strConnection);
            if (dlg.fail)
            {
                return true;
            }
            dlg.ShowDialog();
            if (!dlg.ok)
            {
                return true;
            }
            strConnection = dlg.strConnection;
            return false;
        }
        private bool ReadAllDat()
        {
            int i;
            for (i = 0; i < TotalNum; i++)
            {
                if (ReadDat(tablestruct[i].name, i) == 0)
                {
                    return false;
                }
            }
            return true;            
        }
        private bool ReadAllCfg()
        {
            int i;
            for (i = 0; i < TotalNum; i++)
            {
                if (ReadCfg(tablestruct[i].name, i) == 0)
                {
                    return false;
                }
            }
            return true;     
        }
        private int ReadDat(string datname,int dat_n)
        {
            StreamReader Dat;
            string filename;
            string buffer;
            int i = 0;
            int length;
            if (datname == null)
            {
                return 0;
            }
            else
            {
                filename = datname + ".dat";
                try
                {
                    Dat = File.OpenText(filename);
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                    return 0;
                }
                buffer = Dat.ReadLine();

                // 可以读dat 中 num；
                buffer = Dat.ReadLine();

                while (buffer != null)
                {
                    buffer.Trim();
                    length = buffer.IndexOf(",");
                    if (length != -1)
                    {
                        if (cfgstruct[dat_n, i].name != buffer.Substring(0, length).Trim())
                        {
                            MessageBox.Show("Fail to read " + cfgstruct[dat_n, 0].name + "dat! ");
                            return 0;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Fail to read dat! ");
                        return 0;
                    }

                    //datatype
                    buffer = buffer.Substring(length + 1, buffer.Length - length - 1);
                    length = buffer.IndexOf(",");
                    cfgstruct[dat_n, i].datatype = buffer.Substring(0, length).Trim();

                    //dim
                    buffer = buffer.Substring(length + 1, buffer.Length - length - 1);
                    length = buffer.IndexOf(",");
                    cfgstruct[dat_n, i].dim = buffer.Substring(0, length).Trim();

                    //idx1
                    buffer = buffer.Substring(length + 1, buffer.Length - length - 1);
                    length = buffer.IndexOf(",");
                    cfgstruct[dat_n, i].idx1 = Convert.ToInt32(buffer.Substring(0, length).Trim());

                    //idx2
                    buffer = buffer.Substring(length + 1, buffer.Length - length - 1);
                    length = buffer.IndexOf(",");
                    cfgstruct[dat_n, i].idx2 = Convert.ToInt32(buffer.Substring(0, length).Trim());

                    if ((cfgstruct[dat_n, i].dim == "1") && (tablestruct[dat_n].dim == null))
                    {
                        tablestruct[dat_n].dim = "1";
                        tablestruct[dat_n].idx1 = cfgstruct[dat_n, i].idx1;
                    }
                    else if (cfgstruct[dat_n, i].dim == "1")                         
                    {
                        if (tablestruct[dat_n].idx1 < cfgstruct[dat_n, i].idx1)
                        {
                            tablestruct[dat_n].idx1 = cfgstruct[dat_n, i].idx1;
                        }
                     }
                    else if (cfgstruct[dat_n, i].dim == "2")
                    {
                        tablestruct[dat_n].dim = "2";
                        if (tablestruct[dat_n].idx2 < cfgstruct[dat_n, i].idx2)
                        {
                            tablestruct[dat_n].idx2 = cfgstruct[dat_n, i].idx2;
                        }
                    }

                    //default_value
                    buffer = buffer.Substring(length + 1, buffer.Length - length - 1);
                    length = buffer.IndexOf(",");
                    cfgstruct[dat_n, i].default_value = buffer.Substring(0, length).Trim();

                    //min
                    buffer = buffer.Substring(length + 1, buffer.Length - length - 1);
                    length = buffer.IndexOf(",");
                    cfgstruct[dat_n, i].min = buffer.Substring(0, length).Trim();

                    //max
                    buffer = buffer.Substring(length + 1, buffer.Length - length - 1);
                    length = buffer.IndexOf(",");
                    cfgstruct[dat_n, i].max = buffer.Substring(0, length).Trim();

                    //unit
                    buffer = buffer.Substring(length + 1, buffer.Length - length - 1);
                    length = buffer.IndexOf(",");
                    cfgstruct[dat_n, i].unit = buffer.Substring(0, length).Trim();

                    //boolen
                    buffer = buffer.Substring(length + 1, buffer.Length - length - 1);
                    cfgstruct[dat_n, i].boolen = buffer;

                    buffer = Dat.ReadLine();
                    i++;
                }

            }
            return 1;               
        }
        private int ReadCfg(string cfgname,int nn)     //cfgname 当前所选中的tab的名子
        {
            int key_num=0;
            int i = 0;
            int length = 0;
            int length0 = 0;
            string buffer;
            StreamReader ModTbls;
            string filename;
                i = 0;
            if(cfgname==null)
            {
                return 0;
            }
            else
            {
                filename = cfgname + ".cfg";
                try
                {
                    ModTbls = File.OpenText(filename);
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                    return 0;
                }
                buffer = ModTbls.ReadLine();
                while (buffer != null)
                {
                    buffer.Trim();
                    if ((length0 = buffer.Substring(0,7).IndexOf("(+)")) != -1)
                    {
                        length0 = 3;
                        cfgstruct[nn,i].key = 1;
                        key_num++;
                    }
                    else
                    {
                        length0 = 0;
                        cfgstruct[nn, i].key = 0;
                    }
                    if (buffer.IndexOf("!") == -1)
                    {
                        buffer.Trim();
                        length = buffer.IndexOf(":");
                        if (length > 0)
                            cfgstruct[nn,i].name = buffer.Substring(length0, length - length0);
                        else
                        {
                            return 0;
                        }
                        if (buffer.Length > length + 1)
                        {

                            buffer = buffer.Substring(length + 1, buffer.Trim().Length - length - 1);

                            if (buffer != null)
                                length = buffer.IndexOf(":");
                            if (length > 0)
                                cfgstruct[nn,i].explant = buffer.Substring(0, length);

                            if (buffer.Length > length + 1)
                            {
                                buffer = buffer.Substring(length + 1, (buffer.Trim().Length - length - 1));
                                if (buffer != null)
                                    cfgstruct[nn,i].Job = buffer;
                            }
                        }
                        i++;
                    }
                    buffer = ModTbls.ReadLine();
                }
                tablestruct[nn].number = i;
                tablestruct[nn].key_num = key_num;
                ModTbls.Close();
            }
            return 1;
        }
        private bool Config()
        {
            int i=0;
            StreamReader config;
            string buffer;
            string ModTblPWD="";
            string MasterUID="";
            string ModTblServer="";
            string media;
            int length;

            try
            {
                config = File.OpenText("d:\\Maint\\Config\\INI\\dbservices.ini");
            }
            catch (Exception)
            {
                return false;
            }
            buffer = config.ReadLine();
            if (buffer == null)
            {
                return false;
            }
            while (buffer != null)
            {

                if (buffer.IndexOf("ModTblPWD") != -1)
                {
                    length = buffer.IndexOf("=");
                    ModTblPWD = buffer.Substring(length + 1, buffer.Length - length- 1).Trim();
                }
                else if (buffer.IndexOf("MasterUID") != -1)
                {
                    length = buffer.IndexOf("=");
                    MasterUID = buffer.Substring(length + 1, buffer.Length - length - 1).Trim();
                }
                else if (buffer.IndexOf("ModTblServer") != -1)
                {
                    length = buffer.IndexOf("=");
                    ModTblServer = buffer.Substring(length + 1, buffer.Length - length - 1).Trim();
                }
                i++;
                if (i > 200)
                {
                    return false;
                }
                buffer = config.ReadLine();
            }

            if (ModTblServer.ToLower() == "local")
            {
                strConnection = @"Server=(local);Database=master;Trusted_Connection=True;";
            }
            else
            {
                media = "server=" + ModTblServer + ";Database=master;uid=" + MasterUID + ";pwd=" + ModTblPWD + ";Connect Timeout=1;";
                strConnection = @media;
            }
            return true;
            
        }
        private int InitializeFile()
        {
            int i=0;
            int length;         
            string buffer;
            StreamReader ModTbls;
            try
            {
                ModTbls = File.OpenText("ModTbls.cfg");
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                return 3;
            }
            buffer = ModTbls.ReadLine();
            while (buffer != null)
            {
                if (buffer.Trim() == "")
                {
                    break;
                }
                buffer.Trim();
                if (buffer.IndexOf("!") == -1)
                {
                    buffer.Trim();
                    length = buffer.IndexOf(":");
                    if (length > 0)
                        tablestruct[i].name = buffer.Substring(0, length);
                    else
                    {
                        return 0;
                    }
                    if (buffer.Length > length + 1)
                    {

                        buffer = buffer.Substring(length + 1, buffer.Trim().Length - length -1);

                        if (buffer != null)
                            length = buffer.IndexOf(":");
                        if (length > 0)
                            tablestruct[i].explant = buffer.Substring(0, length);

                        if (buffer.Length > length + 1)
                        {
                            buffer = buffer.Substring(length + 1, (buffer.Trim().Length - length - 1));
                            if (buffer != null)
                                tablestruct[i].Job = buffer;
                        }
                    }
                    i++;
                }
                buffer = ModTbls.ReadLine();
            }
            TotalNum = i;
            ModTbls.Close();
            return 1;         
        }
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainframe));
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printTableOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printEntireScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDBRecordInFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDBRecordInFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDBRecordFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.cmb_cell = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.printTableOnlyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDBRecordsInFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDBRecordsFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.printTableOnlyToolStripMenuItem,
            this.printEntireScreenToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // printTableOnlyToolStripMenuItem
            // 
            this.printTableOnlyToolStripMenuItem.Name = "printTableOnlyToolStripMenuItem";
            this.printTableOnlyToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.printTableOnlyToolStripMenuItem.Text = "Print Table Only";
            // 
            // printEntireScreenToolStripMenuItem
            // 
            this.printEntireScreenToolStripMenuItem.Name = "printEntireScreenToolStripMenuItem";
            this.printEntireScreenToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.printEntireScreenToolStripMenuItem.Text = "Print Entire Screen";
            // 
            // saveDBRecordInFileToolStripMenuItem
            // 
            this.saveDBRecordInFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveDBRecordInFileToolStripMenuItem1,
            this.loadDBRecordFromFileToolStripMenuItem});
            this.saveDBRecordInFileToolStripMenuItem.Name = "saveDBRecordInFileToolStripMenuItem";
            this.saveDBRecordInFileToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.saveDBRecordInFileToolStripMenuItem.Text = "Edit";
            // 
            // saveDBRecordInFileToolStripMenuItem1
            // 
            this.saveDBRecordInFileToolStripMenuItem1.Name = "saveDBRecordInFileToolStripMenuItem1";
            this.saveDBRecordInFileToolStripMenuItem1.Size = new System.Drawing.Size(214, 22);
            this.saveDBRecordInFileToolStripMenuItem1.Text = "Save DB Record In File";
            // 
            // loadDBRecordFromFileToolStripMenuItem
            // 
            this.loadDBRecordFromFileToolStripMenuItem.Name = "loadDBRecordFromFileToolStripMenuItem";
            this.loadDBRecordFromFileToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.loadDBRecordFromFileToolStripMenuItem.Text = "Load DB Record From File";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Silver;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(952, 66);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Silver;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 649);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(952, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.HotTrack = true;
            this.tabControl1.ItemSize = new System.Drawing.Size(0, 21);
            this.tabControl1.Location = new System.Drawing.Point(12, 99);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(928, 544);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // cmb_cell
            // 
            this.cmb_cell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmb_cell.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmb_cell.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_cell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cmb_cell.FormattingEnabled = true;
            this.cmb_cell.IntegralHeight = false;
            this.cmb_cell.Items.AddRange(new object[] {
            "TRUE",
            "FALSE"});
            this.cmb_cell.Location = new System.Drawing.Point(13, 620);
            this.cmb_cell.Name = "cmb_cell";
            this.cmb_cell.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmb_cell.Size = new System.Drawing.Size(123, 21);
            this.cmb_cell.TabIndex = 6;
            this.cmb_cell.SelectedValueChanged += new System.EventHandler(this.cmb_cell_SelectedValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightGray;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(952, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem1,
            this.exitToolStripMenuItem1,
            this.printTableOnlyToolStripMenuItem1});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(41, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+S";
            this.saveToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(207, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+E";
            this.exitToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(207, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // printTableOnlyToolStripMenuItem1
            // 
            this.printTableOnlyToolStripMenuItem1.Name = "printTableOnlyToolStripMenuItem1";
            this.printTableOnlyToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+P";
            this.printTableOnlyToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printTableOnlyToolStripMenuItem1.Size = new System.Drawing.Size(207, 22);
            this.printTableOnlyToolStripMenuItem1.Text = "Print Table Only";
            this.printTableOnlyToolStripMenuItem1.Click += new System.EventHandler(this.printTableOnlyToolStripMenuItem1_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveDBRecordsInFileToolStripMenuItem,
            this.loadDBRecordsFromFileToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // saveDBRecordsInFileToolStripMenuItem
            // 
            this.saveDBRecordsInFileToolStripMenuItem.Name = "saveDBRecordsInFileToolStripMenuItem";
            this.saveDBRecordsInFileToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.saveDBRecordsInFileToolStripMenuItem.Text = "Save DB Records In File";
            this.saveDBRecordsInFileToolStripMenuItem.Click += new System.EventHandler(this.saveDBRecordsInFileToolStripMenuItem_Click);
            // 
            // loadDBRecordsFromFileToolStripMenuItem
            // 
            this.loadDBRecordsFromFileToolStripMenuItem.Name = "loadDBRecordsFromFileToolStripMenuItem";
            this.loadDBRecordsFromFileToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.loadDBRecordsFromFileToolStripMenuItem.Text = "Load DB Records From File";
            this.loadDBRecordsFromFileToolStripMenuItem.Click += new System.EventHandler(this.loadDBRecordsFromFileToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Silver;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(238, 650);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 21);
            this.label3.TabIndex = 18;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(398, 298);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.Silver;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(638, 650);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 21);
            this.label4.TabIndex = 19;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Silver;
            this.button7.BackgroundImage = global::ModelTableUtility.Properties.Resources.File_load;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button7.Location = new System.Drawing.Point(780, 28);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(97, 57);
            this.button7.TabIndex = 17;
            this.button7.Text = "File Load";
            this.button7.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Silver;
            this.button8.BackgroundImage = global::ModelTableUtility.Properties.Resources.File_Save;
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button8.Location = new System.Drawing.Point(684, 28);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(97, 57);
            this.button8.TabIndex = 16;
            this.button8.Text = "File Save";
            this.button8.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.Silver;
            this.button9.BackgroundImage = global::ModelTableUtility.Properties.Resources.DB_save;
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button9.Location = new System.Drawing.Point(588, 28);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(97, 57);
            this.button9.TabIndex = 15;
            this.button9.Text = "DB Save";
            this.button9.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Silver;
            this.button4.BackgroundImage = global::ModelTableUtility.Properties.Resources.refresh;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button4.Location = new System.Drawing.Point(492, 28);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(97, 57);
            this.button4.TabIndex = 13;
            this.button4.Text = "Refresh";
            this.button4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Silver;
            this.button5.BackgroundImage = global::ModelTableUtility.Properties.Resources.copy_file;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button5.Location = new System.Drawing.Point(396, 28);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(97, 57);
            this.button5.TabIndex = 12;
            this.button5.Text = "Copy Fields";
            this.button5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Silver;
            this.button6.BackgroundImage = global::ModelTableUtility.Properties.Resources.Copy_records1;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button6.Location = new System.Drawing.Point(300, 28);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(97, 57);
            this.button6.TabIndex = 11;
            this.button6.Text = "Copy Records";
            this.button6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Silver;
            this.button3.BackgroundImage = global::ModelTableUtility.Properties.Resources.Delete;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.Location = new System.Drawing.Point(204, 28);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(97, 57);
            this.button3.TabIndex = 9;
            this.button3.Text = "Delete Records";
            this.button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Silver;
            this.button2.BackgroundImage = global::ModelTableUtility.Properties.Resources.add;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.Location = new System.Drawing.Point(108, 28);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 57);
            this.button2.TabIndex = 8;
            this.button2.Text = "Add Records";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.BackgroundImage = global::ModelTableUtility.Properties.Resources.select;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Location = new System.Drawing.Point(12, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 57);
            this.button1.TabIndex = 7;
            this.button1.Text = "Select Records";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Mainframe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(952, 671);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmb_cell);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "Mainframe";
            this.Text = "odel Table Utility";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage[] tabPage = new System.Windows.Forms.TabPage[50];
        private System.Windows.Forms.DataGridView[] dataGridView = new System.Windows.Forms.DataGridView[50];
        private System.Windows.Forms.Label[] label = new System.Windows.Forms.Label[50];
        private System.Windows.Forms.Label[] labelexplan = new System.Windows.Forms.Label[50];
        private ModelTableUtility.Clist[] list = new ModelTableUtility.Clist [50];

        private void Initalcompent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            for (int i = 0; i < TotalNum; i++)
            {
                //dataGridView
               
                this.dataGridView[i] = new System.Windows.Forms.DataGridView();

                this.dataGridView[i].CurrentCellDirtyStateChanged += new System.EventHandler(CurrentCellDirtyStateChanged);
                this.dataGridView[i].MouseMove += new System.Windows.Forms.MouseEventHandler(dataGridView_MouseMove);
                this.dataGridView[i].Scroll += new System.Windows.Forms.ScrollEventHandler(dataGridView1_Scroll);
                this.dataGridView[i].RowHeadersWidthChanged += new System.EventHandler(RowHeadersWidthChanged);
                this.dataGridView[i].ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(ColumnWidthChanged);
                this.dataGridView[i].CellDoubleClick += new DataGridViewCellEventHandler(CellEnter); ;//CellEnter   DataGridViewCellEventHandler
                this.dataGridView[i].SelectionChanged += new System.EventHandler(SelectChange);

                ((System.ComponentModel.ISupportInitialize)(this.dataGridView[i])).BeginInit();
                this.dataGridView[i].AllowUserToOrderColumns = true;
                this.dataGridView[i].Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                            | System.Windows.Forms.AnchorStyles.Left)
                            | System.Windows.Forms.AnchorStyles.Right)));
                this.dataGridView[i].ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                this.dataGridView[i].Location = new System.Drawing.Point(15, 30);
                this.dataGridView[i].Name = "dataGridView1";
                dataGridViewCellStyle.BackColor = System.Drawing.Color.LightGray; // System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                this.dataGridView[i].RowsDefaultCellStyle = dataGridViewCellStyle;
                this.dataGridView[i].RowHeadersWidth = 300;
                this.dataGridView[i].RowTemplate.Height = 21; //头行高度
                this.dataGridView[i].Size = new System.Drawing.Size(610, 240);
                this.dataGridView[i].RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
                this.dataGridView[i].TabIndex = 0;
                this.dataGridView[i].ColumnHeadersVisible = false;
                this.dataGridView[i].AllowUserToAddRows = false;
                this.dataGridView[i].MultiSelect = true;
                this.dataGridView[i].RowHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;//LightSeaGreen;//FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
                this.dataGridView[i].RowHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White; //头行 字体颜色
                this.dataGridView[i].RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.dataGridView[i].GridColor = System.Drawing.Color.LightGray;
                this.dataGridView[i].EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;//.;EditProgrammatically;
                this.dataGridView[i].AllowUserToDeleteRows = false;
                
             
                //label
                this.label[i] = new System.Windows.Forms.Label();
                this.label[i].AutoSize = true;
                this.label[i].Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.label[i].ForeColor = System.Drawing.Color.DarkBlue;
                this.label[i].Location = new System.Drawing.Point(716, 6);
                this.label[i].Name = tablestruct[i].name;
                this.label[i].Size = new System.Drawing.Size(32, 16);
                this.label[i].TabIndex = 1;
                this.label[i].Text = tablestruct[i].name;

                //labelexplan

                this.labelexplan[i] = new System.Windows.Forms.Label();
                this.labelexplan[i].AutoSize = true;
                this.labelexplan[i].Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.labelexplan[i].ForeColor = System.Drawing.Color.DarkBlue;
                this.labelexplan[i].Location = new System.Drawing.Point(17, 6);
                this.labelexplan[i].Name = tablestruct[i].explant;
                this.labelexplan[i].Size = new System.Drawing.Size(128, 16);
                this.labelexplan[i].TabIndex = 2;
                this.labelexplan[i].Text = tablestruct[i].explant;

               //tabPage
                this.tabPage[i] = new System.Windows.Forms.TabPage();
                this.tabPage[i].SuspendLayout();
                this.tabControl1.Controls.Add(this.tabPage[i]);

                this.tabPage[i].BackColor = System.Drawing.Color.Silver;//FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
                this.tabPage[i].Controls.Add(this.label[i]);
                this.tabPage[i].Controls.Add(this.labelexplan[i]);
                this.tabPage[i].Controls.Add(this.dataGridView[i]);
                this.tabPage[i].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.tabPage[i].Location = new System.Drawing.Point(4, 4);
                this.tabPage[i].Name = tablestruct[i].name;
                this.tabPage[i].Padding = new System.Windows.Forms.Padding(3);
                this.tabPage[i].Size = new System.Drawing.Size(636, 283);
                this.tabPage[i].TabIndex = 0;
                this.tabPage[i].Text ="    "+ tablestruct[i].name+"    ";
                this.tabPage[i].ForeColor = System.Drawing.Color.DarkBlue;
                ((System.ComponentModel.ISupportInitialize)(this.dataGridView[i])).EndInit();
                this.tabPage[i].ResumeLayout(false);
                this.tabPage[i].PerformLayout();
                //list
                this.list[i] = new ModelTableUtility.Clist();

            }
        }

        private bool duplicate(string buffer,int stubon, int num,string[,] buffer2)
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
        
        private void Data_Grid_View(string [] labl,string [] comboxcontent,int numbe)
        {
            double Float;
            int j = 0;
            int num_idx1 = 0;
            int num_idx2 = 0;
            int col_num = 2;
            int col_num_id2 = 2;
            int id2_row =1;
            int id2_col =2;
            bool repeit = true;
            string tabname = tabControl1.SelectedTab.Name;
            string select = "SELECT * FROM " + tabname + " WHERE ";
            string select_1d = "SELECT * FROM " + tabname + "_1d WHERE ";
            string select_2d = "SELECT * FROM " + tabname + "_2d WHERE ";
            bool dim_1d = false;
            bool dim_2d = false;
            string buffer;
            System.Windows.Forms.DataGridViewRow gridrow;
            int i=0;
            int gridnum;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            SqlConnection thisConnection = new SqlConnection(strConnection);
            try
            {
                thisConnection.Open();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
            SqlCommand thisCommand = thisConnection.CreateCommand();

            select = select + labl[i] + " = '" + comboxcontent[i]+"'";
            select_1d = select_1d + labl[i] + " = '" + comboxcontent[i] + "'";
            select_2d = select_2d + labl[i] + " = '" + comboxcontent[i] + "'";
            for (i = 1; i < numbe; i++)
            {
                select =select +" and ";
                select = select + labl[i] + " = '" + comboxcontent[i] + "'";

                select_1d = select_1d + " and ";
                select_1d = select_1d + labl[i] + " = '" + comboxcontent[i] + "'";

                select_2d = select_2d + " and ";
                select_2d = select_2d + labl[i] + " = '" + comboxcontent[i] + "'";
            }
            thisCommand.CommandText = select;
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            gridnum = SearchGridNum(tabname);
            DataGridViewTextBoxColumn colu = new DataGridViewTextBoxColumn();

            colu.Name= "1";
            colu.HeaderText = "";
            colu.DefaultCellStyle = dataGridViewCellStyle;
            colu.Width = 120;
            //colu.f
            RemoveRowColum(gridnum);
            dataGridView[gridnum].Columns.Add(colu);

            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalDigits = 4; 
            while (thisReader.Read())// 读取符合索引要求得所以得行数。如2行  
            {
                // Output ID and name columns
                for(i=0;i<200;i++)
                {
                    if(cfgstruct[gridnum, i].name == null)
                    {
                        break;
                    }
                    if (cfgstruct[gridnum, i].dim == "0")
                    {
                        buffer = thisReader[cfgstruct[gridnum, i].name].ToString();
                        buffer.Trim();
                        if (cfgstruct[gridnum, i].datatype == "SFLOAT")
                        {
                            if (buffer.Trim() != "")
                            {
                                Float = Convert.ToDouble(buffer);
                                //buffer = String.Format("{0:N4}", Float);
                                buffer = Float.ToString("f4");
                            }
                           
                        }
                        else if (cfgstruct[gridnum, i].datatype == "BOOLEAN8")
                        {
                           // this.dataGridView[gridnum].Rows[i].AccessibilityObject =
                            if (buffer == "0")
                            {
                                buffer = "FALSE";
                            }
                            else
                            {
                                buffer = "TRUE";
                            }
                            
                        }
                        this.dataGridView[gridnum].Rows.Add(buffer.Trim());
                        this.dataGridView[gridnum].Rows[j].ReadOnly = true;
                        this.dataGridView[gridnum].Rows[j].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                        this.dataGridView[gridnum].Rows[j].Cells[0].ReadOnly = false;
                        gridrow = this.dataGridView[gridnum].Rows[i];
                        gridrow.HeaderCell.Value = cfgstruct[gridnum, i].explant;
                        //this.dataGridView[gridnum].Rows[j].Cells[0].ReadOnly = false;
                        j++;
                    }
                    else if (cfgstruct[gridnum, i].dim == "1")
                    {
                        dim_1d = true;
                    }
                    else if (cfgstruct[gridnum, i].dim == "2")
                    {
                        dim_2d = true;
                    }
                }
            }
            
            System.Windows.Forms.DataGridViewCellStyle dataGridViewRow = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewRow.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            if (dim_1d)  // _1d
            {

                //add a NULL column
                colu = new DataGridViewTextBoxColumn();
                colu.Width = 20;
                dataGridView[gridnum].Columns.Add(colu);
                //add a row
                
                // Output ID and name columns
                for (i = 0; i < 200; i++)
                {
                    if (cfgstruct[gridnum, i].name == null)
                    {
                        break;
                    }

                    if (cfgstruct[gridnum, i].dim == "1")
                    {
                        thisReader.Close();
                        thisCommand.CommandText = select_1d;
                        try
                        {
                            thisReader = thisCommand.ExecuteReader();
                        }
                        catch (Exception)
                        {
                            return ;
                        }
                        this.dataGridView[gridnum].Rows.Add(2);
                        this.dataGridView[gridnum].Rows[j].ReadOnly = true;
                        this.dataGridView[gridnum].Rows[j+1].ReadOnly = true;
                        while (thisReader.Read())// 读取符合索引要求得所以得行数。如2行  
                        {
                            // 序号行
                            if (repeit)
                            {
                                colu = new DataGridViewTextBoxColumn();
                                colu.Width = 90;
                                dataGridView[gridnum].Columns.Add(colu);
                            }
                            if (col_num == this.dataGridView[gridnum].Columns.Count)
                            {
                                colu = new DataGridViewTextBoxColumn();
                                colu.Width = 90;
                                dataGridView[gridnum].Columns.Add(colu);
                            } 
                            buffer = thisReader["idx1"].ToString();
                            if (cfgstruct[gridnum, i].idx1 == Convert.ToInt32(buffer.Trim()))
                            {
                                break;
                            }
                            buffer = Convert.ToString(Convert.ToInt32(buffer.Trim()) + 1);
                            this.dataGridView[gridnum].Rows[j].Cells[col_num].Value = buffer;
                            this.dataGridView[gridnum].Rows[j].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;// new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                            this.dataGridView[gridnum].Rows[j].DefaultCellStyle = dataGridViewRow;
                            
                            this.dataGridView[gridnum].Rows[j].Cells[col_num].ReadOnly = false;
                            //参数行
                            buffer = thisReader[cfgstruct[gridnum, i].name].ToString();
                            if ((cfgstruct[gridnum, i].datatype == "SFLOAT") && (buffer != null) && (buffer != ""))
                            {
                                Float = Convert.ToDouble(buffer);
                                //buffer = String.Format("{0:N4}", Float);
                                buffer = Float.ToString("f4");
                            }
                            else if ((cfgstruct[gridnum, i].datatype == "BOOLEAN8") && (buffer != null) && (buffer != ""))
                            {
                                // this.dataGridView[gridnum].Rows[i].AccessibilityObject =
                                if (buffer == "0")
                                {
                                    buffer = "FALSE";
                                }
                                else
                                {
                                    buffer = "TRUE";
                                }
                            }
                            this.dataGridView[gridnum].Rows[j + 1].Cells[col_num].Value = buffer;
                            this.dataGridView[gridnum].Rows[j + 1].Cells[col_num].Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                            this.dataGridView[gridnum].Rows[j + 1].DefaultCellStyle = dataGridViewCellStyle;
                            this.dataGridView[gridnum].Rows[j + 1].Cells[col_num].ReadOnly =false;
                            //
                            col_num++;
                        }
                        this.dataGridView[gridnum].Rows[j].ReadOnly = true;
                        gridrow = this.dataGridView[gridnum].Rows[j + 1];
                        gridrow.HeaderCell.Value = cfgstruct[gridnum, i].explant;
                        j++;
                        j++;
                        col_num = 2;
                        repeit = false;

                    }
                }
            }
            
            repeit = true;
            col_num = this.dataGridView[gridnum].Columns.Count;
            string[] idx1 = new string[200];
            string[] idx2 = new string[100];
            if (dim_2d)  // _2d
            {
                // Output ID and name columns
                for (i = 0; i < 200; i++)
                {
                    if (cfgstruct[gridnum, i].name == null)
                    {
                        break;
                    }

                    if (cfgstruct[gridnum, i].dim == "2")
                    {
                        thisReader.Close();
                        thisCommand.CommandText = select_2d;
                        thisReader = thisCommand.ExecuteReader();
                        this.dataGridView[gridnum].Rows.Add(1);
                        this.dataGridView[gridnum].Rows[j].ReadOnly = true;
                        //   this.dataGridView[gridnum].Rows[j + 1].ReadOnly = true;
                        while (thisReader.Read())// 读取符合索引要求得所以得行数。如2行  
                        {
                            // 序号行
                            if ((repeit) && (num_idx1 > (col_num-1)))
                            {
                                colu = new DataGridViewTextBoxColumn();
                                colu.Width = 90;
                                dataGridView[gridnum].Columns.Add(colu);
                            }
                            buffer = thisReader["idx1"].ToString();

                            // [(thisReader.FieldCount) / 2];
                            buffer = Convert.ToString(Convert.ToInt32(buffer.Trim()) + 1);

                            if (SearchEqual(buffer, idx1, num_idx1))
                            {
                                idx1[num_idx1] = buffer;
                                num_idx1++;

                                this.dataGridView[gridnum].Rows[j].Cells[col_num_id2].Value = buffer;
                                this.dataGridView[gridnum].Rows[j].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;// new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                this.dataGridView[gridnum].Rows[j].DefaultCellStyle = dataGridViewRow;
                                this.dataGridView[gridnum].Rows[j].ReadOnly = true;
                                col_num_id2++;

                            }

                            // 序号列
                            buffer = thisReader["idx2"].ToString();

                            buffer = Convert.ToString(Convert.ToInt32(buffer.Trim()) + 1);
                            if (SearchEqual(buffer, idx2, num_idx2))
                            {
                                idx2[num_idx2] = buffer;
                                this.dataGridView[gridnum].Rows.Add(1);
                                num_idx2++;

                                this.dataGridView[gridnum].Rows[j + num_idx2].Cells[1].Value = buffer;
                                this.dataGridView[gridnum].Rows[j + num_idx2].Cells[1].Style.ForeColor = System.Drawing.Color.Black;// new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                this.dataGridView[gridnum].Rows[j + num_idx2].DefaultCellStyle = dataGridViewRow;
                                this.dataGridView[gridnum].Rows[j + num_idx2].ReadOnly = true;
                                
                            }

                           // 参数行
                            buffer = thisReader[cfgstruct[gridnum, i].name].ToString();
                            if ((cfgstruct[gridnum, i].datatype == "SFLOAT") && (buffer != null) && (buffer != ""))
                            {
                                Float = Convert.ToDouble(buffer);
                                //buffer = String.Format("{0:N4}", Float);
                                buffer = Float.ToString("f4");
                            }
                            else if ((cfgstruct[gridnum, i].datatype == "BOOLEAN8") && (buffer != null) && (buffer != ""))
                            {
                                // this.dataGridView[gridnum].Rows[i].AccessibilityObject =
                                if (buffer == "0")
                                {
                                    buffer = "FALSE";
                                }
                                else
                                {
                                    buffer = "TRUE";
                                }
                            }

                            if (id2_row > num_idx2)
                            {
                                id2_row = 1;
                                id2_col++;
                            }
                            this.dataGridView[gridnum].Rows[j + id2_row].Cells[id2_col].Value = buffer;
                            this.dataGridView[gridnum].Rows[j + id2_row].Cells[id2_col].Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                            this.dataGridView[gridnum].Rows[j + id2_row].DefaultCellStyle = dataGridViewCellStyle;
                            this.dataGridView[gridnum].Rows[j + id2_row].Cells[id2_col].ReadOnly = false;
                            id2_row++;
                           
                        }
                       // this.dataGridView[gridnum].Rows[j+1].ReadOnly = true;
                        gridrow = this.dataGridView[gridnum].Rows[j + 1];
                        gridrow.HeaderCell.Value = cfgstruct[gridnum, i].explant;

                        j=j + num_idx2;
                        col_num_id2 = 2;
                        repeit = false;

                    }
                }
            }

            // key setup
            for (i = 0; i < 5; i++)
            {
                if ((cfgstruct[gridnum, i].key == 1)&&(this.dataGridView[gridnum].Rows.Count>0))
                {
                    this.dataGridView[gridnum].Rows[i].ReadOnly = true;
                    this.dataGridView[gridnum].Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.GreenYellow;
                }
            }
            //close reader and connection
            thisReader.Close();
            thisConnection.Close();
            tablestruct[gridnum].live = true;
        }

        private bool SearchEqual(string buffer,string [] idx1,int num_idx1)
        {
            for (int i = 0; i < num_idx1; i++)
            {
                if (idx1[i] == buffer)
                {
                    
                    return false;
                }
            }
            return true;
        }
        private void RemoveRowColum(int numm)
        {
            int i;
            for (i = 0; i < dataGridView[numm].Rows.Count ; i++)
            {
                dataGridView[numm].Rows.Remove(dataGridView[numm].Rows[i]);
                
            }
            for (i = 0; i < dataGridView[numm].Columns.Count; i++)
            {
                dataGridView[numm].Columns.Remove(dataGridView[numm].Columns[i]);
                
            }
            dataGridView[numm].Rows.Clear();
            dataGridView[numm].Columns.Clear();
        }
        private int SearchGridNum(string buf)  //找到当前所选择的tabpage对应的顺序号；
        {
            int i=0;
            for (i = 0; i < 50; i++)
            {
                if (this.tabPage[i] == null)
                {
                    MessageBox.Show("can't find this tab from ModTbls config");
                    return 51;
                }
                else if (this.tabPage[i].Name == buf)
                    break;
            }
            return i;
        }
        private void AddToList()
        {
            string tab_name = tabControl1.SelectedTab.Name;
            int grid_num = SearchGridNum(tab_name);

            int column = dataGridView[grid_num].CurrentCell.ColumnIndex;
            int row = dataGridView[grid_num].CurrentCell.RowIndex;
            this.dataGridView[grid_num].Rows[row].Cells[column].Style.ForeColor = System.Drawing.Color.Red;
            this.dataGridView[grid_num].Rows[row].Cells[column].Style.BackColor = System.Drawing.Color.White;
            if (!this.list[grid_num].CheckRepeat(row, column))
            {
                list[grid_num].Append(row, column);
            }
        }
        private int SearchNum(string tbn)
        {
            string select = "SELECT * FROM " + tbn+"_1d";
            string buffer;
            string [] idx1 =new string [500];
            int num=0;
            SqlConnection thisConnection = new SqlConnection(strConnection);
            try
            {
                thisConnection.Open();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
            SqlCommand thisCommand = thisConnection.CreateCommand();
            thisCommand.CommandText = select;
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            while (thisReader.Read())
            {
                buffer = thisReader["idx1"].ToString();
                if(SearchEqual(buffer,idx1,num))
                {
                   idx1[num] =buffer;
                    num++;
                }
            }
            return num;
        }
        private int SearchNum2(string tbn)
        {
            string select = "SELECT * FROM " + tbn + "_2d";
            string buffer;
            string[] idx2 = new string[500];
            int num = 0;
            SqlConnection thisConnection = new SqlConnection(strConnection);
            try
            {
                thisConnection.Open();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
            SqlCommand thisCommand = thisConnection.CreateCommand();
            thisCommand.CommandText = select;
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            while (thisReader.Read())
            {
                buffer = thisReader["idx2"].ToString();
                if (SearchEqual(buffer, idx2, num))
                {
                    idx2[num] = buffer;
                    num++;
                }
            }
            return num;
        }
        private void Initial_table()
        {
            string tab_name = tabControl1.SelectedTab.Name;
            int grid_num = SearchGridNum(tab_name);
            int key_num = tablestruct[grid_num].key_num;
            string[] key = new string[key_num];
            int idx_num = tablestruct[grid_num].idx1;
            int idx_num2 = tablestruct[grid_num].idx2;
            string insert;
            string insert_exert;
            string insert_exert_1d;
            string insert_exert_0d;
            int i, n, j, m, p, q;
            string media_str;

            for (i = 0; i < key_num; i++)
            {
                key[i] = cfgstruct[grid_num, i].name;
            }

            Initialize_table dlg = new Initialize_table(tab_name, key_num, key, strConnection);
            dlg.ShowDialog();
            if (!dlg.ok)
            {
                return;
            }
            
            SqlConnection thisConnection = new SqlConnection(strConnection);
            try
            {
                thisConnection.Open();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
            SqlCommand thisCommand = thisConnection.CreateCommand();

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


                    insert_exert = insert + dlg.text1_content + "'";
                    for (n = 0; n < dlg.listbox1_num; n++)
                    {
                        for (m = 0; m < dlg.listbox2_num; m++)
                        {

                            insert_exert_0d = insert_exert + ",'" + dlg.listbox1_content[n] + "'";

                            insert_exert_0d = insert_exert_0d + ",'" + dlg.listbox2_content[m] + "'";


                            for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                            {
                                if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                                {
                                    media_str = Convert.ToString(System.DateTime.Now);
                                }
                                else
                                {
                                    media_str = cfgstruct[grid_num, j].default_value;
                                }
                                if (cfgstruct[grid_num, j].dim == "0")
                                {
                                    insert_exert_0d = insert_exert_0d + ",'" + media_str + "'";
                                }
                            }

                            insert_exert_0d = insert_exert_0d + " ) ";


                            thisCommand.CommandText = insert_exert_0d;
                            thisCommand.ExecuteNonQuery();
                        }
                        if (m == 0)
                        {
                            insert_exert_0d = insert_exert + ",'" + dlg.listbox1_content[n] + "'";

                            for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                            {
                                if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                                {
                                    media_str = Convert.ToString(System.DateTime.Now);
                                }
                                else
                                {
                                    media_str = cfgstruct[grid_num, j].default_value;
                                }
                                if (cfgstruct[grid_num, j].dim == "0")
                                {
                                    insert_exert_0d = insert_exert_0d + ",'" + media_str + "'";
                                }
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
                                media_str = Convert.ToString(System.DateTime.Now);
                            }
                            else
                            {
                                media_str = cfgstruct[grid_num, j].default_value;
                            }
                            if (cfgstruct[grid_num, j].dim == "0")
                                insert_exert = insert_exert + ",'" + media_str + "'";
                        }

                        insert_exert = insert_exert + " ) ";


                        thisCommand.CommandText = insert_exert;
                        try
                        {
                            thisCommand.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Please change another name!");
                            return;
                        }
                    }
                }
                else if (i == 1)
                {
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

                    insert_exert = insert + dlg.text1_content + "'";


                    for (n = 0; n < dlg.listbox1_num; n++)
                    {
                        for (p = 0; p < dlg.listbox2_num; p++)
                        {
                            for (m = 0; m < idx_num; m++)
                            {

                                insert_exert_1d = insert_exert + ",'" + dlg.listbox1_content[n] + "'";
                                insert_exert_1d = insert_exert_1d + ",'" + dlg.listbox2_content[p] + "'";

                                insert_exert_1d = insert_exert_1d + ",'" + m.ToString() + "'";
                                for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                                {
                                    if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                                    {
                                        media_str = Convert.ToString(System.DateTime.Now);
                                    }
                                    else
                                    {
                                        media_str = cfgstruct[grid_num, j].default_value;
                                    }
                                    if (cfgstruct[grid_num, j].dim == "1")
                                        insert_exert_1d = insert_exert_1d + ",'" + media_str + "'";
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
                                insert_exert_1d = insert_exert + ",'" + dlg.listbox1_content[n] + "'";
                                insert_exert_1d = insert_exert_1d + ",'" + m.ToString() + "'";
                                for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                                {
                                    if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                                    {
                                        media_str = Convert.ToString(System.DateTime.Now);
                                    }
                                    else
                                    {
                                        media_str = cfgstruct[grid_num, j].default_value;
                                    }
                                    if (cfgstruct[grid_num, j].dim == "1")
                                        insert_exert_1d = insert_exert_1d + ",'" + media_str + "'";
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
                                    media_str = Convert.ToString(System.DateTime.Now);
                                }
                                else
                                {
                                    media_str = cfgstruct[grid_num, j].default_value;
                                }
                                if (cfgstruct[grid_num, j].dim == "1")
                                    insert_exert_1d = insert_exert_1d + ",'" + media_str + "'";
                            }

                            insert_exert_1d = insert_exert_1d + " ) ";


                            thisCommand.CommandText = insert_exert_1d;
                            thisCommand.ExecuteNonQuery();
                        }

                    }

                }
                if (i == 2)
                {
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

                    insert_exert = insert + dlg.text1_content + "'";

                    for (n = 0; n < dlg.listbox1_num; n++)
                    {
                        for (p = 0; p < dlg.listbox2_num; p++)
                        {
                            for (m = 0; m < idx_num; m++)
                            {
                                for (q = 0; q < idx_num2; q++)
                                {
                                    insert_exert_1d = insert_exert + ",'" + dlg.listbox1_content[n] + "'";
                                    insert_exert_1d = insert_exert_1d + ",'" + dlg.listbox2_content[p] + "'";
                                    insert_exert_1d = insert_exert_1d + ",'" + m.ToString() + "'";

                                    insert_exert_1d = insert_exert_1d + ",'" + q.ToString() + "'";
                                    for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                                    {
                                        if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                                        {
                                            media_str = Convert.ToString(System.DateTime.Now);
                                        }
                                        else
                                        {
                                            media_str = cfgstruct[grid_num, j].default_value;
                                        }
                                        if (cfgstruct[grid_num, j].dim == "2")
                                            insert_exert_1d = insert_exert_1d + ",'" + media_str + "'";
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
                                    insert_exert_1d = insert_exert + ",'" + dlg.listbox1_content[n] + "'";                                    
                                    insert_exert_1d = insert_exert_1d + ",'" + m.ToString() + "'";

                                    insert_exert_1d = insert_exert_1d + ",'" + q.ToString() + "'";

                                    for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                                    {
                                        if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                                        {
                                            media_str = Convert.ToString(System.DateTime.Now);
                                        }
                                        else
                                        {
                                            media_str = cfgstruct[grid_num, j].default_value;
                                        }
                                        if (cfgstruct[grid_num, j].dim == "2")
                                        {
                                            insert_exert_1d = insert_exert_1d + ",'" + media_str + "'";
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
                                insert_exert_1d = insert_exert + ",'" + m.ToString() + "'";
                                insert_exert_1d = insert_exert_1d + ",'" + q.ToString() + "'";

                                for (j = tablestruct[grid_num].key_num; j < tablestruct[grid_num].number; j++)
                                {
                                    if (cfgstruct[grid_num, j].datatype == "GE_TIME")
                                    {
                                        media_str = Convert.ToString(System.DateTime.Now);
                                    }
                                    else
                                    {
                                        media_str = cfgstruct[grid_num, j].default_value;
                                    }
                                    if (cfgstruct[grid_num, j].dim == "2")
                                        insert_exert_1d = insert_exert_1d + ",'" + media_str + "'";
                                }

                                insert_exert_1d = insert_exert_1d + " ) ";


                                thisCommand.CommandText = insert_exert_1d;

                                thisCommand.ExecuteNonQuery();
                            }
                        }

                    }

                }

            }//for(dim)

        }
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printTableOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printEntireScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveDBRecordInFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveDBRecordInFileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadDBRecordFromFileToolStripMenuItem;
        private ComboBox cmb_cell;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem1;
        private ToolStripMenuItem saveToolStripMenuItem1;
        private ToolStripMenuItem exitToolStripMenuItem1;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem printTableOnlyToolStripMenuItem1;
        private ToolStripMenuItem saveDBRecordsInFileToolStripMenuItem;
        private ToolStripMenuItem loadDBRecordsFromFileToolStripMenuItem;
        private Label label3;
        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog openFileDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private PageSetupDialog pageSetupDialog1;
        private PrintPreviewDialog printPreviewDialog1;
        private Label label4;

    }
    ////list
    public class ListNode
    {
        public ListNode(int x, int y)
        {
            Valuex = x;
            Valuey = y;

        }

        public ListNode Previous;// 前一个
        public ListNode Next;// 后一个
        public int Valuex;// 值
        public int Valuey;// 值
    }

    public class Clist
    {
        public Clist()
        {
            //构造函数
            //初始化
            ListCountValue = 0;
            Head = null;
            Tail = null;
        }

        private ListNode Head;// 头指针
        private ListNode Tail;// 尾指针
        private ListNode Current;// 当前指针
        private int ListCountValue;// 链表数据的个数

        /// <summary>
        /// 尾部添加数据
        /// </summary>
        public void Append(int DataValueX, int DataValueY)
        {
            ListNode NewNode = new ListNode(DataValueX, DataValueY);

            if (IsNull()) //如果头指针为空
            {
                Head = NewNode;
                Tail = NewNode;
            }
            else
            {
                Tail.Next = NewNode;
                NewNode.Previous = Tail;
                Tail = NewNode;
            }
            Current = NewNode;
            ListCountValue += 1;//链表数据个数加一
        }

        /// <summary>
        /// 删除列表
        /// </summary>
        public void Destroy()
        {
            ListCountValue = 0;
            Head = null;
            Tail = null;
        }

        /// <summary>
        /// 删除当前的数据
        /// </summary>
        public void Delete()
        {
            if (!IsNull())//若为空链表
            {
                if (IsBof())//若删除头
                {
                    Head = Current.Next;
                    Current = Head;
                    ListCountValue -= 1;
                    return;
                }
                if (IsEof())//若删除尾
                {
                    Tail = Current.Previous;
                    Current = Tail;
                    ListCountValue -= 1;
                    return;
                }
                //若删除中间数据
                Current.Previous.Next = Current.Next;
                Current = Current.Previous;
                ListCountValue -= 1;

                return;
            }
        }

        /// <summary>
        /// 向后移动一个数据
        /// </summary>
        public void MoveNext()
        {
            if (!IsEof())
            {
                Current = Current.Next;
            }
        }

        /// <summary>
        /// 向前移动一个数据
        /// </summary>
        public void MovePrevious()
        {
            if (!IsBof())
            {
                Current = Current.Previous;
            }
        }

        /// <summary>
        /// 移动到第一个数据
        /// </summary>
        public void MoveFrist()
        {
            Current = Head;
        }

        /// <summary>
        /// 移动到最后一个数据
        /// </summary>
        public void MoveLast()
        {
            Current = Tail;
        }

        /// <summary>
        /// 判断是否为空链表
        /// </summary>
        public bool IsNull()
        {
            if (ListCountValue == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否为到达尾部
        /// </summary>
        public bool IsEof()
        {
            if (Current == Tail)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否为到达头部
        /// </summary>
        public bool IsBof()
        {
            if (Current == Head)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获得当前值Valuex
        /// </summary>
        public int GetCurrentValueX()
        {
            return Current.Valuex;
        }
        /// <summary>
        /// 获得当前值Valuey
        /// </summary>
        public int GetCurrentValueY()
        {
            return Current.Valuey;
        }

        /// <summary>
        /// 取得链表的数据个数
        /// </summary>
        public int ListCount
        {
            get
            {
                return ListCountValue;
            }
        }

        /// <summary>
        /// 清空链表
        /// </summary>
        public void Clear()
        {
            MoveFrist();
            while (!IsNull())
            {
                Delete();//若不为空链表,从尾部删除
            }
        }

        /// <summary>
        /// 在当前位置前插入数据
        /// </summary>
        public void Insert(int DataValueX, int DataValueY)
        {
            ListNode NewNode = new ListNode(DataValueX, DataValueY);
            if (IsNull())
            {
                Append(DataValueX, DataValueY);//为空表，则添加
                return;
            }

            if (IsBof())
            {
                //为头部插入
                NewNode.Next = Head;
                Head.Previous = NewNode;
                Head = NewNode;
                Current = Head;
                ListCountValue += 1;

                return;
            }

            //中间插入
            NewNode.Next = Current;
            NewNode.Previous = Current.Previous;
            Current.Previous.Next = NewNode;
            Current.Previous = NewNode;
            Current = NewNode;
            ListCountValue += 1;

        }
        /// <summary>
        /// 检查是否有重复的数据
        /// </summary>
        public bool CheckRepeat(int DataValueX, int DataValueY)
        {
            if (IsNull())
            {
                return false;
            }
            else
            {
                Current = Head;
                while (true)
                {
                    if ((Current.Valuex == DataValueX) && (Current.Valuey == DataValueY))
                    {
                        return true;
                    }
                    if (IsEof())
                    {
                        break;
                    }
                    MoveNext();

                }
            }
            return false;
        }

    }


}

