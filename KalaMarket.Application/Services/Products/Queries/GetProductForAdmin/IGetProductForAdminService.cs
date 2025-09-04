using KalaMarket.Application.Services.Products.Queries.GetProductForAdmin.GetProductForAdminSearch;
using KalaMarket.Common.Dto;
using System;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarket.Application.Services.Products.Queries.GetProductForAdmin
{
    public interface IGetProductForAdminService
    {
        ResultDto<ProductForAdminDto> Execute(RequestGetProductDto requestGetProductDto);
    }
}
