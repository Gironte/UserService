using System;
using System.ServiceModel;
using NUnit.Framework;
using UserInformation.WCFService.Providers;
using UserInformation.WCFService.Objects;

namespace UserService.NUnit.Test.WCFService.Test
{

    [TestFixture]
    public class ServiceTest
    {

        [Test]
        public void IfUserNotExist_Return_FaultException()
        {
            var _missingUser = Guid.Empty;
            var service = new UserInfoProvider
            {
                Reposytory = new FakeRepository()
            };

            Assert.Catch<FaultException<UserNotFound>>(() => service.GetUserInfo(_missingUser));
        }

        [Test]
        public void IfUserExists_Return_User()
        {
            var user = new Guid("9BE9BC92-B968-436F-8409-B4D60DF62729");
                
            var service = new UserInfoProvider
            {
                Reposytory = new FakeRepository()
            };
            Assert.AreEqual(user, service.GetUserInfo(user).UserId);
        }
    }


}
