using Showcase.Domain;
using Showcase.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Showcase.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        public IEnumerable<Customer> Entities { get; set; }

        public async Task SaveAsync()
        {
            // Save functionality is not required.
            await Task.Run(() => { });
        }

        public async Task LoadAsync()
        {
            await Task.Run(() =>
            {
                Entities = new List<Customer> 
                {
                    new Customer
                    {
                        Name = "Customer 1",
                        Orders = new List<Order>
                        {
                            new Order 
                            {
                                Id = "Order1",
                                OrderDate = DateTime.Today.AddDays(-1),
                                OrderedItems = new List<OrderedItem>
                                {
                                    new OrderedItem
                                    {
                                        Item = new Item { Id = "Item1", Name = "Item 1", Price = 20 },
                                        Quantity = 2
                                    }
                                }
                            },                        
                            new Order 
                            {
                                Id = "Order2",
                                OrderDate = DateTime.Today.AddDays(-10),
                                OrderedItems = new List<OrderedItem>
                                {
                                    new OrderedItem
                                    {
                                        Item = new Item { Id = "Item2", Name = "Item 2", Price = 10.5 },
                                        Quantity = 1
                                    },                                
                                    new OrderedItem
                                    {
                                        Item = new Item { Id = "Item3", Name = "Item 3", Price = 1000.3 },
                                        Quantity = 3
                                    }
                                }
                            }
                        }
                    },                
                    new Customer
                    {
                        Name = "Customer 2"
                    }
                };
            });
        }
    }
}