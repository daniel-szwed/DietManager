using System;
using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.Commands
{
    /// <summary>
    /// https://stackoverflow.com/a/47190608
    /// </summary>
    public class EagerCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public EagerCommand(Action<object> onExecuteMethod, Func<object, bool> onCanExecuteMethod = null)
        {
            _execute = onExecuteMethod;
            _canExecute = onCanExecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameters)
        {
            return _canExecute?.Invoke(parameters) ?? true;
        }

        public void Execute(object parameters)
        {
            _execute?.Invoke(parameters);
        }
    }
}