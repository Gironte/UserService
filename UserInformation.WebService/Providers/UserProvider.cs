using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Serilog;
using UserInformation.WebService.Models;

namespace UserInformation.WebService.Providers
{
    public class UserProvider : IUserProvider
    {
        public readonly IUserRepository UserRepository;
        private object _thisLock = new object();

        public UserProvider(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public void ImportUser(MyAccountRequestBase baseRequest)
        {
            if (baseRequest == null) throw new ArgumentException();

            lock (_thisLock)
            {
                if (UserRepository.IsUserAlreadyExists(baseRequest.UserId))
                {
                    UserRepository.Update(baseRequest);
                }
                else
                {
                    UserRepository.Insert(baseRequest);
                }
            }
        }
    }
}
