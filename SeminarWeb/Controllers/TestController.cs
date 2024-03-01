using Microsoft.AspNetCore.Mvc;

namespace SeminarWeb.Controllers
{
    public class TestController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var client = _httpClientFactory.CreateClient();

            // Gửi GET request
            var response = await client.GetAsync("http://localhost:54585/api/product/getall");

            // Xử lý response
            if (response.IsSuccessStatusCode)
            {
                // Xử lý dữ liệu từ response
                var content = await response.Content.ReadAsStringAsync();
            }
            return View();
        }
    }
}
