namespace KalaMarket.Application.Services.Orders.Query.GetUserOrders
{
    public class OrderDetailDto
    {
        public long  OrderDetailId { get; set; }
        public long  ProductId { get; set; }
        public string ProductName { get; set; }


        public long  Price { get; set; }
        public int  Count { get; set; }
    }
}
