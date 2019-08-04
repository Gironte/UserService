using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity.Core;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using UserInformation.WebService.Controllers;
using UserInformation.WebService.Models;
using UserInformation.WebService.Providers;

namespace UserService.NUnit.WebService.Server.Test
{
    public class TestUser : MyAccountRequestBase {}

    [TestFixture]
    public class ControllerTest
    {
        [Test]
        public void UpdateReturnsUserWithSameId()
        {
            var testUserId = Guid.NewGuid();
            var testUser = new TestUser {UserId = testUserId };

            var mockUserRepository = new Mock<IUserRepository>();
                mockUserRepository.Setup(x => x.IsUserAlreadyExists(It.IsAny<Guid>())).Returns(true);
                mockUserRepository.Setup(x => x.Update(It.IsAny<MyAccountRequestBase>()));

            var controller = new ImportController(new UserProvider(mockUserRepository.Object));
            var contentResult = controller.Import(testUser) as OkNegotiatedContentResult<MyAccountRequestBase>;
          
            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(testUserId, contentResult.Content.UserId);

        }


        [Test]
        public void InsertReturnsUserWithSameId()
        {
            var testUserId = Guid.NewGuid();
            var testUser = new TestUser { UserId = testUserId };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.IsUserAlreadyExists(It.IsAny<Guid>())).Returns(false);
            mockUserRepository.Setup(x => x.Insert(It.IsAny<MyAccountRequestBase>()));

            var controller = new ImportController(new UserProvider(mockUserRepository.Object));
            var contentResult = controller.Import(testUser) as OkNegotiatedContentResult<MyAccountRequestBase>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(testUserId, contentResult.Content.UserId);
        }

        [Test]
        public void InsertThrowExceptionIfError()
        {
            var testUserId = Guid.NewGuid();
            var testUser = new TestUser { UserId = testUserId };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.IsUserAlreadyExists(It.IsAny<Guid>())).Returns(false);
            mockUserRepository.Setup(x => x.Insert(It.IsAny<MyAccountRequestBase>())).Throws<InvalidOperationException>();


            var controller = new ImportController(new UserProvider(mockUserRepository.Object));
              

            // Assert
            Assert.Catch<InvalidOperationException>(() => controller.Import(testUser));
        }


        [Test]
        public void UpdateThrowExceptionIfError()
        {
            var testUserId = Guid.NewGuid();
            var testUser = new TestUser { UserId = testUserId };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.IsUserAlreadyExists(It.IsAny<Guid>())).Returns(true);
            mockUserRepository.Setup(x => x.Update(It.IsAny<MyAccountRequestBase>())).Throws<UpdateException>();


            var controller = new ImportController(new UserProvider(mockUserRepository.Object));


            // Assert
            Assert.Catch<UpdateException>(() => controller.Import(testUser));
        }
    }


}
