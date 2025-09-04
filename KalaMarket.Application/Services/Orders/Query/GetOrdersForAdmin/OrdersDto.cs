using KalaMarket.Domain.Entities.Orders;
using System;

namespace KalaMarket.Application.Services.Orders.Query.GetOrdersForAdmin
{
    public class OrdersDto
    {
        public long OrderId { get; set; }
        public DateTime InsetTime { get; set; }
        public long RequestId { get; set; }
        public long UserId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerEmail { get; set; }
        public int ProductCount { get; set; }
        public OrderState OrderState { get; set; }
    }
}
