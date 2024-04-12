using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PontoEletronico.Application.Interfaces;

namespace PontoEletronico.Web.Controllers
{
    public class RegistroPontoController : Controller
    {

        private readonly IRegistroPontoService _registopontoService;
        private readonly IWebHostEnvironment _environment;

        public RegistroPontoController(IRegistroPontoService registroPontoService, IWebHostEnvironment environment)
        {
            _registopontoService = registroPontoService;
            _environment = environment;
        }

        // GET: RegistroPontoController
        public ActionResult Index(int? id)
        {
            return View();
        }

        // GET: RegistroPontoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegistroPontoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegistroPontoController/Create
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

        // GET: RegistroPontoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RegistroPontoController/Edit/5
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

        // GET: RegistroPontoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RegistroPontoController/Delete/5
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
