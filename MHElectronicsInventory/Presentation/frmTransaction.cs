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
    public partial class frmTransaction : Form
    {
        public frmTransaction()
        {
            InitializeComponent();
        }

        private void frmTransaction_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mHElectronicsDataSet9.View_2' table. You can move, or remove it, as needed.
            this.view_2TableAdapter.Fill(this.mHElectronicsDataSet9.View_2);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==6)
            {
                Dal.dalMemo m = new MHElectronicsInventory.Dal.dalMemo();
                m.ST=dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                m.IMEI = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                if (m.Delete() != -1)
                {
                    MessageBox.Show("Successfully Deleted");
                    String name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    Dal.dalModel md = new MHElectronicsInventory.Dal.dalModel();
                    md.Model_name = name;
                    int I=md.getID();
                    Dal.dalNumber n = new MHElectronicsInventory.Dal.dalNumber();
                    n.M_ID = I;
                    int num=n.getnumber();
                    n.Number = num + 1;
                    if (n.update() != -1)
                    {
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        MessageBox.Show(n.Error);
                    }
                }
                else
                {
                    MessageBox.Show(m.Error);
                }
            }
        }
    }
}
