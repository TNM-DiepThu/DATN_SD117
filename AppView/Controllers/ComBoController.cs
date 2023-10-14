using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppView.Controllers
{
    public class ComBoController : Controller
    {
        // GET: ComBoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ComBoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ComBoController/Create
        public ActionResult Create()
        {

            return View();
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ComBoController/Delete/5
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
