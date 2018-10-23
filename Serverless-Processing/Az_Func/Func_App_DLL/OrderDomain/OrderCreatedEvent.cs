using Contracts;
using System;

namespace OrderDomain
{
    public class OrderCreatedEvent: Event
    {
        public OrderCreatedEvent() { }

        public OrderCreatedEvent(Order order)
        {
            EventName = "ORDER_CREATED";
            OrderId = order.Id;
            OrderPrice = order.OrderPrice;
            CreatedOn = DateTime.UtcNow;
            Id = Guid.NewGuid().ToString();
        }

        public string OrderId { get; set; }
        public double OrderPrice { get; set; }
    }
}
