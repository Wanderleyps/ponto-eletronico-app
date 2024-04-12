using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PontoEletronico.Application.DTOs;
using PontoEletronico.Application.Interfaces;
using PontoEletronico.Application.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PontoEletronico.Web.Controllers
{
    public class RegistroPontoController : Controller
    {

        private readonly IRegistroPontoService _registoPontoService;
        private readonly IFuncionarioService _funcionarioService;
        private readonly IWebHostEnvironment _environment;

        public RegistroPontoController(IRegistroPontoService registroPontoService, 
                                       IFuncionarioService funcionarioService, 
                                       IWebHostEnvironment environment)
        {
            _registoPontoService = registroPontoService;
            _funcionarioService = funcionarioService;
            _environment = environment;
        }

        // GET: RegistroPontoController
        public async Task<IActionResult> Index(int? funcionarioId)
        {
            if (funcionarioId == null) return NotFound();

            var registoPontos = await _registoPontoService.GetByFuncionarioIdAsync(funcionarioId.GetValueOrDefault());
            
            if (registoPontos == null) return NotFound();

            var funcionarioDTO = await _funcionarioService.GetByIdAsync(funcionarioId.GetValueOrDefault());

            if (funcionarioDTO == null) return NotFound();

            ViewBag.Funcionario = funcionarioDTO;

            return View(registoPontos);
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
