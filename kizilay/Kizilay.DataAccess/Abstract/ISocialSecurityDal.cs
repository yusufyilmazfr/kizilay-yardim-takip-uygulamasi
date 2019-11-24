using Kizilay.DataAccess.Abstract.Repository;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Abstract
{
    public interface ISocialSecurityDal : IRepository<SocialSecurity>
    {
        int Add(SocialSecurity socialSecurity);
        int Update(SocialSecurity socialSecurity);
        int GetSocialSecurityIdByName(string socialSecurityName);
    }
}
