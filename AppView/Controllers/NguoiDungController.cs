﻿using AppData.model;
using AppData.ViewModal.Usermodalview;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using AppData.Serviece.Interfaces;
using AppData.Serviece.Implements;
using AppData.data;

namespace AppView.Controllers
{
    public class NguoiDungController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<NguoiDung> _userManager;
        private readonly MyDbContext _dbContext;
        public NguoiDungController(IWebHostEnvironment webHostEnvironment, UserManager<NguoiDung> userManager)
        {
            _httpClient = new HttpClient();
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _dbContext = new MyDbContext();
            
        }
        [HttpGet]
        public async Task<IActionResult> NguoiDungView()
        {
            var response = await _httpClient.GetFromJsonAsync<List<NguoiDungVM>>($"https://localhost:7214/api/NguoiDung/GetAllKH");
            return View(response);
        }
        [HttpGet]
        public async Task<IActionResult> NguoiDungTangVoucher()
        {
            var response = await _httpClient.GetFromJsonAsync<List<NguoiDungVM>>($"https://localhost:7214/api/NguoiDung/GetAllKH");
            return Json(new { response });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNV()
        {
            var response = await _httpClient.GetFromJsonAsync<List<NguoiDungVM>>($"https://localhost:7214/api/NguoiDung/GetAllNV");
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
                    return RedirectToAction(nameof(GetAllNV));
                }
                else
                {
                    return BadRequest();
                }
            
        }
        [HttpGet]
        public async Task<IActionResult> EditNguoiDung(Guid Id)
        {
            var response = await _httpClient.GetFromJsonAsync<NguoiDungEditVM>($"https://localhost:7214/api/NguoiDung/GetById/{Id}");
            
            return View(response); 

        }
        [HttpPost]
        public async Task<IActionResult> EditNguoiDung(Guid Id, NguoiDungEditVM UserUpdateVM)
        {
            var roleJson = JsonConvert.SerializeObject(UserUpdateVM);
            HttpContent content = new StringContent(roleJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"https://localhost:7214/api/NguoiDung/Edit/{Id}", content);

            

            // Truyền đường dẫn ảnh tới view thông qua ViewBag hoặc Model
            // Nếu sử dụng ViewBag
                                              // Hoặc sử dụng Model để truyền dữ liệu tới view
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("NguoiDungView", "NguoiDung");
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> EditNV(Guid Id)
        {
            var response = await _httpClient.GetFromJsonAsync<NguoiDungEditVM>($"https://localhost:7214/api/NguoiDung/GetById/{Id}");
            ViewBag.ImagePath = response.Anh;
            return View(response);

        }
        [HttpPost]
        public async Task<IActionResult> EditNV(Guid Id, NguoiDungVM UserUpdateVM)
        {
            var roleJson = JsonConvert.SerializeObject(UserUpdateVM);
            HttpContent content = new StringContent(roleJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"https://localhost:7214/api/NguoiDung/Edit/{Id}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GetAllNV));
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


        

        public async Task<NguoiDung> GetNguoiDungByIdAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }
        [HttpPost]
        public async Task<IActionResult> EditAvatar(Guid id, IFormFile imageFile)
        {
            // Lấy thông tin người dùng từ cơ sở dữ liệu dựa trên id
            NguoiDung nguoiDung = await GetNguoiDungByIdAsync(id);

            if (nguoiDung == null)
            {
                return NotFound();
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "avatar");
                string imagePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                nguoiDung.Anh = uniqueFileName;

                // Lưu thông tin người dùng vào cơ sở dữ liệu bằng UserManager
                var result = await _userManager.UpdateAsync(nguoiDung);
                if (!result.Succeeded)
                {
                    // Xử lý lỗi, ví dụ: result.Errors
                    // Điều hướng hoặc thông báo lỗi
                    return View("Error");
                }
            }

            return RedirectToAction("EditNguoiDung", new { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> EditAvatarNV(Guid id, IFormFile imageFile)
        {
            // Lấy thông tin người dùng từ cơ sở dữ liệu dựa trên id
            NguoiDung nguoiDung = await GetNguoiDungByIdAsync(id);

            if (nguoiDung == null)
            {
                return NotFound();
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "avatar");
                string imagePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                nguoiDung.Anh = uniqueFileName;

                // Lưu thông tin người dùng vào cơ sở dữ liệu bằng UserManager
                var result = await _userManager.UpdateAsync(nguoiDung);
                if (!result.Succeeded)
                {
                    // Xử lý lỗi, ví dụ: result.Errors
                    // Điều hướng hoặc thông báo lỗi
                    return View("Error");
                }
            }

            return RedirectToAction("EditNV", new { Id = id });
        }
        [HttpGet]
        public async Task<IActionResult> PicAvatar(Guid Id)
        {
            var response = await _httpClient.GetFromJsonAsync<NguoiDungEditVM>($"https://localhost:7214/api/NguoiDung/GetById/{Id}");
            return View(response);

        }

        [HttpPost]
        public async Task<IActionResult> PicAvatar(Guid id, IFormFile imageFile)
        {
            // Lấy thông tin người dùng từ cơ sở dữ liệu dựa trên id
            NguoiDung nguoiDung = await GetNguoiDungByIdAsync(id);

            if (nguoiDung == null)
            {
                return NotFound();
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "avatar");
                string imagePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                nguoiDung.Anh = uniqueFileName;

                // Lưu thông tin người dùng vào cơ sở dữ liệu bằng UserManager
                var result = await _userManager.UpdateAsync(nguoiDung);
                if (!result.Succeeded)
                {
                    // Xử lý lỗi, ví dụ: result.Errors
                    // Điều hướng hoặc thông báo lỗi
                    return View("Error");
                }
            }

            return RedirectToAction("NguoiDungView", new { Id = id });
        }
        [HttpGet]
        public async Task<IActionResult> SearchByNameOrCCCD(string? nameOrCCCD)
        {

            try
            {
                if (string.IsNullOrEmpty(nameOrCCCD))
                {
                    // Xử lý trường hợp nếu nameOrCCCD là null hoặc rỗng
                    return RedirectToAction("GetAllNV");
                }

                using (HttpClient client = new HttpClient())
                {
                    var encodedNameOrCCCD = Uri.EscapeDataString(nameOrCCCD);
                    var response = await client.GetFromJsonAsync<List<NguoiDungVM>>($"https://localhost:7214/api/NguoiDung/SeachByNameorCCCD?seachVM={encodedNameOrCCCD}");

                    if (response != null && response.Any())
                    {
                        return View("GetAllNV", response);
                    }
                    else
                    {
                        return RedirectToAction("GetAllNV");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return View("Error", ex.Message);
            }
            catch (JsonException ex)
            {
                return View("Error", ex.Message);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }

        }
        [HttpGet]
        public async Task<IActionResult> SearchByNameOrCCCDKH(string? nameOrCCCD)
        {

            try
            {
                if (string.IsNullOrEmpty(nameOrCCCD))
                {
                    // Xử lý trường hợp nếu nameOrCCCD là null hoặc rỗng
                    return RedirectToAction("NguoiDungView");
                }

                using (HttpClient client = new HttpClient())
                {
                    var encodedNameOrCCCD = Uri.EscapeDataString(nameOrCCCD);
                    var response = await client.GetFromJsonAsync<List<NguoiDungVM>>($"https://localhost:7214/api/NguoiDung/SeachByNameorCCCDKH?seachVM={encodedNameOrCCCD}");

                    if (response != null && response.Any())
                    {
                        return View("NguoiDungView", response);
                    }
                    else
                    {
                        return RedirectToAction("NguoiDungView");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return View("Error", ex.Message);
            }
            catch (JsonException ex)
            {
                return View("Error", ex.Message);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }

        }





    }
}
