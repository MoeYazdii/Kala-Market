using KalaMarket.Common.Dto;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarket.Application.Services.Orders.Query.GetUserOrders
{
    public interface IGetUserOrdersService
    {
        ResultDto<List<GetUserOrdersDto>> Execute(long userId);
    }
}
