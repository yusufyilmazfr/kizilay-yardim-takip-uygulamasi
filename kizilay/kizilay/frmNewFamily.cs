using kizilay.Item;
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
    public partial class frmNewFamily : Form
    {
        private IHousingManager _housingManager { get; set; }
        private ICityManager _cityManager { get; set; }
        private ITownManager _townManager { get; set; }
        private INeighborhoodManager _neighborhoodManager { get; set; }
        private IFamilyManager _familyManager { get; set; }

        public frmNewFamily(IHousingManager housingManager,
            ICityManager cityManager,
            ITownManager townManager,
            INeighborhoodManager neighborhoodManager,
            IFamilyManager familyManager)
        {
            _housingManager = housingManager;
            _cityManager = cityManager;
            _townManager = townManager;
            _neighborhoodManager = neighborhoodManager;
            _familyManager = familyManager;

            InitializeComponent();
        }

        private void FillHousingList()
        {
            foreach (var item in _housingManager.GetAll())
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(item.Name, item.Id);
                cmbHousingList.Items.Add(comboBoxItem);
            }
        }

        private void FillCities()
        {
            foreach (var item in _cityManager.GetAll())
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(item.Name, item.Id);
                cmbCities.Items.Add(comboBoxItem);
            }
        }

        private void FillNeighborhoods()
        {
            int townId = ((ComboBoxItem)cmbTowns.SelectedItem).Value;

            foreach (var item in _neighborhoodManager.GetAllASCByTownId(townId))
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(item.Name, item.Id);
                cmbNeig.Items.Add(comboBoxItem);
            }
        }

        public void FillTowns()
        {
            int cityId = ((ComboBoxItem)cmbCities.SelectedItem).Value;

            foreach (var item in _townManager.GetAllTownsASCByCityId(cityId))
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(item.Name, item.Id);
                cmbTowns.Items.Add(comboBoxItem);
            }
        }

        private void frmNewFamily_Load(object sender, EventArgs e)
        {
            FillHousingList();
        }

        private void txtTCNo_TextChanged(object sender, EventArgs e)
        {
            if (txtTCNo.Text.Length == 11)
            {
                bool isExists = _familyManager.FamilyExistsByFatherTCNo(txtTCNo.Text);

                if (isExists)
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
                    numPriority.Enabled = true;
                }
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
            Family family = new Family()
            {
                FatherNo = txtTCNo.Text,
                HousingId = ((ComboBoxItem)cmbHousingList.SelectedItem).Value,
                Address = rchAddress.Text,
                PersonCount = (int)numPersonCount.Value,
                NeighborhoodsId = ((ComboBoxItem)cmbNeig.SelectedItem).Value,
                Priority = (int)numPriority.Value
            };

            int resultCount = _familyManager.Add(family);


            if (resultCount > 0)
            {
                MessageBox.Show("Başarıyla oluştu. Ana ekrana yönlendiriliyorsunuz.. :)", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
                this.Dispose();
            }
            else
                MessageBox.Show("Kayıt Eklenemedi!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                cmbNeig.Items.Clear();
                cmbTowns.Enabled = true;

                FillTowns();
            }
        }

        private void cmbTowns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTowns.SelectedIndex != -1)
            {
                cmbNeig.Items.Clear();
                cmbNeig.Enabled = true;

                FillNeighborhoods();
            }
        }

        private void cmbNeig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNeig.SelectedIndex != -1)
            {
                rchAddress.Enabled = true;
                btnCreate.Enabled = true;
            }
            else
            {
                btnCreate.Enabled = false;
                rchAddress.Enabled = false;
            }
        }
    }
}
