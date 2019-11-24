using kizilay.Item;
using Kizilay.Business.Abstract;
using Kizilay.Entities.Concrete;
using Kizilay.Entities.Concrete.ComplexTypes;
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
    public partial class frmEditPerson : Form
    {
        private int personId { get; set; }
        public string fatherTCNo { get; set; }
        public string personTCNo { get; set; }

        private Person person;

        private IEducationalStatusManager _educationalStatusManager { get; set; }
        private ISocialSecurityManager _socialSecurityManager { get; set; }
        private IPersonManager _personManager { get; set; }
        private IFamilyManager _familyManager { get; set; }

        public frmEditPerson(IEducationalStatusManager educationalStatusManager,
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

        private void frmEditPerson_Load(object sender, EventArgs e)
        {
            person = _personManager.GetByTCNo(personTCNo);
            personId = person.Id;

            FillSocialSecurityList();
            FillEducationalStatusList();

            FillAllBase();
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

        private void FillAllBase()
        {
            txtTcNo.Text = person.TC;
            txtCitizenship.Text = person.Citizenship;
            txtName.Text = person.Name;
            txtSurname.Text = person.Surname;
            dateBirthDate.Value = person.BirthDate;
            cmbMarried.SelectedText = person.isMarried;
            numSalary.Value = person.Salary;
            rchReference.Text = person.Reference;
            txtJob.Text = person.JobDescription;
            mskPhone.Text = person.MobilePhone;
            txtMotherName.Text = person.MotherName;
            txtFatherName.Text = person.FatherName;
            txtPlaceOfBirth.Text = person.PlaceOfBirth;
            cmbMarried.SelectedItem = person.isMarried;
            chckMan.Checked = person.Gender;
            chckWomen.Checked = !person.Gender;
            chckWorking.Checked = person.JobState;
            chckNotWorking.Checked = !person.JobState;
            txtFatherNo.Text = fatherTCNo;


            foreach (ComboBoxItem item in cmbEducationalStatus.Items)
            {
                if (item.Value == person.EducationalStatus)
                {
                    cmbEducationalStatus.SelectedItem = item;
                    break;
                }
            }

            foreach (ComboBoxItem item in cmbSocialSecurity.Items)
            {
                if (item.Value == person.EducationalStatus)
                {
                    cmbSocialSecurity.SelectedItem = item;
                    break;
                }
            }

            foreach (var item in cmbMarried.Items)
            {
                if (item.ToString() == person.isMarried)
                {
                    cmbMarried.SelectedItem = item;
                }
            }
        }

        private void chckWorking_CheckedChanged(object sender, EventArgs e)
        {
            if (chckWorking.Checked)
            {
                this.txtJob.Enabled = true;
                this.numSalary.Enabled = true;
                this.txtJob.Text = "";
            }
        }

        private void chckNotWorking_CheckedChanged(object sender, EventArgs e)
        {
            if (chckNotWorking.Checked)
            {
                this.txtJob.Enabled = false;
                this.numSalary.Enabled = false;
                this.numSalary.Value = 0;
                this.txtJob.Text = "Çalışmıyor.";
            }
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
                    if (tcNo != personTCNo)
                    {
                        MessageBox.Show("Kişi zaten kayıtlı!", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        btnUpdate.Enabled = false;
                    }
                }
                else
                {
                    if (txtFatherNo.Text.Length == 11)
                        btnUpdate.Enabled = true;
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
                    btnUpdate.Enabled = false;
                }
                else
                {
                    if (txtTcNo.Text.Length == 11)
                        btnUpdate.Enabled = true;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int familyId = _familyManager.GetFamilyByFatherTCNo(txtFatherNo.Text).Id;

            Person person = new Person()
            {
                Id = personId,
                TC = txtTcNo.Text,
                Citizenship = txtCitizenship.Text,
                Name = txtName.Text,
                Surname = txtSurname.Text,
                BirthDate = dateBirthDate.Value,
                PlaceOfBirth = txtPlaceOfBirth.Text,
                Gender = chckMan.Checked,
                JobState = chckWorking.Checked,
                Reference = rchReference.Text,
                JobDescription = txtJob.Text,
                Salary = numSalary.Value,
                MobilePhone = mskPhone.Text,
                HomePhone = mskPhone.Text,
                MotherName = txtMotherName.Text,
                FatherName = txtFatherName.Text,
                isMarried = cmbMarried.SelectedItem.ToString(),
                EducationalStatus = GetCurrentEducationalStatusId(),
                SocialSecurityId = GetCurrentSocialSecurityId(),
                FamilyId = familyId
            };

            FamilyUpdateModel model = new FamilyUpdateModel();

            model.Person = person;
            model.LastTCNo = personTCNo;
            model.FatherTCNo = txtFatherNo.Text;

            var layerResult = _personManager.Update(model);

            if (layerResult.HasError())
            {
                string firstError = layerResult.Errors.FirstOrDefault();
                MessageBox.Show(firstError, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else
            {
                MessageBox.Show("Düzenleme işlemi başarılı, yönlendiriliyorsunuz..", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.Yes;
                this.Close();
                this.Dispose();
            }


            //if (FatherTC == personTC)
            //{
            //    helper.command = helper.connection.CreateCommand();

            //    int familyId = GetFamilyId(personTC);

            //    helper.command.CommandText = "UPDATE Family SET FatherNo=@p1 WHERE Id=@p2";

            //    helper.command.Parameters.AddWithValue("@p1", txtTcNo.Text);
            //    helper.command.Parameters.AddWithValue("@p2", familyId);

            //    helper.command.ExecuteReader();
            //}

        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            txtFatherNo.Text = txtTcNo.Text;
        }
    }
}
