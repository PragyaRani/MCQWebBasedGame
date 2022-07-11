using MCQPuzzleGame.Model;
using System.Threading.Tasks;
namespace MCQPuzzleGame.Repositiories
{
    public interface IUserRepository
    {
      
        Task<int> AddUser(RegisterUser user);
        Task<Users> GetUserById(string Email);
        Task<Users> VerifyUser(LoginUser user);
        string GenerateUserToken(Users user);
        RefreshTokens RefreshUserToken(string ipAddress);
    }
}
