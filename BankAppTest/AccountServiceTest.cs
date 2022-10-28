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
    public class AccountServiceTest
    {
        private readonly Mock<IReadWriteToJson> _dbContext;
        public AccountServiceTest()
        {
            _dbContext = new Mock<IReadWriteToJson>();
        }

        [Fact]
        public void GetAllAccountShouldReturnListOfAllAccounts()
        {
            //Arrange 
            var accounts = Helper.ListOfAccounts();
            _dbContext.Setup(x => x.ReadJson<Accounts>("Accounts.json")).ReturnsAsync(accounts);

            //Act
            var result = new AccountService(_dbContext.Object);
            var actual = result.GetAllAccounts().Result;

            //Assert
            Assert.Equal(accounts, actual);
            Assert.IsType<List<Accounts>>(actual);
        }

        [Fact]
        public void GetAllAccountByAUserShouldReturnListOfAccounts()
        {
            //Arrange 
            var accounts = Helper.ListOfAccounts();
            string userId = "e29899c5-10eb-66a5-a9a9-b866b1cdec9e";
            var userAccs = accounts.FindAll(x => x.UserId == userId);
            _dbContext.Setup(x => x.ReadJson<Accounts>("Accounts.json")).ReturnsAsync(userAccs);

            //Act
            var result = new AccountService(_dbContext.Object);
            var actual = result.GetAllAccountByAUser(userId).Result;

            //Assert
            Assert.Equal(userAccs, actual);
            Assert.IsType<List<Accounts>>(actual);
            Assert.Equal(userAccs.Count, actual.Count);
            Assert.Equal(userAccs[0].AccountNumber, actual[0].AccountNumber);
        }

        [Fact]
        public void GetAccountDetailsShouldReturnAccount()
        {
            //Arrange 
            var accounts = Helper.ListOfAccounts();
            string accNumber = "8693686809";            
            _dbContext.Setup(x => x.ReadJson<Accounts>("Accounts.json")).ReturnsAsync(accounts);
            var userAccs = accounts.Find(x => x.AccountNumber == accNumber);


            //Act
            var result = new AccountService(_dbContext.Object);
            var actual = result.GetAccountDetails(accNumber).Result;

            //Assert
            Assert.Equal(userAccs, actual);
            Assert.IsType<Accounts>(actual);            
            Assert.Equal(userAccs.Balance, actual.Balance);
        }

        [Fact]
        public void GetAccountDetailsShouldReturnNullForInCorrectAccountNumber()
        {
            //Arrange 
            var accounts = Helper.ListOfAccounts();
            string accNumber = "0000000000";
            _dbContext.Setup(x => x.ReadJson<Accounts>("Accounts.json")).ReturnsAsync(accounts);
            var userAccs = accounts.Find(x => x.AccountNumber == accNumber);


            //Act
            var result = new AccountService(_dbContext.Object);
            var actual = result.GetAccountDetails(accNumber).Result;

            //Assert
            Assert.Equal(userAccs, actual);
            Assert.Null(actual);
           
        }

        [Fact]
        public async void ShouldAddAccountWhenNoError()
        {
            //Arrange 
            var account = new Accounts { AccountNumber = "8180347812", Balance = 15000.0, AccountType = "Saving", UserId = "e29844c5-10eb-66a5-a0a9-b844b1cdec9c" };
            _dbContext.Setup(x => x.WriteJson<Accounts>(account, "Accounts.json")).ReturnsAsync(true);

            //Act
            var result = new AccountService(_dbContext.Object);
            var actual = await result.AddAccount(account);

            //Assert
            Assert.True(actual);
            Assert.IsType<bool>(actual);
        }

        [Fact]
        public void ShouldThrowExceptionWhenAddAccountHasError()
        {
            //Arrange 
            var account = new Accounts { };
            _dbContext.Setup(x => x.WriteJson<Accounts>(account, "Accounts.json")).Throws<Exception>();

            //Act
            var result = new AccountService(_dbContext.Object);           

            //Assert
            Assert.ThrowsAsync<Exception>(() => result.AddAccount(account));

        }

        [Fact]
        public void UpdateAccountShouldReturnTrueNoError()
        {
            //Arrange 
            var accounts = Helper.ListOfAccounts();            
            _dbContext.Setup(x => x.ReadJson<Accounts>("Accounts.json")).ReturnsAsync(accounts);
            string accNumber = "2628175905";
            var userAccs = accounts.Find(x => x.AccountNumber == accNumber);
            userAccs.Balance = 40000.0;
            _dbContext.Setup(x => x.UpdateJson<Accounts>(accounts, "Accounts.json")).ReturnsAsync(true);


            //Act
            var result = new AccountService(_dbContext.Object);
            var actual = result.UpdateAccount(userAccs).Result;

            //Assert
            Assert.True(actual);
        }

        [Fact]
        public void UpdateAccountShouldReturnFalseWithError()
        {
            //Arrange 
            var accounts = Helper.ListOfAccounts();
            _dbContext.Setup(x => x.ReadJson<Accounts>("Accounts.json")).ReturnsAsync(accounts);                    
            _dbContext.Setup(x => x.UpdateJson<Accounts>(accounts, "Accounts.json")).ReturnsAsync(false);

            Accounts userAcc = new Accounts { AccountNumber = "0000000000", Balance = 40000.0, AccountType = "Saving", UserId = "e29844c5-10eb-66a5-a0a9-b844b1cdec9c" };


            //Act
            var result = new AccountService(_dbContext.Object);
            var actual = result.UpdateAccount(userAcc).Result;

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void UpdateAccountShouldReturnFalseWhenAccountsIsEmpty()
        {
            //Arrange 
            var accounts = new List<Accounts>();
            _dbContext.Setup(x => x.ReadJson<Accounts>("Accounts.json")).ReturnsAsync(accounts);         
             Accounts userAcc = new Accounts { AccountNumber = "0000000000", Balance = 40000.0, AccountType = "Saving", UserId = "e29844c5-10eb-66a5-a0a9-b844b1cdec9c" };


            //Act
            var result = new AccountService(_dbContext.Object);
            var actual = result.UpdateAccount(userAcc).Result;

            //Assert
            Assert.False(actual);
        }
    }
}
