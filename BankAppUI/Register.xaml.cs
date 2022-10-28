using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using BankAppCore.Models;

namespace BankAppUI
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private readonly IUsers _users;
        private readonly IUtilities _utility;
        

        public Register(IUsers users, IUtilities utilities)
        {
            _users = users;
            _utility = utilities;           
            InitializeComponent();
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Users person = new Users()
            {
                Id = Guid.NewGuid().ToString(),
                FullName = txtFirstName.Text + " " + txtLastName.Text,
                EmailAddress = txtEmail.Text,
                MobileNumber = txtPhone.Text,
                Password = _utility.ComputeSha256Hash(txtPassword.Text),
                IsActive = true,
                IsAdmin = false,
                Age = Convert.ToInt16(txtAge.Text)

            };

            try
            {
                var check = await _users.AddUser(person);
                if (check)
                {
                    MessageBox.Show("User was successfully added");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }


       
    }
}
