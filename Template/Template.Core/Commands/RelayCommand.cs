using System;
using System.Windows.Input;

namespace Template.Core
{
    /// <summary>
    /// Command used for executing an action.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;

        /// <summary>
        /// Creates a relay command with specific action.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        public RelayCommand(Action execute) => this.execute = obj => execute();

        /// <summary>
        /// Creates a relay command with specific action.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        public RelayCommand(Action<object> execute) => this.execute = execute;

        #region ICommand Implementation

        /// <summary>
        /// Never changes because relay command can always execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// It can!
        /// </summary>
        /// <param name="parameter">Doesn't change the output.</param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Executes the given command.
        /// </summary>
        /// <param name="parameter">Parameter to use while executing the command.</param>
        public void Execute(object parameter) => execute?.Invoke(parameter); 

        #endregion
    }
}
