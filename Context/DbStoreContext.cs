using Microsoft.EntityFrameworkCore;
using MCQPuzzleGame.Model;
namespace MCQPuzzleGame.Context
{
    public class DbStoreContext:DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<McqQuestions> QuestionSheet { get; set; }
        public DbStoreContext(DbContextOptions<DbStoreContext> options):base(options)
        {

        }
    }
}
