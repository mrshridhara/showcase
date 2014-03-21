using System;
using System.Threading.Tasks;

namespace Showcase.Wpf.Base.Communication
{
    public interface IEventStore
    {
        Task<TEvent> GetEventOfTypeAsync<TEvent>()
            where TEvent : IEvent, new();
    }
}