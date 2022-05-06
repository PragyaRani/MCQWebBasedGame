using MCQPuzzleGame.Model;
using System.Threading.Tasks;
namespace MCQPuzzleGame.Repositiories
{
    public interface IUserRepository
    {
      
        Task<int> AddUser(RegisterUser user);
        Task<Users> GetUserById(string Email);
        Task<Users> VerifyUser(LoginUser user);
        Task<string> sendToken(Users user);
    }
}
