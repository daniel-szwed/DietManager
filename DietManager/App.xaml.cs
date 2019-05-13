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
            Container.RegisterType<IApiService, ApiService>();
            Container.RegisterType<IIngredientRepository, IngredientRepository>();
            Container.RegisterType<IIngredientViewModel, IngredientViewModel>("IngredientViewModel");
            Container.RegisterType<INutritionFactsViewModel, NutritionFactsViewModel>("NutritionFactsViewModel");
            Container.Resolve<MainView>().Show();
            //Do the same actions for  all views and their viewmodels
        }
    }

}
