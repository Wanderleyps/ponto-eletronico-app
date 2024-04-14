using Microsoft.EntityFrameworkCore;
using PontoEletronico.Domain.Entities;
using PontoEletronico.Domain.Interfaces;
using PontoEletronico.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PontoEletronico.Infra.Data.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {

        private ApplicationDbContext _context;
        public FuncionarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Funcionario> CreateAsync(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();
            return funcionario;
        }

        public async Task<Funcionario> GetByIdAsync(int id)
        {
            return await _context.Funcionarios.Include(c => c.RegistroPontos)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Funcionario> GetByUserIdAsync(string userId)
        {
            return await _context.Funcionarios.Include(c => c.RegistroPontos)
                .SingleOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<IEnumerable<Funcionario>> GetFuncionariosAsync()
        {
            return await _context.Funcionarios.OrderByDescending(f => f.Id).ToListAsync();
        }

        public async Task<Funcionario> UpdateAsync(Funcionario funcionario)
        {
            _context.Funcionarios.Update(funcionario);
            await _context.SaveChangesAsync();
            return funcionario; ;
        }
    }
}
