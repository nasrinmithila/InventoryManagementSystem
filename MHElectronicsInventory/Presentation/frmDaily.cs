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
    public partial class frmDaily : Form
    {
        public frmDaily()
        {
            InitializeComponent();
        }
        int count;
        private void Dataload()
        {
            Dal.dalNumber ct = new MHElectronicsInventory.Dal.dalNumber();
            DataSet ds = new DataSet();
            ds = ct.SelectAll();
            Model_name.DataSource = ds.Tables[0].DefaultView;
            Model_name.DisplayMember = "name";
            Model_name.ValueMember = "ID";
        }
        private void frmDaily_Load(object sender, EventArgs e)
        {
            Dataload();
            count = 0;
            Model_name.SelectedIndex = -1;
            Price.Text = "";
        }   
        private void Model_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dal.dalDaily ct = new MHElectronicsInventory.Dal.dalDaily();
            try
            {
                ct.M_ID = Convert.ToInt32(Model_name.SelectedValue);
            }
            catch(Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
            DataSet ds = new DataSet();
            ds = ct.SelectIMEI();
            IMEI.DataSource = ds.Tables[0].DefaultView;
            IMEI.DisplayMember = "IMEI";
            DataSet d = new DataSet();
            d = ct.SelectST();
            ST.DataSource = d.Tables[0].DefaultView;
            ST.DisplayMember = "ST";
            Price.Text = "";
        }

        private void stockToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Presentation.frmStock m = new MHElectronicsInventory.Presentation.frmStock();
            m.ShowDialog();
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Date.Text != "" && Model_name.Text != "" && Price.Text != "" && IMEI.Text!="" && ST.Text!="")
            {
                Dal.dalMemo ct = new MHElectronicsInventory.Dal.dalMemo();
                ct.Date = Convert.ToDateTime(Date.Text);
                try
                {
                    ct.M_ID = Convert.ToInt32(Model_name.SelectedValue);
                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message);
                }
                ct.IMEI = IMEI.Text;
                ct.ST = ST.Text;
                ct.Price = Convert.ToDouble(Price.Text);
                int i = 1;
                if (i == ct.insert())
                {
                     Dal.dalNumber n = new MHElectronicsInventory.Dal.dalNumber();
                     n.M_ID = ct.M_ID;
                     int num=n.getnumber();
                     n.Number = num - 1;
                     if (n.update()!=-1)
                     {
                         
                     }
                     else
                     {
                         MessageBox.Show(n.Error);
                     }
                     Dal.dalModel d = new MHElectronicsInventory.Dal.dalModel();
                     d.ID = ct.M_ID;
                     String name = d.getname();
                     dataGridView1.Rows.Add(1);
                     dataGridView1.Rows[count].Cells["day"].Value = ct.Date;
                     dataGridView1.Rows[count].Cells["name"].Value = name;
                     dataGridView1.Rows[count].Cells["im"].Value = ct.IMEI;
                     dataGridView1.Rows[count].Cells["M_ID"].Value = ct.M_ID;
                     dataGridView1.Rows[count].Cells["stno"].Value = ct.ST;
                     dataGridView1.Rows[count].Cells["pri"].Value = ct.Price;
                     count++;
                     MessageBox.Show("Successfully inserted");
                     DialogResult dialogResult = MessageBox.Show("Want to print?", "Successfully Inserted", MessageBoxButtons.YesNo);
                     if (dialogResult == DialogResult.Yes)
                     {
                         Report.CrMemo rpt = new Report.CrMemo();
                         int id = ct.SelectMaxID();
                         Double vat=0.00;
                         rpt.SetParameterValue("@invoice", id);
                         rpt.SetParameterValue("@quantity", 1);
                         rpt.SetParameterValue("@model_name", name);
                         rpt.SetParameterValue("@price", ct.Price);
                         rpt.SetParameterValue("@ST", ct.ST);
                         rpt.SetParameterValue("@IMEI", ct.IMEI);
                         rpt.SetParameterValue("@Date", ct.Date);
                         rpt.SetParameterValue("@total", ct.Price);
                         DialogResult Result = MessageBox.Show("Use card to pay?", "Card Using", MessageBoxButtons.YesNo);
                         if (Result == DialogResult.Yes)
                         {
                             vat = (ct.Price * 2) / 100; 
                         }
                         rpt.SetParameterValue("@vat", vat);
                         rpt.SetParameterValue("@grand_total", ct.Price+vat);
                         this.crystalReportViewer1.ReportSource = rpt;
                            
                     }
                     else if (dialogResult == DialogResult.No)
                     {
                         //do something else
                     }
                }
                else
                {
                     MessageBox.Show(ct.Error);
                }
            }
            Model_name.SelectedIndex=-1;
            Price.Text = "";
            IMEI.Text = "";
            ST.Text = "";
            Dataload();
        }

        private void reportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Presentation.frmReport m = new MHElectronicsInventory.Presentation.frmReport();
            m.ShowDialog();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Model_name_Click(object sender, EventArgs e)
        {
            Dataload();
        }

        private void ST_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dal.dalDaily ct = new MHElectronicsInventory.Dal.dalDaily();
            ct.ST=ST.Text;
            Price.Text=Convert.ToString(ct.selectprice());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Presentation.frmUpdatedStock fm = new frmUpdatedStock();
            fm.ShowDialog();
        }

        private void transactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presentation.frmTransaction t = new frmTransaction();
            t.ShowDialog();
        }

      

    }
}
