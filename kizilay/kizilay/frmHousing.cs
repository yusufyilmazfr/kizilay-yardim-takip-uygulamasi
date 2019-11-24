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
    public partial class frmHousing : Form
    {
        private IHousingManager _housingManager { get; set; }

        public frmHousing(IHousingManager housingManager)
        {
            _housingManager = housingManager;

            InitializeComponent();
        }

        public void FillDataGridView()
        {
            dataGridView.DataSource = _housingManager.GetAll();
        }

        public void ClearAllBase()
        {
            txtId.Text = "";
            txtName.Text = "";
        }

        private void frmHousing_Load(object sender, EventArgs e)
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
            FillDataGridView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string Id = txtId.Text;

            if (String.IsNullOrEmpty(Id))
                return;

            DialogResult dialogResult = MessageBox.Show("Seçili ev türü kaydını silmek istediğinize emin misiniz?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                int id = Convert.ToInt32(Id);

                var result = _housingManager.RemoveById(id);

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

            Housing housing = new Housing
            {
                Id = Convert.ToInt32(txtId.Text),
                Name = txtName.Text
            };

            var result = _housingManager.Update(housing);

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
            Housing housing = new Housing
            {
                Name = txtName.Text
            };

            var result = _housingManager.Add(housing);

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
