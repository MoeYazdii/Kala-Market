using KalaMarket.Application.Services.Common.Queries.GetMenuItem;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.ViewComponents
{
    public class GetMenuMobile : ViewComponent
    {
        private readonly IGetMenuItemService _getMenuItemService;
        public GetMenuMobile(IGetMenuItemService getMenuItemService)
        {
            _getMenuItemService = getMenuItemService;
        }

        public IViewComponentResult Invoke()
        {
            var menuItem = _getMenuItemService.Execute();
            return View(viewName: "GetMenuMobile", menuItem.Data);
        }
    }
}


