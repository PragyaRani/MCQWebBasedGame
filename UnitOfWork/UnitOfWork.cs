
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace MCQPuzzleGame.UnitOfWork
{
    public class UnitOfWork<TContext> :
        IUnitOfWork<TContext>, IDisposable where TContext : DbContext, new()
    {
        private readonly TContext _context;
        private bool _disposed;
        private string _errorMessage = string.Empty;
        private DbContextTransaction _objTran;
        private Dictionary<string,object> _repositories;

        //using the constructor we are initilizing  the _context variable is nothing
        // we are storing the DBContext object in TContext
        public UnitOfWork()
        {
            _context = new TContext();
        }
        public TContext Context {get { return _context; } }


        // CreateTransactions method will create a database transaction  so that we can do database opeation
        // by applying all or do nothing priciple
        public void CreateTransactions()
        {
           _objTran = _context.Database.BeginTransaction();
        }
        // If all the transactions are completed successfully  then save the chnages
        // permanently in database
        public void Commit()
        {
            _objTran.Commit();
        }

        //The Dispose() method is used to free unmanaged resources like files, 
        //database connections etc. at any time.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // If atleast one of the transaction is failed then we ned to call Roolback()
        // rollback the database chnages  to its previous state
        public void Rollback()
        {
            _objTran?.Rollback();
            _objTran.Dispose();
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
               foreach(var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach(var validationError in validationErrors.ValidationErrors)
                    {
                        _errorMessage +=string.Format("Prperty {0}, Error {1}",validationError.PropertyName,
                            validationError.ErrorMessage) +Environment.NewLine;
                       
                    }
                    throw new System.Exception(_errorMessage, dbEx);
                }
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if(!_disposed)
                if(disposing)
                    _context.Dispose();
            _disposed = true;
        }
        public GenericRepository<T> GenericRepository<T>() where T : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<string, object>();
            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<T>);
                var repositoryInstance = Activator.CreateInstance(
                    repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);

            }
            return (GenericRepository<T>)_repositories[type];
        }
    }
}
