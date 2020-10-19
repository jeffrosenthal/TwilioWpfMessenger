using Microsoft.Extensions.DependencyInjection;
using ServiceStack;

namespace WpfMessenger
{
    public class AppHost : AppSelfHostBase
    {
        private readonly ServiceProvider _serviceProvider;

        public AppHost(ServiceProvider sp)
            : base("HttpListener Self-Host", typeof(TwilioService).Assembly)
        {
            _serviceProvider = sp;
        }

        public override void Configure(Funq.Container container)
        {
            //ServiceStack internally uses its own IoC
            //By implementing an adapter, the calls are passed to the Microsoft.Extensions.DependencyInjection
            container.Adapter = new MicrosoftDependencyInjectionAdapter(_serviceProvider);
        }
    }
}