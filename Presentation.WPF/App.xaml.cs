using System.Configuration;
using System.Data;
using System.Windows;
using ContactListApp.Business.Data;
using ContactListApp.Business.Interfaces;
using ContactListApp.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Presentation.WPF.ViewModels;

namespace Presentation.WPF
{
    public partial class App : Application
    {
        public IServiceProvider? ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IContactService, ContactService>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainWindow>();
            

            
        }
    }

}
