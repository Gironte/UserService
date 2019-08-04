using System;
using System.Data.Entity.Core;
using System.ServiceModel;
using UserInformation.WCFService.Objects;


namespace UserInformation.WCFService.Providers
{
    public class UserInfoProvider : IUserInfoProvider
    {
        public IUserReposytory Reposytory { get; set; }

        public UserInfoProvider()
        {
            Reposytory = new UserRepository();
        }

        public UserInfo GetUserInfo(Guid userId)
        {
            try
            {
                return Reposytory.GetUserById(userId);
            }
            catch (ObjectNotFoundException e)
            {
                throw new FaultException<UserNotFound>(
                    new UserNotFound { Operation = "Search User ID=" + userId + "in DB", ProblemType = "User ID=" + userId + "is Not Found in DB" },
                    new FaultReason("User ID=" + userId + "is Not Found in DB"));
            }
        }
    }
}

