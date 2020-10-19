using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Configuration;

namespace WpfMessenger
{
    public class MicrosoftDependencyInjectionAdapter : IContainerAdapter
    {
        private readonly ServiceProvider _serviceProvider;

        public MicrosoftDependencyInjectionAdapter(ServiceProvider sp)
        {
            _serviceProvider = sp;
        }
        public T TryResolve<T>()
        {
            return _serviceProvider.TryResolve<T>();
        }

        public T Resolve<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}