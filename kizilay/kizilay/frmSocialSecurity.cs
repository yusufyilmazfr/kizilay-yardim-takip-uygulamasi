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
    public partial class frmSocialSecurity : Form
    {
        private DataTable dataTable { get; set; }

        public frmSocialSecurity()
        {
            InitializeComponent();

            dataTable = new DataTable();
        }

        public void FillDataTable()
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT * FROM SocialSecurity";

            helper.connection.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(helper.command);
            adapter.Fill(this.dataTable);

            helper.connection.Close();
            helper.connection.Dispose();
        }

        public void FillDataGridView()
        {
            this.dataGridView.DataSource = this.dataTable;
        }

        public void ClearAllBase()
        {
            txtId.Text = "";
            txtName.Text = "";
            dataTable.Clear();
            dataGridView.DataSource = null;
        }

        private void frmSocialSecurity_Load(object sender, EventArgs e)
        {
            FillDataTable();
            FillDataGridView();
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView.CurrentRow;

            if (row.Index != -1)
            {
                txtId.Text = row.Cells[0].Value.ToString();
                txtName.Text = row.Cells[1].Value.ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllBase();
            FillDataGridView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;

            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    SqlHelper helper = new SqlHelper();

                    helper.command.CommandText = "INSERT INTO SocialSecurity (Name) values(@p1)";
                    helper.command.Parameters.AddWithValue("@p1", name);

                    helper.connection.Open();

                    int changeCount = helper.command.ExecuteNonQuery();

                    if (changeCount > 0)
                    {
                        MessageBox.Show("Ekleme başarıyla yapıldı!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearAllBase();
                        FillDataTable();
                        FillDataGridView();
                    }
                    else
                    {
                        MessageBox.Show("Ops!, beklenmedik bir şey meydana geldi!, daha sonra tekrar deneyin.", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }

                    helper.connection.Close();
                    helper.connection.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
            else
            {
                MessageBox.Show("Lütfen sosyal güvence adını boş geçmeyiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string Id = txtId.Text;
            string Name = txtName.Text;

            if (!string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(Name))
            {
                SqlHelper helper = new SqlHelper();

                try
                {
                    helper.connection.Open();

                    helper.command.CommandText = "UPDATE SocialSecurity SET Name=@p1 WHERE Id=@p2";

                    helper.command.Parameters.AddWithValue("@p1", Name);
                    helper.command.Parameters.AddWithValue("@p2", Id);

                    helper.command.ExecuteNonQuery();

                    helper.connection.Close();
                    helper.connection.Dispose();

                    ClearAllBase();
                    FillDataTable();
                    FillDataGridView();

                    MessageBox.Show("Güncelleme Başarıyla Yapıldı:)", "", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }




            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek istediğiniz sigorta adına tabloda 2 defa ard arda tıklayınız ve sigorta adının boş olmadığından emin olunuz.", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string Id = txtId.Text;

            if (!string.IsNullOrEmpty(Id))
            {
                DialogResult result = MessageBox.Show("Seçili sosyal güvence kaydını silmek istediğinize emin misiniz?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        SqlHelper helper = new SqlHelper();

                        helper.command.CommandText = string.Format("DELETE FROM SocialSecurity WHERE Id={0}", Id);

                        helper.connection.Open();

                        helper.command.ExecuteNonQuery();

                        MessageBox.Show("Silme işlemi başarıyla yapıldı:)", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        helper.connection.Close();
                        helper.connection.Dispose();

                        ClearAllBase();
                        FillDataTable();
                        FillDataGridView();

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz sigorta adına tabloda 2 defa ard arda tıklayınız.", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}
