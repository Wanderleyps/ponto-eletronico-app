using Microsoft.EntityFrameworkCore;
using PontoEletronico.Domain.Entities;
using PontoEletronico.Domain.Interfaces;
using PontoEletronico.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PontoEletronico.Infra.Data.Repositories
{
    public class RegistroPontoRepository : IRegistroPontoRepository
    {

        private ApplicationDbContext _context;

        public RegistroPontoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RegistroPonto> CreateAsync(RegistroPonto registroPonto)
        {
            _context.Add(registroPonto);
            await _context.SaveChangesAsync();
            return registroPonto;
        }

        public async Task<RegistroPonto> GetByIdAsync(int id)
        {
            return await _context.RegistroPontos.FindAsync(id);
        }

        public async Task<IEnumerable<RegistroPonto>> GetByFuncionarioIdAsync(int funcionarioId)
        {
            return await _context.RegistroPontos
            .Where(rp => rp.FuncionarioId == funcionarioId)
            .ToListAsync();
        }

        public async Task<IEnumerable<RegistroPonto>> GetByMatriculaFuncionarioAsync(string matricula)
        {
            return await _context.RegistroPontos
                .Include(rp => rp.Funcionario)
                .Where(rp => rp.Funcionario.Matricula == matricula)
                .OrderBy(rp => rp.Data)
                .ThenBy(rp => rp.Hora)
                .ToListAsync();
        }

        public async Task<RegistroPonto> RemoveAsync(RegistroPonto registroPonto)
        {
            _context.Remove(registroPonto);
            await _context.SaveChangesAsync();
            return registroPonto;
        }

        public async Task<RegistroPonto> UpdateAsync(RegistroPonto registroPonto)
        {
            _context.Update(registroPonto);
            await _context.SaveChangesAsync();
            return registroPonto;
        }

        public async Task<IEnumerable<RegistroPonto>> GetByDataAsync(string matricula, DateTime data)
        {
            return await _context.RegistroPontos
                .Include(rp => rp.Funcionario)
                .Where(rp => rp.Funcionario.Matricula == matricula && rp.Data.Date == data.Date)
                .OrderBy(rp => rp.Hora)
                .ToListAsync();
        }

        public async Task<IEnumerable<RegistroPonto>> GetByPeriodoAsync(string matricula, DateTime dataInicial, DateTime dataFinal)
        {
            return await _context.RegistroPontos
                .Include(rp => rp.Funcionario)
                .Where(rp => rp.Funcionario.Matricula == matricula && rp.Data.Date >= dataInicial.Date && rp.Data.Date <= dataFinal.Date)
                .OrderBy(rp => rp.Data)
                .ThenBy(rp => rp.Hora)
                .ToListAsync();
        }
    }
}
