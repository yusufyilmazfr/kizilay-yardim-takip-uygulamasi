using kizilay.DependencyResolver.Ninject;
using Kizilay.Business.Abstract;
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
        private IPersonDonationManager _personDonationManager { get; set; }

        public int familyId;

        public frmFamilyDonationProcess(IPersonDonationManager personDonationManager)
        {
            _personDonationManager = personDonationManager;

            InitializeComponent();
        }

        private void LoadDataGridView()
        {
            dataGridView.DataSource = _personDonationManager.GetFamilyDonationHistoriesByFamilyId(familyId);
        }

        private void frmFamilyDonationProcess_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null || dataGridView.CurrentRow.Index == -1)
                return;

            var currentDonationId = (int)dataGridView.CurrentRow.Cells["donationId"].Value;

            Form editDonate = FormDependencyResolver.Resolve<frmEditDonate>();

            ((frmEditDonate)editDonate).donationId = currentDonationId;

            if (editDonate.ShowDialog() == DialogResult.Yes)
                this.Dispose();
        }

        private void btnRemoveDonate_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null || dataGridView.CurrentRow.Index == -1)
                return;

            int donationId = Convert.ToInt32(dataGridView.CurrentRow.Cells["DonationId"].Value);
            int count = _personDonationManager.RemoveById(donationId);

            if (count == 0)
                MessageBox.Show("Kayıt silinemedi!", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            else
            {
                MessageBox.Show("Bağış başarıyla silindi!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDataGridView();
            }
        }
    }
}
