using System.Diagnostics;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using SeminarWeb.Models;

namespace SeminarWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
