using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.ServiceModel;
using System.Configuration;
using System.Data.Entity.Core;
using UserInformation.WCFService.Objects;


namespace UserInformation.WCFService.Providers
{
    public class UserRepository : DbContext, IUserReposytory
    {
        public DbSet<UserInfo> Users { get; set; }

        public UserInfo GetUserById(Guid userId)
        {
            var connect = new SQLiteConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString()
            };

            var getUsers = new SQLiteCommand("SELECT * FROM MyAccountRequestBases", connect);

            connect.Open();
            var dataReader = getUsers.ExecuteReader();
            while (dataReader.Read())
            {
                if (new Guid(dataReader.GetValue(0).ToString()).Equals(userId))
                {
                    return new UserInfo
                    {
                        UserId = userId,
                        AdvertisingOptIn = string.IsNullOrEmpty(dataReader.GetValue(2).ToString())
                            ? (bool?)null
                            : dataReader.GetValue(2).ToString() == "1",
                        CountryIsoCode = dataReader.GetValue(3).ToString(),
                        DateModified = DateTime.Parse(dataReader.GetValue(4).ToString()),
                        Locale = dataReader.GetValue(5).ToString(),

                    };
                }
            }

            throw new ObjectNotFoundException();

        }
    }
}
