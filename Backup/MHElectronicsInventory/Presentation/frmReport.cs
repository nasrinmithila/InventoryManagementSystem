using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MHElectronicsInventory.Presentation
{
    public partial class frmReport : Form
    {
        public frmReport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report.CrReport rpt = new Report.CrReport();
            DateTime d1 = Convert.ToDateTime(dateTimePicker1.Text);
            DateTime d2 = Convert.ToDateTime(dateTimePicker2.Text);
            rpt.SetParameterValue("@startdate", d1);
            rpt.SetParameterValue("@enddate", d2);
            crystalReportViewer1.ReportSource = rpt;
        }
    }
}
