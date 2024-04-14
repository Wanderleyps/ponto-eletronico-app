using PontoEletronico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoEletronico.Domain.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<IEnumerable<Funcionario>> GetFuncionariosAsync();
        Task<Funcionario> GetByIdAsync(int id);
        Task<Funcionario> GetByUserIdAsync(string userId);
        Task<Funcionario> CreateAsync(Funcionario funcionario);
        Task<Funcionario> UpdateAsync(Funcionario funcionario);
    }
}
