using Contracts;
using System;

namespace OrderDomain
{
    public class Order: AggregateRoot
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
        public double ProductPrice { get; set; }
        public double OrderPrice { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime PlacedDate { get; set; }

        public void CreateOrder(OrderDto order)
        {
            Id = Guid.NewGuid().ToString();
            ProductId = order.ProductId;
            CustomerName = order.CustomerName;
            Quantity = order.Quantity;
            ProductPrice = order.ProductPrice;
            DeliveryDate = order.DeliveryDate;
            OrderPrice = Quantity * ProductPrice;
            if (DeliveryDate.ToUniversalTime() < DateTime.UtcNow)
            {
                throw new Exception("Order cannot be created for a past delivery date");
            }
            PlacedDate = DateTime.UtcNow;

            var orderCreatedEvent = new OrderCreatedEvent(this);
            AddEvent(orderCreatedEvent);
        }
    }
}