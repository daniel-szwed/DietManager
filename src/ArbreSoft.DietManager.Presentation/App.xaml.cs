using ArbreSoft.DietManager.Domain.Repositories;
using ArbreSoft.DietManager.Infrastructure;
using ArbreSoft.DietManager.Infrastructure.Repositories;
using ArbreSoft.DietManager.Presentation.ViewModels;
using ArbreSoft.DietManager.Presentation.Views;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace ArbreSoft.DietManager.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Assembly applicationAssembly = typeof(Application.Dummy).Assembly;
            var provider = AppServiceProvider.Instance;
            provider.ServiceCollection
                .AddDbContext<AppDbContext>()
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddMediatR(applicationAssembly)
                .AddSingleton<IUnitOfWork, UnitOfWork>()
                .AddTransient<IMainViewModel, MainViewModel>()
                .AddTransient<INutritionFactsDialogViewModel, NutritionFactsDialogViewModel>()
                .AddTransient<INutritionFactsBrowserViewModel, NutritionFactsBrowserViewModel>();
                
            provider.GetService<AppDbContext>().Migrate();

            LoadConverters();

            new MainView().Show();
        }

        private void LoadConverters()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.GetInterfaces().Any(i => ConverterInterfaces.Any(name => i.Name == name)))
                {
                    Resources.Add(type.Name, Activator.CreateInstance(type));
                }
            }
        }

        private string[] ConverterInterfaces = new[] { nameof(IValueConverter), nameof(IMultiValueConverter) };
    }
}
