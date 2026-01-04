using AppointmentGuard.Core.DTOs;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims; 

namespace AppointmentGuard.Web.Controllers
{
    [Authorize] 
    public class AppointmentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AppointmentController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var client = _httpClientFactory.CreateClient();
            var baseUrl = _configuration["ApiSettings:BaseUrl"];

            var response = await client.GetAsync($"{baseUrl}api/Polyclinics");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var polyclinics = JsonConvert.DeserializeObject<List<PolyclinicDto>>(jsonData);

                ViewBag.Polyclinics = polyclinics;
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login", "Auth");

            var client = _httpClientFactory.CreateClient();
            var baseUrl = _configuration["ApiSettings:BaseUrl"];

            var response = await client.GetAsync($"{baseUrl}api/Appointments/patient/{userId}");

            List<AppointmentDto> appointments = new List<AppointmentDto>();

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                appointments = JsonConvert.DeserializeObject<List<AppointmentDto>>(jsonData);
            }

            return View(appointments);
        }


        public async Task<IActionResult> Cancel(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var baseUrl = _configuration["ApiSettings:BaseUrl"];

            var response = await client.DeleteAsync($"{baseUrl}api/Appointments/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Randevu başarıyla iptal edildi.";
            }
            else
            {
                TempData["Error"] = "İptal işlemi sırasında bir hata oluştu.";
            }

            return RedirectToAction("Index");
        }
    }
}