using KalaMarket.Common.Dto;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarket.Application.Services.Products.Queries.GetProductDetailForSite
{
    public interface IGetProductDetailForSiteService
    {
        ResultDto<ProductDetailForSiteDto> Execute(long Id);
    }
}
