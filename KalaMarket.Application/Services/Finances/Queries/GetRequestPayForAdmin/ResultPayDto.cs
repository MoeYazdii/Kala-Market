
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarket.Application.Services.Finances.Queries.GetRequestPayForAdmin
{
    public class ResultPayDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<PayDto> RequestPay { get; set; }
    }
}
