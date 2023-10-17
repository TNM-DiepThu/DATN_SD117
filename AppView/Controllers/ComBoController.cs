using AppData.data;
using AppData.model;
using AppData.Serviece.ViewModeService;
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

        // GET: ComBoController/Details/5
        public ActionResult Details(int id)
        {
            //https://localhost:7214/api/Combo/GetAll
            return View();
        }

        // GET: ComBoController/Create
        [HttpGet]
        [HttpPost]

        public async Task<ActionResult> CreateComBO(Combo combo)
        {
            string url = $"https://localhost:7214/api/Combo/Create";


            var obj = JsonConvert.SerializeObject(combo);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GellAll", "QuanTri");
            }
            else
            {
                return View("CreateComBO");
            }

        }
        

        // POST: ComBoController/Create
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

        // GET: ComBoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ComBoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ComBoController/Delete/5
        public async Task<ActionResult> DeleteCB(Combo cb)
        {
            string url = $"https://localhost:7214/api/Combo/Delete/{cb.Id}";
            var obj = JsonConvert.SerializeObject(cb);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            return RedirectToAction("GetlistComBO", "QuanTri");

        }

        // POST: ComBoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateCB(Guid id, Combo cb)
        {
            var roleJson = JsonConvert.SerializeObject(cb);
            HttpContent content = new StringContent(roleJson, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"https://localhost:7214/api/Combo/Delete/{cb.Id}", content);
            var roles = await _client.GetFromJsonAsync<List<Combo>>($"https://localhost:7214/api/Combo/GetAll");
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GetlistComBO));
            }
            return View();
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
    }
}
