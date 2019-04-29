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

        public int CityId { get; set; }
        public int TownId { get; set; }

        public string fullAddress { get; set; } = "";

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


            helper.command.CommandText = "SELECT " +
                "P.TC," +
                "P.Name," +
                "P.Surname," +
                "P.BirthDate," +
                "C.Name as CityName," +
                "T.Name as TownName," +
                "N.Name as NeighborhoodsName," +
                "F.Address," +
                "P.JobDescription," +
                "P.MobilePhone," +
                "F.Priority," +
                "F.FatherNo as FatherNo " +
                "FROM  (" +
                "         (" +
                "            (" +
                "               (" +
                "                   Person as P " +
                "                       INNER JOIN Family as F ON F.Id = P.FamilyId) " +
                "                           INNER JOIN Neighborhoods as N ON N.Id = F.NeighborhoodsId) " +
                "                               INNER JOIN Towns as T ON T.Id = N.TownId) " +
                "                                   INNER JOIN Cities as C ON C.Id = T.CityId)" +
                "                                               ORDER By F.FatherNo";

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
                        familyPriority = Convert.ToInt32(reader["Priority"]),
                        FatherTC = reader["FatherNo"].ToString(),
                        Address = (reader["CityName"].ToString() + " " + reader["TownName"].ToString() + " " + reader["NeighborhoodsName"].ToString() + " " + reader["Address"].ToString()).ToUpper()
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

        private void FillCities()
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT Name FROM Cities ORDER BY Name";

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cmbCities.Items.Add(reader["Name"].ToString());
                }
            }

            reader.Close();
            reader.Dispose();

            helper.connection.Close();
            helper.connection.Dispose();
        }

        private void FindCityId(string cityName)
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT TOP 1 Id FROM Cities WHERE Name = @p1";
            helper.command.Parameters.AddWithValue("@p1", cityName);

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CityId = Convert.ToInt32(reader["Id"]);
                }
            }

            reader.Close();
            reader.Dispose();

            helper.connection.Close();
            helper.connection.Dispose();
        }

        public void FillTowns()
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT Name FROM Towns WHERE CityId = @cityId ORDER BY Name";
            helper.command.Parameters.AddWithValue("@cityId", CityId);

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            if (reader.HasRows)
            {
                cmbTowns.Items.Clear();

                while (reader.Read())
                {
                    cmbTowns.Items.Add(reader["Name"].ToString());
                }
            }

            helper.connection.Close();
            helper.connection.Dispose();
        }

        public void FindTownId(string townName)
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT TOP 1 Id FROM Towns WHERE Name = @p1";
            helper.command.Parameters.AddWithValue("@p1", townName);

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TownId = Convert.ToInt32(reader["Id"]);
                }
            }

            reader.Close();
            reader.Dispose();

            helper.connection.Close();
            helper.connection.Dispose();
        }

        private void FillNeig()
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT Name FROM Neighborhoods WHERE TownId = @p1 ORDER BY Name";
            helper.command.Parameters.AddWithValue("@p1", TownId);

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            if (reader.HasRows)
            {
                cmbNeig.Items.Clear();

                while (reader.Read())
                {
                    cmbNeig.Items.Add(reader["Name"].ToString());
                }
            }

            helper.connection.Close();
            helper.connection.Dispose();
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
            FillCities();
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


            if (cmbFamilyPriority.SelectedItem == null)
            {
                dataGridView1.DataSource = allPerson
                    .Where(i => i.TC.Contains(filterText) &&
                                i.FatherTC.Contains(fatherTC) &&
                                (i.Name + ' ' + i.Surname).ToLower().Contains(fullName.ToLower()) &&
                                i.Address.Contains(fullAddress.ToUpper())
                            )
                    .ToList();

            }
            else
            {
                int priority = Convert.ToInt32(cmbFamilyPriority.SelectedItem.ToString());

                dataGridView1.DataSource = allPerson
                    .Where(i => i.TC.Contains(filterText) &&
                                i.FatherTC.Contains(fatherTC) &&
                                (i.Name + ' ' + i.Surname).ToLower().Contains(fullName.ToLower()) &&
                                i.Address.Contains(fullAddress.ToUpper()) &&
                                i.familyPriority == priority
                            )
                    .ToList();
            }
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

        private void cmbCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindCityId(cmbCities.SelectedItem.ToString());
            FillTowns();

            fullAddress = string.Empty;

            fullAddress += cmbCities.SelectedItem.ToString();

            textBox1_TextChanged(null, null);
        }

        private void cmbTowns_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindTownId(cmbTowns.SelectedItem.ToString());
            FillNeig();
            cmbNeig.Enabled = true;

            fullAddress = cmbCities.SelectedItem + " " + cmbTowns.SelectedItem;

            textBox1_TextChanged(null, null);
        }

        private void cmbNeig_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAddress.Enabled = true;

            fullAddress = cmbCities.SelectedItem + " " + cmbTowns.SelectedItem + " " + cmbNeig.SelectedItem;

            textBox1_TextChanged(null, null);
        }

        private void btnHasDonatePerson_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtDonateContent.Text))
            {
                FillAllBase();
                return;
            }

            SqlHelper helper = new SqlHelper();


            helper.command.CommandText = "SELECT " +
               "P.TC," +
               "P.Name," +
               "P.Surname," +
               "P.BirthDate," +
               "C.Name as CityName," +
               "T.Name as TownName," +
               "N.Name as NeighborhoodsName," +
               "F.Address," +
               "P.JobDescription," +
               "P.MobilePhone," +
               "F.Priority," +
               "F.FatherNo as FatherNo " +
               "FROM  (" +
               "         (" +
               "            (" +
               "                (" +
               "                   (" +
               "                       (" +
               "                           Person as P " +
               "                               INNER JOIN Family as F ON F.Id = P.FamilyId) " +
               "                                   INNER JOIN Neighborhoods as N ON N.Id = F.NeighborhoodsId) " +
               "                                       INNER JOIN Towns as T ON T.Id = N.TownId) " +
               "                                           INNER JOIN Cities as C ON C.Id = T.CityId) " +
               "                                               INNER JOIN Person_Donation as PD ON P.Id = PD.PersonId) " +
               "                                                   INNER JOIN Donation as D ON PD.DonationId = D.Id) ";

            helper.command.CommandText += "WHERE [PD.Description] LIKE '%" + txtDonateContent.Text + "%'";



            helper.connection.Open();


            OleDbDataReader reader = helper.command.ExecuteReader();

            allPerson = null;

            allPerson = new List<AllPersonModel>();

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
                        familyPriority = Convert.ToInt32(reader["Priority"]),
                        FatherTC = reader["FatherNo"].ToString(),
                        Address = (reader["CityName"].ToString() + " " + reader["TownName"].ToString() + " " + reader["NeighborhoodsName"].ToString() + " " + reader["Address"].ToString()).ToUpper()
                    });
                }
            }

            helper.connection.Close();
            helper.connection.Dispose();

            dataGridView1.DataSource = allPerson;

        }
    }
}
