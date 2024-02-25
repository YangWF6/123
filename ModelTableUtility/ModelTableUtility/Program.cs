using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//using Oracle.ManagedDataAccess.Client;
using System.IO;
using System.Data.SqlClient;
using CSPComm;

using System.Diagnostics;
public static class conn
{
    public static string strconn = null;
    //public const String strconn = "Provider=OraOLEDB.Oracle.1;Password=MODTBL;Persist Security Info=True;User ID=MODTBL;Data Source=DBMC";MODEL
    //public const String FilePath = "D:\\maint\\ModelTableUtility\\config\\"; 20240223 Modified by Yang
    public const String FilePath = "D:\\maintnasia\\ModelTableUtility\\config\\";
    public static string[] IP_Port = new string[4];
    public static string Port = null;
    
}
public static class contant
{ 
    public static int MaxSize = 200;
    public static string DimIdx1 = "1";
    public static string DimIdx2 = "2";
    public static string DimIdx0 = "0";
    public static int s_idx_num = 0;
    public static int s_idx_num_2d = 0;
}

public static class gl
{
    public static string tabname = "smainframe";
    public static string win_shut = "open";
    public static bool ReFrash=false;
    public static int scount = 1;
    public static string subtabname = "";
    public static string []TempName = new string[5];
    public static string[] Gradeidx = new string[350];
    public static string[] familyidx = new string[20];
    public static string[] widthidx = new string[20];
    public static string[] spyidx = new string[20];
    public static string[] thickidx = new string[20];
    public static int[] TData_Same = new int[30];
}
namespace ModelTableUtility
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static void condatabase(string connstr)
        {
            SqlConnection thisConnection = new SqlConnection(conn.strconn);
            SqlCommand thisCommand = thisConnection.CreateCommand();
            SqlTransaction Trn;
            Trn = thisConnection.BeginTransaction();

            thisCommand.Connection = thisConnection;
            thisCommand.Transaction = Trn;
        }
        static void Main()
        {
            Process[] app = Process.GetProcessesByName("ModelTableUtility");

            if (app.Length > 1)
            {
                app[0].Kill();
                //MessageBox.Show("请关闭已经启动的程序后再进行尝试");
                return;
            }
            else
            {
                //System.Diagnostics.Process.Start("AMS.exe");
            }

            StreamReader Dat;
            string filename;
            int i = 0;
            int length=0;
            filename = conn.FilePath + "configmodeltable.dat";
            string buffer;
            try
            {
                Dat = File.OpenText(filename);
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                return;
            }
            buffer = Dat.ReadLine();
            conn.strconn = buffer.ToString().Trim();
            
            while (buffer != null)
            {
                buffer = Dat.ReadLine();
                if (buffer != null)
                {
                    buffer.Trim();
                    //length = buffer.IndexOf(":");
                    //conn.IP_Port[i] = buffer.Substring(length + 1, buffer.Length - length-1).Trim();
                    // i++;
                    conn.IP_Port = buffer.Split(' ');
                }
            }
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);    
            //Application.Run(new Mainframe());//改变入口对话框
            Application.Run(  new Select_Data_Source(conn.strconn));
            
        }
    }
}
