using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kizilay
{
    public partial class frmFamilyDonationProcess : Form
    {
        public List<DonationProcessModel> donationProcessList { get; set; }

        public frmFamilyDonationProcess()
        {
            InitializeComponent();
        }

        public frmFamilyDonationProcess(List<DonationProcessModel> model)
        {
            InitializeComponent();

            dataGridView.DataSource = model.OrderByDescending(i => i.DonationDate).ToList();
        }



        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmEditDonate frm = new frmEditDonate();
            frm.model = dataGridView.CurrentRow.DataBoundItem as DonationProcessModel;

            if (frm.ShowDialog() == DialogResult.Yes)
            {
                this.Hide();
                this.Close();
                this.Dispose();
            }
        }
    }
}
