using AppData.data;
using AppData.model;
using AppData.Serviece.Implements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AppView.Controllers
{
    public class VoucherController : Controller
    {
        private readonly ILogger<VoucherController> _logger;
        HttpClient _client = new HttpClient();
        private readonly MyDbContext _context;
        private readonly VoucherServices _voucher;
        private readonly IWebHostEnvironment _hosting;

        public VoucherController(ILogger<VoucherController> logger, IWebHostEnvironment hosting)
        {
            _context = new MyDbContext();
            _logger = logger;
            _hosting = hosting;
            _voucher = new VoucherServices();
        }

        // GET: VoucherController
        [HttpGet]
        public ActionResult GetAllVoucher()
        {
            string url = "https://localhost:7214/api/Voucher/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<Voucher> lstVoucher = JsonConvert.DeserializeObject<List<Voucher>>(data);
            return View(lstVoucher);
        }

        //// GET: VoucherController/Details/5

        //public async Task<ActionResult> Details(Voucher voucher)
        //{
        //    string url = $"{voucher.Id}"; 
        //    HttpResponseMessage response = await _client.GetAsync(url);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string responseBody = await response.Content.ReadAsStringAsync();
        //        Voucher voucherdetail = JsonConvert.DeserializeObject<Voucher>(responseBody);
        //        return View("VoucherDetails", voucherdetail);
        //    }
        //    else
        //    {
        //        return View("Error");
        //    }
        //}

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateVoucher(Voucher voucher)
        {
            string url = $"https://localhost:7214/api/Voucher/Create";
            var obj = JsonConvert.SerializeObject(voucher);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            return View("CreateVoucher");
        }

        [HttpGet]
        [HttpPut]
        public async Task<ActionResult> EditVoucher(Voucher voucher)
        {
            string url = $"https://localhost:7214/api/Voucher/Edit/{voucher.Id}";
            var obj = JsonConvert.SerializeObject(voucher);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);
            return View("GetAllVoucher");
        }

        [HttpGet]
        [HttpDelete]
        public async Task<ActionResult> DeleteVoucher(Voucher voucher)
        {
            string url = $"https://localhost:7214/api/Voucher/Delete/{voucher.Id}";
            var obj = JsonConvert.SerializeObject(voucher);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            return RedirectToAction("GellAllCVoucher");
        }
    }
}
