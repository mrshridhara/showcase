using Showcase.Wpf.Base.Communication;
using Showcase.Wpf.Base.Communication.Defaults;
using Showcase.Wpf.Base.Presentation;
using Showcase.Wpf.Base.Presentation.Defaults;
using StructureMap.Configuration.DSL;

namespace Showcase.Wpf.Base.Defaults
{
    public sealed class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            For<IDependencyResolver>().Use<StructureMapDependencyResolver>();

            For<ICommandFactory>().Use<RelayCommandFactory>();
            For<IEventStore>().Singleton().Use<EventStore>();

            For<IDataTemplateStoreFactory>().Use<DataTemplateStoreFactory>();
            For<IDataTemplateStore>().Use<DataTemplateStore>();
            For<IRegionStore>().Singleton().Use<RegionStore>();
        }
    }
}