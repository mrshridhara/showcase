using System;
using System.Threading.Tasks;

namespace Showcase.Wpf.Base.Communication
{
    public interface IEvent<TEventData> : IEvent
    {
        Task RaiseAsync(TEventData eventData);

        Task HookAsync(Func<TEventData, Task> asyncListener);

        Task UnhookAsync(Func<TEventData, Task> asyncListener);
    }

    public interface IEvent
    {
        Task RaiseAsync();

        Task HookAsync(Func<Task> asyncListener);

        Task UnhookAsync(Func<Task> asyncListener);

        Task ClearAsync();
    }
}