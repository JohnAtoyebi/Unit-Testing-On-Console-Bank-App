using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BankAppCore.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BankAppUI
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        private readonly IAccount _account;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAccountOperation _accountOperation;
        public Customer(IAccount account, IServiceProvider serviceProvider, IAccountOperation accountOperation)
        {
            InitializeComponent();
            _account = account;
            _serviceProvider = serviceProvider;
            _accountOperation = accountOperation;
        }

        

        private async void Customer_Load(object sender, RoutedEventArgs e)
        {
            lblCustomerWelcome.Content = $"Welcome {GlobalVariable.GlobalUser.FullName} to your Piggy Bank";
            var accounts = await _account.GetAllAccountByAUser(GlobalVariable.GlobalUser.Id);
            if (accounts != null)
            {
                foreach (var item in accounts)
                {
                    cmdAccountNum.Items.Add(item.AccountNumber);
                    
                }
            }
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            CreateAccount account = _serviceProvider.GetRequiredService<CreateAccount>();
            account.ShowDialog();
        }

        private async void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            if (cmdAccountNum.Text != "" && txtAmount.Text != "")
            {
                bool check = await _accountOperation.Deposit(cmdAccountNum.Text, txtAmount.Text);
                if (check)
                {
                    MessageBox.Show("Deposit was Successful");
                }
                else
                {
                    MessageBox.Show("Error Occurs");
                }
            }
        
            else
            {
                MessageBox.Show("Please select an Account Number");
            }
        }
        private async void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            if (cmdAccountNum.Text != "" && txtAmount.Text != "")
            {
                bool check = await _accountOperation.Withdraw(cmdAccountNum.Text, txtAmount.Text);
                if (check)
                {
                    MessageBox.Show("Withdraw was Successful");
                }
                else
                {
                    MessageBox.Show("Insufficient Fund");
                }

            }
            else
            {
                MessageBox.Show("Please select an Account Number");
            }
        }

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            Transfer transfer = _serviceProvider.GetRequiredService<Transfer>();
            transfer.ShowDialog();
        }

        private void btnTransactions_Click(object sender, RoutedEventArgs e)
        {
            AccountStatement account = _serviceProvider.GetRequiredService<AccountStatement>();
            account.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow fm = _serviceProvider.GetRequiredService<MainWindow>();
            fm.Show();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtAmount.Text = "";
        }

        private async void cmdAccountNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = cmdAccountNum.SelectedItem.ToString();
           
            var account = await _account.GetAccountDetails(selected);

            if (account != null)
            {
                txtBalance.Text = "₦" + account.Balance.ToString("N");
            }
        }
    }
}
