using PontoEletronico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoEletronico.Domain.Interfaces
{
    public interface IRegistroPontoRepository
    {
        Task<IEnumerable<RegistroPonto>> GetByFuncionarioIdAsync(int funcionarioId);
        Task<IEnumerable<RegistroPonto>> GetByFuncionarioIdDataAsync(int funcionarioId, DateTime data);
        Task<RegistroPonto> GetByIdAsync(int id);
        Task<RegistroPonto> CreateAsync(RegistroPonto registroPonto);
        Task<RegistroPonto> UpdateAsync(RegistroPonto registroPonto);
    }
}
