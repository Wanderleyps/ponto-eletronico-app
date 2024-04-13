using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PontoEletronico.Application.DTOs;
using PontoEletronico.Application.Interfaces;
using PontoEletronico.Application.Services;
using PontoEletronico.Domain.Entities;
using System;
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
            //if (funcionarioId == null) return NotFound();
            if (funcionarioId == null) funcionarioId = 1;
            
            var registoPontos = await _registoPontoService.GetByFuncionarioIdDataAsync(funcionarioId.GetValueOrDefault(), DateTime.Now.Date);
            
            if (!registoPontos.Any()) return RedirectToAction("Index", "Funcionario");

            var funcionarioDTO = registoPontos.FirstOrDefault().Funcionario;

            if (funcionarioDTO == null) return NotFound();

            ViewBag.Funcionario = funcionarioDTO;
            ViewBag.BuscarPorData = DateTime.Now.ToString("yyyy-MM-dd");

            return View(registoPontos);
        }

        // GET: RegistroPontoController/Details/5
        public ActionResult Detail(int id)
        {
            return View();
        }

        // GET: RegistroPontoController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: RegistroPontoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BaterPonto(RegistroPontoDTO registroPontoDTO)
        {
            DateTime dataHoraAtual = DateTime.Now;
            DateTime dataAtual = dataHoraAtual.Date; // Obtém apenas a data (sem a hora)
            TimeSpan horaAtual = dataHoraAtual.TimeOfDay; // Obtém apenas a hora (sem a data)

            var registroPonto = await _registoPontoService.CreateAsync(new RegistroPontoDTO
            {
                Data = dataAtual,
                Hora = horaAtual,
                FuncionarioId = 1
            });
            //var registroGravado = await _registoPontoService.CreateAsync(registroPontoDTO);

            return RedirectToAction(nameof(Index));           
            
        }

        [HttpPost]
        public async Task<ActionResult> BuscarPorData(int? funcionarioId, DateTime buscarPorData)
        {
            //if (funcionarioId == null) return NotFound();
            if (funcionarioId == null) funcionarioId = 1;

            var registoPontos = await _registoPontoService.GetByFuncionarioIdDataAsync(funcionarioId.GetValueOrDefault(), buscarPorData);

            //if (!registoPontos.Any()) return RedirectToAction(nameof(Index));
            if (!registoPontos.Any())
            { 
                var funcionario = await _funcionarioService.GetByIdAsync(funcionarioId.GetValueOrDefault());
                ViewBag.Funcionario = funcionario;
                ViewBag.BuscarPorData = buscarPorData.ToString("yyyy-MM-dd");
                return View("Index", registoPontos); 
            }

            var funcionarioDTO = registoPontos.FirstOrDefault().Funcionario;

            if (funcionarioDTO == null) return NotFound();

            ViewBag.Funcionario = funcionarioDTO;
            ViewBag.BuscarPorData = buscarPorData.ToString("yyyy-MM-dd");

            return View("Index", registoPontos);
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
