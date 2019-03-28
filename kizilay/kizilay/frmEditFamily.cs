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
                helper.command.CommandText = "SELECT * FROM Family WHERE FatherNo=@p1";

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

                    while (reader.Read())
                    {
                        numPersonCount.Value = Convert.ToInt32(reader["PersonCount"]);
                        cmbHousingList.SelectedItem = housingList.FirstOrDefault(i => i.Key == Convert.ToInt32(reader["HousingId"])).Value;
                    }
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

            try
            {


                SqlHelper helper = new SqlHelper();

                helper.connection.Open();

                helper.command.CommandText = "UPDATE Family SET HousingId=@p1,PersonCount=@p2 WHERE FatherNo=@p3";

                helper.command.Parameters.AddWithValue("@p1", housingList.FirstOrDefault(i => i.Value == cmbHousingList.SelectedItem).Key);
                helper.command.Parameters.AddWithValue("@p2", numPersonCount.Value);
                helper.command.Parameters.AddWithValue("@p3", txtTCNo.Text);

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
    }
}
