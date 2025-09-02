using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class PayController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"; // sandbox merchant ID
        private const string ApiBase = "https://sandbox.zarinpal.com/pg/v4/payment/";

        public PayController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var amount = 1000; // test amount
            var callbackUrl = Url.Action("Verify", "Payment", null, Request.Scheme);

            var payload = new
            {
                merchant_id = MerchantId,
                amount = amount,
                callback_url = callbackUrl,
                description = "Test Payment"
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
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Verify(string authority, string status)
        {
            if (status != "OK")
                return View("Failed");

            var payload = new
            {
                merchant_id = MerchantId,
                authority = authority,
                amount = 1000
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync(ApiBase + "verify.json", content);
            var body = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(body);
            var data = doc.RootElement.GetProperty("data");

            if (data.TryGetProperty("ref_id", out var refId))
            {
                ViewBag.RefId = refId.GetInt64();
                return View("Success");
            }

            return View("Failed");
        }
    }
}
