using System.Collections.Generic;

namespace Showcase.Domain
{
    public class Customer
    {
        public string Name { get; set; }

        public List<Order> Orders { get; set; }
    }
}