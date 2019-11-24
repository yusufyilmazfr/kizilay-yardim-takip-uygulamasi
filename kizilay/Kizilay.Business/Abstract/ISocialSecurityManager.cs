using Kizilay.Business.Concrete.Result;
using Kizilay.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Abstract
{
    public interface ISocialSecurityManager
    {
        bool IsExistsById(int Id);
        List<SocialSecurity> GetAll();
        int GetSocialSecurityIdByName(string socialSecurityName);
        LayerResult<SocialSecurity> Add(SocialSecurity socialSecurity);
        LayerResult<SocialSecurity> Update(SocialSecurity socialSecurity);
        LayerResult<SocialSecurity> RemoveById(int Id);
    }
}
