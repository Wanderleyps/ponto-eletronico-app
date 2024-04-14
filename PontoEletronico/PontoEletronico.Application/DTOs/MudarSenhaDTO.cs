using System.ComponentModel.DataAnnotations;

namespace PontoEletronico.Application.DTOs
{
    public class MudarSenhaDTO
    {
        [Required]
        [DataType(DataType.Password)]
        public string SenhaAtual { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("NovaSenha", ErrorMessage = "As senhas não coincidem")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
