using KalaMarket.Application.Services.Orders.Query.GetOrdersForAdmin;
using KalaMarket.Domain.Entities.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin,Operator")]
    public class OrdersController : Controller
    {
        private readonly IGetOrdersForAdminService _getOrdersForAdminService;
        public OrdersController(IGetOrdersForAdminService getOrdersForAdminService)
        {
            _getOrdersForAdminService = getOrdersForAdminService;
        }
        public IActionResult Index(OrderState orderState,string searchKey, int Page = 1, int PageSize = 20)
        {
            return View(_getOrdersForAdminService.Execute(new RequestGetOrder
            {
                Page = Page,
                PageSize = PageSize,
                SearchKey = searchKey,
                OrderState = orderState,
            }).Data);
        }
    }
}
