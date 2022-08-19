using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MURANI_GYM
{
    public partial class frmDashboard : Form
    {
        private int hr, min, sec;
        public frmDashboard()
        {
            InitializeComponent();
            hr = DateTime.UtcNow.Hour;
            min = DateTime.UtcNow.Minute;
            sec = DateTime.UtcNow.Second;
            Timer.Enabled = true;
            
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            frmInsertRecords frmInsertRecords = new frmInsertRecords();
            frmInsertRecords.ShowDialog();
        }

        private void btnViewRecords_Click(object sender, EventArgs e)
        {
            frmViewRecords frmViewRecords = new frmViewRecords();
            frmViewRecords.ShowDialog();
        }

        private void btnInstructor_Click(object sender, EventArgs e)
        {
            //frmGymInstructor frmGymInstructor = new frmGymInstructor();
            //frmGymInstructor.ShowDialog();
        }

        private void btnViewTrends_Click(object sender, EventArgs e)
        {
            frmViewTrends frmViewTrends = new frmViewTrends();
            frmViewTrends.ShowDialog();
        }

        private void Timer_Tick_1(object sender, EventArgs e)
        {
            DateTime todaysDate = DateTime.Today;
            string variable = todaysDate.ToString();
            variable = variable.Truncate(10);
            //lblDate.Text = todaysDate.ToString();
            lblDate.Text = variable;
            //DateTime currentTime = DateTime.Now.ToString("h:mm:ss tt");
            //lblTime.Text = currentTime.ToString();

            DayOfWeek currentDay = DateTime.Today.DayOfWeek;
            lblDay.Text = currentDay.ToString();
            hr = DateTime.UtcNow.Hour;
            hr = hr + 3;
            min = DateTime.UtcNow.Minute;
            sec = DateTime.UtcNow.Second;

            if (hr > 12)
                hr -= 12;

            if (sec % 2 == 0)
            {
                lblTime.Text = +hr + ":" + min + ":" + sec;
            }
            else
            {
                lblTime.Text = hr + ":" + min + ":" + sec;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
           
        }
    }
    public static class StringExt
    {
        public static string Truncate(this string variable, int Length)
        {
            if (string.IsNullOrEmpty(variable)) return variable;
            return variable.Length <= Length ? variable : variable.Substring(0, Length);
        }
    }
}
