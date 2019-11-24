using Kizilay.Business.Abstract;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Concrete.Services.ImportService.Excel
{
    public class ExcelImportService : IImportService, IReadableFile
    {
        private DataTable _invalidDataList { get; set; }

        private IPersonManager _personManager { get; set; }
        private IFamilyManager _familyManager { get; set; }
        private ICityManager _cityManager { get; set; }
        private ITownManager _townManager { get; set; }
        private INeighborhoodManager _neighborhoodManager { get; set; }
        private IEducationalStatusManager _educationalStatusManager { get; set; }
        private ISocialSecurityManager _socialSecurityManager { get; set; }

        public ExcelImportService(IPersonManager personManager,
        IFamilyManager familyManager,
        ITownManager townManager,
        ICityManager cityManager,
        INeighborhoodManager neighborhoodManager,
        IEducationalStatusManager educationalStatusManager,
        ISocialSecurityManager socialSecurityManager)
        {
            _personManager = personManager;
            _familyManager = familyManager;
            _cityManager = cityManager;
            _townManager = townManager;
            _neighborhoodManager = neighborhoodManager;
            _educationalStatusManager = educationalStatusManager;
            _socialSecurityManager = socialSecurityManager;

        }

        public DataTable Import(DataTable dataTable)
        {
            _invalidDataList = dataTable.Clone();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                try
                {
                    var personTCNo = dataTable.Rows[i]["TC"].ToString();

                    bool isPersonExists = _personManager.PersonExistsByTCNo(personTCNo);

                    if (isPersonExists)
                        continue;

                    string fatherTCNo = dataTable.Rows[i]["FatherNo"].ToString();

                    var family = _familyManager.GetFamilyByFatherTCNo(fatherTCNo);

                    if (family == null)
                    {
                        string fatherNo = dataTable.Rows[i]["FatherNo"].ToString();
                        string address = dataTable.Rows[i]["Address"].ToString();
                        int priority = Convert.ToInt32(dataTable.Rows[i]["Priority"]);

                        //string housingName = dataTable.Rows[i]["HousingName"].ToString();

                        string cityName = dataTable.Rows[i]["CityName"].ToString();
                        string townName = dataTable.Rows[i]["TownName"].ToString();
                        string neighborhoodName = dataTable.Rows[i]["NeighborhoodsName"].ToString();

                        int neighborhoodId = _neighborhoodManager.GetNeighborhoodIdByName(neighborhoodName);


                        Family newFamily = new Family
                        {
                            Address = address,
                            FatherNo = fatherNo,
                            PersonCount = 3,
                            Priority = priority > 0 && priority <= 5 ? priority : 1,
                            NeighborhoodsId = neighborhoodId,
                            HousingId = 3 //daha sonradan düzeltilecek!
                        };

                        int count = _familyManager.Add(newFamily);

                        if (count == 0)
                            throw new Exception();
                    }

                    int familyId = _familyManager.GetFamilyByFatherTCNo(fatherTCNo).Id;

                    string educationalStatusName = dataTable.Rows[i]["EducationalStatusName"].ToString();
                    string socialSecurityName = dataTable.Rows[i]["SocialSecurityName"].ToString();

                    int socialSecurityId = _socialSecurityManager
                        .GetAll()
                        .Where(j => j.Name == socialSecurityName)
                        .Select(j => j.Id)
                        .FirstOrDefault();

                    int educationalStatusId = _educationalStatusManager
                        .GetAll()
                        .Where(j => j.Name == educationalStatusName)
                        .Select(j => j.Id)
                        .FirstOrDefault();

                    //int socialSecurityId = _socialSecurityManager.GetSocialSecurityIdByName(socialSecurityName);
                    //int educationalStatusId = _educationalStatusManager.GetEducationalStatusIdByName(educationalStatusName);

                    Person person = new Person()
                    {
                        FamilyId = familyId,
                        TC = dataTable.Rows[i]["TC"].ToString(),
                        Citizenship = dataTable.Rows[i]["Citizenship"].ToString(),
                        Name = dataTable.Rows[i]["Name"].ToString(),
                        Surname = dataTable.Rows[i]["Surname"].ToString(),
                        BirthDate = Convert.ToDateTime(dataTable.Rows[i]["BirthDate"].ToString()),
                        PlaceOfBirth = dataTable.Rows[i]["PlaceOfBirth"].ToString(),
                        Gender = dataTable.Rows[i]["Gender"].ToString().ToLower() == "erkek" ? true : false,
                        JobState = dataTable.Rows[i]["JobState"].ToString().ToLower() == "çalışmıyor" ? false : true,
                        JobDescription = dataTable.Rows[i]["JobDescription"].ToString(),
                        Salary = Convert.ToDecimal(dataTable.Rows[i]["Salary"]),
                        MobilePhone = dataTable.Rows[i]["MobilePhone"].ToString(),
                        HomePhone = dataTable.Rows[i]["HomePhone"].ToString(),
                        MotherName = dataTable.Rows[i]["MotherName"].ToString(),
                        FatherName = dataTable.Rows[i]["FatherName"].ToString(),
                        isMarried = dataTable.Rows[i]["isMarried"].ToString(),
                        EducationalStatus = educationalStatusId,
                        SocialSecurityId = socialSecurityId
                    };

                    var layerResult = _personManager.Add(person);

                    if (layerResult.HasError())
                        throw new Exception();

                }
                catch (Exception ex)
                {
            
                    var desRow = _invalidDataList.NewRow();
                    var sourceRow = dataTable.Rows[i];
                    desRow.ItemArray = sourceRow.ItemArray.Clone() as object[];

                    _invalidDataList.Rows.Add(desRow);
                }

            }
            return _invalidDataList;
        }

        public DataTable ReadFile(string path)
        {
            using (var connection = new OleDbConnection())
            {
                connection.ConnectionString =
                    $"provider=Microsoft.ACE.OLEDB.12.0;Data Source='{path}';Extended Properties=Excel 8.0;";

                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = "SELECT * FROM [Sayfa1$]";

                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);

                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
        }

        public Task<DataTable> ImportAsync(DataTable dataTable)
        {
            return Task.Run(() =>
            {
                return Import(dataTable);
            });
        }
    }
}