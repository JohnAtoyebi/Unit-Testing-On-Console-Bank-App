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
using Microsoft.Extensions.DependencyInjection;

namespace BankAppUI
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        private readonly IServiceProvider _serviceProvider;
        public Admin(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            Register register = _serviceProvider.GetRequiredService<Register>();
            register.ShowDialog();
        }

        private void btnAllUsers_Click(object sender, RoutedEventArgs e)
        {
            GetAllUsers getAllUsers = _serviceProvider.GetRequiredService<GetAllUsers>();
            getAllUsers.Show();
        }

        private void btnGetAUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddUserRole_Click(object sender, RoutedEventArgs e)
        {
            UserRole userRole = _serviceProvider.GetRequiredService<UserRole>();
            userRole.ShowDialog();
        }

        private void btnGetAllTransactions_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow fm = _serviceProvider.GetRequiredService<MainWindow>();
            fm.Show();
        }
    }
}
