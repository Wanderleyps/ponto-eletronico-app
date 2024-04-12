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
        Task<IEnumerable<RegistroPonto>> GetByMatriculaFuncionarioAsync(string matricula);
        Task<IEnumerable<RegistroPonto>> GetByMatriculaDataAsync(string matricula, DateTime data);
        Task<IEnumerable<RegistroPonto>> GetByFuncionarioIdDataAsync(int funcionarioId, DateTime data);
        Task<IEnumerable<RegistroPonto>> GetByPeriodoAsync(string matricula, DateTime dataInicial, DateTime dataFinal);
        Task<RegistroPonto> GetByIdAsync(int id);
        Task<RegistroPonto> CreateAsync(RegistroPonto registroPonto);
        Task<RegistroPonto> UpdateAsync(RegistroPonto registroPonto);
        Task<RegistroPonto> RemoveAsync(RegistroPonto registroPonto);
    }
}
