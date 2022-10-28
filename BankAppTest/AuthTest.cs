using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using BankAppCore.Interfaces;
using BankAppCore.Models;
using BankAppCore.Services;

namespace BankAppTest
{
    
    public class AuthTest
    {
        private readonly Mock<IUsers> _users;
        private readonly Mock<IUserInRole> _userInRole;
        private readonly Mock<IUtilities> _utility;
        private readonly Mock<IAuth> _auth;

        public AuthTest()
        {
            _users = new Mock<IUsers>();
            _userInRole = new Mock<IUserInRole>();
            _utility = new Mock<IUtilities>();
            _auth = new Mock<IAuth>();
        }


        [Fact]
        public async void LoginShouldReturnUserWithCorrectCredential()
        {
            //Arrange
            string email = "admin@gmail.com";
            string password = "admin@123";
            var users = Helper.ListOfUsers();
            Dictionary<string, Users> user = new Dictionary<string, Users>();
            user["Admin"] = users.FirstOrDefault(x => x.Id == "e29844c5-10eb-44a5-a0a9-b922b1cdec9b");
            _utility.Setup(x => x.ComputeSha256Hash(password)).Returns("7676aaafb027c825bd9abab78b234070e702752f625b752e55e55b48e607e358");
            _users.Setup(x => x.GetAllUsers()).ReturnsAsync(users);
            _userInRole.Setup(x => x.GetUserRoles("e29844c5-10eb-44a5-a0a9-b922b1cdec9b")).ReturnsAsync("Admin");
            _auth.Setup(x => x.Login(email, password)).ReturnsAsync(user);

            //Act
            var result = new AuthService(_users.Object, _userInRole.Object, _utility.Object);
            var actual = await result.Login(email, password);

            //Assert
            Assert.NotNull(actual);
            Assert.IsType<Dictionary<string, Users>>(actual);
            

        }

        [Theory]
        [InlineData("bimbola@gmail.com", "admin@123", null)]
        [InlineData("admin@gmail.com", "bimbola@123", null)]        
        public async void LoginShouldReturnNullWithInCorrectCredential(string email, string password, Dictionary<string, Users> expected)
        {
            //Arrange
            var users = Helper.ListOfUsers();            
            _utility.Setup(x => x.ComputeSha256Hash(password)).Returns("737eh3d6fc3hc3773hd37dt7d37d3d73738u");
            _users.Setup(x => x.GetAllUsers()).ReturnsAsync(users);          
            
            //Act
            var result = new AuthService(_users.Object, _userInRole.Object, _utility.Object);
            var actual = await result.Login(email, password);

            //Assert
            Assert.Equal(expected, actual);           


        }
    }
}
