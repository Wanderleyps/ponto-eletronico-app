using AutoMapper;
using PontoEletronico.Application.DTOs;
using PontoEletronico.Application.Interfaces;
using PontoEletronico.Domain.Entities;
using PontoEletronico.Domain.Interfaces;
using PontoEletronico.Domain.Enums;
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
        private readonly IFuncionarioService _funcionarioService;
        private readonly IMapper _mapper;
        private readonly TimeSpan seisHoras = TimeSpan.FromHours(6);
        private readonly TimeSpan oitoHoras = TimeSpan.FromHours(8);

        public RegistroPontoService(IRegistroPontoRepository registroPontoRepository, IFuncionarioService funcionarioService, IMapper mapper)
        {
            _registroPontoRepository = registroPontoRepository;
            _funcionarioService = funcionarioService;
            _mapper = mapper;
        }

        public async Task<RegistroPontoDTO> CreateAsync(RegistroPontoDTO registroPontoDTO)
        {
            try
            {
                if (registroPontoDTO == null) return null;

                var registroPonto = _mapper.Map<RegistroPonto>(registroPontoDTO);

                registroPonto.Tipo = await DefinirTipoRegistroAsync(registroPontoDTO.FuncionarioId, registroPontoDTO.Data);

                var registroPontoCriado = await _registroPontoRepository.CreateAsync(registroPonto);

                if (registroPontoCriado == null) return null;

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

        public async Task<IEnumerable<RegistroPontoDTO>> GetByMatriculaDataAsync(string matricula, DateTime data)
        {
            try
            {
                if (string.IsNullOrEmpty(matricula) || data == null) return null;

                var registrosPonto = await _registroPontoRepository.GetByMatriculaDataAsync(matricula, data);

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

        private async Task<TipoRegistro> DefinirTipoRegistroAsync(int funcionarioId, DateTime data)
        {
            var registrosFuncionario = await _registroPontoRepository.GetByFuncionarioIdAsync(funcionarioId);
            var quantBatidasDia = registrosFuncionario.Count(b => b.Data == data);

            if (quantBatidasDia == 0)
            {
                return TipoRegistro.Entrada;
            }
            else if (quantBatidasDia % 2 == 0)
            {
                return TipoRegistro.Entrada;
            }
            else
            {
                return TipoRegistro.Saida;
            }
        }

        public async Task<IEnumerable<RegistroPontoDTO>> GetByFuncionarioIdDataAsync(int funcionarioId, DateTime data)
        {
            try
            {
                if (funcionarioId == 0 || data == null) return null;

                var registrosPonto = await _registroPontoRepository.GetByFuncionarioIdDataAsync(funcionarioId, data);

                if (registrosPonto == null) return null;

                return _mapper.Map<IEnumerable<RegistroPontoDTO>>(registrosPonto);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<RelatorioRegistoPontoDTO> GerarRelatorioRegistrosPontos(int funcionarioId, DateTime buscarPorData)
        {        
            if (funcionarioId == 0 || buscarPorData == null) return null;

            var registoPontos = await this.GetByFuncionarioIdDataAsync(funcionarioId, buscarPorData);

            var funcionarioDTO = await _funcionarioService.GetByIdAsync(funcionarioId);
            
            if (registoPontos == null || funcionarioDTO == null) return null;

            var totalHoras = CalcularHorasRealizadas(registoPontos);
            
            var isJornadaCompleta = (funcionarioDTO.TipoJornada == TipoJornada.SeisHorasDiarias && totalHoras >= this.seisHoras) ||
                        (funcionarioDTO.TipoJornada == TipoJornada.OitoHorasDiarias && totalHoras >= this.oitoHoras);

            var horasRestantes = CalcularHorasRestantes(funcionarioDTO, totalHoras, isJornadaCompleta);

            var horasExtras = CalcularHorasExtras(funcionarioDTO, totalHoras, isJornadaCompleta);

            RelatorioRegistoPontoDTO relatorio = new()
            {
                RegistroPontoDTOs = registoPontos,
                Funcionario = funcionarioDTO,
                BuscarPorData = buscarPorData.ToString("yyyy-MM-dd"),
                HorasRealizadas = totalHoras.ToString(@"hh\:mm\:ss"),
                HorasFimJornada = horasRestantes.ToString(@"hh\:mm\:ss"),
                HorasExtras = horasExtras.ToString(@"hh\:mm\:ss"),
                IsJornadaCompleta = isJornadaCompleta,
            };
            return relatorio;
        }

        private TimeSpan CalcularHorasRealizadas(IEnumerable<RegistroPontoDTO> registros)
        {
            double horasTotal = 0;
            RegistroPontoDTO entradaAnterior = null;

            foreach (var registro in registros.OrderBy(r => r.Data).ThenBy(r => r.Hora))
            {
                if (registro.Tipo == TipoRegistro.Entrada)
                {
                    entradaAnterior = registro;
                }
                else if (registro.Tipo == TipoRegistro.Saida && entradaAnterior != null)
                {
                    horasTotal += (registro.Hora - entradaAnterior.Hora).TotalHours;
                    entradaAnterior = null;
                }
            }

            TimeSpan horasTotalFormatado = TimeSpan.FromHours(horasTotal);
            return horasTotalFormatado;
        }

        private TimeSpan CalcularHorasRestantes(FuncionarioDTO funcionarioDTO, TimeSpan totalHoras, bool isJornadaCompleta)
        {
            TimeSpan horasRestantes = TimeSpan.Zero;

            if (isJornadaCompleta)
            {
                return horasRestantes;
            }

            if (funcionarioDTO.TipoJornada == TipoJornada.SeisHorasDiarias)
            {
                horasRestantes = this.seisHoras - totalHoras;
            }
            else if (funcionarioDTO.TipoJornada == TipoJornada.OitoHorasDiarias)
            {
                horasRestantes = this.oitoHoras - totalHoras;
            }            

            return horasRestantes;
        }

        private TimeSpan CalcularHorasExtras(FuncionarioDTO funcionarioDTO, TimeSpan totalHoras, bool isJornadaCompleta)
        {
            TimeSpan horasExtras = TimeSpan.Zero;

            if (!isJornadaCompleta)
            {
                return horasExtras;
            }

            if (funcionarioDTO.TipoJornada == TipoJornada.SeisHorasDiarias && totalHoras > this.seisHoras)
            {
                horasExtras = totalHoras - this.seisHoras;
            }
            else if (funcionarioDTO.TipoJornada == TipoJornada.OitoHorasDiarias && totalHoras > this.oitoHoras)
            {
                horasExtras = totalHoras - this.oitoHoras;
            }

            return horasExtras;
        }


    }
}
