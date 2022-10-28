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
    public class UserServicesTest
    {
               
        private readonly Mock<IReadWriteToJson> _dbContext;

        public UserServicesTest()
        {
            _dbContext = new Mock<IReadWriteToJson>();
            
        }


        [Fact]

        public void ShouldGetAllUsers()
        {
            //Arrange 
            var users = Helper.ListOfUsers();            
            _dbContext.Setup(x => x.ReadJson<Users>("Users.json")).ReturnsAsync(users);

            //Act
            var result = new UserService(_dbContext.Object);
            var actual = result.GetAllUsers().Result;
            
            //Assert
            Assert.Equal(users, actual);
            Assert.IsType<List<Users>>(actual);
        }

       

        [Fact]
        public async void ShouldAddUserWhenNoError()
        {
            //Arrange 
            var user =  new Users {Id ="e21234c5-10eb-44a5-a0a7-b922b1fdec9b", FullName ="Chisom Okno", EmailAddress="chisom@gmail.com", Password="7676aaafb027c825bd9abab78b234070e702752f625b752e55e55b48e607e358", MobileNumber="08975435678", IsActive=true, IsAdmin=false, Age=33, Created_at = DateTime.Now};
            _dbContext.Setup(x => x.WriteJson<Users>(user, "Users.json")).ReturnsAsync(true);

            //Act
            var result = new UserService(_dbContext.Object);
            var actual = await result.AddUser(user);

            //Assert
            Assert.True(actual);
            Assert.IsType<bool>(actual);
        }

        [Fact]
        public void ShouldThrowExceptionWhenAddUserError()
        {
            //Arrange 
            var user = new Users {  };
            _dbContext.Setup(x => x.WriteJson<Users>(user, "Users.json")).Throws<Exception>();

            //Act
            var result = new UserService(_dbContext.Object);
            //var actual = await result.AddUser(user);

            //Assert
            Assert.ThrowsAsync<Exception>(() => result.AddUser(user));
            
        }

        [Fact]
        public async void ShouldGetAUserWhenWithCorrectId()
        {
            //Arrange 
            var users = Helper.ListOfUsers();
            _dbContext.Setup(x => x.ReadJson<Users>("Users.json")).ReturnsAsync(users);

            //Act
            var result = new UserService(_dbContext.Object);
            var actual = await result.GetUserById("e29844c5-10eb-44a5-a0a9-b922b1cdec9b");

            //Assert
            Assert.NotNull(actual);

        }

        [Fact]
        public async void ShouldReturnNullWhenWithInCorrectId()
        {
            //Arrange 
            var users = Helper.ListOfUsers();
            _dbContext.Setup(x => x.ReadJson<Users>("Users.json")).ReturnsAsync(users);

            //Act
            var result = new UserService(_dbContext.Object);
            var actual = await result.GetUserById("e29844c5-10eb");

            //Assert
            Assert.Null(actual);

        }
    }
}
