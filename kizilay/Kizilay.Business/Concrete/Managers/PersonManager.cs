using Kizilay.Business.Abstract;
using Kizilay.Business.Concrete.Result;
using Kizilay.Core.Aspects.Ninject.UnitOfWork;
using Kizilay.DataAccess.Abstract;
using Kizilay.Entities.Concrete;
using Kizilay.Entities.Concrete.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Concrete.Managers
{
    public class PersonManager : IPersonManager
    {
        private IPersonDal _personDal { get; set; }
        private IFamilyManager _familyManager { get; set; }

        public PersonManager(IPersonDal personDal,IFamilyManager familyManager)
        {
            _personDal = personDal;
            _familyManager = familyManager;
        }

        public LayerResult<Person> RemoveByTCNo(string tcNo)
        {
            int count = _personDal.RemovePersonByTCNo(tcNo);

            LayerResult<Person> layerResult = new LayerResult<Person>();

            if (count == 0)
                layerResult.AddError("Kayıtlı kullanıcı silinemedi, lütfen ilişkileri kontrol ediniz!");

            return layerResult;
        }

        public bool PersonExistsByTCNo(string tcNo)
        {
            return _personDal.PersonExistsByTCNo(tcNo);
        }

        public LayerResult<Person> Add(Person person)
        {
            LayerResult<Person> layerResult = new LayerResult<Person>();
            layerResult.Result = person;

            if (string.IsNullOrEmpty(person.TC))
                layerResult.AddError("T.C kimlik numarası boş geçilemez!");
            if (person.TC.Length != 11)
                layerResult.AddError("T.C kimlik numarası 11 karakter olmalıdır!");
            if (string.IsNullOrEmpty(person.Citizenship))
                layerResult.AddError("Uyruk boş geçilemez!");
            if (string.IsNullOrEmpty(person.Name))
                layerResult.AddError("İsim alanı boş geçilemez!");
            if (string.IsNullOrEmpty(person.Surname))
                layerResult.AddError("Soyisim alanı boş geçilemez!");
            if (person.BirthDate == null)
                layerResult.AddError("Doğum Tarihi boş geçilemez!");
            if (string.IsNullOrEmpty(person.PlaceOfBirth))
                layerResult.AddError("Doğum yeri boş geçilemez!");
            if (string.IsNullOrEmpty(person.MotherName))
                layerResult.AddError("Anne adı boş geçilemez!");
            if (string.IsNullOrEmpty(person.FatherName))
                layerResult.AddError("Baba adı numarası boş geçilemez!");

            if (layerResult.HasError())
                return layerResult;

            int count = _personDal.Add(person);

            if (count == 0)
                layerResult.AddError("Ekleme işlemi gerçekleştirilemedi!");

            return layerResult;
        }

        public Person GetById(int Id)
        {
            return _personDal.GetById(Id);
        }

        public Person GetByTCNo(string personTCNo)
        {
            return _personDal.GetByTCNo(personTCNo);
        }

        public LayerResult<Person> Update(FamilyUpdateModel model)
        {
            LayerResult<Person> layerResult = new LayerResult<Person>();
            layerResult.Result = model.Person;

            //this validation base will be refactoring!
            if (string.IsNullOrEmpty(model.Person.TC))
                layerResult.AddError("T.C kimlik numarası boş geçilemez!");
            if (model.Person.TC.Length != 11)
                layerResult.AddError("T.C kimlik numarası 11 karakter olmalıdır!");
            if (string.IsNullOrEmpty(model.Person.Citizenship))
                layerResult.AddError("Uyruk boş geçilemez!");
            if (string.IsNullOrEmpty(model.Person.Name))
                layerResult.AddError("İsim alanı boş geçilemez!");
            if (string.IsNullOrEmpty(model.Person.Surname))
                layerResult.AddError("Soyisim alanı boş geçilemez!");
            if (model.Person.BirthDate == null)
                layerResult.AddError("Doğum Tarihi boş geçilemez!");
            if (string.IsNullOrEmpty(model.Person.PlaceOfBirth))
                layerResult.AddError("Doğum yeri boş geçilemez!");
            if (string.IsNullOrEmpty(model.Person.MotherName))
                layerResult.AddError("Anne adı boş geçilemez!");
            if (string.IsNullOrEmpty(model.Person.FatherName))
                layerResult.AddError("Baba adı numarası boş geçilemez!");

            if (layerResult.HasError())
                return layerResult;

            int count = _personDal.Update(model);

            if (count == 0)
                layerResult.AddError("Güncelleme işlemi gerçekleştirilemedi!");

            else
            {
                if (model.LastTCNo == model.FatherTCNo)
                {
                    var family = _familyManager.GetFamilyByFatherTCNo(model.FatherTCNo);

                    family.FatherNo = model.Person.TC;

                    _familyManager.Update(family);
                }
            }


            return layerResult;
        }

        public DataTable GetAllPersonInformationForExport()
        {
            return _personDal.GetAllPersonInformationForExport();
        }
    }
}
