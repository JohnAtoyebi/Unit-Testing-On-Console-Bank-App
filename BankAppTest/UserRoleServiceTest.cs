using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAppCore.Interfaces;
using BankAppCore.Models;
using BankAppCore.Services;
using Moq;
using Xunit;

namespace BankAppTest
{
    public class UserRoleServiceTest
    {
        private readonly Mock<IReadWriteToJson> _dbContext;
        private readonly Mock<IUserInRole> _user;     

        public UserRoleServiceTest()
        {
            _dbContext = new Mock<IReadWriteToJson>();
            _user = new Mock<IUserInRole>();
        }

        [Fact]
        public void GetAllRolesShouldReturnListOfAllRoles()
        {
            //Arrange 
            var roles = Helper.ListOfRoles();
            _dbContext.Setup(x => x.ReadJson<Roles>("Roles.json")).ReturnsAsync(roles);

            //Act
            var result = new UserRolesService(_dbContext.Object);
            var actual = result.GetAllRoles().Result;

            //Assert
            Assert.Equal(roles, actual);
            Assert.IsType<List<Roles>>(actual);
        }


        [Fact]
        public async void AddUserToRolesShouldReturnTrueWhenNoError()
        {
            //Arrange 
            var role = new UserInRoles { RoleId = 3, UserId = "e29899c5-10eb-66a5-a9a9-b866b1cdec9e" };
            _dbContext.Setup(x => x.WriteJson<UserInRoles>(role, "UserInRole.json")).ReturnsAsync(true);

            //Act
            var result = new UserRolesService(_dbContext.Object);
            var actual = await result.AddUserToRole("e29899c5-10eb-66a5-a9a9-b866b1cdec9e", 3);

            //Assert
            Assert.True(actual);
            
        }
        [Fact]
        public void AddUserToRolesShouldThrowExceptionWhenIsError()
        {
            //Arrange 
            var role = new UserInRoles {RoleId=3,  UserId="" };
            _dbContext.Setup(x => x.WriteJson<UserInRoles>(role, "UserInRole.json")).Throws<Exception>();

            //Act
            var result = new UserRolesService(_dbContext.Object);


            //Assert
            Assert.ThrowsAsync<Exception>(() => result.AddUserToRole("", 3));

        }

        [Fact]
        public async void GetAUserRolesShouldReturnRoleWithCorrectId()
        {
            //Arrange 
            var userInRoles = Helper.ListOfUserInRoles();
            var roles = Helper.ListOfRoles();
            _dbContext.Setup(x => x.ReadJson<UserInRoles>("UserInRole.json")).ReturnsAsync(userInRoles);
            _dbContext.Setup(x => x.ReadJson<Roles>("Roles.json")).ReturnsAsync(roles);           

            //Act
            var result = new UserRolesService(_dbContext.Object);
            var actual = await result.GetUserRoles("e29844c5-10eb-44a5-a0a9-b922b1cdec9b");

            //Assert
            Assert.Equal("Admin", actual);
            Assert.NotNull(actual);

        }

        [Fact]
        public async void GetAUserRoleShouldReturnNullWithInCorrectId()
        {
            //Arrange 
            var userInRoles = Helper.ListOfUserInRoles();
            var roles = Helper.ListOfRoles();
            _dbContext.Setup(x => x.ReadJson<UserInRoles>("UserInRole.json")).ReturnsAsync(userInRoles);
            _dbContext.Setup(x => x.ReadJson<Roles>("Roles.json")).ReturnsAsync(roles);

            //Act
            var result = new UserRolesService(_dbContext.Object);
            var actual = await result.GetUserRoles("e29844c5-10eb-44a5");

            //Assert            
            Assert.Null(actual);

        }
    }
}
