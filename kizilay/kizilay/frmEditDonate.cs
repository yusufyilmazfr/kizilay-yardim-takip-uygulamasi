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
    public partial class frmEditDonate : Form
    {
        private IPersonDonationManager _personDonationManager { get; set; }

        public int donationId { get; set; }

        public frmEditDonate(IPersonDonationManager personDonationManager)
        {
            _personDonationManager = personDonationManager;

            InitializeComponent();
        }

        private void frmEditDonate_Load(object sender, EventArgs e)
        {
            var donation = _personDonationManager.GetById(donationId);

            rchDescription.Text = donation.Description;
            dateTimePicker.Value = donation.AddedDate;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Person_Donation donation = new Person_Donation()
            {
                Id = donationId,
                AddedDate = dateTimePicker.Value,
                Description = rchDescription.Text
            };

            var layerResult = _personDonationManager.Update(donation);

            if (!layerResult.HasError())
            {
                MessageBox.Show("Bağış düzenleme başarıyla gerçekleştirildi, yönlendiriliyorsunuz...", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.Yes;
            }
            else
            {
                var firstError = layerResult.Errors.FirstOrDefault();
                MessageBox.Show(firstError, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}
