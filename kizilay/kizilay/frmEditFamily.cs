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
    public partial class frmEditFamily : Form
    {
        private IHousingManager _housingManager { get; set; }
        private ICityManager _cityManager { get; set; }
        private ITownManager _townManager { get; set; }
        private INeighborhoodManager _neighborhoodManager { get; set; }
        private IFamilyManager _familyManager { get; set; }

        public frmEditFamily(IHousingManager housingManager,
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

        private void ClearHousingList() => cmbHousingList.Items.Clear();
        private void ClearCityList() => cmbCities.Items.Clear();
        private void ClearTownList() => cmbTowns.Items.Clear();
        private void ClearNeighborhoodList() => cmbNeig.Items.Clear();


        private int GetCurrentCityId()
        {
            if (cmbCities.SelectedItem == null)
                throw new Exception("Please, select a city!");

            int cityId = ((ComboBoxItem)cmbCities.SelectedItem).Value;

            return cityId;
        }

        private int GetCurrentTownId()
        {
            if (cmbTowns.SelectedItem == null)
                throw new Exception("Please, select a town!");

            int townId = ((ComboBoxItem)cmbTowns.SelectedItem).Value;

            return townId;
        }

        private int GetCurrentNeighborhoodId()
        {
            if (cmbNeig.SelectedItem == null)
                throw new Exception("Please, select a neighborhood!");

            int neighborhoodId = ((ComboBoxItem)cmbNeig.SelectedItem).Value;

            return neighborhoodId;
        }

        private void FillNeigByTownId(int townId)
        {
            ClearNeighborhoodList();

            var allNeighborhoods = _neighborhoodManager.GetAllASCByTownId(townId);

            foreach (var item in allNeighborhoods)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(item.Name, item.Id);
                cmbNeig.Items.Add(comboBoxItem);
            }
        }

        public void FillTownsByCityId(int cityId)
        {
            ClearTownList();

            var allCities = _townManager.GetAllTownsASCByCityId(cityId);

            foreach (var item in allCities)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(item.Name, item.Id);
                cmbTowns.Items.Add(comboBoxItem);
            }
        }

        private void FillCities()
        {
            ClearCityList();

            foreach (var item in _cityManager.GetAll())
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(item.Name, item.Id);
                cmbCities.Items.Add(comboBoxItem);
            }
        }

        public void FillHousingList()
        {
            ClearHousingList();

            var allHousingTypes = _housingManager.GetAll();

            foreach (var item in allHousingTypes)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(item.Name, item.Id);
                cmbHousingList.Items.Add(comboBoxItem);
            }
        }

        private void frmEditFamily_Load(object sender, EventArgs e)
        {

        }

        private void txtTCNo_TextChanged(object sender, EventArgs e)
        {
            string txtFatherTCNo = txtTCNo.Text;

            if (txtFatherTCNo.Length == 11)
            {
                var isExists = _familyManager.FamilyExistsByFatherTCNo(txtFatherTCNo);

                if (!isExists)
                {
                    MessageBox.Show("Böyle bir aile bulunmamaktadır!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                btnUpdate.Enabled = true;
                numPersonCount.Enabled = true;
                cmbHousingList.Enabled = true;
                cmbCities.Enabled = true;
                cmbNeig.Enabled = true;
                cmbTowns.Enabled = true;
                numPriority.Enabled = true;
                rchAddress.Enabled = true;

                var familyWithAddress = _familyManager.GetFamilyWithAddressByFatherTCNo(txtFatherTCNo);


                rchAddress.Text = familyWithAddress.Address;
                numPersonCount.Value = familyWithAddress.PersonCount;
                numPriority.Value = familyWithAddress.Priority;

                FillHousingList();

                FillCities();
                FillTownsByCityId(familyWithAddress.CityId);
                FillNeigByTownId(familyWithAddress.TownId);

                foreach (ComboBoxItem city in cmbCities.Items)
                {
                    if (city.Value == familyWithAddress.CityId)
                    {
                        cmbCities.SelectedItem = city;
                        break;
                    }
                }

                foreach (ComboBoxItem town in cmbTowns.Items)
                {
                    if (town.Value == familyWithAddress.TownId)
                    {
                        cmbTowns.SelectedItem = town;
                        break;
                    }
                }

                foreach (ComboBoxItem neighborhood in cmbNeig.Items)
                {
                    if (neighborhood.Value == familyWithAddress.NeighborhoodsId)
                    {
                        cmbNeig.SelectedItem = neighborhood;
                        cmbNeig.SelectedItem = neighborhood;
                        break;
                    }
                }

                foreach (ComboBoxItem housing in cmbHousingList.Items)
                {
                    if (housing.Value == familyWithAddress.HousingId)
                    {
                        cmbHousingList.SelectedItem = housing;
                        break;
                    }
                }

            }
            else
            {
                numPersonCount.Value = 0;
                numPriority.Value = 0;
                rchAddress.Text = "";

                ClearCityList();
                ClearTownList();
                ClearNeighborhoodList();
                ClearHousingList();

                cmbCities.Enabled = false;
                cmbTowns.Enabled = false;
                cmbNeig.Enabled = false;
                cmbHousingList.Enabled = false;

                rchAddress.Enabled = false;
                numPersonCount.Enabled = false;
                numPriority.Enabled = false;
                btnUpdate.Enabled = false;

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

            Family family = new Family()
            {
                FatherNo = txtTCNo.Text,
                Address = rchAddress.Text,
                NeighborhoodsId = GetCurrentNeighborhoodId(),
                PersonCount = (int)numPersonCount.Value,
                Priority = (int)numPriority.Value,
                HousingId = ((ComboBoxItem)cmbHousingList.SelectedItem).Value
            };

            var layerResult = _familyManager.UpdateByFatherTCNo(family);

            if (layerResult.HasError())
            {
                MessageBox.Show(layerResult.Errors.FirstOrDefault(), "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                MessageBox.Show("Aile güncellemesi başarılı, yönlendiriliyorsunuz....", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                this.Dispose();
            }
        }

        private void cmbCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCities.SelectedIndex != -1)
            {
                int currentCityId = GetCurrentCityId();

                FillTownsByCityId(currentCityId);

                cmbTowns.Enabled = true;
                cmbNeig.Enabled = false;
            }
        }

        private void cmbTowns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTowns.SelectedIndex != -1)
            {
                int currentTownId = GetCurrentTownId();
                FillNeigByTownId(currentTownId);

                cmbNeig.Enabled = true;
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
