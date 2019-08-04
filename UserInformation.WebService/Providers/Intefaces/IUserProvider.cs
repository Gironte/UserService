using UserInformation.WebService.Models;

namespace UserInformation.WebService.Providers
{
    public interface IUserProvider
    {
        void ImportUser(MyAccountRequestBase syncProfileRequest);
    }
}
