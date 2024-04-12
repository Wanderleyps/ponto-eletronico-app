using PontoEletronico.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoEletronico.Domain.Entities
{
    public class RegistroPonto
    {
        public int Id { get; set; }

        public TipoRegistro Tipo { get; set; }

        public DateTime Data { get; set; }

        public TimeSpan Hora { get; set; }

        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
