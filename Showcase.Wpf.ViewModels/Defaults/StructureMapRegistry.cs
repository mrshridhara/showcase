using Showcase.Wpf.Base;
using StructureMap.Configuration.DSL;

namespace Showcase.Wpf.ViewModels.Defaults
{
    public sealed class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<ViewModelBase>();
                scanner.WithDefaultConventions();
                scanner.AddAllTypesOf<IViewModel>();
            });
        }
    }
}
