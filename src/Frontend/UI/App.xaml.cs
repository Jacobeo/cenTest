using System;
using System.Windows;
using Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UI
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider serviceProvider;

        public static IConfiguration Config { get; private set; }


        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();

            Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddHttpClient<IDataService, APIDataService>(client =>
            {
                client.BaseAddress = new Uri(Config["ApiSettings:BaseAddress"]);
            });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
