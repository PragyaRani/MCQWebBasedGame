using MCQPuzzleGame.Model;
using System.Threading.Tasks;

namespace MCQPuzzleGame.Repositiories
{
    public interface IMcqQuestionRepo
    {
        Task<int> AddQuestion(McqQuestions[] questions);
    }
}
