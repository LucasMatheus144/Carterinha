using System.ComponentModel.DataAnnotations;
using Carterinha.DOMAIN.Interface;

namespace Carterinha.DOMAIN.Entities
{
    public class Carteira: IEntidade
    {
        public long Ra { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Instituicao { get; set; } = "GitHub One Student";

        public string Cpf { get; set; } = string.Empty;

        public string Rg { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }

        public DateTime Validade { get; set; }
        
    }
}
