using System.ComponentModel.DataAnnotations;

namespace KalaMarket.Domain.Entities.Orders
{
    public enum OrderState
    {
        [Display(Name ="در حال پردازش")]
        Processing = 0,
        [Display(Name = "لغو شده")]
        Cancelled = 1,
        [Display(Name = "تحویل شده")]
        Delivered = 2,
    }
}
