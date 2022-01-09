using Microsoft.Extensions.DependencyInjection;

namespace ArbreSoft.DietManager.Infrastructure
{
    public class AppServiceProvider
    {
        private static object _lock = new();
        private static volatile AppServiceProvider _instance;
        private static ServiceCollection _serviceCollection;

        private AppServiceProvider()
        {
            _serviceCollection = new();
        }

        public static AppServiceProvider Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance is null)
                    {
                        _instance = new();
                    }
                }

                return _instance;
            }
        }

        public ServiceCollection ServiceCollection => _serviceCollection;

        public T GetService<T>() 
        {
            var provider = _serviceCollection.BuildServiceProvider();

            return provider.GetService<T>();
        }
    }
}
