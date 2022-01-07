using ArbreSoft.DietManager.Domain.Repositories;
using ArbreSoft.DietManager.Infrastructure;
using ArbreSoft.DietManager.Infrastructure.Repositories;
using ArbreSoft.DietManager.Presentation.ViewModels;
using ArbreSoft.DietManager.Presentation.Views;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Windows;

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
            var provider = new ServiceCollection()
            .AddDbContext<AppDbContext>()
            .AddSingleton<IUnitOfWork, UnitOfWork>()
            .AddTransient<IMainViewModel, MainViewModel>()
            .AddTransient<IIngredientViewModel, IngredientViewModel>()
            //.AddTransient<IIngredientService, IngredientService>()
            //.AddTransient<IMealService, MealService>()
            //.AddTransient<ITransferService, FileService>()
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddMediatR(applicationAssembly)
            .BuildServiceProvider();
            provider.GetService<AppDbContext>().Migrate();
            new MainView(provider).Show();
        }
    }
}
