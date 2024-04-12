﻿using AutoMapper;
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
    public class RegistroPontoService : IRegistroPontoService
    {
        private readonly IRegistroPontoRepository _registroPontoRepository;
        private readonly IMapper _mapper;

        public RegistroPontoService(IRegistroPontoRepository registroPontoRepository, IMapper mapper)
        {
            _registroPontoRepository = registroPontoRepository;
            _mapper = mapper;
        }

        public async Task<RegistroPontoDTO> CreateAsync(RegistroPontoDTO registroPontoDTO)
        {
            try
            {
                if (registroPontoDTO == null) return null;

                var registroPonto = _mapper.Map<RegistroPonto>(registroPontoDTO);
                var registroPontoCriado = await _registroPontoRepository.CreateAsync(registroPonto);

                if (registroPontoCriado != null) return null;

                return _mapper.Map<RegistroPontoDTO>(registroPontoCriado);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<RegistroPontoDTO>> GetByFuncionarioIdAsync(int funcionarioId)
        {
            try
            {
                if (funcionarioId <= 0) return null;

                var registrosPonto = await _registroPontoRepository.GetByFuncionarioIdAsync(funcionarioId);

                if (registrosPonto == null) return null;

                return _mapper.Map<IEnumerable<RegistroPontoDTO>>(registrosPonto);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<RegistroPontoDTO>> GetByMatriculaFuncionarioAsync(string matricula)
        {
            try
            {
                if (string.IsNullOrEmpty(matricula)) return null;

                var registrosPonto = await _registroPontoRepository.GetByMatriculaFuncionarioAsync(matricula);

                if (registrosPonto == null) return null;

                return _mapper.Map<IEnumerable<RegistroPontoDTO>>(registrosPonto);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<RegistroPontoDTO> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0) return null;

                var registroPonto = await _registroPontoRepository.GetByIdAsync(id);

                if (registroPonto == null) return null;

                return _mapper.Map<RegistroPontoDTO>(registroPonto);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<RegistroPontoDTO>> GetByDataAsync(string matricula, DateTime data)
        {
            try
            {
                if (string.IsNullOrEmpty(matricula) || data == null) return null;

                var registrosPonto = await _registroPontoRepository.GetByDataAsync(matricula, data);

                if (registrosPonto == null) return null;

                return _mapper.Map<IEnumerable<RegistroPontoDTO>>(registrosPonto);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<RegistroPontoDTO>> GetByPeriodoAsync(string matricula, DateTime dataInicial, DateTime dataFinal)
        {
            try
            {
                if (string.IsNullOrEmpty(matricula) || dataInicial == null || dataFinal == null) return null;

                //if (dataInicial > dataFinal)
                //{
                //    return null;
                //}

                var registrosPonto = await _registroPontoRepository.GetByPeriodoAsync(matricula, dataInicial, dataFinal);

                if (registrosPonto == null) return null;

                return _mapper.Map<IEnumerable<RegistroPontoDTO>>(registrosPonto);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<RegistroPontoDTO> UpdateAsync(RegistroPontoDTO registroPontoDTO)
        {
            try
            {
                if (registroPontoDTO == null) return null;

                var registroPonto = _mapper.Map<RegistroPonto>(registroPontoDTO);

                var registroPontoAtualizado = await _registroPontoRepository.UpdateAsync(registroPonto);                

                if (registroPontoAtualizado == null) return null;

                return _mapper.Map<RegistroPontoDTO>(registroPontoAtualizado);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

    }
}
