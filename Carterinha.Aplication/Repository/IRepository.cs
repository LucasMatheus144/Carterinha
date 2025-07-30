using Carterinha.DOMAIN.Interface;

namespace Carterinha.Aplication.Repository
{
    public interface IRepository<T>
    {
        Task<T> IncluirAsync(T entity);

        Task<T> SalvarAsync(T entity);

        Task ExcluirAsync(long ra);

        Task<T> BuscarPorIdAsync(long ra);

        IQueryable<T> Query();

        IDisposable IniciarTransacao();

        Task CommitTransicao();

        Task RollbackTransicao();
    }
}
