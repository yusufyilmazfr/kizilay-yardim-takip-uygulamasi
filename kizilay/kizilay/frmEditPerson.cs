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
        Dictionary<string, int> EducationalStateList;
        Dictionary<string, int> SocialSecurityList;

        public string personTC { get; set; }
        public string FatherTC { get; set; }
        public int newFamilyId { get; set; }


        private void GetPersonInformation()
        {
            SqlHelper helper = new SqlHelper();

        }

        private void FillAllBase()
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = string.Format("SELECT * FROM Person WHERE TC='{0}'", personTC);

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            while (reader.Read())
            {
                txtTcNo.Text = reader["TC"].ToString();
                txtCitizenship.Text = reader["Citizenship"].ToString();
                txtName.Text = reader["Name"].ToString();
                txtSurname.Text = reader["Surname"].ToString();
                dateBirthDate.Value = (DateTime)reader["BirthDate"];
                cmbMarried.SelectedValue = reader["Gender"];
                numSalary.Value = (decimal)reader["Salary"];
                txtJob.Text = reader["JobDescription"].ToString();
                mskPhone.Text = reader["MobilePhone"].ToString();
                txtMotherName.Text = reader["MotherName"].ToString();
                txtFatherName.Text = reader["FatherName"].ToString();
                txtPlaceOfBirth.Text = reader["PlaceOfBirth"].ToString();
                cmbMarried.SelectedItem = reader["isMarried"].ToString();

                chckMan.Checked = (bool)reader["Gender"] == true;
                chckWomen.Checked = (bool)reader["Gender"] == false;

                chckWorking.Checked = (bool)reader["JobState"] == true;
                chckNotWorking.Checked = (bool)reader["JobState"] == false;

                txtFatherNo.Text = FatherTC;
            }

            helper.connection.Close();
            helper.connection.Dispose();
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

        public frmEditPerson()
        {
            InitializeComponent();

            EducationalStateList = new Dictionary<string, int>();
            SocialSecurityList = new Dictionary<string, int>();

            GetPersonInformation();
        }

        private void frmEditPerson_Load(object sender, EventArgs e)
        {
            FillEducationStateList();
            FillSocialSecurityList();
            FillComboBoxEducation();
            FillComboBoxSocialSecurity();
            FillAllBase();
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
                    btnUpdate.Enabled = false;
                }
                else
                {
                    while (reader.Read())
                    {
                        newFamilyId = reader.GetInt32(0);
                        btnUpdate.Enabled = true;
                        break;
                    }
                }

                reader.Close();
                reader.Dispose();
                helper.connection.Close();
                helper.connection.Dispose();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtSurname.Text))
            {
                try
                {
                    bool working = chckWorking.Checked ? true : false;
                    bool gender = chckMan.Checked ? true : false;

                    SqlHelper helper = new SqlHelper();

                    helper.command.CommandText = "UPDATE Person SET TC=@p1,Citizenship=@p2,Name=@p3,Surname=@p4,BirthDate=@p5,PlaceOfBirth=@p6,Gender = @p7,JobState = @p8,JobDescription = @p9,Salary = @p10,MobilePhone = @p11,HomePhone = @p12,MotherName = @p14,FatherName = @p15,isMarried = @p16,EducationalStatus = @p17,SocialSecurityId = @p18,FamilyId = @p19 WHERE TC=@p20";

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
                    helper.command.Parameters.AddWithValue("@p19", newFamilyId);
                    helper.command.Parameters.AddWithValue("@p20", personTC);


                    helper.connection.Open();

                    helper.command.ExecuteNonQuery();

                    if (FatherTC == personTC)
                    {
                        helper.command = helper.connection.CreateCommand();

                        int familyId = GetFamilyId(personTC);

                        helper.command.CommandText = "UPDATE Family SET FatherNo=@p1 WHERE Id=@p2";

                        helper.command.Parameters.AddWithValue("@p1", txtTcNo.Text);
                        helper.command.Parameters.AddWithValue("@p2", familyId);

                        helper.command.ExecuteReader();
                    }

                    helper.connection.Close();
                    helper.connection.Dispose();

                    MessageBox.Show("Güncelleme Başarılı...", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.Yes;
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

        private int GetFamilyId(string personTC)
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT Id FROM Family WHERE FatherNo=@p1";

            helper.command.Parameters.AddWithValue("@p1", personTC);

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            int familyId = 0;

            while (reader.Read())
            {
                familyId = Convert.ToInt32(reader[0]);
                break;
            }
            reader.Close();
            reader.Dispose()
                ;
            helper.connection.Close();
            helper.connection.Dispose();

            return familyId;
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            txtFatherNo.Text = txtTcNo.Text;
        }
    }
}
