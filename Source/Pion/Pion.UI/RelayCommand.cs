using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Pion.UI
{
    /// <summary>
    /// Represents an executable command.
    /// </summary>
    public sealed class RelayCommand : ICommand
    {
        /// <summary>
        /// The predicate which decides whether the command can execute or not.
        /// </summary>
        readonly Predicate<object> _canExecute;

        /// <summary>
        /// The Action to execute.
        /// </summary>
        readonly Action<object> _execute;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">The Action to execute.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">The Action to execute.</param>
        /// <param name="canExecute">The Predicate which decides whether the command may be executed.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Check if the command may be executed.
        /// </summary>
        /// <param name="parameter">The parameter of the caller.</param>
        /// <returns>True, if the command may be executed.</returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }


        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">The parameter of the command.</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// Registers the CanExecuteChangedEvent in the WPF Commanding System.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
