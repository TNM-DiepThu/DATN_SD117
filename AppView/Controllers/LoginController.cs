using AppData.model;
using AppData.ViewModal.Login;
using AppData.ViewModal.Usermodalview;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppView.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet]
        public IActionResult LoginJWT()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginJWT(LoginRequestVM loginUser)
        {

            {
                var loginUserJSON = JsonConvert.SerializeObject(loginUser);
                var stringContent = new StringContent(loginUserJSON, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"https://localhost:7214/api/NguoiDung/Login", stringContent);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    if (token == "") return View();
                    HttpContext.Session.SetString("JWTToken", token);
                    var handler = new JwtSecurityTokenHandler();
                    if (handler == null) return View();
                    var jwt = handler.ReadJwtToken(token);
                    if (jwt == null) return View();

                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Email, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email).Value));

                    // Lấy danh sách roles từ JWT
                    var roles = jwt.Claims.Where(u => u.Type == ClaimTypes.Role).Select(u => u.Value).ToList();

                    // Thêm các roles vào danh tính
                    foreach (var role in roles)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }

                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync
                        (CookieAuthenticationDefaults.AuthenticationScheme,
                         principal, new AuthenticationProperties() { IsPersistent = loginUser.RememberMe });

                    return RedirectToAction("GellAllSanPhamCT", "QuanTri");
                }
            }                 
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("JWTToken");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("GellAllSanPhamCT", "QuanTri");
        }
        public IActionResult MyAction( string anh)
        {
            var nguoiDung = new NguoiDung
            {
                Anh = anh
            };
            ViewBag.NguoiDung = nguoiDung; // Hoặc truyền thông tin người dùng vào Model
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DoiMatKhau(Guid Id)
        {
            var response = await _httpClient.GetFromJsonAsync<DoiMatKhauVM>($"https://localhost:7214/api/NguoiDung/GetByIdDMK/{Id}");
            return View(response);

        }
        [HttpPost]
        public async Task<IActionResult> DoiMatKhau(Guid Id, DoiMatKhauVM UserUpdateVM)
        {
            var roleJson = JsonConvert.SerializeObject(UserUpdateVM);
            HttpContent content = new StringContent(roleJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"https://localhost:7214/api/NguoiDung/DoiMatKhau/{Id}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GellAllSanPhamCT", "QuanTri");
            }
            return View();

        }

    }
}
