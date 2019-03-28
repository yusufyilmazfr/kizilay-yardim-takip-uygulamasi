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
    public partial class frmEditPassword : Form
    {
        public DataTable table { get; set; }

        public void AdminExist()
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT TOP 1 * FROM Admin WHERE Username=@p1 AND Passwd=@p2";

            helper.command.Parameters.AddWithValue("@p1", txtUsername.Text);
            helper.command.Parameters.AddWithValue("@p2", CreateMD5.Create(txtLastPassword.Text));

            helper.connection.Open();

            table = new DataTable();

            OleDbDataAdapter adapter = new OleDbDataAdapter(helper.command);

            adapter.Fill(table);


            helper.connection.Close();
            helper.connection.Dispose();
        }

        public frmEditPassword()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text != txtRePassword.Text)
            {
                MessageBox.Show("Yeni girilen şifreler uyuşmuyor...", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
                
            AdminExist();

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("Böyle bir yönetici bulunmamaktadır..");
                return;
            }

            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "UPDATE Admin SET Passwd=@p1 WHERE Id=@p2";

            helper.command.Parameters.AddWithValue("@p1", CreateMD5.Create(txtNewPassword.Text));
            helper.command.Parameters.AddWithValue("@p2", Convert.ToInt32(table.Rows[0]["Id"]));


            try
            {
                helper.connection.Open();

                helper.command.ExecuteNonQuery();

                MessageBox.Show("Güncelleme işlemi başarılı... yönlendiriliyorsunuz.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                helper.connection.Close();
                helper.connection.Dispose();

                this.Hide();
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
