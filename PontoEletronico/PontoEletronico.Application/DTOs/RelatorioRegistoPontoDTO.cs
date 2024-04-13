using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoEletronico.Application.DTOs
{
    public class RelatorioRegistoPontoDTO
    {
        public IEnumerable<RegistroPontoDTO> RegistroPontoDTOs { get; set; }

        public FuncionarioDTO Funcionario { get; set; }

        public string BuscarPorData { get; set; }

        public string HorasRealizadas { get; set; }

        public string HorasFimJornada { get; set; }

        public string HorasExtras { get; set; }

        public bool IsJornadaCompleta { get; set; }
    }
}
