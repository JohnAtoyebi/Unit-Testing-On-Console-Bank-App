using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using BankAppCore.Interfaces;
using BankAppCore.Models;

namespace BankAppTest
{
    public class ReadWriteJsonTest
    {
        private readonly Mock<IReadWriteToJson> _dbContext;

        public ReadWriteJsonTest()
        {
            _dbContext = new Mock<IReadWriteToJson>();
        }



        [Fact]

        public async void ShouldWriteToJson()
        {
            //Arrange 
            var user = new Users { Id = "e21234c5-10eb-44a5-a0a7-b922b1fdec9b", FullName = "Chisom Okno", EmailAddress = "chisom@gmail.com", Password = "7676aaafb027c825bd9abab78b234070e702752f625b752e55e55b48e607e358", MobileNumber = "08975435678", IsActive = true, IsAdmin = false, Age = 33, Created_at = DateTime.Now };
            _dbContext.Setup(x => x.WriteJson<Users>(user, "Users.json")).ReturnsAsync(true);

            //Act
            var actual = await _dbContext.Object.WriteJson<Users>(user, "Users.json");

            //Assert
            Assert.True(actual);
            Assert.IsType<bool>(actual);
            _dbContext.Verify(x => x.WriteJson<Users>(user, "Users.json"), Times.Once);
        }


       
    }
}
