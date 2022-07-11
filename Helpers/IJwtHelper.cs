using MCQPuzzleGame.Model;
using System.Threading.Tasks;

namespace MCQPuzzleGame.Helpers
{
    public interface IJwtHelper
    {
      string GenerateTokens(Users user);
       RefreshTokens GenerateRefreshToken(string ipAddress);
    }
}
