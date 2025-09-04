using KalaMarket.Application.Services.Finances.Queries.GetRequestPayForAdmin;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RequestPayController : Controller
    {
        private readonly IGetRequestPayForAdminService _getRequestPayForAdminService;
        public RequestPayController(IGetRequestPayForAdminService getRequestPayForAdminService)
        {
            _getRequestPayForAdminService = getRequestPayForAdminService;
        }
        public IActionResult Index(string searchKey, int Page = 1, int PageSize = 20)
        {
            return View(_getRequestPayForAdminService.Execute(new RequestPaysDto
            {
                SearchKey = searchKey,
                PageSize = PageSize ,
                Page = Page
            }).Data);
        }
    }
}
