using KalaMarket.Application.Interfaces.Contexts;
using KalaMarket.Common;
using KalaMarket.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace KalaMarket.Application.Services.Products.Queries.GetProductForSite
{
    public class GetProductForSiteService : IGetProductForSiteService
    {

        private readonly IDataBaseContext _context;
        public GetProductForSiteService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultProductForSiteDto> Execute(int Page)
        {
            int totalRow = 0;
            var poducts = _context.Products
                .Include(p=> p.ProductImages)
                .ToPaged(Page, 5, out totalRow);

            Random rd = new Random();
            return new ResultDto<ResultProductForSiteDto>
            {
                Data = new ResultProductForSiteDto
                {
                    TotalRow = totalRow,
                    Products = poducts.Select(p => new ProductForSiteDto
                    {
                        Id = p.Id,
                        Star = rd.Next(1, 5),
                        Title = p.Name,
                        ImageSrc = p.ProductImages.FirstOrDefault().Src,
                        Price=p.Price
                    }).ToList(),
                },
                IsSuccess = true,
            };
        }
    }

}
