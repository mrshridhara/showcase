using Showcase.Wpf.Base;
using StructureMap;
using BaseDefaultsRegistry = Showcase.Wpf.Base.Defaults.StructureMapRegistry;
using RepositoriesRegistry = Showcase.Repositories.Defaults.StructureMapRegistry;
using ViewModelsRegistry = Showcase.Wpf.ViewModels.Defaults.StructureMapRegistry;

namespace Showcase.Wpf
{
    class IocConfig
    {
        public static IDependencyResolver RegisterAllDependencies()
        {
            ObjectFactory.Initialize(config =>
            {
                config.AddRegistry<BaseDefaultsRegistry>();
                config.AddRegistry<RepositoriesRegistry>();
                config.AddRegistry<ViewModelsRegistry>();
            });

            return ObjectFactory.GetInstance<IDependencyResolver>();
        }
    }
}