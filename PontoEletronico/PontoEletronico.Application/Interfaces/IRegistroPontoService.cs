using PontoEletronico.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoEletronico.Application.Interfaces
{
    public interface IRegistroPontoService
    {
        Task<RegistroPontoDTO> CreateAsync(RegistroPontoDTO registroPontoDTO);
        Task<IEnumerable<RegistroPontoDTO>> GetByFuncionarioIdAsync(int funcionarioId);
        //Task<IEnumerable<RegistroPontoDTO>> GetByMatriculaFuncionarioAsync(string matricula);
        Task<RegistroPontoDTO> GetByIdAsync(int id);
        //Task<IEnumerable<RegistroPontoDTO>> GetByMatriculaDataAsync(string matricula, DateTime data);
        Task<IEnumerable<RegistroPontoDTO>> GetByFuncionarioIdDataAsync(int funcionarioId, DateTime data);
        Task<RegistroPontoDTO> UpdateAsync(RegistroPontoDTO registroPontoDTO);
        Task<RelatorioRegistoPontoDTO> GerarRelatorioRegistrosPontos(int funcionarioId, DateTime buscarPorData);
    }

}
