using AppData.data;
using Microsoft.AspNetCore.Mvc;
using Bill.Serviece.Implements;
using AppData.model;
using Newtonsoft.Json;
using System.Text;

namespace AppView.Controllers
{
    public class HinhThucThanhToanController : Controller
    {
        private readonly ILogger<HinhThucThanhToanController> _logger;
        HttpClient _client = new HttpClient();
        private readonly MyDbContext _context;
        private readonly HinhThucThanhToanServiece _httt;
        private readonly IWebHostEnvironment _hosting;

        public HinhThucThanhToanController(ILogger<HinhThucThanhToanController> logger, IWebHostEnvironment hosting)
        {
            _context = new MyDbContext();
            _logger = logger;
            _hosting = hosting;
            _httt = new HinhThucThanhToanServiece();
        }
        [HttpGet]
        public ActionResult GetAllHinhThucThanhToan()
        {
            string url = "";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<HinhThucThanhToan> httt = JsonConvert.DeserializeObject<List<HinhThucThanhToan>>(data);
            return View(httt);
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateHinhThucThanhToan(HinhThucThanhToan httt)
        {
            string url = $"";
            var obj = JsonConvert.SerializeObject(httt);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            return View("CreateHinhThucThanhToan");
        }

        [HttpGet]
        [HttpPut]
        public async Task<ActionResult> EditHinhThucThanhToan(HinhThucThanhToan httt)
        {
            string url = $"{httt.Id}";
            var obj = JsonConvert.SerializeObject(httt);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);
            return View("GetAllHinhThucThanhToan");
        }

        [HttpGet]
        [HttpDelete]
        public async Task<ActionResult> DeleteHinhThucThanhToan(HinhThucThanhToan httt)
        {
            string url = $"https://localhost:7214/api/HinhThucThanhToan/Delete/{httt.Id}";
            var obj = JsonConvert.SerializeObject(httt);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            return RedirectToAction("GellAllHinhThucThanhToan");
        }
    }
}
