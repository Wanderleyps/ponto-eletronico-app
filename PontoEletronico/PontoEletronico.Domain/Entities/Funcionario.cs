using PontoEletronico.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoEletronico.Domain.Entities
{
    public class Funcionario
    {
        public int Id { get; set; }

        public string Matricula { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Cargo { get; set; }

        public TipoJornada TipoJornada { get; set; }

        public string UserId { get; set; }

        public IEnumerable<RegistroPonto> RegistroPontos { get; set; }
    }
}
