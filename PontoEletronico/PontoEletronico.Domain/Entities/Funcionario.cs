using PontoEletronico.Domain.Enums;
using System;
using System.Collections.Generic;

namespace PontoEletronico.Domain.Entities
{
    public class Funcionario
    {
        public int Id { get; set; }

        public string Matricula { get; set; }

        public string Nome { get; set; }

        public string Cargo { get; set; }

        public TipoJornada TipoJornada { get; set; }

        public Guid UserId { get; set; }

        public IEnumerable<RegistroPonto> RegistroPontos { get; set; }
    }
}
