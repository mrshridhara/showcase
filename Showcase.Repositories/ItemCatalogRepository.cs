using Showcase.Domain;
using Showcase.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Showcase.Repositories
{
    public class ItemCatalogRepository : IRepository<ItemCatalog>
    {
        public IEnumerable<ItemCatalog> Entities { get; set; }

        public async Task SaveAsync()
        {
            // Save functionality is not required.
            await Task.Run(() => { });
        }

        public async Task LoadAsync()
        {
            await Task.Run(() =>
            {
                Entities = new List<ItemCatalog>
                {
                    new ItemCatalog
                    {
                        Name = "Catalog 1",
                        AvailableItems = new List<AvailableItem>
                        {
                            new AvailableItem
                            {
                                Item = new Item { Id = "Item1", Name = "Item 1", Price = 20 },
                                Quantity = 20
                            },
                            new AvailableItem
                            {
                                Item = new Item { Id = "Item2", Name = "Item 2", Price = 10.5 },
                                Quantity = 5
                            },
                            new AvailableItem
                            {
                                Item = new Item { Id = "Item3", Name = "Item 3", Price = 1000.3 },
                                Quantity = 0
                            }
                        }
                    }
                };
            });
        }
    }
}