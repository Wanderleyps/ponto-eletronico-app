using AutoMapper;
using PontoEletronico.Application.DTOs;
using PontoEletronico.Application.Interfaces;
using PontoEletronico.Domain.Entities;
using PontoEletronico.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoEletronico.Application.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IMapper _mapper;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository, IMapper mapper)
        {
            _funcionarioRepository = funcionarioRepository;
            _mapper = mapper;
        }

        public async Task<FuncionarioDTO> CreateAsync(FuncionarioDTO funcionarioDTO)
        {
            try
            {
                var funcionario = _mapper.Map<Funcionario>(funcionarioDTO);
                var funcionarioCriado = await _funcionarioRepository.CreateAsync(funcionario);

                if (funcionarioCriado == null) return null;

                return _mapper.Map<FuncionarioDTO>(funcionarioCriado);
                
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<FuncionarioDTO> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0) return null;

                var funcionario = await _funcionarioRepository.GetByIdAsync(id);

                if (funcionario == null) return null;

                return _mapper.Map<FuncionarioDTO>(funcionario);
                               
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<FuncionarioDTO>> GetFuncionariosAsync()
        {
            try
            {
                var funcionarios = await _funcionarioRepository.GetFuncionariosAsync();

                if (funcionarios == null) return null;
                
                return _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionarios);                                 
                
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<FuncionarioDTO> UpdateAsync(FuncionarioDTO funcionarioDTO)
        {
            try
            {
                if(funcionarioDTO == null) return null;

                var funcionario = _mapper.Map<Funcionario>(funcionarioDTO);
                var funcionarioAtualizado = await _funcionarioRepository.UpdateAsync(funcionario);

                if (funcionarioAtualizado == null) return null;

                return _mapper.Map<FuncionarioDTO>(funcionarioAtualizado);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

    }

}
