using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoEletronico.Application.DTOs;
using PontoEletronico.Domain.Account;
using PontoEletronico.Infra.Data.Interfaces;
using System.Threading.Tasks;

namespace PontoEletronico.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IExtendedAuthenticate _authentication;

        public AccountController(IExtendedAuthenticate authentication)
        {
            _authentication = authentication;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginDTO()
            {
                //cria o obj setando a url que o usuário se encontra
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var result = await _authentication.Authenticate(model.Email, model.Password);

            if (result)//se true redireciona o usuário de acordo com a url que ele se encontra
            {
                if (string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(model.ReturnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida");
                return View(model);
            }
        }

        [Authorize]
        public IActionResult MudarSenha()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MudarSenha(MudarSenhaDTO model)
        {
            var result = await _authentication.UpdatePasswordAsync(this.HttpContext.User.Identity.Name, model.SenhaAtual, model.NovaSenha);

            if (result)
            {
                return Redirect("/");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Tentativa de registro inválida. A senha deve conter letras maiuscula, menusculas e caracteres especiais");
                return View(model);
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authentication.Logout();
            return Redirect("/Account/Login");
        }
    }
}
