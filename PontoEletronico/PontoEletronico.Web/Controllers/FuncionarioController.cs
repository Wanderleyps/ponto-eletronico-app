using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            //ViewBag.CategoryId =
            //new SelectList(await _categoryService.GetCategories(), "Id", "Name");

            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            //ViewBag.CategoryId =
            //new SelectList(await _categoryService.GetCategories(), "Id", "Name");

            return View();
        }
    }
}
