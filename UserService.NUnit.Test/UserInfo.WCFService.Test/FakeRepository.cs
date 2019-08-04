using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using UserInformation.WCFService.Providers;

namespace UserService.NUnit.Test.WCFService.Test
{
    internal class FakeRepository : IUserReposytory
    {
        List<UserInformation.WCFService.Objects.UserInfo> Collection { get; set; }

        internal FakeRepository()
        {
            Collection = new List<UserInformation.WCFService.Objects.UserInfo>()
            {
                new UserInformation.WCFService.Objects.UserInfo() { UserId = new Guid("71942EA3-52E4-4051-949F-83EF07B09716")},
                new UserInformation.WCFService.Objects.UserInfo() { UserId = new Guid("2B969741-1A7D-4F95-A08A-601E4ACC5B7A")},
                new UserInformation.WCFService.Objects.UserInfo() { UserId = new Guid("9BE9BC92-B968-436F-8409-B4D60DF62729")}
            };
        }

        public UserInformation.WCFService.Objects.UserInfo GetUserById(Guid userId)
        {
            var user = Collection.Where(x => x.UserId.Equals(userId));

            if (user.Any()) return user.FirstOrDefault();

            throw new ObjectNotFoundException();
        }
    }

}
