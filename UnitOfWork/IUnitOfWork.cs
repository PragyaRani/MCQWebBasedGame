using System.Data.Entity;

namespace MCQPuzzleGame.UnitOfWork
{
    public interface IUnitOfWork<out TContext> where TContext : DbContext,new()
    {
        TContext Context { get; }
        void CreateTransactions();
        void Commit();
        void Rollback();
        void Save();    }
}
