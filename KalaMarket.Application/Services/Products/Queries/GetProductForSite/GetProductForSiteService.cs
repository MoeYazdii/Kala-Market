using KalaMarket.Application.Interfaces.Contexts;
using KalaMarket.Common;
using KalaMarket.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
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
        public ResultDto<ResultProductForSiteDto> Execute(Ordering ordering, string searchKey,int Page,int pageSize, long? CatId)
        {
            int totalRow = 0;
            var productsQuery = _context.Products
                .Include(p => p.ProductImages).AsQueryable();
            productsQuery = productsQuery.Where(p=> p.Displayed == true);
            if(CatId != null)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == CatId || p.Category.ParentCategoryId == CatId).AsQueryable();
            }

            switch (ordering)
            {
                case Ordering.NotOrder:
                    productsQuery = productsQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.MostVisited:
                    productsQuery = productsQuery.OrderByDescending(p => p.ViewCount).AsQueryable();
                    break;
                case Ordering.Bestselling:
                    //not complete
                    productsQuery = productsQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.MostPopular:
                    //not complete
                    productsQuery = productsQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.theNewest:
                    productsQuery = productsQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.Cheapest:
                    productsQuery = productsQuery.OrderBy(p => p.Price).AsQueryable();
                    break;
                case Ordering.theMostExpensive:
                    productsQuery = productsQuery.OrderByDescending(p => p.Price).AsQueryable();
                    break;
            }

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                var sk = searchKey;
                productsQuery = productsQuery.Where(p => p.Name.Contains(searchKey) || p.Brand.Contains(searchKey)
                || p.Brand.Contains(searchKey)).AsQueryable();
            }

            var product = productsQuery.ToPaged(Page,pageSize,out totalRow);

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
