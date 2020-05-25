namespace WebMarket.Model.Domain
{
    public class CartItem
    {
        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }
    }
}
