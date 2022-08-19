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
    public partial class frmInsertRecords : Form
    {
        DataTable dt = new DataTable();
        DataRow dr;
        public frmInsertRecords()
        {
            InitializeComponent();
            dt.Columns.Add("Name");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Session");
            dt.Columns.Add("Duration");

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = GYMDB.accdb; Persist Security Info = False; ");
                OleDbCommand command = new OleDbCommand("", connection);
                int insPay = Convert.ToInt32(txtInstructorPay.Text);
                int totalCollected = 0;
                int totalEarnings = 0;
                foreach (DataGridViewRow r in dgvRecords.Rows)
                {
                    totalCollected += Convert.ToInt32(r.Cells[1].Value);
                }
                totalEarnings = totalCollected - insPay;
                var dateTime = DateTime.Now;
                var myDate = dateTime.ToShortDateString();

                command.Parameters.AddWithValue("@Collected", totalCollected);
                command.Parameters.AddWithValue("@Earnings", totalEarnings);
                command.Parameters.AddWithValue("@Instructor_Pay", insPay);
                command.Parameters.AddWithValue("@Dates", myDate);
                

                command.CommandText = "INSERT INTO Records VALUES (@Collected, @Earnings, @Instructor_Pay, @Dates)";

                connection.Open();
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                connection.Close();

                for (int s = 0; s < dgvRecords.Rows.Count - 1; s++)
                {
                    command.Parameters.AddWithValue("@Name", dgvRecords.Rows[s].Cells[0].Value);
                    command.Parameters.AddWithValue("@Amount", dgvRecords.Rows[s].Cells[1].Value);
                    command.Parameters.AddWithValue("@Session", dgvRecords.Rows[s].Cells[2].Value);
                    command.Parameters.AddWithValue("@Duration", dgvRecords.Rows[s].Cells[3].Value);
                    command.Parameters.AddWithValue("@Datez", myDate);

                    command.CommandText = "INSERT INTO Payments VALUES (@Name, @Amount, @Session, @Duration, @Datez)";

                    connection.Open();

                    //this line is not needed as it is set in the command constructor above
                    //command.Connection = connection; 

                    command.ExecuteNonQuery();
                    

                    connection.Close();

                    //edit - this needs to run or you will have duplicate values inserted
                    command.Parameters.Clear();
                    
                }
                MessageBox.Show("Data Saved Successfully");
                dt.Rows.Clear();
                txtInstructorPay.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error   " + ex.Message);
            }
        }

        private void btnAddRecords_Click(object sender, EventArgs e)
        {
            dr = dt.NewRow();
            dr["Name"] = txtName.Text;
            dr["Amount"] = txtAmount.Text;
            dr["Session"] = cmbSession.Text;
            dr["Duration"] = cmbDuration.Text;
            dt.Rows.Add(dr);
            dgvRecords.DataSource = dt;
            txtAmount.Text = "";
            cmbSession.Text = "";
            txtName.Text = "";
            cmbDuration.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
