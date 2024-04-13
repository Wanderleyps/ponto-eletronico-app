using Microsoft.AspNetCore.Identity;
using PontoEletronico.Domain.Account;
using PontoEletronico.Domain.Enums;
using PontoEletronico.Domain.Interfaces;
using PontoEletronico.Infra.Data.Interfaces;
using System.Threading.Tasks;

namespace PontoEletronico.Infra.Data.Identity
{
    public class AuthenticateService : IExtendedAuthenticate
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public AuthenticateService(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IFuncionarioRepository funcionarioRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var user = _userManager.FindByEmailAsync(email).Result;

            if (user == null) return false;

            var funcionario = _funcionarioRepository.GetByUserIdAsync(user.Id);

            if (funcionario == null) return false;

            var result = await _signInManager.PasswordSignInAsync(email,
                password, false, lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = email,
                Email = email,             
            };

            var result = await _userManager.CreateAsync(applicationUser, password);            

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(applicationUser, TypeRole.Funcionario.ToString()).Wait();
                await _signInManager.SignInAsync(applicationUser, isPersistent: false);
            }
           
            return result.Succeeded;
        }

        public async Task<string> GetUserIdByEmailAsync(string email)
        {
            var user = _userManager.FindByEmailAsync(email).Result;

            return user != null ? user.Id : string.Empty;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
