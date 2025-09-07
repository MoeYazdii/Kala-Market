using KalaMarket.Application.Services.Common.Queries.GetHomePageImages;
using KalaMarket.Application.Services.Common.Queries.GetSlider;
using KalaMarket.Application.Services.Products.Queries.GetProductForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Models.ViewModels.HomePages
{
    public class HomePageViewModel
    {
        public List<SliderDto> Sliders {get;set;}
        public List<HomePageImagesDto> PageImages { get; set; }
        public List<ProductForSiteDto>  PowerBank { get; set; }
        public List<ProductForSiteDto>  Headphone { get; set; }
        public List<ProductForSiteDto>  PhoneCover { get; set; }
    }
}
