using kizilay.DependencyResolver.Ninject;
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
    public partial class frmNewDonate : Form
    {
        private IDonationManager _donationManager { get; set; }
        private IPersonDonationManager _personDonationManager { get; set; }
        private IPersonManager _personManager { get; set; }
        private IFamilyManager _familyManager { get; set; }


        public frmNewDonate(IPersonDonationManager personDonationManager,
            IDonationManager donationManager,
            IPersonManager personManager,
            IFamilyManager familyManager)
        {
            _personDonationManager = personDonationManager;
            _donationManager = donationManager;
            _personManager = personManager;
            _familyManager = familyManager;

            InitializeComponent();
        }

        private void frmNewDonate_Load(object sender, EventArgs e)
        {
            FillDonationTypeList();
        }

        public void FillDonationTypeList()
        {
            var allParentDonationList = _donationManager.GetAll().Where(i => i.DonationID == 0);

            foreach (Donation item in allParentDonationList)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(item.Name, item.Id);
                cmbDonateType.Items.Add(comboBoxItem);
            }
        }

        public int GetCurrentDonationTypeId()
        {
            return ((ComboBoxItem)cmbDonateType.SelectedItem).Value;
        }

        public void ClearDonationNameList()
        {
            cmbDonateName.Items.Clear();
        }

        public void FillDonationNameListByParentId(int parentId)
        {
            var donationNameList = _donationManager.GetAll().Where(i => i.DonationID == parentId);

            foreach (Donation item in donationNameList)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(item.Name, item.Id);
                cmbDonateName.Items.Add(comboBoxItem);
            }
        }

        public DataTable GetAllPerson(string TC)
        {
            SqlHelper helper = new SqlHelper();
            helper.command.CommandText = string.Format("SELECT * FROM Person WHERE TC='{0}'", TC);

            helper.connection.Open();

            DataTable dataTable = new DataTable();

            OleDbDataAdapter adapter = new OleDbDataAdapter(helper.command);

            adapter.Fill(dataTable);

            helper.connection.Close();
            helper.connection.Dispose();

            return dataTable;
        }

        public DataTable GetAllPerson(int FamilyId)
        {

            SqlHelper helper = new SqlHelper();

            string commandText = string.Format("SELECT * FROM ((Person as P " +
                "INNER JOIN Family as F ON P.FamilyId = F.Id) " +
                " INNER JOIN Person_Donation as PD ON PD.PersonId = P.Id) " +
                "INNER JOIN Donation as D ON PD.DonationId = D.Id WHERE P.FamilyId={0}", FamilyId);

            helper.command.CommandText = commandText;

            helper.connection.Open();

            DataTable dataTable = new DataTable();

            OleDbDataAdapter adapter = new OleDbDataAdapter(helper.command);

            adapter.Fill(dataTable);

            helper.connection.Close();
            helper.connection.Dispose();

            return dataTable;
        }

        public int GetFamilyId(string TC)
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = string.Format("SELECT FamilyId FROM Person WHERE TC='{0}'", TC);

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            int FamilyId = 0;

            while (reader.Read())
            {
                FamilyId = reader.GetInt32(0);
            }

            helper.connection.Close();
            helper.connection.Dispose();

            reader.Close();
            reader.Dispose();

            return FamilyId;
        }

        private void txtTcNo_TextChanged(object sender, EventArgs e)
        {
            string personTCNo = txtTcNo.Text;

            if (personTCNo.Length == 11)
            {
                bool isExists = _personManager.PersonExistsByTCNo(personTCNo);

                if (!isExists)
                {
                    MessageBox.Show("Böyle bir personel bulunmamaktadır...", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                int familyId = _personManager.GetByTCNo(personTCNo).FamilyId;

                bool hasDonation = _familyManager.FamilyHasDonationInRangeDayByFamilyId(familyId, 365);

                if (hasDonation)
                {
                    DialogResult result = MessageBox.Show("Son 1 yıl içerisinde bu aile yardım almıştır. Aileye ait yardım kayıtlarını görmek ister misiniz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        var donationProcess = (frmFamilyDonationProcess)FormDependencyResolver.Resolve<frmFamilyDonationProcess>();

                        //this base will be refactor!!

                        donationProcess.familyId = _personManager.GetByTCNo(personTCNo).FamilyId;

                        donationProcess.Show();
                    }
                }

                btnCreate.Enabled = true;
            }
        }

        private void txtTcNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmbDonateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearDonationNameList();

            int donationTypeId = GetCurrentDonationTypeId();

            FillDonationNameListByParentId(donationTypeId);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (cmbDonateType.SelectedItem == null || cmbDonateName.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir bağış seçiniz...", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Person_Donation donation = new Person_Donation
            {
                Description = rchDescription.Text,
                DonationId = GetCurrentDonationNameId(),
                AddedDate = dtPicker.Value.Date,
                PersonId = _personManager.GetByTCNo(txtTcNo.Text).Id
            };


            var layerResult = _personDonationManager.Add(donation);

            if (!layerResult.HasError())
            {
                MessageBox.Show("Yardım işlemi başarılı bir şekilde yapıldı :), yönlendiriliyorsunuz...", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.Yes;

                this.Hide();
                this.Dispose();
            }
            else
            {
                var firstError = layerResult.Errors.FirstOrDefault();
                MessageBox.Show(firstError, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private int GetCurrentDonationNameId()
        {
            return ((ComboBoxItem)cmbDonateName.SelectedItem).Value;
        }
    }
}
