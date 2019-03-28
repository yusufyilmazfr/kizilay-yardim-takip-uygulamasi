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
    public partial class frmAccountLogin : Form
    {
        public frmAccountLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string username = txtUsername.Text;
            string password = CreateMD5.Create(txtPassword.Text);

            SqlHelper helper = new SqlHelper();
            // SQL Helper Sınıfında Command Propertysine Sorgu Metni İşlemi Yapıldı.
            helper.command.CommandText = "SELECT * FROM Admin WHERE Username=@username AND Passwd=@password";

            helper.command.Parameters.AddWithValue("@username", username);
            helper.command.Parameters.AddWithValue("@password", password);

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            if (reader.Read())
            {
                MessageBox.Show("Giriş Başarılı:)", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                new frmDashboard().Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Giriş Başarısız!", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            reader.Close();
            reader.Dispose();
            helper.connection.Close();
            helper.connection.Dispose();

        }

        private void frmAccountLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
