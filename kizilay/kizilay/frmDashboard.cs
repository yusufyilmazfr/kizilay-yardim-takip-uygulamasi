using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using Excel = Microsoft.Office.Interop.Excel;

namespace kizilay
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        public System.Data.DataTable dataTable { get; set; }
        public Thread thread { get; set; }

        public Dictionary<int, string> educationalStatusList { get; set; }
        public Dictionary<int, string> socialSecurityList { get; set; }
        public Dictionary<int, string> homeTypeList { get; set; }


        public void StartImport()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.Yes || result == DialogResult.OK)
            {
                FillSocialSecurityList();
                FillEducationalStatusList();

                DataTable list = new ReadExcel(fileDialog.FileName).ReadXml();

                int counter = 0;

                for (int i = 0; i < list.Rows.Count; i++)
                {
                    try
                    {
                        if (!PersonExist(list.Rows[i]["TC"].ToString()))
                        {
                            int familyId = GetFamilyId(list.Rows[i]["FatherNo"].ToString());

                            if (familyId == 0)
                            {
                                if (!CreateNewFamily(list.Rows[i]["FatherNo"].ToString()))
                                {
                                    break;
                                }
                            }

                            familyId = GetFamilyId(list.Rows[i]["FatherNo"].ToString());

                            PersonModel model = new PersonModel();

                            bool jobstate = list.Rows[i]["JobState"].ToString().ToLower() == "çalışmıyor" ? false : true;
                            bool gender = list.Rows[i]["Gender"].ToString().ToLower() == "erkek" ? true : false;

                            int educationalStatusId = educationalStatusList.Values.Contains(list.Rows[i]["EducationalStatusName"].ToString()) ? educationalStatusList.Where(k => k.Value == list.Rows[i]["EducationalStatusName"].ToString()).Select(k => k.Key).FirstOrDefault() : educationalStatusList.Where(k => k.Value.ToLower() == "ilköğretim").Select(k => k.Key).FirstOrDefault();

                            int socialSecurityId = socialSecurityList.Values.Contains(list.Rows[i]["SocialSecurityName"].ToString()) ? socialSecurityList.Where(k => k.Value == list.Rows[i]["SocialSecurityName"].ToString()).Select(k => k.Key).FirstOrDefault() : socialSecurityList.Where(k => k.Value.ToLower() == "yok").Select(k => k.Key).FirstOrDefault();


                            model = new PersonModel()
                            {
                                TC = list.Rows[i]["TC"].ToString(),
                                Citizenship = list.Rows[i]["Citizenship"].ToString(),
                                Name = list.Rows[i]["Name"].ToString(),
                                Surname = list.Rows[i]["Surname"].ToString(),
                                BirthDate = list.Rows[i]["BirthDate"].ToString(),
                                PlaceOfBirth = list.Rows[i]["PlaceOfBirth"].ToString(),
                                Gender = gender,
                                JobState = jobstate,
                                JobDescription = list.Rows[i]["JobDescription"].ToString(),
                                Salary = Convert.ToInt32(list.Rows[i]["Salary"]),
                                MobilePhone = list.Rows[i]["MobilePhone"].ToString(),
                                HomePhone = list.Rows[i]["HomePhone"].ToString(),
                                Address = list.Rows[i]["Address"].ToString(),
                                MotherName = list.Rows[i]["MotherName"].ToString(),
                                FatherName = list.Rows[i]["FatherName"].ToString(),
                                isMarried = list.Rows[i]["isMarried"].ToString(),
                                EducationalStatusName = educationalStatusId,
                                SocialSecurity = socialSecurityId,
                                FamilyId = familyId
                            };

                            AddNewPerson(model);
                            model = null;
                        }
                        else
                        {
                            counter++;
                        }
                    }
                    catch (Exception ex)
                    {
                        counter++;
                    }
                }
                MessageBox.Show($"{list.Rows.Count - counter} adet kayıt eklendi, {counter} adet kayıt eklenemedi..");
            }
            else
            {
                MessageBox.Show("Lütfen import yapacak dosyayı seçiniz...", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            thread = null;
        }

        public void FillSocialSecurityList()
        {
            socialSecurityList = new Dictionary<int, string>();

            SqlHelper helper = new SqlHelper();
            helper.command.CommandText = "SELECT Id,Name FROM SocialSecurity";

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            while (reader.Read())
            {
                socialSecurityList.Add((int)reader["Id"], reader["Name"].ToString());
            }

            reader.Close();
            reader.Dispose();

            helper.connection.Close();
            helper.connection.Dispose();
        }

        public void FillEducationalStatusList()
        {
            educationalStatusList = new Dictionary<int, string>();

            SqlHelper helper = new SqlHelper();
            helper.command.CommandText = "SELECT Id,Name FROM EducationalStatus";

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            while (reader.Read())
            {
                educationalStatusList.Add((int)reader["Id"], reader["Name"].ToString());
            }

            reader.Close();
            reader.Dispose();

            helper.connection.Close();
            helper.connection.Dispose();
        }

        public bool PersonExist(string TC)
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT TC FROM Person WHERE TC=@p1";
            helper.command.Parameters.AddWithValue("@p1", TC);

            helper.connection.Open();

            OleDbDataReader reader = helper.command.ExecuteReader();

            bool exist = false;
            if (reader.HasRows)
            {
                exist = true;
            }

            reader.Close();
            reader.Dispose();

            helper.connection.Close();
            helper.connection.Dispose();
            return exist;
        }

        public int GetFamilyId(string FatherNo)
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT Id FROM Family WHERE FatherNo=@p1";

            helper.command.Parameters.AddWithValue("@p1", FatherNo);

            int familyId = 0;

            try
            {
                helper.connection.Open();


                OleDbDataReader reader = helper.command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        familyId = Convert.ToInt32(reader[0]);
                        break;
                    }
                }

                helper.connection.Close();
                helper.connection.Dispose();

            }
            catch (Exception)
            {

            }

            return familyId;
        }

        public bool CreateNewFamily(string FatherNo)
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "INSERT INTO Family (HousingId,FatherNo,PersonCount) values(1,@p2,3)";

            helper.command.Parameters.AddWithValue("@p2", FatherNo);

            bool result;

            try
            {
                helper.connection.Open();

                result = helper.command.ExecuteNonQuery() == 1 ? true : false;

                helper.connection.Close();
                helper.connection.Dispose();
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public void AddNewPerson(PersonModel model)
        {
            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "INSERT INTO Person (TC, Citizenship, Name, Surname, BirthDate, PlaceOfBirth, Gender, JobState, JobDescription, Salary, MobilePhone, HomePhone, Address, MotherName, FatherName, isMarried, EducationalStatus, SocialSecurityId, FamilyId) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@16,@p17,@p18,@p19)";

            helper.command.Parameters.AddWithValue("@p1", model.TC.ToString());
            helper.command.Parameters.AddWithValue("@p2", model.Citizenship);
            helper.command.Parameters.AddWithValue("@p3", model.Name);
            helper.command.Parameters.AddWithValue("@p4", model.Surname);
            helper.command.Parameters.AddWithValue("@p5", model.BirthDate);
            helper.command.Parameters.AddWithValue("@p6", model.PlaceOfBirth);
            helper.command.Parameters.AddWithValue("@p7", model.Gender);
            helper.command.Parameters.AddWithValue("@p8", model.JobState);
            helper.command.Parameters.AddWithValue("@p9", model.JobDescription);
            helper.command.Parameters.AddWithValue("@p10", model.Salary);
            helper.command.Parameters.AddWithValue("@p11", model.MobilePhone);
            helper.command.Parameters.AddWithValue("@p12", model.HomePhone);
            helper.command.Parameters.AddWithValue("@p13", model.Address);
            helper.command.Parameters.AddWithValue("@p14", model.MotherName);
            helper.command.Parameters.AddWithValue("@p15", model.FatherName);
            helper.command.Parameters.AddWithValue("@p16", model.isMarried);
            helper.command.Parameters.AddWithValue("@p17", model.EducationalStatusName);
            helper.command.Parameters.AddWithValue("@p18", model.SocialSecurity);
            helper.command.Parameters.AddWithValue("@p19", model.FamilyId);

            helper.connection.Open();

            helper.command.ExecuteNonQuery();

            helper.connection.Close();
            helper.connection.Dispose();
        }

        public void FillAllPerson()
        {
            dataTable = new System.Data.DataTable();

            SqlHelper helper = new SqlHelper();

            helper.command.CommandText = "SELECT " +
                "P.TC, P.Citizenship, P.Name, P.Surname, P.BirthDate, P.PlaceOfBirth,  Replace(Replace(P.Gender,0,'Kadın'), 1, 'Erkek') as Gender, Replace(Replace(P.JobState,0,'Çalışmıyor'), 1, 'Çalışıyor') as JobState, P.JobDescription, P.Salary, P.MobilePhone, P.HomePhone, P.Address, P.MotherName, P.FatherName, P.isMarried, ES.Name as EducationalStatusName, SS.Name as SocialSecurityName, F.FatherNo FROM  (((Person as P " +
                "INNER JOIN EducationalStatus as ES ON P.EducationalStatus = ES.Id) " +
                "INNER JOIN SocialSecurity as SS ON P.SocialSecurityId = SS.Id) " +
                "INNER JOIN Family as F ON F.Id = P.FamilyId)";

            helper.connection.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(helper.command);

            adapter.Fill(dataTable);

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dataTable.Rows[i]["Gender"] = dataTable.Rows[i]["Gender"].ToString().Trim('-');
                dataTable.Rows[i]["JobState"] = dataTable.Rows[i]["JobState"].ToString().Trim('-');
            }
            
            helper.connection.Close();
            helper.connection.Dispose();

        }

        private bool WriteToExcel(string path)
        {
            WriteDataToExcel write = new WriteDataToExcel(dataTable, path);
            return write.WriteToExcel() ? true : false;
        }

        private void frmDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnSocialSecurity_Click(object sender, EventArgs e)
        {
            new frmSocialSecurity().ShowDialog();
        }

        private void btnEducational_Click(object sender, EventArgs e)
        {
            new frmEducationalStatus().ShowDialog();
        }

        private void btnHousing_Click(object sender, EventArgs e)
        {
            new frmHousing().ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            new frmEditPassword().ShowDialog();
        }

        private void btnFamily_Click(object sender, EventArgs e)
        {
            new frmFamilyDashboard().ShowDialog();
        }

        private void btnDonation_Click(object sender, EventArgs e)
        {
            new frmDonateDashboard().Show();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.Yes || result == DialogResult.OK)
            {
                string path = folderBrowserDialog.SelectedPath;

                FillAllPerson();

                if (WriteToExcel(path))
                {
                    MessageBox.Show("Data başarıyla dışarı çıkartıldı..");
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir konum seçiniz...");
            }

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(StartImport)) { ApartmentState = ApartmentState.STA };
            thread.Start();
        }
    }
}
