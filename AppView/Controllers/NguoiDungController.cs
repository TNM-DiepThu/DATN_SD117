using AppData.model;
using AppData.ViewModal.Usermodalview;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace AppView.Controllers
{
    public class NguoiDungController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<NguoiDung> _userManager;
        public NguoiDungController(IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = new HttpClient();
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> NguoiDungView()
        {
            var response = await _httpClient.GetFromJsonAsync<List<NguoiDungVM>>($"https://localhost:7214/api/NguoiDung/GetAllKH");
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNV()
        {
            var response = await _httpClient.GetFromJsonAsync<List<NguoiDungVM>>($"https://localhost:7214/api/NguoiDung/GetAllNV");
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKH()
        {
            var response = await _httpClient.GetFromJsonAsync<List<NguoiDungVM>>($"https://localhost:7214/api/NguoiDung/GetAllKH");
            return View(response);
        }

        [HttpGet]
        public ActionResult CreateNguoiDung()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> CreateNguoiDung(NguoiDungVM Create)
        {

            var jsonObj = JsonConvert.SerializeObject(Create);
            StringContent content = new StringContent(jsonObj, Encoding.UTF8, "application/json");
            var respones = await _httpClient.PostAsync("https://localhost:7214/api/NguoiDung/Create", content);
            if (respones.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(NguoiDungView));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public ActionResult CreateNV()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> CreateNV(NguoiDungVM Create)
        {
           
               

                var jsonObj = JsonConvert.SerializeObject(Create);
                StringContent content = new StringContent(jsonObj, Encoding.UTF8, "application/json");
                HttpResponseMessage respones = await _httpClient.PostAsync("https://localhost:7214/api/NguoiDung/CreateNV", content);
                if (respones.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(NguoiDungView));
                }
                else
                {
                    return BadRequest();
                }
            return View(Create);
        }
        [HttpGet]
        public async Task<IActionResult> EditNguoiDung(Guid Id)
        {
            var response = await _httpClient.GetFromJsonAsync<NguoiDungVM>($"https://localhost:7214/api/NguoiDung/GetById/{Id}");
            var roles = await _httpClient.GetFromJsonAsync<List<Quyen>>($"https://localhost:7214/api/Quyen/GetAllActive");
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            return View(response);

        }
        [HttpPost]
        public async Task<IActionResult> EditNguoiDung(Guid Id, NguoiDungVM UserUpdateVM)
        {
            var roleJson = JsonConvert.SerializeObject(UserUpdateVM);
            HttpContent content = new StringContent(roleJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"https://localhost:7214/api/NguoiDung/Edit/{Id}", content);
            var roles = await _httpClient.GetFromJsonAsync<List<QuyenVM>>($"https://localhost:7214/api/Quyen/GetAllActive");
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CreateNguoiDung", "NguoiDung");
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _httpClient.DeleteAsync($"https://localhost:7214/api/NguoiDung/Delete/{Id}");


            if (obj.IsSuccessStatusCode)
            {

                return RedirectToAction(nameof(NguoiDungView));
            }
            else
            {
                return BadRequest("sai roi");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNV(Guid Id)
        {
            var obj = await _httpClient.DeleteAsync($"https://localhost:7214/api/NguoiDung/DeleteNV/{Id}");


            if (obj.IsSuccessStatusCode)
            {

                return RedirectToAction(nameof(GetAllNV));
            }
            else
            {
                return BadRequest("sai roi");
            }
        }


        [HttpPost]
        public async Task<IActionResult> UploadAvatar(IFormFile imageFile, string userId)
        {
            if (imageFile != null && imageFile.Length > 0 && !string.IsNullOrEmpty(userId))
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "avatar");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Tạo tên tệp ảnh duy nhất
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Lấy thông tin người dùng từ cơ sở dữ liệu
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    // Cập nhật đường dẫn ảnh vào thuộc tính Anh của người dùng
                    user.Anh = Path.Combine("avatar", fileName);

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("UserProfile", new { userId });
                    }
                }
            }

            // Xử lý lỗi hoặc trả về view tùy theo trường hợp
            return View("Error");
        }
    }
}
