using KalaMarket.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarket.Application.Services.Finances.Queries.GetRequestPayForAdmin
{
    public class RequestPaysDto
    {
        public string SearchKey { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
