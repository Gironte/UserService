using System;
using UserInformation.WebService.Models;

namespace UserInformation.WebService.Providers
{
    public interface IUserRepository
    {
        bool IsUserAlreadyExists(Guid userId);

        void Update(MyAccountRequestBase myAccountRequestBase);
        void Insert(MyAccountRequestBase myAccountRequestBase);
    }
}
