using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Showcase.Wpf.Base.Communication.Defaults
{
    public class RelayCommandFactory : ICommandFactory
    {
        public async Task<ICommand> CreateCommandAsync<TParameter>(Func<TParameter, Task> execute)
        {
            return await Task.Run(() => new RelayCommand<TParameter>(execute));
        }

        public async Task<ICommand> CreateCommandAsync<TParameter>(Func<TParameter, Task> execute, Func<TParameter, Task<bool>> canExecute)
        {
            return await Task.Run(() => new RelayCommand<TParameter>(execute, canExecute));
        }

        public async Task<ICommand> CreateCommandAsync(Func<Task> execute)
        {
            return await Task.Run(() => new RelayCommand(execute));
        }

        public async Task<ICommand> CreateCommandAsync(Func<Task> execute, Func<Task<bool>> canExecute)
        {
            return await Task.Run(() => new RelayCommand(execute, canExecute));
        }
    }
}