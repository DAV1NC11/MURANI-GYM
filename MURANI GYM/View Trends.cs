using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MURANI_GYM
{
    public partial class frmViewTrends : Form
    {
        
        public frmViewTrends()
        {
            InitializeComponent();
        }

        private void frmViewTrends_Load(object sender, EventArgs e)
        {
            OleDbConnection connectiondb = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=GYMDB.accdb; Persist Security Info = False; ");
            OleDbCommand com3;
            string str3;
            connectiondb.Open();
            str3 = "select top 7 Dates,Earnings from Records";
            com3 = new OleDbCommand(str3, connectiondb);
            OleDbDataAdapter da = new OleDbDataAdapter(com3);
            DataTable dt = new DataTable();
            da.Fill(dt);
            connectiondb.Close();
            string[] N = new string[dt.Rows.Count];
            int[] M = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                N[i] = dt.Rows[i][0].ToString();
                M[i] = Convert.ToInt32(dt.Rows[i][1]);
            }
            chrtTrends.Series[0].Points.DataBindXY(N, M);
        }
    }
}
