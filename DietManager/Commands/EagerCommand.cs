using System;
using System.Windows.Input;

namespace DietManager.Commands
{
    /// <summary>
    /// https://stackoverflow.com/a/47190608
    /// </summary>
    public class EagerCommand : ICommand
    {
        public delegate void ICommandOnExecute(object parameters);
        public delegate bool ICommandOnCanExecute(object parameters);

        private ICommandOnExecute _execute;
        private ICommandOnCanExecute _canExecute;

        public EagerCommand(ICommandOnExecute onExecuteMethod, ICommandOnCanExecute onCanExecuteMethod = null)
        {
            _execute = onExecuteMethod;
            _canExecute = onCanExecuteMethod;
        }

        #region ICommand Members

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

        #endregion
    }
}
