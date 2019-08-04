using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Net.Security;
using System.ServiceModel;
using UserInformation.WCFService.Objects;

namespace UserInformation.WCFService.Providers
{
    [ServiceContract]
    public interface IUserInfoProvider
    {
        [OperationContract]
        [FaultContract(typeof(UserNotFound))]
        UserInfo GetUserInfo(Guid userId);
    }
}

