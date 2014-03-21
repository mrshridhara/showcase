using Showcase.Domain;
using Showcase.Domain.Repositories;
using StructureMap.Configuration.DSL;

namespace Showcase.Repositories.Defaults
{
    public sealed class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            For<IRepository<Customer>>().Use<CustomerRepository>();
            For<IRepository<ItemCatalog>>().Use<ItemCatalogRepository>();
        }
    }
}