using System;
using System.Windows.Input;

namespace WebDownloader.Commands
{
    public class DelegateCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExceute;
        event EventHandler? ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public DelegateCommand(Action<object> execute, Func<object, bool> canExceute = null)
        {
            this.execute = execute;
            this.canExceute = canExceute;
        }
        bool ICommand.CanExecute(object? parameter)
        {
            return this.canExceute == null || this.canExceute(parameter);
        }

        void ICommand.Execute(object? parameter)
        {
            this.execute(parameter);
        }
    }
}
