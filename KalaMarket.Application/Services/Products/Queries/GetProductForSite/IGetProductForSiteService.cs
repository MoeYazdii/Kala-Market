using KalaMarket.Common.Dto;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarket.Application.Services.Products.Queries.GetProductForSite
{
    public interface IGetProductForSiteService
    {
        ResultDto<ResultProductForSiteDto> Execute(string searchKey,int Page,long? CatId);
    }

}
