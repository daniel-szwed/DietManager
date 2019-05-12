using System;
using System.Windows.Input;

namespace DietManager.Commands
{
    public class Command : ICommand
    {
        Action<object> _TargetExecuteMethod;
        Func<object, bool> _TargetCanExecuteMethod;

        public Command(Action<object> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public Command(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {

            if (_TargetCanExecuteMethod != null)
            {
                return _TargetCanExecuteMethod(parameter);
            }

            if (_TargetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        // Beware - should use weak references if command instance lifetime is longer than lifetime of UI objects that get hooked up to command

        // Prism commands solve this in their implementation 
        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            _TargetExecuteMethod?.Invoke(parameter);
        }
    }
}
