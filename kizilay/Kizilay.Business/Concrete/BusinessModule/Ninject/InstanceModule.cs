using Kizilay.Business.Abstract;
using Kizilay.Business.Concrete.Managers;
using Kizilay.Business.Concrete.Services.ImportService;
using Kizilay.Business.Concrete.Services.ImportService.Excel;
using Kizilay.Core.CrossCuttingConcerns.Cache;
using Kizilay.Core.CrossCuttingConcerns.Cache.MicrosoftMemoryCache;
using Kizilay.Core.HashService;
using Kizilay.Core.HashService.Md5HashService;
using Kizilay.DataAccess.Abstract;
using Kizilay.DataAccess.Concrete.AdoNet;
using Kizilay.DataAccess.Concrete.AdoNet.SqlHelper;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Concrete.BusinessModule.Ninject
{
    public class InstanceModule : NinjectModule
    {
        public override void Load()
        {
            #region Business Dependency Structures

            Bind<IPersonDonationManager>().To<PersonDonationManager>().InSingletonScope();
            Bind<IDonationManager>().To<DonationManager>().InSingletonScope();
            Bind<IPersonManager>().To<PersonManager>().InSingletonScope();
            Bind<IFamilyManager>().To<FamilyManager>().InSingletonScope();
            Bind<IHousingManager>().To<HousingManager>().InSingletonScope();


            Bind<ISocialSecurityManager>().To<SocialSecurityManager>();
            Bind<IEducationalStatusManager>().To<EducationalStatusManager>();
            Bind<ICityManager>().To<CityManager>();
            Bind<ITownManager>().To<TownManager>();
            Bind<IAdminManager>().To<AdminManager>();
            Bind<INeighborhoodManager>().To<NeighborhoodManager>();


            Bind<IImportService>().To<ExcelImportService>();

            #endregion

            #region Data Access Dependency Structures

            Bind<IPersonDonationDal>().To<AdoNetPersonDonationDal>().InSingletonScope();
            Bind<IDonationDal>().To<AdoNetDonationDal>().InSingletonScope();
            Bind<IFamilyDal>().To<AdoNetFamilyDal>().InSingletonScope();
            Bind<IPersonDal>().To<AdoNetPersonDal>().InSingletonScope();
            Bind<IHousingDal>().To<AdoNetHousingDal>().InSingletonScope();

            Bind<ISocialSecurityDal>().To<AdoNetSocialSecurityDal>();
            Bind<IEducationalStatusDal>().To<AdoNetEducationalStatusDal>();
            Bind<ICityDal>().To<AdoNetCityDal>();
            Bind<ITownDal>().To<AdoNetTownDal>();
            Bind<IAdminDal>().To<AdoNetAdminDal>();
            Bind<INeighborhoodDal>().To<AdoNetNeighborhoodDal>();

            Bind<AdoNetDbHelper>().ToSelf().InTransientScope();

            #endregion

            #region Core Layer Dependency Structures

            Bind<ICache>().To<InMemoryCache>();

            Bind<IHashGeneratorService>().To<Md5HashGeneratorService>();

            #endregion
        }
    }
}
