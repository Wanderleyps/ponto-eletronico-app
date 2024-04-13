using AutoMapper;
using PontoEletronico.Application.DTOs;
using PontoEletronico.Application.Interfaces;
using PontoEletronico.Domain.Account;
using PontoEletronico.Domain.Entities;
using PontoEletronico.Domain.Interfaces;
using PontoEletronico.Infra.Data.Interfaces;
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
        private readonly IExtendedAuthenticate _authentication;
        private readonly IMapper _mapper;
        private readonly string senhaPadrao = "Senh@123";

        public FuncionarioService(IFuncionarioRepository funcionarioRepository, IExtendedAuthenticate authentication, IMapper mapper)
        {
            _funcionarioRepository = funcionarioRepository;
            _authentication = authentication;
            _mapper = mapper;
        }

        public async Task<FuncionarioDTO> CreateAsync(FuncionarioDTO funcionarioDTO)
        {
            try
            {
                var userId = string.Empty;

                if (!await _authentication.RegisterUser(funcionarioDTO.Email, this.senhaPadrao))
                {
                    return null;
                }
                
                userId = await _authentication.GetUserIdByEmailAsync(funcionarioDTO.Email);

                funcionarioDTO.UserId = userId;

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
