using kizilay.Item;
using Kizilay.Business.Abstract;
using Kizilay.Entities.Concrete;
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

namespace kizilay
{
    public partial class frmDonate : Form
    {
        private IDonationManager _donationManager { get; set; }

        public frmDonate(IDonationManager donationManager)
        {
            _donationManager = donationManager;

            InitializeComponent();
        }

        public void FillDataGridView()
        {
            dataGridView.DataSource = _donationManager
                .GetAll()
                .OrderBy(i => i.DonationID)
                .Select(i => new
                {
                    i.Id,
                    i.Name
                }).ToList();
        }

        public void FillComboBox()
        {
            foreach (var item in _donationManager.GetAll().Where(i => i.DonationID == 0))
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(item.Name, item.Id);

                cmbDonateCategories.Items.Add(comboBoxItem);
            }
        }

        private void frmDonate_Load(object sender, EventArgs e)
        {
            FillDataGridView();
            FillComboBox();
        }

        public void ClearAllBase()
        {
            txtDonateName.Enabled = true;
            txtDonateName.Text = "";

            chckAddCategory.Enabled = true;
            chckAddCategory.Checked = false;

            cmbDonateCategories.Items.Clear();

            FillComboBox();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            cmbDonateCategories.Enabled = chckAddCategory.Checked ? false : true;
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.CurrentRow.Index == -1)
                return;

            int donationId = (int)dataGridView.CurrentRow.Cells["Id"].Value;
            string donationName = dataGridView.CurrentRow.Cells["Name"].Value.ToString();

            txtDonateName.Text = donationName;

            var currentDonation = _donationManager.GetById(donationId);


            if (currentDonation.DonationID == 0)
            {
                cmbDonateCategories.Enabled = false;
                chckAddCategory.Enabled = false;
            }
            else
            {
                cmbDonateCategories.Enabled = true;
                chckAddCategory.Enabled = false;

                var parentDonation = _donationManager.GetById(currentDonation.DonationID);

                foreach (ComboBoxItem item in cmbDonateCategories.Items)
                {
                    if (item.Value == currentDonation.DonationID)
                    {
                        cmbDonateCategories.SelectedItem = item;
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!chckAddCategory.Checked && cmbDonateCategories.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir ana kategori seçiniz...", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Donation donation = new Donation()
            {
                Name = txtDonateName.Text,
                DonationID = 0
            };

            if (!chckAddCategory.Checked)
            {
                int parentDonationId = ((ComboBoxItem)cmbDonateCategories.SelectedItem).Value;
                donation.DonationID = parentDonationId;
            }

            var result = _donationManager.Add(donation);

            if (result.HasError())
            {
                string firstError = result.Errors.FirstOrDefault();
                MessageBox.Show(firstError, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            else
            {
                MessageBox.Show("Ekleme başarıyla yapıldı.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearAllBase();

                FillComboBox();
                FillDataGridView();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow.Index == -1)
                return;

            if (cmbDonateCategories.Enabled && cmbDonateCategories.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir üst kategori seçiniz!");
                return;
            }

            int parentId = 0;
            int donationId = (int)dataGridView.CurrentRow.Cells["Id"].Value;
            string donationName = txtDonateName.Text;

            Donation donation = new Donation()
            {
                Id = donationId,
                Name = donationName,
                DonationID = parentId
            };

            if (cmbDonateCategories.Enabled)
                donation.DonationID = ((ComboBoxItem)cmbDonateCategories.SelectedItem).Value;


            var result = _donationManager.Update(donation);

            if (result.HasError())
            {
                string firstError = result.Errors.FirstOrDefault();
                MessageBox.Show(firstError, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            else
            {
                MessageBox.Show("Güncelle işlemi başarılı..", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearAllBase();
                FillComboBox();
                FillDataGridView();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow.Index == -1)
                return;

            DialogResult dialogResult = MessageBox.Show("Eğer bu bağışı silerseniz buna ait bütün yardımlar kalıcı olarak silinecektir, silmeden önce emin misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.No)
                return;

            int Id = (int)dataGridView.CurrentRow.Cells["Id"].Value;

            var result = _donationManager.RemoveById(Id);

            if (result.HasError())
            {
                string firstError = result.Errors.FirstOrDefault();

                MessageBox.Show(firstError, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {

                MessageBox.Show("Kayıt silme işlemi başarıyla gerçekleşti..", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearAllBase();

                FillComboBox();
                FillDataGridView();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllBase();
        }
    }
}
