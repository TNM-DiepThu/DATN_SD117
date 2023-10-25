using AppData.model;
using AppData.ViewModal.Usermodalview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace AppView.Controllers
{
    public class GioHangController : Controller
    {
        HttpClient _client = new HttpClient();


        // GET: GioHangController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GioHangController/Details/5
        public async Task<ActionResult> GetallGH()
        {
            var response = await _client.GetFromJsonAsync<List<GioHang>>($"https://localhost:7214/api/GioHang/GetAll");
            return View(response);
        }

        // GET: GioHangController/Create
        public async Task<ActionResult> CreateGioHang(GioHang gh)
        {
            string url = $"https://localhost:7214/api/GioHang/Create?{gh.GhiChu}";


            var obj = JsonConvert.SerializeObject(gh);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetallGH");
            }
            else
            {
                return View("CreateGioHang");
            }
        }

        // POST: GioHangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GioHangController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GioHangController/Edit/5
        [HttpGet]
        public IActionResult UpdateGH(Guid Id)
        {
            string apiurl = $"https://localhost:7214/api/GioHang/Update/{Id}";

            var respone = _client.GetAsync(apiurl).Result;
            var data = respone.Content.ReadAsStringAsync().Result;
            var sp = JsonConvert.DeserializeObject<GioHang>(data);





            return View(sp);

        }
        [HttpPost]
        public IActionResult UpdateGH(Guid id, GioHang gh)
        {
            //string url = $"https://localhost:7214/api/Combo/Update/{cb.Id}";
            //var obj = JsonConvert.SerializeObject(cb);
            //StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            //HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            //return RedirectToAction("GetlistComBO");
            string apiurl = $"https://localhost:7214/api/GioHang/Update/{id}?Ten={gh.GhiChu}";

            var obj = JsonConvert.SerializeObject(gh);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            var respone = _client.PutAsJsonAsync(apiurl, obj).Result;
            if (respone.IsSuccessStatusCode)
            {
                return RedirectToAction("GetlistComBO");
            }
            else
            {
                return RedirectToAction("GetallGH");
            }


        }

        // GET: GioHangController/Delete/5
        public async Task<ActionResult> DeleteAsync(GioHang gio)
        {
            string url = $"https://localhost:7214/api/GioHang/Delete/{gio.Id}";
            var obj = JsonConvert.SerializeObject(gio);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            return RedirectToAction("GetallGH", "QuanTri");
        }

        // POST: GioHangController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
