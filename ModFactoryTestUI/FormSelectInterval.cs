using ModFactoryTestCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModFactoryTestUI
{
    public partial class FormSelectInterval : Form
    {
        TestCoreController tc;
        private System.Resources.ResourceManager rm;
        private FormPrincipal formPrincipal;
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public FormSelectInterval(FormPrincipal formPrincipal, System.Resources.ResourceManager rm)
        {
            InitializeComponent();
            mskTxtBoxInitialDate.Text = monthCalendarStartDate.TodayDate.ToShortDateString();
            mskTxtBoxFinalDate.Text = monthCalendarStartDate.TodayDate.ToShortDateString();
            this.rm = rm;
            this.formPrincipal = formPrincipal;

            this.startDate = monthCalendarStartDate.SelectionStart;
            this.endDate = new DateTime(
                monthCalendarEndDate.SelectionStart.Year,
                monthCalendarEndDate.SelectionStart.Month,
                monthCalendarEndDate.SelectionStart.Day,
                23, 59, 59);
            
        }

        private void monthCalendarStartDate_DateSelected(object sender, DateRangeEventArgs e)
        {
            mskTxtBoxInitialDate.Text = e.Start.ToShortDateString();
            startDate = new DateTime(e.Start.Year, e.Start.Month, e.Start.Day, 0, 0, 0);
        }

        private void monthCalendarEndDate_DateChanged(object sender, DateRangeEventArgs e)
        {
            mskTxtBoxFinalDate.Text = e.Start.ToShortDateString();
            endDate = new DateTime(e.Start.Year, e.Start.Month, e.Start.Day, 23, 59, 59);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (startDate > endDate)
            {
                txtBoxMessages.Text = rm.GetString("uiInvalidDateInterval");
                return;
            }

            formPrincipal.startDate = this.startDate;
            formPrincipal.endDate = this.endDate;
            this.Close();
        }

    }
}
