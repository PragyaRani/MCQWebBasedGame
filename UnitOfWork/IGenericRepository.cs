using System.Collections.Generic;

namespace MCQPuzzleGame.UnitOfWork
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void insert(T entity);
        void update(T entity);
        void delete(T entity);
    }
}
