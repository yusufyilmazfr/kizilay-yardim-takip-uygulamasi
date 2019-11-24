using Kizilay.Business.Abstract;
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
    public partial class frmEducationalStatus : Form
    {
        private IEducationalStatusManager _educationalStatusManager { get; set; }

        public frmEducationalStatus(IEducationalStatusManager educationalStatusManager)
        {
            _educationalStatusManager = educationalStatusManager;

            InitializeComponent();
        }

        public void FillDataGridView()
        {
            dataGridView.DataSource = _educationalStatusManager.GetAll();
        }

        public void ClearAllBase()
        {
            txtId.Text = "";
            txtName.Text = "";
        }

        private void frmEducationalStatus_Load(object sender, EventArgs e)
        {
            FillDataGridView();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string Id = txtId.Text;

            if (String.IsNullOrEmpty(Id))
                return;

            DialogResult dialogResult = MessageBox.Show("Seçili eğitim türü kaydını silmek istediğinize emin misiniz?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                int id = Convert.ToInt32(Id);

                var result = _educationalStatusManager.RemoveById(id);

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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtId.Text))
                return;

            EducationalStatus educationalStatus = new EducationalStatus
            {
                Id = Convert.ToInt32(txtId.Text),
                Name = txtName.Text
            };

            var result = _educationalStatusManager.Update(educationalStatus);

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EducationalStatus educationalStatus = new EducationalStatus
            {
                Name = txtName.Text
            };

            var result = _educationalStatusManager.Add(educationalStatus);

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
    }
}
