using Microsoft.AspNetCore.Identity;
using PontoEletronico.Domain.Account;
using PontoEletronico.Domain.Entities;
using PontoEletronico.Domain.Enums;
using PontoEletronico.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoEletronico.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public SeedUserRoleInitial(RoleManager<IdentityRole> roleManager,
              UserManager<ApplicationUser> userManager,
              IFuncionarioRepository funcionarioRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _funcionarioRepository = funcionarioRepository;
        }

        public async void SeedUsers()
        {           
            if (_userManager.FindByEmailAsync(UserDefault.Email).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = UserDefault.Email;
                user.Email = UserDefault.Email;
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, UserDefault.Senha).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, TypeRole.Admin.ToString()).Wait();

                    user = _userManager.FindByEmailAsync(UserDefault.Email).Result;

                    await _funcionarioRepository.CreateAsync(new Funcionario
                    {
                        Nome = TypeRole.Admin.ToString(),
                        Email = UserDefault.Email,
                        Matricula = UserDefault.Matricula,
                        Cargo = TypeRole.Admin.ToString(),
                        TipoJornada = TipoJornada.OitoHorasDiarias,
                        UserId = user.Id,
                    });
                }                
            }

        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync(TypeRole.Funcionario.ToString()).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = TypeRole.Funcionario.ToString();
                role.NormalizedName = TypeRole.Funcionario.ToString().ToUpper();
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync(TypeRole.Admin.ToString()).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = TypeRole.Admin.ToString();
                role.NormalizedName = TypeRole.Admin.ToString().ToUpper();
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }
    }
}
