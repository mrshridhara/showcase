namespace Showcase.Domain
{
    public class OrderedItem
    {
        public Item Item { get; set; }

        public int Quantity { get; set; }

        public double GetTotalPrice()
        {
            return Item.Price * Quantity;
        }
    }
}