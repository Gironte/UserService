using System;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.SqlServer;
using System.Linq;
using Serilog;
using Newtonsoft.Json;
using UserInformation.WebService.Models;

namespace UserInformation.WebService.Providers
{
    public class UserWebApiRepository : DbContext, IUserRepository
    {
        private static SqlProviderServices instance = SqlProviderServices.Instance;

        public DbSet<MyAccountRequestBase> Users { get; set; }

        public UserWebApiRepository() : base("DBConnection")
        { }

        public bool IsUserAlreadyExists(Guid UserId)
        {
            if (UserId == null || UserId.Equals(Guid.Empty)) throw new ArgumentNullException();

            this.Users.Load();
            return this.Users.Local.Any(x => x.UserId.Equals(UserId));
        }

        public void Insert(MyAccountRequestBase myAccountRequestBase)
        {
            if (myAccountRequestBase == null) 
            {
                throw new ArgumentNullException();
            }

            try
            {
                this.Users.Add(myAccountRequestBase);
                this.SaveChanges();
                Log.Logger.Information("The entity UserId:{UserId} Inserted", myAccountRequestBase.UserId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Insert Error: " + JsonConvert.SerializeObject(myAccountRequestBase));
            }
        }

        public void Update(MyAccountRequestBase myAccountRequestBase)
        {
            if (myAccountRequestBase == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                Users.Load();
                var user = Users.Local.FirstOrDefault(x => x.UserId.Equals(myAccountRequestBase.UserId));
                Entry(user).CurrentValues.SetValues(myAccountRequestBase);
                this.SaveChanges();

                Log.Logger.Information("The entity UserId:{UserId} Updated", myAccountRequestBase.UserId);
            }
            catch (Exception e)
            {
                throw new UpdateException("Update Error: " + JsonConvert.SerializeObject(myAccountRequestBase));
            }
        }
    }
}
