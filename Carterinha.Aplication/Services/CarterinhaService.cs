using System.ComponentModel.DataAnnotations;
using Carterinha.Aplication.Dto;
using Carterinha.Aplication.Repository;
using Carterinha.DOMAIN.Entities;

namespace Carterinha.Aplication.Services
{
    public class CarterinhaService
    {
        private readonly ValidatorService _validar;
        private readonly IRepository<Carteira> _db;

        public CarterinhaService(ValidatorService validar, IRepository<Carteira> db)
        {
            _validar = validar;
            _db = db;
        }

        public virtual async Task<OperacaoResult> GetPorId(long ra)
        {
            try
            {
                var todos = await _db.BuscarPorIdAsync(ra);
                return OperacaoResult.Sucess(ra);

            }
            catch(Exception ex)
            {
                return OperacaoResult.ErroException(ex);
            }
        }

        public virtual async Task<OperacaoResult> CadastraCarteia ( CreateCarterinhaDTO obj )
        {
            if (!_validar.ValidarDto(obj)) return OperacaoResult.ErroValidation();

            var carteira = new Carteira.Builder()
                .SetNome(obj.Nome)
                .SetCpf(obj.Cpf)
                .SetRg(obj.Rg)
                .SetNascimento(obj.DataNascimento)
                .SetValidade(obj.Validade)
                .Build();

            if(!_validar.ValidaEntidade(carteira)) return OperacaoResult.ErroValidation();

            try
            {
                using var inicia = _db.IniciarTransacao();

                await _db.IncluirAsync(carteira);
                await _db.CommitTransicao();
                return OperacaoResult.Sucess(carteira);
            }
            catch (Exception ex)
            {
                await _db.RollbackTransicao();
                return OperacaoResult.ErroException(ex);
            }
        }

        public virtual async Task<OperacaoResult> ExcluirCarteira(long identificacao)
        {
            var localiza = await GetPorId(identificacao);

            if (localiza == null)
            {
                return OperacaoResult.ErroValidation();
            }

            return OperacaoResult.Sucess(localiza);
        }
    }
}
