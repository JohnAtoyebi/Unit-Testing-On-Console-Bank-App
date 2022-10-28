using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BankAppCore.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BankAppUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAuth _auth;
        private IServiceProvider _serviceProvider;
        public MainWindow(IAuth auth, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _auth = auth;
            _serviceProvider = serviceProvider;
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = await _auth.Login(txtEmail.Text, txtPassword.Text);
                if (user != null)
                {
                    GlobalVariable.UserRole = user.Keys.SingleOrDefault();
                    GlobalVariable.GlobalUser = user[GlobalVariable.UserRole];

                    if (GlobalVariable.GlobalUser.IsAdmin)
                    {

                        Hide();
                        Admin admin = _serviceProvider.GetRequiredService<Admin>();
                        admin.Show();

                    }
                    else
                    {
                        Hide();
                        Customer customer = _serviceProvider.GetRequiredService<Customer>();
                        customer.Show();

                    }
                }
                else
                {
                    MessageBox.Show("Invalid Credientail");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit1_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
