using System.Collections.Generic;

namespace KalaMarket.Application.Services.Carts
{
    public class CartDto
    {
        public int ProductCount { get; set; }
        public int SumAmount { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
}
