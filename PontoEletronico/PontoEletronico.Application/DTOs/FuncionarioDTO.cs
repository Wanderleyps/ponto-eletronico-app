using PontoEletronico.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PontoEletronico.Application.DTOs
{
    public class FuncionarioDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória.")]
        [StringLength(50, ErrorMessage = "A matrícula deve ter no máximo 50 caracteres.")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [StringLength(256, ErrorMessage = "O e-mail deve ter no máximo 256 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O cargo é obrigatório.")]
        [StringLength(100, ErrorMessage = "O cargo deve ter no máximo 100 caracteres.")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "O tipo de jornada é obrigatório.")]
        public TipoJornada TipoJornada { get; set; }

        public string UserId { get; set; }
    }
}
