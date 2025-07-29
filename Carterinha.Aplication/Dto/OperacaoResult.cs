using NHibernate.Mapping.ByCode.Impl;

namespace Carterinha.Aplication.Dto
{
    public class OperacaoResult
    {
        public bool Sucesso { get; set; }

        public List<MensagemErro> Erros { get; set; } = new List<MensagemErro>();

        public required Object Resultado { get; set; }
    }

    public class MensagemErro
    {
        public string Propriedade { get; set; }
        public string Mensagem { get; set; }

        public MensagemErro(string propriedade, string mensagem)
        {
            Propriedade = propriedade;
            Mensagem = mensagem;
        }
    }

}
