using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Showcase.Wpf.Base.Communication.Defaults
{
    internal class WeakListener
    {
        private readonly WeakReference _reference;
        private readonly MethodInfo _method;

        public WeakListener(Delegate listerner)
        {
            _reference = new WeakReference(listerner.Target);
            _method = listerner.Method;
        }

        public void Clear()
        {
            _reference.Target = null;
        }

        public bool IsAlive()
        {
            return _reference.IsAlive;
        }

        public async Task InvokeAsync(params object[] args)
        {
            var target = _reference.Target;

            if (target != null)
            {
                await Task.Run(() => _method.Invoke(target, args));
            }
        }

        public override bool Equals(object obj)
        {
            var listener = obj as Delegate;
            if (listener == null)
            {
                return false;
            }

            var target = _reference.Target;
            if (target == null)
            {
                return false;
            }

            return target.Equals(listener.Target) && _method.Equals(listener.Method);
        }

        public override int GetHashCode()
        {
            var target = _reference.Target;
            if (target == null)
            {
                return _method.GetHashCode();
            }

            return target.GetHashCode() ^ _method.GetHashCode();
        }
    }
}
