using KalaMarket.Domain.Entities.Orders;
using System.Collections.Generic;

namespace KalaMarket.Application.Services.Orders.Query.GetUserOrders
{
    public class GetUserOrdersDto
    {
        public long OrderId { get; set; }
        public OrderState OrderState { get; set; }
        public long RequestPayId { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
