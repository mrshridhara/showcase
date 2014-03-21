using System.Collections.Generic;

namespace Showcase.Domain
{
    public class ItemCatalog
    {
        public string Name { get; set; }

        public List<AvailableItem> AvailableItems { get; set; }
    }
}