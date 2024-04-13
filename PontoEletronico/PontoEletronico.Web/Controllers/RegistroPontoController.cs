using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PontoEletronico.Application.DTOs;
using PontoEletronico.Application.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<IActionResult> Index(int? funcionarioId)
        {
            //if (funcionarioId == null) return NotFound();
            if (funcionarioId == null) funcionarioId = 1;

            RelatorioRegistoPontoDTO relatorio = await _registoPontoService.
                GerarRelatorioRegistrosPontos(funcionarioId.GetValueOrDefault(), DateTime.Now.Date);

            if (relatorio == null) return RedirectToAction("Index", "Funcionario");

            return View(relatorio);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BaterPonto(RegistroPontoDTO registroPontoDTO)
        {
            DateTime dataHoraAtual = DateTime.Now;
            DateTime dataAtual = dataHoraAtual.Date;
            TimeSpan horaAtual = dataHoraAtual.TimeOfDay;

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
            
            var relatorio = await _registoPontoService.
                GerarRelatorioRegistrosPontos(funcionarioId.GetValueOrDefault(), buscarPorData);

            if (relatorio == null) return RedirectToAction("Index");

            return View("Index", relatorio);
        }
    }
}
