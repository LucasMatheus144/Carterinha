using Carterinha.Aplication.Services;
using System.ComponentModel.DataAnnotations;

namespace Carterinha.Aplication.Dto
{
    public class CreateCarterinhaDTO
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [ValidatorCpf]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Rg é obrigatório.")]
        public string Rg { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Ra é obrigatório.")]
        public DateTime DataNascimento { get; set; }

        public DateTime Validade => DateTime.Today.AddYears(1);
    }
}
