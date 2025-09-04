using KalaMarket.Application.Interfaces.Contexts;
using KalaMarket.Application.Services.Products.Queries.GetProductForAdmin.GetProductForAdminSearch;
using KalaMarket.Application.Services.Users.Queries.GetUsers;
using KalaMarket.Common;
using KalaMarket.Common.Dto;
using KalaMarket.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KalaMarket.Application.Services.Products.Queries.GetProductForAdmin
{
    public class GetProductForAdminService : IGetProductForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetProductForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        //old code
        //public ResultDto<ProductForAdminDto> Execute(RequestGetProductDto request)
        //{
        //    int rowCount = 0;

        //    List<ProductsFormAdminList_Dto> product;
        //    var products = _context.Products
        //        .Include(p => p.Category)
        //        .Select(p => new ProductsFormAdminList_Dto
        //        {
        //            Id = p.Id,
        //            Brand = p.Brand,
        //            Category = p.Category.Name,
        //            Description = p.Description,
        //            Displayed = p.Displayed,
        //            Inventory = p.Inventory,
        //            Name = p.Name,
        //            Price = p.Price,
        //        }).AsQueryable();
        //    if (!string.IsNullOrWhiteSpace(request.SearchKey))
        //    {
        //        product = products.Where(p => p.Name.Contains(request.SearchKey) && p.Description.Contains(request.SearchKey)).
        //            ToPaged(request.Page, request.PageSize, out rowCount).ToList();
        //    }
        //    else
        //    {
        //        product = products.ToPaged(request.Page, request.PageSize, out rowCount).ToList();
        //    }

        //    return new ResultDto<ProductForAdminDto>()
        //    {
        //        Data = new ProductForAdminDto()
        //        {
        //            Products = product,
        //            CurrentPage = request.Page,
        //            PageSize = request.PageSize,
        //            RowCount = rowCount
        //        },
        //        IsSuccess = true,
        //        Message = "",
        //    };
        //}


        // New Optimize Code
        public ResultDto<ProductForAdminDto> Execute(RequestGetProductDto request)
        {
            int rowCount;

            // Build base query (only IQueryable for now, no projection yet)
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            // Apply search filter if needed
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                query = query.Where(p =>
                    p.Name.Contains(request.SearchKey) ||
                    p.Description.Contains(request.SearchKey));
            }

            // Apply projection after filtering
            var productList = query
                .Select(p => new ProductsFormAdminList_Dto
                {
                    Id = p.Id,
                    Brand = p.Brand,
                    Category = p.Category.Name,
                    Description = p.Description,
                    Displayed = p.Displayed,
                    Inventory = p.Inventory,
                    Name = p.Name,
                    Price = p.Price,
                })
                .ToPaged(request.Page, request.PageSize, out rowCount)
                .ToList();

            // Build result
            return new ResultDto<ProductForAdminDto>
            {
                Data = new ProductForAdminDto
                {
                    Products = productList,
                    CurrentPage = request.Page,
                    PageSize = request.PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = string.Empty
            };
        }


    }
}
