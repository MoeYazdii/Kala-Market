using KalaMarket.Application.Interfaces.Contexts;
using KalaMarket.Common;
using KalaMarket.Common.Dto;
using KalaMarket.Domain.Entities.Orders;
using KalaMarket.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KalaMarket.Application.Services.Finances.Queries.GetRequestPayForAdmin
{
    public interface IGetRequestPayForAdminService
    {
        ResultDto<ResultPayDto> Execute(RequestPaysDto requestPayDto);
    }
    public class GetRequestPayForAdminService : IGetRequestPayForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetRequestPayForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultPayDto> Execute(RequestPaysDto request)
        {
            int rowCount = 1;
            var query = _context.RequestPays
                .OrderByDescending(p => p.Id)
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                query = query.Where(p => p.Authority.Contains(request.SearchKey)
                || p.RefId.ToString().Contains(request.SearchKey)
                || p.User.FullName.Contains(request.SearchKey)
                || p.Id.ToString().Contains(request.SearchKey));
            }
            var PaymentList = query.Select(p => new PayDto
            {
                Id = p.Id,
                Amount = p.Amount,
                Authority = p.Authority,
                Guid = p.Guid,
                IsPay = p.IsPay,
                PayDate = p.PayDate,
                RefId = p.RefId,
                UserId = p.UserId,
                UserName = p.User.FullName
            }).ToPaged(request.Page, request.PageSize, out  rowCount)
                .ToList();

            return new ResultDto<ResultPayDto>
            {
                Data = new ResultPayDto
                {
                    RequestPay = PaymentList,
                    PageSize = request.PageSize,
                    CurrentPage = request.Page,
                    RowCount = rowCount,
                },

                IsSuccess = true
            };
        }
    }
}
