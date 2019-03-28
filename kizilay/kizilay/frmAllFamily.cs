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
    public partial class frmAllFamily : Form
    {
        public List<AllPersonModel> allPerson { get; set; }
        public List<DonationProcessModel> donationList { get; set; }

        private bool DeletePerson(string PersonTC)
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = string.Format("DELETE FROM Person WHERE TC='{0}'", PersonTC);

            helper.connection.Open();

            int count = helper.command.ExecuteNonQuery();

            helper.connection.Close();
            helper.connection.Dispose();

            return count > 0 ? true : false;
        }

        public List<AllPersonModel> GetAllPerson()
        {
            SqlHelper helper = new SqlHelper();


            helper.command.CommandText = "SELECT * FROM (Person as P " +
                "INNER JOIN Family as F ON P.FamilyId = F.Id) ORDER BY F.FatherNo";

            helper.connection.Open();


            OleDbDataReader reader = helper.command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    allPerson.Add(new AllPersonModel()
                    {
                        TC = reader["TC"].ToString(),
                        Name = reader["Name"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        BirthDate = reader["BirthDate"].ToString(),
                        Phone = reader["MobilePhone"].ToString(),
                        JobDescription = reader["JobDescription"].ToString(),
                        FatherTC = reader["FatherNo"].ToString(),
                    });
                }
            }

            helper.connection.Close();
            helper.connection.Dispose();

            return allPerson;

        }

        public DataTable GetFamilyInformation(string FatherNO)
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = string.Format("SELECT P.Name as Name, P.Surname, PD.Id as DonationId, PD.AddedDate, D.Name as DonationName , PD.Description  FROM ((Person as P " +
                "INNER JOIN Family as F ON P.FamilyId = F.Id) " +
                "INNER JOIN Person_Donation as PD ON P.Id = PD.personId) " +
                "INNER JOIN Donation as D ON D.Id = PD.DonationId WHERE FatherNo='{0}'", FatherNO);

            helper.connection.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(helper.command);

            DataTable table = new DataTable();
            adapter.Fill(table);

            helper.connection.Close();
            helper.connection.Dispose();
            return table;
        }

        public void FillAllBase()
        {
            allPerson = new List<AllPersonModel>();
            dataGridView1.DataSource = GetAllPerson();
        }

        public frmAllFamily()
        {
            InitializeComponent();
            FillAllBase();
        }

        private void btnShowDonateProcess_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index == -1)
            {
                MessageBox.Show("Lütfen bir satır seçiniz...");
                return;
            }

            donationList = new List<DonationProcessModel>();

            string FatherNo = dataGridView1.CurrentRow.Cells["FatherTC"].Value.ToString();
            DataTable table = GetFamilyInformation(FatherNo);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                donationList.Add(new DonationProcessModel()
                {
                    donationId = (int)table.Rows[i]["donationId"],
                    PersonName = table.Rows[i]["Name"].ToString(),
                    PersonLastName = table.Rows[i]["Surname"].ToString(),
                    Description = table.Rows[i]["Description"].ToString(),
                    DonationDate = (DateTime)table.Rows[i]["AddedDate"],
                    DonationName = table.Rows[i]["DonationName"].ToString(),
                });
            }

            new frmFamilyDonationProcess(donationList).Show();
        }

        private void btnRemovePerson_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index == -1)
            {
                MessageBox.Show("Lütfen bir satır seçiniz...");
                return;
            }

            DialogResult result = MessageBox.Show("Kayıtlı kullanıcıyı silmek istediğinize emin misiniz?", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.No)
            {
                return;
            }

            string TC = dataGridView1.CurrentRow.Cells["TC"].Value.ToString();

            try
            {
                if (DeletePerson(TC))
                {
                    MessageBox.Show("Silme işlemi başarılı", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillAllBase();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string filterText = string.IsNullOrEmpty(txtTC.Text) ? "" : txtTC.Text;
            string fatherTC = string.IsNullOrEmpty(txtFatherTC.Text) ? "" : txtFatherTC.Text;
            string fullName = string.IsNullOrEmpty(txtFullName.Text) ? "" : txtFullName.Text;

            dataGridView1.DataSource = allPerson.Where(i => i.TC.Contains(filterText) && i.FatherTC.Contains(fatherTC) && (i.Name + ' ' + i.Surname).ToLower().Contains(fullName.ToLower())).ToList();

        }

        private void btnEditPerson_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index == -1)
            {
                MessageBox.Show("Lütfen bir satır seçiniz...");
                return;
            }

            frmEditPerson frm = new frmEditPerson();
            frm.personTC = dataGridView1.CurrentRow.Cells["TC"].Value.ToString();
            frm.FatherTC = dataGridView1.CurrentRow.Cells["FatherTC"].Value.ToString();

            if (frm.ShowDialog() == DialogResult.Yes)
            {
                allPerson.Clear();
                FillAllBase();
            }
        }

        private void btnNewPerson_Click(object sender, EventArgs e)
        {
            new frmNewPerson().Show();
        }
    }
}
