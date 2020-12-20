using DietManager.Data;
using DietManager.Repositories;
using DietManager.Services;
using DietManager.ViewModels;
using DietManager.Views;
using System.Windows;
using Unity;

namespace DietManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IUnityContainer Container { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Container = new UnityContainer();
            Container.RegisterType<AppDbContext>();
            Container.Resolve<AppDbContext>().Migrate();
            Container.RegisterType<IMainViewModel, MainViewModel>("MainViewModel");
            Container.RegisterType<IIngredientViewModel, IngredientViewModel>("IngredientViewModel");
            Container.RegisterType<IApiService, ApiService>();
            Container.RegisterType<IIngredientBaseRepository, IngredientBaseRepository>();
            Container.RegisterType<IIngredientRepository, IngredientRepository>();
            Container.RegisterType<IMealRepository, MealRepository>();
            Container.RegisterType<IIngredientService, IngredientService>();
            Container.RegisterType<IMealService, MealService>();
            Container.RegisterType<IImportExportService, FileImportExportService>();
            Container.Resolve<MainView>().Show();
        }
    }

}