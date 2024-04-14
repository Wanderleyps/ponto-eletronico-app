using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PontoEletronico.Application.DTOs;
using PontoEletronico.Application.Interfaces;
using PontoEletronico.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PontoEletronico.Web.Controllers
{
    [Authorize]
    public class RegistroPontoController : Controller
    {

        private readonly IRegistroPontoService _registoPontoService;
        private readonly IFuncionarioService _funcionarioService;
        private readonly IExtendedAuthenticate _authentication;
        private readonly IWebHostEnvironment _environment;

        public RegistroPontoController(IRegistroPontoService registroPontoService,
                                       IFuncionarioService funcionarioService,
                                       IExtendedAuthenticate authentication,
                                       IWebHostEnvironment environment)
        {
            _registoPontoService = registroPontoService;
            _funcionarioService = funcionarioService;
            _authentication = authentication;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? funcionarioId)
        {

            if (funcionarioId == null)
            {
                funcionarioId = await _authentication.GetFuncionarioIdByUserNameAsync(this.HttpContext.User.Identity.Name);                
            }

            RelatorioRegistoPontoDTO relatorio = await _registoPontoService.
                GerarRelatorioRegistrosPontos(funcionarioId.GetValueOrDefault(), DateTime.Now.Date);

            if (relatorio == null) return RedirectToAction("Index", "Funcionario");

            return View(relatorio);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> BaterPonto(RegistroPontoDTO registroPontoDTO)
        {
            DateTime dataHoraAtual = DateTime.Now;
            DateTime dataAtual = dataHoraAtual.Date;
            TimeSpan horaAtual = new TimeSpan(dataHoraAtual.Hour, dataHoraAtual.Minute, dataHoraAtual.Second);
            var funcionarioId = await _authentication.GetFuncionarioIdByUserNameAsync(this.HttpContext.User.Identity.Name);

            var registroPonto = await _registoPontoService.CreateAsync(new RegistroPontoDTO
            {
                Data = dataAtual,
                Hora = horaAtual,
                FuncionarioId = funcionarioId
            });

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<ActionResult> BuscarPorData(int? funcionarioId, DateTime buscarPorData)
        {            
            var relatorio = await _registoPontoService.
                GerarRelatorioRegistrosPontos(funcionarioId.GetValueOrDefault(), buscarPorData);

            if (relatorio == null) return RedirectToAction("Index");

            return View("Index", relatorio);
        }
    }
}
