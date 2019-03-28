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
        public DonationProcessModel model { get; set; }

        public frmEditDonate()
        {
            InitializeComponent();
        }

        private void frmEditDonate_Load(object sender, EventArgs e)
        {
            rchDescription.Text = model.Description;
            dateTimePicker.Value = model.DonationDate;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                SqlHelper helper = new SqlHelper();

                helper.command.CommandText = "UPDATE Person_Donation SET AddedDate=@p1, Description=@p2 WHERE Id=@p3";

                helper.command.Parameters.AddWithValue("@p1", dateTimePicker.Value);
                helper.command.Parameters.AddWithValue("@p2", rchDescription.Text);
                helper.command.Parameters.AddWithValue("@p3", model.donationId);

                helper.connection.Open();

                int count = helper.command.ExecuteNonQuery();

                if (count > 0)
                {
                    MessageBox.Show("Bağış düzenleme başarıyla gerçekleştirildi, yönlendiriliyorsunuz...", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.Yes;
                }
                else
                {
                    MessageBox.Show("Herhangi bir güncelleme işlemi gerçekleştirilemedi, daha sonra tekrar deneyin.", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }

                helper.connection.Close();
                helper.connection.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
