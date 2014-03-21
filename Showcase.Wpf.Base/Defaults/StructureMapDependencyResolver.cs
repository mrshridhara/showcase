using System;
using System.Collections.Generic;
using System.Linq;

using StructureMap;

namespace Showcase.Wpf.Base.Defaults
{
    public sealed class StructureMapDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public IDictionary<Type, Type> RegisteredTypes
        {
            get
            {
                return _container.Model.PluginTypes.ToDictionary(config => config.PluginType, config => config.Default.ConcreteType);
            }
        }

        public TInstance GetInstance<TInstance>()
        {
            return _container.GetInstance<TInstance>();
        }

        public object GetInstance(Type instanceType)
        {
            return _container.GetInstance(instanceType);
        }
    }
}