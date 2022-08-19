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

namespace MURANI_GYM
{
    public partial class frmViewRecords : Form
    {
        public frmViewRecords()
        {
            InitializeComponent();
            OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = GYMDB.accdb; Persist Security Info = False; ");
            string newSql = "SELECT * FROM PAYMENTS";
            OleDbDataAdapter myCmd = new OleDbDataAdapter(newSql, connection);

            DataSet daSet = new DataSet();
            myCmd.Fill(daSet, "Payments");
            DataTable dt = daSet.Tables[0];
            dgvRecordView.DataSource = daSet.Tables["Payments"];
            connection.Close();

        }
    }
}
