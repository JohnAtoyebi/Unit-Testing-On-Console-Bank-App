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
    public class AccountOperationsTest
    {
        private readonly Mock<IAccount> _account;
        private readonly Mock<ITransactions> _transactions;

        public AccountOperationsTest()
        {
            _account = new Mock<IAccount>();
            _transactions = new Mock<ITransactions>();
        }

        [Fact]
        public void DepositShouldReturnTrueWhenNoError()
        {
            //Arrange
            var account = new Accounts { AccountNumber = "8693686809", Balance = 60000.0, AccountType = "Current", UserId = "e29899c5-10eb-66a5-a9a9-b866b1cdec9e" };
            double newAmount = 10000.0;
            account.Balance += newAmount;
            _account.Setup(x => x.GetAccountDetails(account.AccountNumber)).ReturnsAsync(account);
            _account.Setup(x => x.UpdateAccount(account)).ReturnsAsync(true);
            Transactions transactions = new Transactions() { AccountNumber = account.AccountNumber, Amount = newAmount.ToString(), Balance = account.Balance.ToString(), Description = "Money Deposit" };
            _transactions.Setup(x => x.AddTransaction(transactions)).ReturnsAsync(true);

            //Act

            var result = new AccountOperation(_transactions.Object, _account.Object);
            var actual = result.Deposit("8693686809", newAmount.ToString()).Result;

            //Assert
            Assert.True(actual);
        }

        [Fact]
        public void DepositShouldThrowExceptionWhenWrongAccount()
        {
            //Arrange
            var account = new Accounts {  };            
            _account.Setup(x => x.GetAccountDetails(account.AccountNumber)).Throws<Exception>();           

            //Act

            var result = new AccountOperation(_transactions.Object, _account.Object);


            //Assert
            Assert.ThrowsAsync<Exception>(() => result.Deposit("0000000000", ""));
        }

        [Fact]
        public void WithdrawShouldReturnTrueWhenNoError()
        {
            //Arrange
            var account = new Accounts { AccountNumber = "8693686809", Balance = 60000.0, AccountType = "Current", UserId = "e29899c5-10eb-66a5-a9a9-b866b1cdec9e" };
            double newAmount = 10000.0;
            account.Balance -= newAmount;
            _account.Setup(x => x.GetAccountDetails(account.AccountNumber)).ReturnsAsync(account);
            _account.Setup(x => x.UpdateAccount(account)).ReturnsAsync(true);
            Transactions transactions = new Transactions() { AccountNumber = account.AccountNumber, Amount = newAmount.ToString(), Balance = account.Balance.ToString(), Description = "Money Withdraw" };
            _transactions.Setup(x => x.AddTransaction(transactions)).ReturnsAsync(true);

            //Act

            var result = new AccountOperation(_transactions.Object, _account.Object);
            var actual = result.Withdraw("8693686809", newAmount.ToString()).Result;

            //Assert
            Assert.True(actual);
        }

        [Fact]
        public void WithdrawShouldReturnTrueWhenIsError()
        {
            //Arrange
            var account = new Accounts { AccountNumber = "0000000000", Balance = 60000.0, AccountType = "Saving", UserId = "e29899c5-10eb-66a5-a9a9-b866b1cdec9e" };
            double newAmount = 60000.0;
            _account.Setup(x => x.GetAccountDetails(account.AccountNumber)).Throws<Exception>();

            //Act

            var result = new AccountOperation(_transactions.Object, _account.Object);

            //Assert
            Assert.ThrowsAsync<Exception>(() => result.Withdraw("0000000000", newAmount.ToString()));
        }

        [Fact]
        public void WithdrawShouldReturnFalseWhenBalanceBelowMinBalance()
        {
            //Arrange
            var account = new Accounts { AccountNumber = "8693686809", Balance = 60000.0, AccountType = "Saving", UserId = "e29899c5-10eb-66a5-a9a9-b866b1cdec9e" };
            double newAmount = 60000.0;
            account.Balance -= newAmount;
            _account.Setup(x => x.GetAccountDetails(account.AccountNumber)).ReturnsAsync(account);                     

            //Act
            var result = new AccountOperation(_transactions.Object, _account.Object);
            var actual = result.Withdraw("8693686809", newAmount.ToString()).Result;

            //Assert
            Assert.False(actual);
        }

    }
}
