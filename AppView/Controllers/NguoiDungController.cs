using AppData.model;
using AppData.ViewModal.Usermodalview;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System.Text;

namespace AppView.Controllers
{
    public class NguoiDungController : Controller
    {
        private readonly HttpClient _httpClient;
        public NguoiDungController(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
            HttpContent content = new StringContent(jsonObj, Encoding.UTF8, "application/json");
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
            HttpContent content = new StringContent(jsonObj, Encoding.UTF8, "application/json");
            var respones = await _httpClient.PostAsync("https://localhost:7214/api/NguoiDung/CreateNV", content);
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
                return RedirectToAction("CreateNguoiDung","NguoiDung");
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
    }
}
