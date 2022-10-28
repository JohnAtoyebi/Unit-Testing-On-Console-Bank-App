using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAppCore.Models;

namespace BankAppTest
{
    public static class Helper
    {
        public static List<Users> ListOfUsers ()
        {
            var users = new List<Users>()
            {
                 new Users {Id ="e29844c5-10eb-44a5-a0a9-b922b1cdec9b", FullName ="Admin Admin", EmailAddress="admin@gmail.com", Password="7676aaafb027c825bd9abab78b234070e702752f625b752e55e55b48e607e358", MobileNumber="08975435678", IsActive=true, IsAdmin=true, Age=33, Created_at = DateTime.Now},
                 new Users {Id ="e29844c5-10eb-66a5-a0a9-b844b1cdec9c", FullName ="Abimbola Olaitan", EmailAddress="bimbo@gmail.com", Password="7676aaafb027c825bd9abab78b234070e702752f625b752e55e55b48e607e358", MobileNumber="08973764078", IsActive=true, IsAdmin=false, Age=44, Created_at = DateTime.Now},
                 new Users {Id ="e29899c5-10eb-66a5-a9a9-b866b1cdec9e", FullName ="Angelo Akuma", EmailAddress="angelo@gmail.com", Password="7676aaafb027c825bd9abab78b234070e702752f625b752e55e55b48e607e358", MobileNumber="08973764078", IsActive=true, IsAdmin=false, Age=44, Created_at = DateTime.Now},
            };

            return users;
        }

        public static List<Roles> ListOfRoles()
        {
            var roles = new List<Roles>()
            {
                 new Roles {Id = 1, RoleName ="Admin", Description= "For Admin only"},
                 new Roles {Id = 2, RoleName ="Staff", Description= "For Staff only"},
                 new Roles {Id = 3, RoleName ="Customer", Description= "For Customer only"},
            };

            return roles;
        }

        public static List<UserInRoles> ListOfUserInRoles()
        {
            var userInrole = new List<UserInRoles>()
            {
                  new UserInRoles {RoleId = 1, UserId="e29844c5-10eb-44a5-a0a9-b922b1cdec9b"},
                  new UserInRoles {RoleId = 2, UserId="e29844c5-10eb-66a5-a0a9-b844b1cdec9c"},
                  new UserInRoles {RoleId = 3, UserId="e29899c5-10eb-66a5-a9a9-b866b1cdec9e"},

            };

            return userInrole;
        }

        public static List<Accounts> ListOfAccounts()
        {
            var roles = new List<Accounts>()
            {
                  new Accounts {AccountNumber = "2628175905", Balance= 10000.0, AccountType="Saving", UserId="e29899c5-10eb-66a5-a9a9-b866b1cdec9e"},
                  new Accounts {AccountNumber = "8693686809", Balance= 60000.0, AccountType="Current", UserId="e29899c5-10eb-66a5-a9a9-b866b1cdec9e"},
                  new Accounts {AccountNumber = "3011667709", Balance= 5000.0, AccountType="Current", UserId="e29844c5-10eb-66a5-a0a9-b844b1cdec9c"},

            };

            return roles;
        }

        public static List<Transactions> ListOfTransactions()
        {
            var roles = new List<Transactions>()
            {
                  new Transactions {AccountNumber = "2628175905", Amount="8000.0", Balance="18000.0", Description="Deposit Money", Date=DateTime.Now},
                  new Transactions {AccountNumber = "2628175905", Amount="2000.0", Balance="16000.0", Description="Withdraw Money", Date=DateTime.Now},
                  new Transactions {AccountNumber = "8693686809", Amount="2000.0", Balance="62000.0", Description="Deposit Money", Date=DateTime.Now},
                  new Transactions {AccountNumber = "3011667709", Amount="5000.0", Balance="10000.0", Description="Deposit Money", Date=DateTime.Now},

            };

            return roles;
        }
    }
}
