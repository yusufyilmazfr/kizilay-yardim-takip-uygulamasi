using Kizilay.Business.Abstract;
using Kizilay.Business.Concrete.Result;
using Kizilay.Entities.Concrete;
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
        private ISocialSecurityManager _socialSecurityManager { get; set; }

        public frmSocialSecurity(ISocialSecurityManager socialSecurityManager)
        {
            _socialSecurityManager = socialSecurityManager;

            InitializeComponent();
        }

        private void frmSocialSecurity_Load(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        public void FillDataGridView()
        {
            var allList = _socialSecurityManager.GetAll();
            dataGridView.DataSource = allList;
        }

        public void ClearAllBase()
        {
            txtId.Text = "";
            txtName.Text = "";
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView.CurrentRow;

            if (row.Index != -1)
            {
                txtId.Text = row.Cells["Id"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllBase();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SocialSecurity socialSecurity = new SocialSecurity
            {
                Name = txtName.Text
            };

            var result = _socialSecurityManager.Add(socialSecurity);

            if (result.HasError())
            {
                string firstError = result.Errors.FirstOrDefault();
                MessageBox.Show(firstError, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                MessageBox.Show("Ekleme başarıyla yapıldı!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearAllBase();
                FillDataGridView();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtId.Text))
                return;

            SocialSecurity socialSecurity = new SocialSecurity
            {
                Id = Convert.ToInt32(txtId.Text),
                Name = txtName.Text
            };

            var result = _socialSecurityManager.Update(socialSecurity);

            if (result.HasError())
            {
                string firstError = result.Errors.FirstOrDefault();

                MessageBox.Show(firstError, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                ClearAllBase();
                FillDataGridView();

                MessageBox.Show("Güncelleme Başarıyla Yapıldı:)", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string Id = txtId.Text;

            if (String.IsNullOrEmpty(Id))
                return;

            DialogResult dialogResult = MessageBox.Show("Seçili sosyal güvence kaydını silmek istediğinize emin misiniz?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                int id = Convert.ToInt32(Id);
                var result = _socialSecurityManager.RemoveById(id);

                if (result.HasError())
                {
                    string firstError = result.Errors.FirstOrDefault();

                    MessageBox.Show(firstError, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    MessageBox.Show("Silme işlemi başarıyla yapıldı:)", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearAllBase();
                    FillDataGridView();
                }
            }
        }
    }
}