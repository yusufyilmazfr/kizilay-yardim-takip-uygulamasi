using Kizilay.Core.Aspects.Ninject.UnitOfWork;
using Kizilay.Core.Extensions.DataRowExtensions;
using Kizilay.DataAccess.Abstract;
using Kizilay.DataAccess.Abstract.Repository;
using Kizilay.DataAccess.Concrete.AdoNet.Repository;
using Kizilay.DataAccess.Concrete.AdoNet.SqlHelper;
using Kizilay.Entities.Concrete;
using Kizilay.Entities.Concrete.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Concrete.AdoNet
{
    public class AdoNetPersonDal : AdoNetRepository<Person>, IPersonDal
    {
        public AdoNetPersonDal(AdoNetDbHelper adoNetDbHelper) : base(adoNetDbHelper) { }

        public override string TableName { get { return "Person"; } set { } }

        public int Add(Person person)
        {
            var command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            command.CommandText = $"INSERT INTO {TableName} (TC,Citizenship,Name,Surname,BirthDate,PlaceOfBirth,Gender,JobState,JobDescription,Reference,Salary,MobilePhone,HomePhone,MotherName,FatherName,isMarried,EducationalStatus,SocialSecurityId,FamilyId) values(@tcNo,@citizenship,@name,@surname,@birthDate,@placeOfBirth,@gender,@jobState,@jobDescription,@reference,@salary,@mobilePhone,@homePhone,@motherName,@fatherName,@isMarried,@educationalStatusId,@socialSecurityId,@familyId)";

            command.Parameters.AddWithValue("@tcNo", person.TC);
            command.Parameters.AddWithValue("@citizenship", person.Citizenship);
            command.Parameters.AddWithValue("@name", person.Name);
            command.Parameters.AddWithValue("@surname", person.Surname);
            command.Parameters.AddWithValue("@birthDate", person.BirthDate.Date); //short date
            command.Parameters.AddWithValue("@placeOfBirth", person.PlaceOfBirth);
            command.Parameters.AddWithValue("@gender", person.Gender);
            command.Parameters.AddWithValue("@jobState", person.JobState);
            command.Parameters.AddWithValue("@jobDescription", person.JobDescription);
            command.Parameters.AddWithValue("@reference", person.Reference ?? "");
            command.Parameters.AddWithValue("@salary", person.Salary);
            command.Parameters.AddWithValue("@mobilePhone", person.MobilePhone);
            command.Parameters.AddWithValue("@homePhone", person.HomePhone);
            command.Parameters.AddWithValue("@motherName", person.MotherName);
            command.Parameters.AddWithValue("@fatherName", person.FatherName);
            command.Parameters.AddWithValue("@isMarried", person.isMarried);
            command.Parameters.AddWithValue("@educationalStatusId", person.EducationalStatus);
            command.Parameters.AddWithValue("@socialSecurityId", person.SocialSecurityId);
            command.Parameters.AddWithValue("@familyId", person.FamilyId);


            _adoNetDbHelper.OpenSafeConnection();

            int count = command.ExecuteNonQuery();

            _adoNetDbHelper.CloseSafeConnection();

            command.Dispose();

            return count;
        }

        public DataTable GetAllPersonInformationForExport()
        {
            _adoNetDbHelper.command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            _adoNetDbHelper.command.CommandText =
                "SELECT " +
                "P.TC," +
                "P.Citizenship," +
                "P.Name," +
                "P.Surname," +
                "P.BirthDate," +
                "P.PlaceOfBirth," +
                "C.Name as CityName," +
                "T.Name as TownName," +
                "N.Name as NeighborhoodsName," +
                "F.Address," +
                "Replace(Replace(P.Gender,0,'Kadın'), 1, 'Erkek') as Gender," +
                "Replace(Replace(P.JobState,0,'Çalışmıyor'), 1, 'Çalışıyor') as JobState," +
                "P.JobDescription," +
                "P.Salary," +
                "P.MobilePhone," +
                "P.HomePhone," +
                "P.MotherName," +
                "P.FatherName," +
                "P.isMarried," +
                "ES.Name as EducationalStatusName," +
                "SS.Name as SocialSecurityName," +
                "F.Priority," +
                "F.FatherNo as FatherNo " +
                "FROM  (" +
                "        (" +
                "          (" +
                "            (" +
                "              (" +
                "                (" +
                "                   Person as P " +
                "                       INNER JOIN EducationalStatus as ES ON P.EducationalStatus = ES.Id) " +
                "                          INNER JOIN SocialSecurity as SS ON P.SocialSecurityId = SS.Id) " +
                "                               INNER JOIN Family as F ON F.Id = P.FamilyId) " +
                "                                   INNER JOIN Neighborhoods as N ON N.Id = F.NeighborhoodsId) " +
                "                                       INNER JOIN Towns as T ON T.Id = N.TownId) " +
                "                                           INNER JOIN Cities as C ON C.Id = T.CityId)";


            return _adoNetDbHelper.GetDataTable();
        }

        public Person GetByTCNo(string personTCNo)
        {
            _adoNetDbHelper.command = _adoNetDbHelper.CreateCommandInCurrentConnection();

            _adoNetDbHelper.command.CommandText = $"SELECT * FROM {TableName} WHERE TC = @personTCNo";
            _adoNetDbHelper.command.Parameters.AddWithValue("@personTCNo", personTCNo);

            var dataTable = _adoNetDbHelper.GetDataTable();

            Person entity = dataTable.Rows[0].Map<Person>();

            return entity;
        }

        public bool PersonExistsByTCNo(string tcNo)
        {
            _adoNetDbHelper.command = _adoNetDbHelper.connection.CreateCommand();

            _adoNetDbHelper.command.CommandText = $"SELECT * FROM {TableName} WHERE TC=@tc";
            _adoNetDbHelper.command.Parameters.AddWithValue("@tc", tcNo);

            return _adoNetDbHelper.Any(_adoNetDbHelper.command);
        }

        //[UnitOfWorkAspect]
        public int RemovePersonByTCNo(string tcNo)
        {
            _adoNetDbHelper.command = _adoNetDbHelper.connection.CreateCommand();

            _adoNetDbHelper.command.CommandText = $"DELETE FROM {TableName} WHERE TC = @tcNo";

            _adoNetDbHelper.command.Parameters.AddWithValue("@tcNo", tcNo);

            _adoNetDbHelper.OpenSafeConnection();


            int count = 0;
            try
            {
                count = _adoNetDbHelper.command.ExecuteNonQuery();
            }
            catch (Exception) { }


            _adoNetDbHelper.CloseSafeConnection();

            _adoNetDbHelper.command.Dispose();

            return count;
        }

        public int Update(FamilyUpdateModel model)
        {
            _adoNetDbHelper.command = _adoNetDbHelper.connection.CreateCommand();

            _adoNetDbHelper.command.CommandText =
                $"UPDATE {TableName} SET TC=@newTCNo, Citizenship=@citizenship,Name=@name, Surname=@surname, BirthDate=@birthDate, PlaceOfBirth=@placeOfBirth, Gender=@gender, JobState=@jobState, JobDescription=@jobDescription,Reference=@reference, Salary=@salary, MobilePhone=@mobilePhone, HomePhone=@homePhone, MotherName=@motherName, FatherName=@fatherName, isMarried=@isMarried, EducationalStatus=@educationalStatusId, SocialSecurityId=@socialSecurityId, FamilyId=@familyId WHERE Id={model.Person.Id}";

            _adoNetDbHelper.command.Parameters.AddWithValue("@newTCNo", model.Person.TC);
            _adoNetDbHelper.command.Parameters.AddWithValue("@citizenship", model.Person.Citizenship);
            _adoNetDbHelper.command.Parameters.AddWithValue("@name", model.Person.Name);
            _adoNetDbHelper.command.Parameters.AddWithValue("@surname", model.Person.Surname);
            _adoNetDbHelper.command.Parameters.AddWithValue("@birthDate", model.Person.BirthDate.Date);
            _adoNetDbHelper.command.Parameters.AddWithValue("@placeOfBirth", model.Person.PlaceOfBirth);
            _adoNetDbHelper.command.Parameters.AddWithValue("@gender", model.Person.Gender);
            _adoNetDbHelper.command.Parameters.AddWithValue("@jobState", model.Person.JobState);
            _adoNetDbHelper.command.Parameters.AddWithValue("@jobDescription", model.Person.JobDescription);
            _adoNetDbHelper.command.Parameters.AddWithValue("@reference", model.Person.Reference ?? "");
            _adoNetDbHelper.command.Parameters.AddWithValue("@salary", model.Person.Salary);
            _adoNetDbHelper.command.Parameters.AddWithValue("@mobilePhone", model.Person.MobilePhone);
            _adoNetDbHelper.command.Parameters.AddWithValue("@homePhone", model.Person.HomePhone);
            _adoNetDbHelper.command.Parameters.AddWithValue("@motherName", model.Person.MotherName);
            _adoNetDbHelper.command.Parameters.AddWithValue("@fatherName", model.Person.FatherName);
            _adoNetDbHelper.command.Parameters.AddWithValue("@isMarried", model.Person.isMarried);
            _adoNetDbHelper.command.Parameters.AddWithValue("@educationalStatusId", model.Person.EducationalStatus);
            _adoNetDbHelper.command.Parameters.AddWithValue("@socialSecurityId", model.Person.SocialSecurityId);
            _adoNetDbHelper.command.Parameters.AddWithValue("@familyId", model.Person.FamilyId);

            _adoNetDbHelper.OpenSafeConnection();

            int count = _adoNetDbHelper.command.ExecuteNonQuery();

            _adoNetDbHelper.CloseSafeConnection();

            _adoNetDbHelper.command.Dispose();

            return count;
        }
    }
}
