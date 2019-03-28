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
    public partial class frmNewFamily : Form
    {
        public List<string> housingList { get; set; }
        public int CityId { get; set; }
        public int TownId { get; set; }
        public int NeigId { get; set; }

        public void FillHousingList()
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT Name FROM Housing ORDER BY Name";

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    housingList.Add(reader.GetString(0));
                }
            }

            reader.Close();
            reader.Dispose();

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
        
        public void FillCombobox()
        {
            foreach (var item in housingList)
            {
                cmbHousingList.Items.Add(item);
            }
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

        public frmNewFamily()
        {
            InitializeComponent();

            this.housingList = new List<string>();
        }

        private void frmNewFamily_Load(object sender, EventArgs e)
        {
            FillHousingList();
            FillCombobox();
        }

        private void txtTCNo_TextChanged(object sender, EventArgs e)
        {
            if (txtTCNo.Text.Length == 11)
            {
                SqlHelper helper = new SqlHelper();
                helper.command.CommandText = string.Format("SELECT * FROM Family WHERE FatherNo='{0}'", txtTCNo.Text);

                helper.connection.Open();

                OleDbDataReader reader = helper.command.ExecuteReader();

                if (reader.HasRows)
                {
                    MessageBox.Show("Böyle bir aile zaten bulunmaktadır!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    cmbCities.Items.Clear();

                    FillCities();

                    numPersonCount.Enabled = true;
                    cmbHousingList.Enabled = true;
                    cmbCities.Enabled = true;
                }

                helper.connection.Close();
                helper.connection.Dispose();
            }
            else
            {
                btnCreate.Enabled = false;
                numPersonCount.Enabled = false;
                cmbHousingList.Enabled = false;
                cmbCities.Enabled = false;
                cmbNeig.Enabled = false;
                cmbTowns.Enabled = false;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string TCNo = txtTCNo.Text;
            object houseType = cmbHousingList.SelectedItem;
            int personCount = (int)numPersonCount.Value;
            string houseTypeId = string.Empty;

            if (!string.IsNullOrEmpty(TCNo) && houseType != null)
            {
                SqlHelper helper = new SqlHelper();

                helper.command.CommandText = string.Format("SELECT Id From Housing WHERE Name='{0}'", houseType.ToString());

                helper.connection.Open();

                OleDbDataReader reader = helper.command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        houseTypeId = reader.GetInt32(0).ToString();
                        break;
                    }
                }

                reader.Close();
                reader.Dispose();

                helper.command = helper.connection.CreateCommand();


                helper.command.CommandText =
                    string.Format(
                        "INSERT INTO Family (FatherNo,HousingId,PersonCount) values ('{0}',{1},{2})"
                        , TCNo, houseTypeId, personCount);
                try
                {
                    helper.command.ExecuteNonQuery();

                    MessageBox.Show("Başarıyla oluştu. Ana ekrana yönlendiriliyorsunuz!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                helper.connection.Close();
                helper.connection.Dispose();

                this.Close();
                this.Dispose();
            }
        }

        private void txtTCNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmbCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCities.SelectedIndex != -1)
            {
                cmbTowns.Items.Clear();
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

     
    }
}
