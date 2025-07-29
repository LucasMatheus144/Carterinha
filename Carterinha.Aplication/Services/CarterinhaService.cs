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


        public virtual async Task<Carteira?> CadastraCarteia ( CreateCarterinhaDTO obj )
        {
            if (!_validar.ValidarDto(obj)) return null;

            var carteira = new Carteira
            {
                Nome = obj.Nome,
                Cpf = obj.Cpf,
                Rg = obj.Rg,
                DataNascimento = obj.DataNascimento,
                Validade = obj.Validade
            };

            if( !_validar.ValidaEntidade(carteira) ) return null;

            try
            {
                using var inicia = _db.IniciarTransacao();

                await _db.IncluirAsync(carteira);
                await _db.CommitTransicao();
                return carteira;
            }
            catch (Exception ex)
            {
                await _db.RollbackTransicao();
                throw new Exception($"Erro ao cadastrar a carteira: {ex.Message}", ex);
            }
        }
    }
}
