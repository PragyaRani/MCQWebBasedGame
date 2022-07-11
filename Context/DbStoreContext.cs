using Microsoft.EntityFrameworkCore;
using MCQPuzzleGame.Model;
namespace MCQPuzzleGame.Context
{
    public class DbStoreContext:DbContext
    {
        
        public DbStoreContext(DbContextOptions<DbStoreContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Users> UserInfo { get; set; }

        public DbSet<McqQuestions> QuestionSheet { get; set; }
    }
}
