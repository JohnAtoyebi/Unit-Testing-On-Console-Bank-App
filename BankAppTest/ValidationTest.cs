using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAppCore.Helper;
using Xunit;

namespace BankAppTest
{
    public class ValidationTest
    {
        private Validators _validators;
        public ValidationTest()
        {
            _validators = new Validators();
        }

        [Theory]
        [InlineData("john.smith@company.com", true)]
        [InlineData("johnsmith@company.com", true)]
        [InlineData("john.smith@company.comma", true)]
        [InlineData("john.smith@company.it", true)]
        [InlineData("john.smith.company.com", false)]
        [InlineData("john@smith@company.com", false)]
        [InlineData("john", false)]
        [InlineData("", false)]

        public void ShouldValidateEmail(string email, bool expected)
        {
            //Arrange 
            
           
            //Act
            var actual = _validators.CheckEmail(email);

            //Assert
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData("Bimbola", true)]       
        [InlineData("bimbola", false)]
        [InlineData("1234", false)]
        [InlineData("3bimbola", false)]        
        [InlineData("", false)]

        public void ShouldValidateName(string name, bool expected)
        {
            //Arrange 

            //Act
            var actual = _validators.CheckName(name);

            //Assert
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData("Bimbo@20", true)]
        [InlineData("Bimbo!33", true)]
        [InlineData("bimbola", false)]
        [InlineData("1234", false)]
        [InlineData("Bim*1", false)]
        [InlineData("", false)]

        public void ShouldValidatePassword(string password, bool expected)
        {
            //Arrange 

            //Act
            var actual = _validators.CheckPassword(password);

            //Assert
            Assert.Equal(expected, actual);

        }
    }
}
