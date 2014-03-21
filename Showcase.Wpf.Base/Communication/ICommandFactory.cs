using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Showcase.Wpf.Base.Communication
{
    public interface ICommandFactory
    {
        Task<ICommand> CreateCommandAsync<TParameter>(Func<TParameter, Task> execute);

        Task<ICommand> CreateCommandAsync<TParameter>(Func<TParameter, Task> execute, Func<TParameter, Task<bool>> canExecute);

        Task<ICommand> CreateCommandAsync(Func<Task> execute);

        Task<ICommand> CreateCommandAsync(Func<Task> execute, Func<Task<bool>> canExecute);
    }
}
