using System.Linq.Expressions;

namespace AlkemyChallengeDisney.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int? id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int? id);
        IEnumerable<T> find(Expression<Func<T, bool>> predicate);
    }
}
