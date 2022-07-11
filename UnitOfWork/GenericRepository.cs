using MCQPuzzleGame.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace MCQPuzzleGame.UnitOfWork
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private IDbSet<T> entities;
        private string _errorMessage = string.Empty;
        private bool _isDisposed;
        public MCQDbContext Context { get; set; }

        public GenericRepository(IUnitOfWork<MCQDbContext> unitOfWork) : this(unitOfWork.Context)
        {

        }
        public GenericRepository(MCQDbContext context)
        {
            _isDisposed = false;
            Context = context;
        }

        
        public void delete(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public T GetById(object id)
        {
            throw new System.NotImplementedException();
        }

        public void insert(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void update(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
