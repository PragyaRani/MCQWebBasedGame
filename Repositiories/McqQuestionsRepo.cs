using EFCore.BulkExtensions;
using MCQPuzzleGame.Context;
using MCQPuzzleGame.Model;
using System.Threading.Tasks;

namespace MCQPuzzleGame.Repositiories
{
    public class McqQuestionsRepo : IMcqQuestionRepo
    {
        private readonly DbStoreContext dbStoreContext;
        public McqQuestionsRepo(DbStoreContext _dbStoreContext)
        {
            dbStoreContext = _dbStoreContext;
        }
        public async Task<int> AddQuestion(McqQuestions[] questions)
        {
            //return await dbStoreContext.SaveChangesAsync();
            try
            {
                await dbStoreContext.BulkInsertAsync(questions);
            }catch (System.Exception ex)
            {
                return 0;
            }
            return 1;
        }
    }
}
