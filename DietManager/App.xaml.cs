using Data.Context;
using Data.Repositories;
using DietManager.Services;
using DietManager.ViewModels;
using DietManager.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace DietManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var provider = new ServiceCollection()
            .AddDbContext<AppDbContext>()
            .AddTransient<IMainViewModel, MainViewModel>()
            .AddTransient<IIngredientViewModel, IngredientViewModel>()
            .AddTransient<IApiService, ApiService>()
            .AddTransient<IIngredientBaseRepository, IngredientBaseRepository>()
            .AddTransient<IIngredientRepository, IngredientRepository>()
            .AddTransient<IMealRepository, MealRepository>()
            .AddTransient<IIngredientService, IngredientService>()
            .AddTransient<IMealService, MealService>()
            .AddTransient<IImportExportService, FileImportExportService>()
            .BuildServiceProvider();
            provider.GetService<AppDbContext>().Migrate();
            new MainView(provider).Show();
        }
    }

}