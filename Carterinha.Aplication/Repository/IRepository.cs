using Carterinha.DOMAIN.Interface;

namespace Carterinha.Aplication.Repository
{
    public interface IRepository<T> where T : IEntidade
    {
        Task<T> IncluirAsync(T entity);

        Task<T> SalvarAsync(T entity);

        Task<T> ExcluirAsync(long ra);

        Task<T> BuscarPorIdAsync(long ra);
    }
}
