using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInformation.WCFService.Objects;

namespace UserInformation.WCFService.Providers
{
    public interface IUserReposytory
    {
        UserInfo GetUserById(Guid userId);
    }
}
