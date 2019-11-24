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
    public partial class frmNewPerson : Form
    {
        private IEducationalStatusManager _educationalStatusManager { get; set; }
        private ISocialSecurityManager _socialSecurityManager { get; set; }
        private IPersonManager _personManager { get; set; }
        private IFamilyManager _familyManager { get; set; }

        public frmNewPerson(IEducationalStatusManager educationalStatusManager,
            ISocialSecurityManager socialSecurityManager,
            IPersonManager personManager,
            IFamilyManager familyManager)
        {
            _educationalStatusManager = educationalStatusManager;
            _socialSecurityManager = socialSecurityManager;
            _personManager = personManager;
            _familyManager = familyManager;

            InitializeComponent();
        }



        private void frmNewPerson_Load(object sender, EventArgs e)
        {
            FillEducationalStatusList();
            FillSocialSecurityList();
        }

        public void FillEducationalStatusList()
        {
            var educationalStatusList = _educationalStatusManager.GetAll();

            foreach (var item in educationalStatusList)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(item.Name, item.Id);
                cmbEducationalStatus.Items.Add(comboBoxItem);
            }
        }

        public int GetCurrentEducationalStatusId()
        {
            return ((ComboBoxItem)cmbEducationalStatus.SelectedItem).Value;
        }

        public void FillSocialSecurityList()
        {
            var allSocialSecurityList = _socialSecurityManager.GetAll();

            foreach (var item in allSocialSecurityList)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem(item.Name, item.Id);
                cmbSocialSecurity.Items.Add(comboBoxItem);
            }
        }

        public int GetCurrentSocialSecurityId()
        {
            return ((ComboBoxItem)cmbSocialSecurity.SelectedItem).Value;
        }

        private void txtTcNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void txtTcNo_TextChanged(object sender, EventArgs e)
        {
            string tcNo = txtTcNo.Text;

            if (tcNo.Length == 11)
            {
                bool personExists = _personManager.PersonExistsByTCNo(tcNo);

                if (personExists)
                {
                    MessageBox.Show("Kişi zaten kayıtlı!", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    btnCreate.Enabled = false;
                }
                else
                {
                    if (txtFatherNo.Text.Length == 11)
                        btnCreate.Enabled = true;
                }
            }
        }

        private void txtFatherNo_TextChanged(object sender, EventArgs e)
        {
            string fatherTCNo = txtFatherNo.Text;

            if (fatherTCNo.Length == 11)
            {
                bool familyIsExists = _familyManager.FamilyExistsByFatherTCNo(fatherTCNo);

                if (!familyIsExists)
                {
                    MessageBox.Show("Böyle bir aile bulunmamaktadır!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnCreate.Enabled = false;
                }
                else
                {
                    if (txtTcNo.Text.Length == 11)
                        btnCreate.Enabled = true;
                }
            }
        }

        private void chckNotWorking_CheckedChanged(object sender, EventArgs e)
        {
            if (chckNotWorking.Checked)
            {
                txtJob.Enabled = false;
                numSalary.Enabled = false;
                numSalary.Value = 0;
                txtJob.Text = "Çalışmıyor.";
            }
        }

        private void chckWorking_CheckedChanged(object sender, EventArgs e)
        {
            if (chckWorking.Checked)
            {
                txtJob.Enabled = true;
                numSalary.Enabled = true;
                txtJob.Text = "";
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            int familyId = _familyManager.GetFamilyByFatherTCNo(txtFatherNo.Text).Id;

            Person person = new Person()
            {
                TC = txtTcNo.Text,
                Citizenship = txtCitizenship.Text,
                Name = txtName.Text,
                Surname = txtSurname.Text,
                BirthDate = dateBirthDate.Value,
                PlaceOfBirth = txtPlaceOfBirth.Text,
                Gender = chckMan.Checked,
                JobState = chckWorking.Checked,
                JobDescription = txtJob.Text,
                Salary = numSalary.Value,
                Reference = rchReference.Text,
                MobilePhone = mskPhone.Text,
                HomePhone = mskPhone.Text,
                MotherName = txtMotherName.Text,
                FatherName = txtFatherName.Text,
                isMarried = cmbMarried.SelectedItem.ToString(),
                EducationalStatus = GetCurrentEducationalStatusId(),
                SocialSecurityId = GetCurrentSocialSecurityId(),
                FamilyId = familyId
            };

            var layerResult = _personManager.Add(person);

            if (layerResult.HasError())
            {
                string firstError = layerResult.Errors.FirstOrDefault();
                MessageBox.Show(firstError, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            MessageBox.Show("Aile kaydı başarıyla oluşturuldu! :) \n Yönlendiriliyorsunuz...", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
            this.Dispose();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            txtFatherNo.Text = txtTcNo.Text;
        }
    }
}
