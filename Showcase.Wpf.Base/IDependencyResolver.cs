using System;
using System.Collections.Generic;

namespace Showcase.Wpf.Base
{
    public interface IDependencyResolver
    {
        IDictionary<Type, Type> RegisteredTypes { get; }

        TInstance GetInstance<TInstance>();

        object GetInstance(Type instanceType);
    }
}