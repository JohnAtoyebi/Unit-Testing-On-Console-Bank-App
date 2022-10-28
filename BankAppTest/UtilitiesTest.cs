using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAppCore.Helper;
using Xunit;

namespace BankAppTest
{
    public class UtilitiesTest
    {
        private Utilities _utilities;
        public UtilitiesTest()
        {
            _utilities = new Utilities();
        }


        [Theory]
        [InlineData(10, 10)]
        [InlineData(3, 3)]       
        public void ShouldGenerateNumbers(int num, int expected)
        {
            //Arrange 
            string accNum = _utilities.RandomDigits(num);
            //Act
            int actual = accNum.Length;

            //Assert
            Assert.Equal(expected, actual);

        }

        [Fact]

        public void ShouldHashAPassword()
        {
            // Arrange 
            string expected = "7676aaafb027c825bd9abab78b234070e702752f625b752e55e55b48e607e358";

            //Act 
            var actual = _utilities.ComputeSha256Hash("admin@123");

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
