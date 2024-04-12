using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PontoEletronico.Application.DTOs;
using PontoEletronico.Application.Interfaces;
using System.Threading.Tasks;

namespace PontoEletronico.Web.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioService _funcionarioService;
        private readonly IWebHostEnvironment _environment;

        public FuncionarioController(IFuncionarioService funcionarioService, IWebHostEnvironment environment)
        {
            _funcionarioService = funcionarioService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var funcionarios = await _funcionarioService.GetFuncionariosAsync();
            return View(funcionarios);
        }

        public async Task<IActionResult> Create()
        {         
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FuncionarioDTO funcionarioDto)
        {
            if (ModelState.IsValid)
            {
                await _funcionarioService.CreateAsync(funcionarioDto);
                return RedirectToAction(nameof(Index));
            }
            return View(funcionarioDto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var funcionarioDto = await _funcionarioService.GetByIdAsync(id.GetValueOrDefault());

            if (funcionarioDto == null) return NotFound();
           
            return View(funcionarioDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FuncionarioDTO funcionarioDto)
        {
            if (ModelState.IsValid)
            {
                await _funcionarioService.UpdateAsync(funcionarioDto);
                return RedirectToAction(nameof(Index));
            }
            return View(funcionarioDto);
        }
    }
}
