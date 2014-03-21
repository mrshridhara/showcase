using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Showcase.Wpf.Base.Communication.Defaults
{
    public sealed class EventStore : IEventStore
    {
        private readonly ConcurrentDictionary<Type, IEvent> _eventDictionary;

        public EventStore()
        {
            _eventDictionary = new ConcurrentDictionary<Type, IEvent>();
        }

        public async Task<TEvent> GetEventOfTypeAsync<TEvent>()
            where TEvent : IEvent, new()
        {
            return await Task.Run(() => (TEvent)_eventDictionary.GetOrAdd(typeof(TEvent), eventType => new TEvent()));
        }
    }
}