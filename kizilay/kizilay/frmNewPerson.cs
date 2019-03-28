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
        public int FatherId { get; set; }
        Dictionary<string, int> EducationalStateList;
        Dictionary<string, int> SocialSecurityList;

        public frmNewPerson()
        {
            InitializeComponent();
            EducationalStateList = new Dictionary<string, int>();
            SocialSecurityList = new Dictionary<string, int>();
        }

        public void FillEducationStateList()
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT Id,Name FROM EducationalStatus";

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EducationalStateList.Add(reader.GetString(1), reader.GetInt32(0));
                }
            }

            reader.Close();
            reader.Dispose();

            helper.connection.Close();
            helper.connection.Dispose();
        }

        public void FillSocialSecurityList()
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT Id, Name FROM SocialSecurity";

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SocialSecurityList.Add(reader.GetString(1), reader.GetInt32(0));
                }
            }

            reader.Close();
            reader.Dispose();

            helper.connection.Close();
            helper.connection.Dispose();
        }

        public void FillComboBoxEducation()
        {
            foreach (var item in EducationalStateList)
            {
                this.cmbEducationalStatus.Items.Add(item.Key);
            }
        }

        public void FillComboBoxSocialSecurity()
        {
            foreach (var item in SocialSecurityList)
            {
                this.cmbSocialSecurity.Items.Add(item.Key);
            }
        }

        public bool PersonExist(string PersonNo)
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT TC FROM Person";

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader.GetString(0) == PersonNo)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                reader.Close();
                reader.Dispose();
                helper.connection.Close();
                helper.connection.Dispose();
                return false;
            }

        }

        private void txtTcNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTcNo_TextChanged(object sender, EventArgs e)
        {
            if (txtTcNo.Text.Length == 11)
            {
                if (PersonExist(txtTcNo.Text))
                {
                    MessageBox.Show("Kişi zaten kayıtlı!", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    btnCreate.Enabled = false;
                }
            }
        }

        private void txtFatherNo_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length == 11)
            {
                SqlHelper helper = new SqlHelper();

                helper.command.CommandText = string.Format("SELECT Id FROM Family WHERE FatherNo='{0}'", ((TextBox)sender).Text);

                helper.connection.Open();

                OleDbDataReader reader = helper.command.ExecuteReader();

                if (!reader.HasRows)
                {
                    MessageBox.Show("Böyle bir aile bulunmamaktadır!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnCreate.Enabled = false;
                }
                else
                {
                    while (reader.Read())
                    {
                        FatherId = reader.GetInt32(0);
                        break;
                    }
                    btnCreate.Enabled = true;
                }

                helper.connection.Close();
                helper.connection.Dispose();
            }
            else
            {
                this.btnCreate.Enabled = false;
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

        private void chckWorking_CheckedChanged(object sender, EventArgs e)
        {
            if (chckWorking.Checked)
            {
                this.txtJob.Enabled = true;
                this.numSalary.Enabled = true;
                this.txtJob.Text = "";
            }
        }

        private void frmNewPerson_Load(object sender, EventArgs e)
        {
            FillEducationStateList();
            FillSocialSecurityList();
            FillComboBoxEducation();
            FillComboBoxSocialSecurity();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtSurname.Text))
            {
                try
                {
                    bool working = chckWorking.Checked ? true : false;
                    bool gender = chckMan.Checked ? true : false;

                    SqlHelper helper = new SqlHelper();

                    helper.command.CommandText = "INSERT INTO Person (TC,Citizenship,Name,Surname,BirthDate,PlaceOfBirth,Gender,JobState,JobDescription,Salary,MobilePhone,HomePhone,MotherName,FatherName,isMarried,EducationalStatus,SocialSecurityId,FamilyId) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p14,@p15,@p16,@p17,@p18,@p19)";

                    helper.command.Parameters.AddWithValue("@p1", txtTcNo.Text);
                    helper.command.Parameters.AddWithValue("@p2", txtCitizenship.Text);
                    helper.command.Parameters.AddWithValue("@p3", txtName.Text);
                    helper.command.Parameters.AddWithValue("@p4", txtSurname.Text);
                    helper.command.Parameters.AddWithValue("@p5", dateBirthDate.Value); //short date
                    helper.command.Parameters.AddWithValue("@p6", txtPlaceOfBirth.Text);
                    helper.command.Parameters.AddWithValue("@p7", gender);
                    helper.command.Parameters.AddWithValue("@p8", working);
                    helper.command.Parameters.AddWithValue("@p9", txtJob.Text);
                    helper.command.Parameters.AddWithValue("@p10", numSalary.Value);
                    helper.command.Parameters.AddWithValue("@p11", mskPhone.Text);
                    helper.command.Parameters.AddWithValue("@p12", mskPhone.Text);
                    helper.command.Parameters.AddWithValue("@p14", txtMotherName.Text);
                    helper.command.Parameters.AddWithValue("@p15", txtFatherName.Text);
                    helper.command.Parameters.AddWithValue("@p16", cmbMarried.SelectedItem);
                    helper.command.Parameters.AddWithValue("@p17", EducationalStateList.First(i => i.Key == cmbEducationalStatus.SelectedItem).Value);
                    helper.command.Parameters.AddWithValue("@p18", SocialSecurityList.First(i => i.Key == cmbSocialSecurity.SelectedItem).Value);
                    helper.command.Parameters.AddWithValue("@p19", FatherId);







                    helper.connection.Open();

                    helper.command.ExecuteNonQuery();

                    helper.connection.Close();
                    helper.connection.Dispose();

                    MessageBox.Show("Aile kaydı başarıyla oluşturuldu! :) \n Yönlendiriliyorsunuz...", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                    this.Dispose();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString(), "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ad, Soyad, TC, Adres gibi alanların doldurulması zorunludur:)", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            txtFatherNo.Text = txtTcNo.Text;
        }
    }
}
