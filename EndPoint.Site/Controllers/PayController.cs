using EndPoint.Site.Utilities;
using KalaMarket.Application.Services.Carts;
using KalaMarket.Application.Services.Fainances.Commands.AddRequestPay;
using KalaMarket.Application.Services.Fainances.Queries.GetRequestPayService;
using KalaMarket.Application.Services.Orders.Commands.AddNewOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    [Authorize]
    public class PayController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IAddRequestPayService _addRequestPayService;
        private readonly ICartService _cartService;
        private readonly CookiesManeger _cookiesManeger;
        private readonly IGetRequestPayService _getRequestPayService;
        private readonly IAddNewOrderService _addNewOrderService;
        private const string MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"; // sandbox merchant ID
        private const string ApiBase = "https://sandbox.zarinpal.com/pg/v4/payment/";
        private const string ApiBaseVerify = "https://sandbox.zarinpal.com/pg/v4/payment/";

        public PayController(IHttpClientFactory httpClientFactory, ICartService cartService,
            IAddRequestPayService addRequestPayService, IGetRequestPayService getRequestPayService,
            IAddNewOrderService addNewOrderService)
        {
            _addRequestPayService = addRequestPayService;
            _httpClient = httpClientFactory.CreateClient();
            _cookiesManeger = new CookiesManeger();
            _cartService = cartService;
            _getRequestPayService = getRequestPayService;
            _addNewOrderService = addNewOrderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // var amount = 1000; // test amount
            // var callbackUrl = Url.Action("Verify", "Payment", null, Request.Scheme);
            long? UserId = ClaimUtility.GetUserId(User);
            var cart = _cartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), UserId);
            var requestPay = _addRequestPayService.Execute(cart.Data.SumAmount, UserId.Value);

            var payload = new
            {
                merchant_id = MerchantId,
                amount = requestPay.Data.Amount,
                callback_url = $"https://kalamarket.somee.com/Pay/Verify?guid={requestPay.Data.guid}",
                description = "پرداخت فاکتور شماره:" + requestPay.Data.RequestPayId
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync(ApiBase + "request.json", content);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = body;
                return View("Error");
            }

            using var doc = JsonDocument.Parse(body);
            var data = doc.RootElement.GetProperty("data");

            if (data.TryGetProperty("authority", out var authority))
            {
                return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{authority.GetString()}");
            }
            
            ViewBag.Error = body;
            return View("PaymentError");
        }

        [HttpGet]
        public async Task<IActionResult> Verify(Guid guid, string authority, string status)
        {
            var requestPay = _getRequestPayService.Execute(guid);
            
            if (status != "OK")
                return View("PaymentError");

            var payload = new
            {
                merchant_id = MerchantId,
                authority = authority,
                amount = requestPay.Data.Amount
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync(ApiBase + "verify.json", content);
            var body = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(body);
            var data = doc.RootElement.GetProperty("data");
            
            if (data.TryGetProperty("ref_id", out var refId))
            {
                long? UserId = ClaimUtility.GetUserId(User);
                var cart = _cartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), UserId);
                _addNewOrderService.Execute(new RequestAddNewOrderServiceDto
                {
                    CartId= cart.Data.CartId,
                    UserId=UserId.Value,
                    RequestPayId = requestPay.Data.Id,
                    RefId = refId.GetInt64(),
                    Authority = authority.ToString(),
                });
                return RedirectToAction("Index","Orders");
            }

            return View("PaymentError");
        }
    }
}
