using AppointmentGuard.Core.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppointmentGuard.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AuthController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(registerDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var baseUrl = _configuration["ApiSettings:BaseUrl"];
            var response = await client.PostAsync($"{baseUrl}api/Auth/register", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Error = "Kayıt başarısız! Bilgileri kontrol edin.";
            return View(registerDto);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(loginDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var baseUrl = _configuration["ApiSettings:BaseUrl"];
                var response = await client.PostAsync($"{baseUrl}api/Auth/login", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var tokenModel = JsonConvert.DeserializeObject<TokenDto>(jsonString);

                    if (tokenModel != null && !string.IsNullOrEmpty(tokenModel.AccessToken))
                    {
                        var handler = new JwtSecurityTokenHandler();
                        var jwtSecurityToken = handler.ReadJwtToken(tokenModel.AccessToken);

                        var claims = new List<Claim>(jwtSecurityToken.Claims);

                        var claimsIdentity = new ClaimsIdentity(
                            claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);

                        HttpContext.Response.Cookies.Append("AppointmentGuardToken", tokenModel.AccessToken, new CookieOptions
                        {
                            HttpOnly = true,
                            Expires = DateTime.Now.AddMinutes(60)
                        });

                        return RedirectToAction("Index", "Home");
                    }
                }

                ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
                return View(loginDto);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Bir hata oluştu: " + ex.Message;
                return View(loginDto);
            }
        }

        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("AppointmentGuardToken");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
