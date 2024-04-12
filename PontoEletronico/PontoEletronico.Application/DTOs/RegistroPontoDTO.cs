using PontoEletronico.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoEletronico.Application.DTOs
{
    public class RegistroPontoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O tipo de registro é obrigatório.")]
        public TipoRegistro Tipo { get; set; }
       
        public DateTime Data { get; set; }

        public TimeSpan Hora { get; set; }

        [Required(ErrorMessage = "O ID do funcionário é obrigatório.")]
        public int FuncionarioId { get; set; }

        public string Erro { get; set; }    
    }
}
