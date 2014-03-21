using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Showcase.Wpf.Base.Communication.Defaults
{
    public abstract class EventBase<TEventData> : EventBase, IEvent<TEventData>
    {
        public async Task RaiseAsync(TEventData eventData)
        {
            await RaiseEventAsync(eventData);
        }

        public async Task HookAsync(Func<TEventData, Task> asyncListener)
        {
            await AddSubscriberAsync(asyncListener);
        }

        public async Task UnhookAsync(Func<TEventData, Task> asyncListener)
        {
            await RemoveSubscriberAsync(asyncListener);
        }
    }

    public abstract class EventBase : IEvent
    {
        private readonly ConcurrentBag<WeakListener> _subscribers;

        protected EventBase()
        {
            _subscribers = new ConcurrentBag<WeakListener>();
        }

        public async Task RaiseAsync()
        {
            await RaiseEventAsync(null);
        }

        public async Task HookAsync(Func<Task> asyncListener)
        {
            await AddSubscriberAsync(asyncListener);
        }

        public async Task UnhookAsync(Func<Task> asyncListener)
        {
            await RemoveSubscriberAsync(asyncListener);
        }

        public async Task ClearAsync()
        {
            await Task.Run(() =>
            {
                while (_subscribers.IsEmpty == false)
                {
                    WeakListener weakListener;
                    if (_subscribers.TryTake(out weakListener))
                    {
                        weakListener.Clear();
                    }
                }
            });
        }

        protected async Task RaiseEventAsync(params object[] args)
        {
            foreach (var eachWeakListener in _subscribers.AsParallel())
            {
                await eachWeakListener.InvokeAsync(args);
            }
        }

        protected async Task AddSubscriberAsync(Delegate listener)
        {
            await Task.Run(() =>
            {
                var availableListener = _subscribers.FirstOrDefault(eachListener => eachListener.Equals(listener));
                if (availableListener == null)
                {
                    _subscribers.Add(new WeakListener(listener));
                }
            });
        }

        protected async Task RemoveSubscriberAsync(Delegate listener)
        {
            await Task.Run(() =>
            {
                var listenersToBeRemoved = _subscribers.Where(eachListener => eachListener.Equals(listener) || eachListener.IsAlive() == false);
                if (listenersToBeRemoved.Any())
                {
                    WeakListener weakListener;
                    if (_subscribers.TryTake(out weakListener))
                    {
                        if (listenersToBeRemoved.Contains(weakListener))
                        {
                            weakListener.Clear();
                        }
                        else
                        {
                            _subscribers.Add(weakListener);
                        }
                    }
                }
            });
        }
    }
}