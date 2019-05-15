using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DietManager.ViewModels
{
    public interface IMainViewModel
    {
        ICommand ManageIngredients { get; }
        ICommand AddMeal { get; }
    }
}
