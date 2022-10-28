using System;
using System.Windows;
using BankAppCore.Helper;
using BankAppCore.Interfaces;
using BankAppCore.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BankAppUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {            
            services.AddSingleton<MainWindow>();
            services.AddScoped<IUsers, UserService>();
            services.AddScoped<IAccount, AccountService>();
            services.AddScoped<ITransactions, TransactionsService>();
            services.AddScoped<IUserInRole, UserRolesService>();
            services.AddScoped<IAuth, AuthService>();
            services.AddScoped<IAccountOperation, AccountOperation>();
            services.AddScoped<IUtilities, Utilities>();
            services.AddScoped<IValidators, Validators>();
            services.AddScoped<IReadWriteToJson, ReadWriteToJson>();
            services.AddScoped<MainWindow>();
            services.AddScoped<Admin>();
            services.AddScoped<Customer>();
            services.AddScoped<Register>();
            services.AddScoped<GetAllUsers>();
            services.AddScoped<UserRole>();
            services.AddScoped<CreateAccount>();
            services.AddScoped<AccountStatement>();
            services.AddScoped<Transfer>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
