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
    public class TransactionsServiceTest
    {
        private readonly Mock<IReadWriteToJson> _dbContext;
        public TransactionsServiceTest()
        {
            _dbContext = new Mock<IReadWriteToJson>();
        }

        [Fact]
        public void GetAllTransactionsForAnAccountShouldReturAListOfTransaction()
        {
            //Arrange 
            var trans = Helper.ListOfTransactions();
            string accNum = "2628175905";
            var accTrans = trans.FindAll(x => x.AccountNumber == accNum);
            _dbContext.Setup(x => x.ReadJson<Transactions>("Transactions.json")).ReturnsAsync(trans);

            //Act
            var result = new TransactionsService(_dbContext.Object);
            var actual = result.GetAllTransactionsForAnAccount(accNum).Result;

            //Assert
            Assert.Equal(accTrans, actual);
            Assert.IsType<List<Transactions>>(actual);
        }

        [Fact]
        public async void AddTransactionShouldReturnTrueWhenNoError()
        {
            //Arrange 
            var trans = new Transactions { AccountNumber = "3011667709", Amount = "1000.0", Balance = "15000.0", Description = "Deposit Money", Date = DateTime.Now };
            _dbContext.Setup(x => x.WriteJson<Transactions>(trans, "Transactions.json")).ReturnsAsync(true);

            //Act
            var result = new TransactionsService(_dbContext.Object);
            var actual = await result.AddTransaction(trans);

            //Assert
            Assert.True(actual);
            Assert.IsType<bool>(actual);
        }


        [Fact]
        public void AddTransactionShouldReturnFalseWhenIsError()
        {
            //Arrange 
            var trans = new Transactions {  };
            _dbContext.Setup(x => x.WriteJson<Transactions>(trans, "Transactions.json")).Throws<Exception>();

            //Act
            var result = new TransactionsService(_dbContext.Object);


            //Assert
            Assert.ThrowsAsync<Exception>(() => result.AddTransaction(trans));
        }
    }
}
