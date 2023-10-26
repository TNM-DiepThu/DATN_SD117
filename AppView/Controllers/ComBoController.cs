using AppData.data;
using AppData.model;
using AppData.Serviece.Interfaces;
using AppData.Serviece.ViewModeService;
using AppData.ViewModal.Usermodalview;
using AppData.ViewModal.VoucherVM;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace AppView.Controllers
{
    public class ComBoController : Controller
    {
        // GET: ComBoController
        private readonly ILogger<ComBoController> _logger;
        HttpClient _client = new HttpClient();
        private readonly MyDbContext _context;
        private readonly SanPhamChiTietViewModelService _spctViewModel;
        private readonly IComboService _cb;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public ComBoController(ILogger<ComBoController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _spctViewModel = new SanPhamChiTietViewModelService();

            _context = new MyDbContext();
        }
        [HttpGet]
        public IActionResult GetlistComBO()
        {
            string url = "https://localhost:7214/api/Combo/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<Combo> lstCombo = JsonConvert.DeserializeObject<List<Combo>>(data);
            return View(lstCombo);
        }

        //// GET: ComBoController/Details/5
        //public ActionResult Details(int id)
        //{
        //    //https://localhost:7214/api/Combo/GetAll
        //    return View();
        //}
        [HttpPost]

        public async Task<ActionResult> CreateComBO(Combo combo)
        {
            string url = $"https://localhost:7214/api/Combo/Create?Ten={combo.TenCombo}&mota={combo.MoTaCombo}&giatien={combo.TienGiamGia}";


            var obj = JsonConvert.SerializeObject(combo);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetlistComBO");
            }
            else
            {
                return View("CreateComBO");
            }

        }




        // GET: ComBoController/Delete/5

        public async Task<ActionResult> DeleteCBAsync(Combo cb)
        {
            string url = $"https://localhost:7214/api/Combo/Delete/{cb.Id}";
            var obj = JsonConvert.SerializeObject(cb);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.DeleteAsync(url);

            return RedirectToAction("GetlistComBO");
            //string apiurl = $"https://localhost:7214/api/Combo/Delete/{cb.Id}";
            //var httpClient = new HttpClient();
            //var respone = httpClient.DeleteAsync(apiurl).Result;
            //respone.EnsureSuccessStatusCode();

            //return RedirectToAction("GetlistComBO");

        }

        // POST: ComBoController/Delete/5
        //[HttpGet]
        //[HttpPost]


        //public async Task<ActionResult> UpdateCB(Guid id, Combo cb)
        //{
        //    var roleJson = JsonConvert.SerializeObject(cb);
        //    HttpContent content = new StringContent(roleJson, Encoding.UTF8, "application/json");
        //    var response = await _client.PutAsync($"https://localhost:7214/api/Combo/Update/{id}", content);
        //    var roles = await _client.GetFromJsonAsync<Combo>($"https://localhost:7214/api/Combo/GetbyID?id={id}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction(nameof(GetlistComBO));
        //    }
        //    else
        //    {
        //        return View(roles);
        //    }

        //}
        [HttpGet]
        public IActionResult UpdateCB(Guid Id)
        {
            string apiurl = $"https://localhost:7214/api/Combo/Update/{Id}";

            var respone = _client.GetAsync(apiurl).Result;
            var data = respone.Content.ReadAsStringAsync().Result;
            var sp = JsonConvert.DeserializeObject<Combo>(data);





            return View(sp);

        }
        [HttpPost]
        public IActionResult UpdateCB(Guid id, Combo cb)
        {
            //string url = $"https://localhost:7214/api/Combo/Update/{cb.Id}";
            //var obj = JsonConvert.SerializeObject(cb);
            //StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            //HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            //return RedirectToAction("GetlistComBO");
            string apiurl = $"https://localhost:7214/api/Combo/Update/{id}?Ten={cb.TenCombo}&mota={cb.MoTaCombo}&giatien={cb.TienGiamGia}";

            var obj = JsonConvert.SerializeObject(cb);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            var respone = _client.PutAsJsonAsync(apiurl, obj).Result;
            if (respone.IsSuccessStatusCode)
            {
                return RedirectToAction("GetlistComBO");
            }
            else
            {
                return RedirectToAction("UpdateCB");
            }


        }
        //public async Task<ActionResult> CreateComBOCT(Combo combo)
        //{
        //    string url = $"https://localhost:7214/api/Combo/Create";


        //    var obj = JsonConvert.SerializeObject(combo);
        //    StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
        //    HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
        //    if (httpResponseMessage.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("GellAll", "QuanTri");
        //    }
        //    else
        //    {
        //        return View("CreateComBO");
        //    }

        //}
        public IActionResult GetlistComBOCT()
        {
            string url = "https://localhost:7214/api/ComBoChiTiet/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<ComboChiTiet> lstCombo = JsonConvert.DeserializeObject<List<ComboChiTiet>>(data);
            return View(lstCombo);
        }
        public IActionResult Detail(Guid id)
        {
            string apiurl = $"https://localhost:7214/api/Combo/{id}";
            var httpClient = new HttpClient();
            var respone = httpClient.GetAsync(apiurl).Result;
            var data = respone.Content.ReadAsStringAsync().Result;
            var sp = JsonConvert.DeserializeObject<Combo>(data);

            //string apiurl = $"https://localhost:7240/api/SanPham/{id}";
            //var httpclient = new HttpClient();
            //var resposne = httpclient.GetAsync(apiurl).Result;
            //resposne.EnsureSuccessStatusCode();
            //var data = resposne.Content.ReadAsStringAsync().Result;
            //var sp=JsonConvert.DeserializeObject(data);


            return View(sp);

        }
    }
}
