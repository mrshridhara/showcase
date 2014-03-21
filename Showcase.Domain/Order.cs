using System;
using System.Collections.Generic;
using System.Linq;

namespace Showcase.Domain
{
    public class Order
    {
        public string Id { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderedItem> OrderedItems { get; set; }

        public double GetTotalPrice()
        {
            return OrderedItems.Sum(eachOrderedItem => eachOrderedItem.GetTotalPrice());
        }
    }
}