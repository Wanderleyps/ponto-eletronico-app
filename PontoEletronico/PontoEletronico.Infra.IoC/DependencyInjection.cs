using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PontoEletronico.Application.Mappings;
using PontoEletronico.Domain.Interfaces;
using PontoEletronico.Infra.Data.Context;
using PontoEletronico.Infra.Data.Repositories;
using PontoEletronico.Application.Interfaces;
using PontoEletronico.Application.Services;

namespace PontoEletronico.Infra.IoC
{
    /*
        * 
        * Classe responsável por tratar das injeções de dependências do projeto.
        * Faz o registro das entidades mapeando interface / classe concreta.
        *  
        * **/
    public static class DependencyInjection
    {
        /*
         * 
         *Trata-se método de extenção, adiciona funcionalidades a um tipo já existente
         * que neste caso é a interface IServiceCollection.
         * 
         * **/
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
             //define que os arquivos de migração irão ficar na pasta onde está definido o arquivo de contexto (Infra.Data)
             b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            services.AddScoped<IRegistroPontoRepository, RegistroPontoRepository>();

            services.AddScoped<IFuncionarioService, FuncionarioService>();
            services.AddScoped<IRegistroPontoService, RegistroPontoService>();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }
}
