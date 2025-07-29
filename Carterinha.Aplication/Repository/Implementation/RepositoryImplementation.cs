using Carterinha.DOMAIN.Interface;
using NHibernate;

namespace Carterinha.Aplication.Repository.Implementation
{
    public class RepositoryImplementation<T> : IRepository<T> where T : IEntidade
    {

        private readonly ISessionFactory session;
        private readonly ISession db;

        public RepositoryImplementation(ISessionFactory sessionFactory)
        {
            session = sessionFactory;
            db = session.OpenSession();
        }

        public async Task<T> IncluirAsync(T entity)
        {
            await db.SaveAsync(entity);
            return entity;
        }

        public async Task<T> SalvarAsync(T entity)
        {
            await db.UpdateAsync(entity);
            return entity;
        }

        public async Task ExcluirAsync(long ra)
        {
            await db.DeleteAsync(ra);
        }

        public async Task<T> BuscarPorIdAsync(long ra)
        {
            return await db.GetAsync<T>(ra);
        }

        public IQueryable<T> Query()
        {
            return db.Query<T>();
        }

        public async Task RollbackTransicao()
        {
           await db.GetCurrentTransaction().RollbackAsync();
        }

        public async Task CommitTransicao()
        {
            await db.GetCurrentTransaction().CommitAsync();
        }

        public IDisposable IniciarTransacao()
        {
            return  db.BeginTransaction();
        }
    }
}
