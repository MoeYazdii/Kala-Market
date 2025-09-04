
using KalaMarket.Application.Services.Orders.Query.GetOrdersForAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarket.Application.Services.Orders.Query.GetOrdersForAdmin
{
    public class ResultGetOrderDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<OrdersDto> Orders { get; set; }
    }
}
