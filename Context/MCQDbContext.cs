using MCQPuzzleGame.Model;
using System.Data.Entity;

namespace MCQPuzzleGame.Context
{
    public class MCQDbContext:DbContext
    {
        public MCQDbContext() : base("name=MCQDbContext")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Users> Users { get; set; }
    }
}
