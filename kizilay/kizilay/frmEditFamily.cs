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
    public partial class frmEditFamily : Form
    {
        public Dictionary<int, string> housingList { get; set; }

        public string familyCityName { get; set; }
        public string familyTownName { get; set; }
        public string familyNeigName { get; set; }


        public int familyCityId { get; set; }
        public int familyTownId { get; set; }
        public int familyNeigId { get; set; }


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
                    familyTownId = Convert.ToInt32(reader["Id"]);
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
            helper.command.Parameters.AddWithValue("@p1", familyTownId);

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

        private void FindNeigId()
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT TOP 1 Id FROM Neighborhoods WHERE Name = @p1 AND TownId=@p2";

            helper.command.Parameters.AddWithValue("@p1", cmbNeig.SelectedItem.ToString());
            helper.command.Parameters.AddWithValue("@p1", familyTownId);

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    familyNeigId = Convert.ToInt32(reader["Id"]);
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
            helper.command.Parameters.AddWithValue("@cityId", familyCityId);

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
                    familyCityId = Convert.ToInt32(reader["Id"]);
                }
            }

            reader.Close();
            reader.Dispose();

            helper.connection.Close();
            helper.connection.Dispose();
        }

        public void FillHousingList()
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT Id,Name FROM Housing";

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    housingList.Add(Convert.ToInt32(reader["Id"]), reader["Name"].ToString());
                }
            }

            reader.Close();
            reader.Dispose();

            helper.connection.Close();
            helper.connection.Dispose();
        }

        public void FillCombobox()
        {
            foreach (var item in housingList)
            {
                cmbHousingList.Items.Add(item.Value);
            }
        }

        public frmEditFamily()
        {
            InitializeComponent();

            this.housingList = new Dictionary<int, string>();
        }

        private void frmEditFamily_Load(object sender, EventArgs e)
        {
            FillHousingList();
            FillCombobox();
        }

        private void txtTCNo_TextChanged(object sender, EventArgs e)
        {
            if (txtTCNo.Text.Length == 11)
            {
                SqlHelper helper = new SqlHelper();
                helper.command.CommandText = "SELECT TOP 1 " +
                    "F.PersonCount as PersonCount," +
                    "F.Priority as Priority," +
                    "F.Address as Address," +
                    "F.HousingId as HousingId," +
                    "F.FatherNo as FatherNo," +
                    "F.NeighborhoodsId as NeighborhoodsId," +
                    "N.Name as NeigName," +
                    "C.Id as CityId," +
                    "C.Name as CityName," +
                    "T.Id as TownId," +
                    "T.Name as TownName " +
                        "FROM (((Family as F INNER JOIN Neighborhoods as N ON F.NeighborhoodsId = N.Id)" +
                        "INNER JOIN Towns as T ON N.TownId = T.Id)" +
                        "INNER JOIN Cities AS C ON C.Id = T.CityId) WHERE FatherNo=@p1";

                helper.command.Parameters.AddWithValue("@p1", txtTCNo.Text);

                helper.connection.Open();

                OleDbDataReader reader = helper.command.ExecuteReader();

                if (!reader.HasRows)
                {
                    MessageBox.Show("Böyle bir aile bulunmamaktadır!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    btnUpdate.Enabled = true;
                    numPersonCount.Enabled = true;
                    cmbHousingList.Enabled = true;
                    cmbCities.Enabled = true;
                    cmbNeig.Enabled = true;
                    cmbTowns.Enabled = true;
                    numPriority.Enabled = true;

                    while (reader.Read())
                    {
                        numPersonCount.Value = Convert.ToInt32(reader["PersonCount"]);
                        rchAddress.Text = reader["Address"].ToString();
                        numPriority.Value = Convert.ToDecimal(reader["Priority"]);
                        cmbHousingList.SelectedItem = housingList.FirstOrDefault(i => i.Key == Convert.ToInt32(reader["HousingId"])).Value;


                        familyNeigId = (int)reader["NeighborhoodsId"];
                        familyCityId = (int)reader["CityId"];
                        familyTownId = (int)reader["TownId"];

                        familyCityName = reader["CityName"].ToString();
                        familyTownName = reader["TownName"].ToString();
                        familyNeigName = reader["NeigName"].ToString();

                    }

                    cmbCities.Items.Clear();
                    cmbNeig.Items.Clear();
                    cmbTowns.Items.Clear();


                    FillCities();
                    cmbCities.SelectedItem = familyCityName;

                    FillTowns();
                    cmbTowns.SelectedItem = familyTownName;

                    FillNeig();
                    cmbNeig.SelectedItem = familyNeigName;
                }

                helper.connection.Close();
                helper.connection.Dispose();


            }
            else
            {
                numPersonCount.Value = 0;
                cmbHousingList.SelectedItem = "";
                btnUpdate.Enabled = false;
                numPersonCount.Enabled = false;
                cmbHousingList.Enabled = false;
                numPriority.Enabled = false;
            }
        }

        private void txtTCNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbNeig.SelectedItem == null || string.IsNullOrEmpty(rchAddress.Text))
            {
                MessageBox.Show("lütfen bütün alanları doldurunuz..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            try
            {
                FindNeigId();

                SqlHelper helper = new SqlHelper();

                helper.connection.Open();

                helper.command.CommandText = "UPDATE Family SET Priority=@p0, HousingId=@p1,PersonCount=@p2,Address=@p3,NeighborhoodsId=@p4 WHERE FatherNo=@p5";

                helper.command.Parameters.AddWithValue("@p0", numPriority.Value);

                helper.command.Parameters.AddWithValue("@p1", housingList.FirstOrDefault(i => i.Value == cmbHousingList.SelectedItem).Key);
                helper.command.Parameters.AddWithValue("@p2", numPersonCount.Value);
                helper.command.Parameters.AddWithValue("@p3", rchAddress.Text);
                helper.command.Parameters.AddWithValue("@p4", familyNeigId);
                helper.command.Parameters.AddWithValue("@p5", txtTCNo.Text);

                int count = helper.command.ExecuteNonQuery();


                helper.connection.Close();
                helper.connection.Dispose();

                if (count > 0)
                {
                    MessageBox.Show("Aile güncellemesi başarılı, yönlendiriliyorsunuz....", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Hiçbir kayıt etkinlenmedi, daha sonra tekrar deneyiniz...", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cmbCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCities.SelectedIndex != -1)
            {
                cmbTowns.Items.Clear();
                cmbNeig.Items.Clear();
                cmbTowns.Enabled = true;

                FindCityId(cmbCities.SelectedItem.ToString());
                FillTowns();
            }
        }

        private void cmbTowns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTowns.SelectedIndex != -1)
            {
                cmbNeig.Items.Clear();
                cmbNeig.Enabled = true;

                FindTownId(cmbTowns.SelectedItem.ToString());
                FillNeig();
            }
        }

        private void cmbNeig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNeig.SelectedIndex != -1)
            {
                rchAddress.Enabled = true;
                btnUpdate.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
                rchAddress.Enabled = false;
            }
        }
    }
}
