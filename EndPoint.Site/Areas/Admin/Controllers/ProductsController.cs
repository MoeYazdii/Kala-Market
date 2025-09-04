using KalaMarket.Application.Interfaces.FacadPatterns;
using KalaMarket.Application.Services.Products.Commands.AddNewProduct;
using KalaMarket.Application.Services.Products.Queries.GetProductForAdmin.GetProductForAdminSearch;
using KalaMarket.Application.Services.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {

        private readonly IProductFacad _productFacad;

        public ProductsController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }
        public IActionResult Index(string searchKey, int Page = 1, int PageSize = 20)
        {
            return View(_productFacad.GetProductForAdminService.Execute(new RequestGetProductDto
            {
                Page = Page,
                SearchKey = searchKey,
                PageSize = PageSize
            }).Data);
        }

        public IActionResult Detail(long Id)
        {
            return View(_productFacad.GetProductDetailForAdminService.Execute(Id).Data);
        }

        [HttpGet]
        public IActionResult AddNewProduct()
        {
            ViewBag.Categories = new SelectList(_productFacad.GetAllCategoriesService.Execute().Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult AddNewProduct(RequestAddNewProductDto request, List<AddNewProduct_Features> Features)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            request.Images = images;
            request.Features = Features;
            return Json(_productFacad.AddNewProductService.Execute(request));
        }
    }
}