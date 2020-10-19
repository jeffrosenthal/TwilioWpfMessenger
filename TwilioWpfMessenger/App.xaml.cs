using System;
using System.ComponentModel.Design;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WpfMessenger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public static IConfigurationRoot Configuration { get; set; }
        private readonly ServiceCollection _services;
        public App()
        {
            var builder = new ConfigurationBuilder();
            var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) ||
                                devEnvironmentVariable.ToLower() == "development";

            if (isDevelopment)//UserSecrets should only be used while under development
            {
                builder.AddUserSecrets<App>();
            }

            Configuration = builder.Build();
            _services = new ServiceCollection();
            _serviceProvider = ConfigureServices(_services);
        }
        private ServiceProvider ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddSingleton<ViewModel>();
            services.AddScoped<TwilioService>();
            
            services.AddSingleton<MessageSender>();
            services.AddSingleton<MainWindow>();
            return _services.BuildServiceProvider();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                var listeningOn = "http://*:1337/";

                //Start listening on port 1337 in the self host
                var appHost = new AppHost(_serviceProvider);//_serviceProvider.TryResolve<AppHost>());//     .GetService<AppHost>();
                appHost.Init();
                appHost.Start(listeningOn);

                
                //Start the MainWindow
                var mainWindow = _serviceProvider.GetService<MainWindow>();
                mainWindow.Show();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            
        }
    }
}
