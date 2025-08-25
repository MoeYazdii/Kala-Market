using KalaMarket.Common.Dto;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarket.Application.Services.Products.Commands.AddNewProduct
{
    public interface IAddNewProductService
    {
        ResultDto Execute(RequestAddNewProductDto request);
    }
}
