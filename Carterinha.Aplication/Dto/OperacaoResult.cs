using System.ComponentModel.DataAnnotations;

namespace Carterinha.Aplication.Dto
{
    public class OperacaoResult
    {
        public bool Sucesso { get; set; }

        public List<MensagemErro> Erros { get; set; } = new List<MensagemErro>();

        public required Object Resultado { get; set; }

        public T Get<T>() where T: class
        {
            if(Resultado is T data)
            {
                return data;
            }
            throw new InvalidOperationException($"Result error -> {typeof(T).Name}");
        }

        public static OperacaoResult Sucess(object data)
        {
            return new OperacaoResult{ Sucesso = true , Erros = [] , Resultado = data };
        }

        public static OperacaoResult Falhou(ValidationResult[] errors)
        {
            return new OperacaoResult 
            { 
                Sucesso = false ,
                Erros = errors.Select(e =>
                    new MensagemErro(e.ErrorMessage, e.MemberNames.FirstOrDefault())
                ).ToList() ,
                Resultado = false
            };
        }

        public static OperacaoResult ErroException( Exception ex)
        {
            return new OperacaoResult
            { 
                Sucesso = false,
                Erros = new List<MensagemErro>
                {
                    new MensagemErro(ex.GetType().Name, ex.Message)
                },
                Resultado = false
            };
        }

        public static OperacaoResult ErroValidation()
        {
            return new OperacaoResult
            { Sucesso = false , Erros = [], Resultado = false };
        }
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
