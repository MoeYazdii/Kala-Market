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
        public ResultDto<ResultProductForSiteDto> Execute(string searchKey,int Page, long? CatId)
        {
            int totalRow = 0;
            var productsQuery = _context.Products
                .Include(p => p.ProductImages).AsQueryable();
            productsQuery = productsQuery.Where(p=> p.Displayed == true);
            if(CatId != null)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == CatId || p.Category.ParentCategoryId == CatId).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                var sk = searchKey;
                productsQuery = productsQuery.Where(p => p.Name.Contains(searchKey) || p.Brand.Contains(searchKey)
                || p.Brand.Contains(searchKey)).AsQueryable();
            }

            var product = productsQuery.ToPaged(Page,5,out totalRow);

            Random rd = new Random();
            return new ResultDto<ResultProductForSiteDto>
            {
                Data = new ResultProductForSiteDto
                {
                    TotalRow = totalRow,
                    Products = product.Select(p => new ProductForSiteDto
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
