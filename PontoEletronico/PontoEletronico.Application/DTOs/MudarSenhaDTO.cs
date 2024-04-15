using System.ComponentModel.DataAnnotations;

namespace PontoEletronico.Application.DTOs
{
    public class MudarSenhaDTO
    {
        [Required(ErrorMessage = "Senha atual é obrigatória.")]
        [DataType(DataType.Password)]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Nova senha atual é obrigatória.")]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Confirmar senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("NovaSenha", ErrorMessage = "As senhas não coincidem")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
