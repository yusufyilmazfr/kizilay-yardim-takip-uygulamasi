using kizilay.DependencyResolver.Ninject;
using kizilay.Item;
using Kizilay.Business.Abstract;
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
    public partial class frmAllFamily : Form
    {
        private ICityManager _cityManager { get; set; }
        private ITownManager _townManager { get; set; }
        private INeighborhoodManager _neighborhoodManager { get; set; }
        private IPersonManager _personManager { get; set; }
        private IFamilyManager _familyManager;

        public frmAllFamily(ICityManager cityManager,
            ITownManager townManager,
            INeighborhoodManager neighborhoodManager,
            IPersonManager personManager,
            IFamilyManager familyManager)
        {
            _cityManager = cityManager;
            _townManager = townManager;
            _neighborhoodManager = neighborhoodManager;
            _personManager = personManager;
            _familyManager = familyManager;

            InitializeComponent();

            FillAllBase();
            FillCities();
        }

        public List<AllPersonModel> allPerson { get; set; }

        public string fullAddress { get; set; } = "";

        public List<AllPersonModel> GetAllPerson()
        {
            SqlHelper helper = new SqlHelper();


            helper.command.CommandText = "SELECT " +
                "P.TC," +
                "P.Name," +
                "P.Surname," +
                "P.BirthDate," +
                "C.Name as CityName," +
                "T.Name as TownName," +
                "N.Name as NeighborhoodsName," +
                "F.Address," +
                "P.JobDescription," +
                "P.MobilePhone," +
                "F.Priority," +
                "F.FatherNo as FatherNo " +
                "FROM  (" +
                "         (" +
                "            (" +
                "               (" +
                "                   Person as P " +
                "                       INNER JOIN Family as F ON F.Id = P.FamilyId) " +
                "                           INNER JOIN Neighborhoods as N ON N.Id = F.NeighborhoodsId) " +
                "                               INNER JOIN Towns as T ON T.Id = N.TownId) " +
                "                                   INNER JOIN Cities as C ON C.Id = T.CityId)" +
                "                                               ORDER By F.FatherNo";

            helper.connection.Open();


            OleDbDataReader reader = helper.command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    allPerson.Add(new AllPersonModel()
                    {
                        TC = reader["TC"].ToString(),
                        Name = reader["Name"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        BirthDate = reader["BirthDate"].ToString(),
                        Phone = reader["MobilePhone"].ToString(),
                        JobDescription = reader["JobDescription"].ToString(),
                        familyPriority = Convert.ToInt32(reader["Priority"]),
                        FatherTC = reader["FatherNo"].ToString(),
                        Address = (reader["CityName"].ToString() + " " + reader["TownName"].ToString() + " " + reader["NeighborhoodsName"].ToString() + " " + reader["Address"].ToString()).ToUpper()
                    });
                }
            }

            helper.connection.Close();
            helper.connection.Dispose();

            return allPerson;

        }

        private void FillCities()
        {
            var allCities = _cityManager.GetAll();

            foreach (var city in allCities)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(city.Name, city.Id);
                cmbCities.Items.Add(comboBoxItem);
            }
        }

        private int GetCurrentCityId()
        {
            return ((ComboBoxItem)cmbCities.SelectedItem).Value;
        }

        public void FillTownsByCityId(int cityId)
        {
            var allTowns = _townManager.GetAllTownsASCByCityId(cityId);

            foreach (var item in allTowns)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(item.Name, item.Id);
                cmbTowns.Items.Add(comboBoxItem);
            }
        }

        private int GetCurrentTownId()
        {
            return ((ComboBoxItem)cmbTowns.SelectedItem).Value;
        }

        private void RemoveTowsList()
        {
            cmbTowns.Items.Clear();
        }

        private void FillNeighborhoodsByTownId(int townId)
        {
            var allNeighborhoods = _neighborhoodManager.GetAllASCByTownId(townId);

            foreach (var item in allNeighborhoods)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(item.Name, item.Id);
                cmbNeig.Items.Add(comboBoxItem);
            }
        }

        private int GetCurrentNeighborhoodsId()
        {
            return ((ComboBoxItem)cmbNeig.SelectedItem).Value;
        }

        private void RemoveNeighborhoodList()
        {
            cmbNeig.Items.Clear();
        }

        public void FillAllBase()
        {
            allPerson = new List<AllPersonModel>();
            dataGridView1.DataSource = GetAllPerson();
        }

        private void btnShowDonateProcess_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index == -1)
            {
                MessageBox.Show("Lütfen bir satır seçiniz...");
                return;
            }

            string FatherNo = dataGridView1.CurrentRow.Cells["FatherTC"].Value.ToString();

            var familyDonationProcess = (frmFamilyDonationProcess)FormDependencyResolver.Resolve<frmFamilyDonationProcess>();

            familyDonationProcess.familyId = _familyManager.GetFamilyByFatherTCNo(FatherNo).Id;

            familyDonationProcess.Show();
        }

        private void btnRemovePerson_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index == -1)
            {
                MessageBox.Show("Lütfen bir satır seçiniz...");
                return;
            }

            DialogResult result = MessageBox.Show("Kayıtlı kullanıcıyı silmek istediğinize emin misiniz?", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.No)
                return;

            string personTCNo = dataGridView1.CurrentRow.Cells["TC"].Value.ToString();

            var layerResult = _personManager.RemoveByTCNo(personTCNo);

            if (layerResult.HasError())
            {
                string firstError = layerResult.Errors.FirstOrDefault();
                MessageBox.Show(firstError, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                FillAllBase();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string filterText = string.IsNullOrEmpty(txtTC.Text) ? "" : txtTC.Text;
            string fatherTC = string.IsNullOrEmpty(txtFatherTC.Text) ? "" : txtFatherTC.Text;
            string fullName = string.IsNullOrEmpty(txtFullName.Text) ? "" : txtFullName.Text;


            if (cmbFamilyPriority.SelectedItem == null)
            {
                dataGridView1.DataSource = allPerson
                    .Where(i => i.TC.Contains(filterText) &&
                                i.FatherTC.Contains(fatherTC) &&
                                (i.Name + ' ' + i.Surname).ToLower().Contains(fullName.ToLower()) &&
                                i.Address.Contains(fullAddress.ToUpper())
                            )
                    .ToList();

            }
            else
            {
                int priority = Convert.ToInt32(cmbFamilyPriority.SelectedItem.ToString());

                dataGridView1.DataSource = allPerson
                    .Where(i => i.TC.Contains(filterText) &&
                                i.FatherTC.Contains(fatherTC) &&
                                (i.Name + ' ' + i.Surname).ToLower().Contains(fullName.ToLower()) &&
                                i.Address.Contains(fullAddress.ToUpper()) &&
                                i.familyPriority == priority
                            )
                    .ToList();
            }
        }

        private void btnEditPerson_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index == -1)
            {
                MessageBox.Show("Lütfen bir satır seçiniz...");
                return;
            }

            var editPerson = (frmEditPerson)FormDependencyResolver.Resolve<frmEditPerson>();

            editPerson.fatherTCNo = dataGridView1.CurrentRow.Cells["FatherTC"].Value.ToString();
            editPerson.personTCNo = dataGridView1.CurrentRow.Cells["TC"].Value.ToString();

            if (editPerson.ShowDialog() == DialogResult.Yes)
            {
                allPerson.Clear();
                FillAllBase();
            }
        }

        private void btnNewPerson_Click(object sender, EventArgs e)
        {
            FormDependencyResolver.Resolve<frmNewPerson>().Show();
        }

        private void cmbCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currentCityId = GetCurrentCityId();

            RemoveTowsList();
            FillTownsByCityId(currentCityId);

            fullAddress = string.Empty;

            fullAddress += cmbCities.SelectedItem.ToString();

            textBox1_TextChanged(null, null);
        }

        private void cmbTowns_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currentTownId = GetCurrentTownId();

            RemoveNeighborhoodList();
            FillNeighborhoodsByTownId(currentTownId);


            cmbNeig.Enabled = true;

            fullAddress = cmbCities.SelectedItem + " " + cmbTowns.SelectedItem;

            textBox1_TextChanged(null, null);
        }

        private void cmbNeig_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAddress.Enabled = true;

            fullAddress = cmbCities.SelectedItem + " " + cmbTowns.SelectedItem + " " + cmbNeig.SelectedItem;

            textBox1_TextChanged(null, null);
        }

        private void btnHasDonatePerson_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtDonateContent.Text))
            {
                FillAllBase();
                return;
            }

            SqlHelper helper = new SqlHelper();


            helper.command.CommandText = "SELECT " +
               "P.TC," +
               "P.Name," +
               "P.Surname," +
               "P.BirthDate," +
               "C.Name as CityName," +
               "T.Name as TownName," +
               "N.Name as NeighborhoodsName," +
               "F.Address," +
               "P.JobDescription," +
               "P.MobilePhone," +
               "F.Priority," +
               "F.FatherNo as FatherNo " +
               "FROM  (" +
               "         (" +
               "            (" +
               "                (" +
               "                   (" +
               "                       (" +
               "                           Person as P " +
               "                               INNER JOIN Family as F ON F.Id = P.FamilyId) " +
               "                                   INNER JOIN Neighborhoods as N ON N.Id = F.NeighborhoodsId) " +
               "                                       INNER JOIN Towns as T ON T.Id = N.TownId) " +
               "                                           INNER JOIN Cities as C ON C.Id = T.CityId) " +
               "                                               INNER JOIN Person_Donation as PD ON P.Id = PD.PersonId) " +
               "                                                   INNER JOIN Donation as D ON PD.DonationId = D.Id) ";

            helper.command.CommandText += "WHERE [PD.Description] LIKE '%" + txtDonateContent.Text + "%'";



            helper.connection.Open();


            OleDbDataReader reader = helper.command.ExecuteReader();

            allPerson = null;

            allPerson = new List<AllPersonModel>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    allPerson.Add(new AllPersonModel()
                    {
                        TC = reader["TC"].ToString(),
                        Name = reader["Name"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        BirthDate = reader["BirthDate"].ToString(),
                        Phone = reader["MobilePhone"].ToString(),
                        JobDescription = reader["JobDescription"].ToString(),
                        familyPriority = Convert.ToInt32(reader["Priority"]),
                        FatherTC = reader["FatherNo"].ToString(),
                        Address = (reader["CityName"].ToString() + " " + reader["TownName"].ToString() + " " + reader["NeighborhoodsName"].ToString() + " " + reader["Address"].ToString()).ToUpper()
                    });
                }
            }

            helper.connection.Close();
            helper.connection.Dispose();

            dataGridView1.DataSource = allPerson;

        }
    }
}
