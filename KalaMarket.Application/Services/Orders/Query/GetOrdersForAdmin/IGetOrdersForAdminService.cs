using KalaMarket.Application.Services.Orders.Query.GetOrdersForAdmin;
using KalaMarket.Common.Dto;
using KalaMarket.Domain.Entities.Orders;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarket.Application.Services.Orders.Query.GetOrdersForAdmin
{
    public interface IGetOrdersForAdminService
    {
        ResultDto<ResultGetOrderDto> Execute(RequestGetOrder requestGetOrder);
    }
}
