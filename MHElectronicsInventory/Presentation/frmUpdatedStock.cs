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
    public partial class frmUpdatedStock : Form
    {
        public frmUpdatedStock()
        {
            InitializeComponent();
        }

        private void frmUpdatedStock_Load(object sender, EventArgs e)
        {
            Report.CrUpdatedStock rpt=new MHElectronicsInventory.Report.CrUpdatedStock();
            crystalReportViewer1.ReportSource = rpt;
        }
    }
}
