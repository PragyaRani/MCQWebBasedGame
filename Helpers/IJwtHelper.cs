using MCQPuzzleGame.Model;
using System.Threading.Tasks;

namespace MCQPuzzleGame.Helpers
{
    public interface IJwtHelper
    {
      Task<string> GenerateTokens(Users user);

    }
}
