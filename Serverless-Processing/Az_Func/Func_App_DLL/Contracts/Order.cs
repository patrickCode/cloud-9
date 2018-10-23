using System;

namespace Contracts
{
    public class OrderDto
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
        public double ProductPrice { get; set; }
        public double OrderPrice { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime? PlacedDate { get; set; }
    }
}
