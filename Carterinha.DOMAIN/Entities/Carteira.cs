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

        private Carteira() { }

        public class Builder
        {
            private readonly Carteira _carteira = new Carteira();

            public Builder SetRa(long ra) { _carteira.Ra = ra; return this; }

            public Builder SetNome(string nome) { _carteira.Nome = nome; return this; }

            public Builder SetCpf(string cpf) { _carteira.Cpf = cpf; return this; }

            public Builder SetRg(string rg) { _carteira.Rg = rg; return this; }

            public Builder SetNascimento(DateTime nascimento) { _carteira.DataNascimento = nascimento; return this; }

            public Builder SetValidade(DateTime validade) { _carteira.Validade =  validade; return this; }


            public Carteira Build()
            {
                return _carteira;
            }
        }

    }
}
