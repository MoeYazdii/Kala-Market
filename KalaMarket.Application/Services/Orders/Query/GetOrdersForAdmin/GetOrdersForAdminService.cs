using KalaMarket.Application.Interfaces.Contexts;
using KalaMarket.Application.Services.Orders.Query.GetOrdersForAdmin;
using KalaMarket.Application.Services.Products.Queries.GetProductForAdmin;
using KalaMarket.Common;
using KalaMarket.Common.Dto;
using KalaMarket.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KalaMarket.Application.Services.Orders.Query.GetOrdersForAdmin
{
    public class GetOrdersForAdminService : IGetOrdersForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetOrdersForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetOrderDto> Execute(RequestGetOrder request)
        {
            int rowCount = 1;
            var query = _context.Orders
                .Include(o => o.OrderDetails)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                query = query.Where(o => o.User.FullName.Contains(request.SearchKey)
                || o.User.Email.Contains(request.SearchKey));
            }
            var ordersList = query.Where(o => o.OrderState == request.OrderState)
                .OrderByDescending(o => o.Id)
                .Select(o => new OrdersDto
                {
                    OrderId = o.Id,
                    OrderState = o.OrderState,
                    InsetTime = o.InsertTime,
                    ProductCount = o.OrderDetails.Count(),
                    RequestId = o.RequestPayId,
                    UserId = o.UserId,
                    BuyerEmail = o.User.Email,
                    BuyerName = o.User.FullName,
                })
                .ToPaged(request.Page, request.PageSize, out rowCount)
                .ToList();


            return new ResultDto<ResultGetOrderDto>
            {
                Data = new ResultGetOrderDto
                {
                    Orders = ordersList,
                    CurrentPage = request.Page,
                    PageSize = request.PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = string.Empty,
            };

    }
    }

}