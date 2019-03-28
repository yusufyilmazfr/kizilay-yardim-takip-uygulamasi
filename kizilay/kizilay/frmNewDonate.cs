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
    public partial class frmNewDonate : Form
    {
        public List<DonationModel> DonationList { get; set; }
        public List<DonationProcessModel> donationProcesses { get; set; }

        public int PersonId { get; set; }

        public void GetDonationList()
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT * FROM Donation";

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DonationList.Add(new DonationModel()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        ParentId = (int)reader["DonationId"]
                    });
                }
            }

            reader.Close();
            reader.Dispose();

            helper.connection.Close();
            helper.connection.Dispose();
        }

        public void FillDonationTypeList()
        {
            foreach (DonationModel model in DonationList.Where(i => i.ParentId == 0))
            {
                cmbDonateType.Items.Add(model.Name);
            }
        }

        public void FillDonationNameList(string Name)
        {
            cmbDonateName.Items.Clear();

            int parentId = DonationList.Where(i => i.Name == Name).Select(i => i.Id).FirstOrDefault();

            foreach (DonationModel model in DonationList.Where(i => i.ParentId == parentId))
            {
                cmbDonateName.Items.Add(model.Name);
            }
        }

        public DataTable GetAllPerson(string TC)
        {
            SqlHelper helper = new SqlHelper();
            helper.command.CommandText = string.Format("SELECT * FROM Person WHERE TC='{0}'", TC);

            helper.connection.Open();

            DataTable dataTable = new DataTable();

            OleDbDataAdapter adapter = new OleDbDataAdapter(helper.command);

            adapter.Fill(dataTable);

            helper.connection.Close();
            helper.connection.Dispose();

            return dataTable;
        }

        public DataTable GetAllPerson(int FamilyId)
        {

            SqlHelper helper = new SqlHelper();

            string commandText = string.Format("SELECT * FROM ((Person as P " +
                "INNER JOIN Family as F ON P.FamilyId = F.Id) " +
                " INNER JOIN Person_Donation as PD ON PD.PersonId = P.Id) " +
                "INNER JOIN Donation as D ON PD.DonationId = D.Id WHERE P.FamilyId={0}", FamilyId);

            helper.command.CommandText = commandText;

            helper.connection.Open();

            DataTable dataTable = new DataTable();

            OleDbDataAdapter adapter = new OleDbDataAdapter(helper.command);

            adapter.Fill(dataTable);

            helper.connection.Close();
            helper.connection.Dispose();

            return dataTable;
        }

        public int GetFamilyId(string TC)
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = string.Format("SELECT FamilyId FROM Person WHERE TC='{0}'", TC);

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            int FamilyId = 0;

            while (reader.Read())
            {
                FamilyId = reader.GetInt32(0);
            }

            helper.connection.Close();
            helper.connection.Dispose();

            reader.Close();
            reader.Dispose();

            return FamilyId;
        }

        public frmNewDonate()
        {
            InitializeComponent();

            DonationList = new List<DonationModel>();
            donationProcesses = new List<DonationProcessModel>();

            GetDonationList();
            FillDonationTypeList();
        }

        private void txtTcNo_TextChanged(object sender, EventArgs e)
        {
            if (txtTcNo.Text.Length == 11)
            {
                if (GetAllPerson(txtTcNo.Text).Rows.Count > 0)
                {
                    btnCreate.Enabled = true;

                    DataTable dataTable = GetAllPerson(GetFamilyId(txtTcNo.Text));

                    if (dataTable.Rows.Count > 0)
                    {
                        DateTime lastDate;
                        TimeSpan rangeOfDate;

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {

                            lastDate = Convert.ToDateTime(dataTable.Rows[i]["AddedDate"].ToString());

                            rangeOfDate = DateTime.Now.Subtract(lastDate);

                            if (rangeOfDate.Days < 360)
                            {
                                DialogResult result = MessageBox.Show("Son 1 yıl içerisinde bu aile yardım almıştır. Aileye ait yardım kayıtlarını görmek ister misiniz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                if (result == DialogResult.Yes)
                                {
                                    donationProcesses.Clear();
                                    for (int j = 0; j < dataTable.Rows.Count; j++)
                                    {
                                        donationProcesses.Add(new DonationProcessModel()
                                        {
                                            // Name kolonu hatalı..
                                            PersonName = dataTable.Rows[j][3].ToString(),
                                            PersonLastName = dataTable.Rows[j]["Surname"].ToString(),
                                            Description = dataTable.Rows[j]["Description"].ToString(),
                                            DonationDate = (DateTime)dataTable.Rows[j]["AddedDate"],
                                            DonationName = dataTable.Rows[i][7].ToString()
                                        });
                                    }

                                    new frmFamilyDonationProcess(donationProcesses).Show();
                                    return;
                                }
                            }

                        }
                    }

                }
                else
                {
                    MessageBox.Show("Böyle bir personel bulunmamaktadır...", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void txtTcNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmbDonateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDonationNameList(cmbDonateType.SelectedItem.ToString());
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (cmbDonateType.SelectedItem == null || cmbDonateName.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir bağış seçiniz...", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            SqlHelper helper = new SqlHelper();

            int donationId = DonationList.Where(i => i.Name == cmbDonateName.SelectedItem.ToString() && i.ParentId != 0).Select(i => i.Id).FirstOrDefault();

            string date = dtPicker.Value.ToShortDateString();

            int PersonId = (int)GetAllPerson(txtTcNo.Text).Rows[0]["Id"];

            string description = rchDescription.Text;

            helper.command.CommandText = "INSERT INTO Person_Donation (PersonId,DonationId,AddedDate,Description) values(@p1,@p2,@p3,@p4)";

            helper.command.Parameters.AddWithValue("@p1", PersonId);
            helper.command.Parameters.AddWithValue("@p2", donationId);
            helper.command.Parameters.AddWithValue("@p3", date);
            helper.command.Parameters.AddWithValue("@p4", description);


            helper.connection.Open();

            try
            {
                helper.command.ExecuteNonQuery();

                MessageBox.Show("Yardım işlemi başarılı bir şekilde yapıldı :), yönlendiriliyorsunuz...", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                this.Dispose();

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
