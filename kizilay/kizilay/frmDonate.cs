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
        //private DataTable dataTable { get; set; }
        public List<DonationModel> donationList { get; set; }

        public frmDonate()
        {
            InitializeComponent();

            donationList = new List<DonationModel>();
        }

        public void FillDonationList()
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT * FROM Donation";

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    donationList.Add(new DonationModel()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        ParentId = (int)reader["DonationID"],
                    });
                }
            }

            reader.Close();
            reader.Dispose();

            helper.connection.Close();
            helper.connection.Dispose();
        }

        public void FillDataGridView()
        {
            dataGridView.DataSource = donationList.OrderBy(i => i.ParentId).Select(i => new
            {
                i.Id,
                i.Name
            }).ToList();

        }

        public void FillComboBox()
        {
            foreach (var item in donationList.Where(i => i.ParentId == 0).ToList())
            {
                cmbDonateCategories.Items.Add(item.Name);
            }
        }

        public void ClearAllBase()
        {
            dataGridView.DataSource = null;
            chckAddCategory.Checked = false;
            donationList.Clear();
            txtDonateName.Text = "";
            cmbDonateCategories.Items.Clear();
        }

        public void FillAllBase()
        {
            FillDonationList();
            FillComboBox();
            FillDataGridView();
        }

        private void frmDonate_Load(object sender, EventArgs e)
        {
            FillDonationList();
            FillDataGridView();
            FillComboBox();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            cmbDonateCategories.Enabled = chckAddCategory.Checked ? false : true;
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.CurrentRow.Index == -1)
            {
                return;
            }


            int donationId = (int)dataGridView.CurrentRow.Cells["Id"].Value;
            string donationName = dataGridView.CurrentRow.Cells["Name"].Value.ToString();

            int parentId = donationList.Where(i => i.Id == donationId).Select(i => i.ParentId).FirstOrDefault();
            string parentName = donationList.Where(i => i.Id == parentId).Select(i => i.Name).FirstOrDefault();

            if (parentId == 0)
            {
                cmbDonateCategories.Enabled = false;
                chckAddCategory.Enabled = false;
            }
            else
            {
                cmbDonateCategories.Enabled = true;
            }


            cmbDonateCategories.SelectedItem = parentName;
            txtDonateName.Text = donationName;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!chckAddCategory.Checked && cmbDonateCategories.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir ana kategori seçiniz...", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            int parentId = chckAddCategory.Checked ? 0 : donationList.Where(i => i.Name == cmbDonateCategories.SelectedItem.ToString()).Select(i => i.Id).FirstOrDefault();


            SqlHelper helper = new SqlHelper();



            helper.command.CommandText = "INSERT INTO Donation (Name,DonationID) values(@p1,@p2)";

            helper.command.Parameters.AddWithValue("@p1", txtDonateName.Text);
            helper.command.Parameters.AddWithValue("@p2", parentId);

            helper.connection.Open();

            try
            {
                helper.command.ExecuteReader();
                MessageBox.Show("Ekleme başarıyla yapıldı.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);


                ClearAllBase();
                FillAllBase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            helper.connection.Close();
            helper.connection.Dispose();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow.Index == -1)
            {
                MessageBox.Show("Lütfen bir satır seçiniz..", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int parentId = 0;
            int donationId = (int)dataGridView.CurrentRow.Cells["Id"].Value;
            string donationName = txtDonateName.Text;

            if (cmbDonateCategories.Enabled)
            {
                string parentName = cmbDonateCategories.SelectedItem.ToString();
                parentId = donationList.Where(i => i.Name == parentName).Select(i => i.Id).FirstOrDefault();
            }


            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = string.Format("UPDATE  Donation SET Name='{0}',DonationID={1} WHERE Id={2}", donationName, parentId, donationId);


            helper.command.CommandText = "UPDATE  Donation SET Name=@p1,DonationID=@p2 WHERE Id=@p3";

            helper.command.Parameters.AddWithValue("@p1", donationName);
            helper.command.Parameters.AddWithValue("@p2", parentId);
            helper.command.Parameters.AddWithValue("@p3", donationId);


            helper.connection.Open();

            try
            {
                helper.command.ExecuteNonQuery();

                MessageBox.Show("Güncelle işlemi başarılı..", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearAllBase();
                FillAllBase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            helper.connection.Close();
            helper.connection.Dispose();


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow.Index == -1)
            {
                MessageBox.Show("Lütfen silmek istediğiniz satırı seçiniz...", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            DialogResult result = MessageBox.Show("Eğer bu bağışı silerseniz buna ait bütün yardımlar kalıcı olarak silinecektir, silmeden önce emin misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            SqlHelper helper = new SqlHelper();

            int Id = (int)dataGridView.CurrentRow.Cells["Id"].Value;

            helper.command.CommandText = string.Format("DELETE FROM Donation WHERE Id = {0}", Id);

            helper.connection.Open();

            try
            {
                helper.command.ExecuteReader();
                MessageBox.Show("Kayıt silme işlemi başarıyla gerçekleşti..", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAllBase();
                FillAllBase();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

            helper.connection.Close();
            helper.connection.Dispose();
        }
    }
}
