using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Showcase.Wpf.Base.Communication.Defaults
{
    internal class RelayCommand<TParameter> : ICommand
    {
        #region Fields

        private readonly Func<TParameter, Task> _execute;
        private readonly Func<TParameter, Task<bool>> _canExecute;

        #endregion // Fields

        #region Constructors

        public RelayCommand(Func<TParameter, Task> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Func<TParameter, Task> execute, Func<TParameter, Task<bool>> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute((TParameter)parameter).Result;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            var executeTask = _execute((TParameter)parameter);
            executeTask.Start();

            Task.WaitAll(executeTask);
        }

        #endregion // ICommand Members
    }

    internal class RelayCommand : ICommand
    {
        #region Fields

        private readonly Func<Task> _execute;
        private readonly Func<Task<bool>> _canExecute;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Func<Task> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Func<Task> execute, Func<Task<bool>> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute().Result;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            var executeTask = _execute();
            executeTask.Start();

            Task.WaitAll(executeTask);
        }

        #endregion // ICommand Members
    }
}