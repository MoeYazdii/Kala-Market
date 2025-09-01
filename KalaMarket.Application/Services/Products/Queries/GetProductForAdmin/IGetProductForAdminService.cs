using KalaMarket.Common.Dto;
using System;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarket.Application.Services.Products.Queries.GetProductForAdmin
{
    public interface IGetProductForAdminService
    {
        ResultDto<ProductForAdminDto> Execute(int Page = 1, int PageSize = 20);
    }
}
