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
    public partial class frmStock : Form
    {
        public int count;
        public frmStock()
        {
            InitializeComponent();
        }
        private void frmDailyInsert_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mHElectronicsDataSet5.Model' table. You can move, or remove it, as needed.
            this.modelTableAdapter.Fill(this.mHElectronicsDataSet5.Model);
            count = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(Date.Text!="" && ModelName.Text!="" && IMEI.Text!="" && ST.Text!="" && Price.Text!="" && Profit.Text!="")
            {
                Dal.dalDaily ct = new MHElectronicsInventory.Dal.dalDaily();
                ct.Date = Convert.ToDateTime(Date.Text);
                ct.IMEI = IMEI.Text;
                try
                {
                    ct.M_ID = Convert.ToInt32(ModelName.SelectedValue);
                }
                catch(Exception ex)
                {
                }
                ct.Price = Convert.ToDouble(Price.Text);
                ct.Profit = Convert.ToDouble(Profit.Text);
                ct.ST = ST.Text;
                int i = 1;
                if (i == ct.insert())
                {
                    MessageBox.Show("Successfully Inserted");
                    Dal.dalNumber n = new MHElectronicsInventory.Dal.dalNumber();
                    n.M_ID = ct.M_ID;
                    int num = n.getnumber();
                    n.Number = num+1;
                    if (n.update() != -1)
                    {
                    }
                    else
                    {
                        MessageBox.Show(n.Error);
                    }
                    Dal.dalModel dm = new MHElectronicsInventory.Dal.dalModel();
                    dm.ID = ct.M_ID;
                    dataGridView1.Rows.Add(1);
                    dataGridView1.Rows[count].Cells["day"].Value = ct.Date;
                    dataGridView1.Rows[count].Cells["name"].Value = dm.getname();
                    dataGridView1.Rows[count].Cells["im"].Value = ct.IMEI;
                    dataGridView1.Rows[count].Cells["stno"].Value = ct.ST;
                    dataGridView1.Rows[count].Cells["pri"].Value = ct.Price;
                    dataGridView1.Rows[count].Cells["prof"].Value = ct.Profit;
                    dataGridView1.Rows[count].Cells["M_ID"].Value = ct.M_ID;
                    count++;
                }
                else
                {
                    MessageBox.Show(ct.Error);
                }
            }
            ModelName.SelectedIndex = -1;
            IMEI.Text = "";
            ST.Text = "";
            Price.Text = "";
            Profit.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Model_name.Text != "")
            {
                Dal.dalModel ct = new MHElectronicsInventory.Dal.dalModel();
                ct.Model_name = Model_name.Text;
                int i = 1;
                if (i == ct.insert())
                {
                    Dal.dalNumber n = new MHElectronicsInventory.Dal.dalNumber();
                    n.M_ID = ct.getID();
                    n.Number = 0;
                    if (n.insert() != -1)
                    {
                    }
                    else
                    {
                        MessageBox.Show(n.Error);
                    }
                    MessageBox.Show("Successfully inserted");
                }
                else
                {
                    MessageBox.Show(ct.Error);
                }
            }
            Model_name.Text = "";
            this.modelTableAdapter.Fill(this.mHElectronicsDataSet5.Model);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dal.dalModel dm = new MHElectronicsInventory.Dal.dalModel();
            try
            {
                dm.ID = Convert.ToInt32(listBox1.SelectedValue);
            }
            catch(Exception)
            {
            }
            Model_name.Text = dm.getname();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Model_name.Text != "")
            {
                Dal.dalModel ct = new MHElectronicsInventory.Dal.dalModel();
                ct.Model_name = Model_name.Text;
                ct.ID = Convert.ToInt32(listBox1.SelectedValue);
                int i = 1;
                if (i == ct.updatename())
                {
                    MessageBox.Show("Successfully updated");
                }
                else
                {
                    MessageBox.Show(ct.Error);
                }
            }
            Model_name.Text = "";
            this.modelTableAdapter.Fill(this.mHElectronicsDataSet5.Model);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                Dal.dalDaily ct = new MHElectronicsInventory.Dal.dalDaily();
                ct.IMEI = dataGridView1.Rows[e.RowIndex].Cells["im"].Value.ToString();
                try
                {
                    ct.M_ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["M_ID"].Value);
                }
                catch (Exception ex)
                {
                }
                ct.Price = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells["pri"].Value);
                ct.Profit = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells["prof"].Value);
                ct.ST = dataGridView1.Rows[e.RowIndex].Cells["stno"].Value.ToString();
                int i = 1;
                if (i == ct.Update())
                {
                    MessageBox.Show("Successfully Updated");
                }
                else
                {
                    MessageBox.Show(ct.Error);
                }
            }
        }

    }
}
