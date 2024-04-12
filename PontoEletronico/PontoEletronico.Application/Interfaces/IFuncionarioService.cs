using PontoEletronico.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoEletronico.Application.Interfaces
{
    public interface IFuncionarioService
    {
        Task<FuncionarioDTO> CreateAsync(FuncionarioDTO funcionarioDTO);
        Task<FuncionarioDTO> GetByIdAsync(int id);
        Task<IEnumerable<FuncionarioDTO>> GetFuncionariosAsync();
        Task<FuncionarioDTO> UpdateAsync(FuncionarioDTO funcionarioDTO);
    }

}
