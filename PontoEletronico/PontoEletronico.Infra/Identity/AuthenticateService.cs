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

            var funcionario = await _funcionarioRepository.GetByUserIdAsync(user.Id);

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

        public async Task<int> GetFuncionarioIdByUserNameAsync(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            if (user == null) return 0;

            var funcionario = await _funcionarioRepository.GetByUserIdAsync(user.Id);

            if (funcionario == null) return 0;

            return funcionario.Id;
        }

        public async Task<bool> UpdateEmailByUserIdAsync(string email, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return false;

            user.Email = email;
            user.UserName = email;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> UpdatePasswordAsync(string email, string currentPassword, string newPassword)
        {
            var user = _userManager.FindByEmailAsync(email).Result;

            if (user == null) return false;

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            return result.Succeeded;
        }
    }
}
