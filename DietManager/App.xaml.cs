using DietManager.Repositories;
using DietManager.ViewModels;
using DietManager.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
            Container.RegisterType<IIngredientRepository, IngredientRepository>();
            Container.RegisterType<IIngredientViewModel, IngredientViewModel>("test");
            Container.Resolve<MainView>().Show();
            //Do the same actions for  all views and their viewmodels
        }
    }

}
