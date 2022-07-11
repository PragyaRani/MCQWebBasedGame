using MCQPuzzleGame.Model;
using System.Collections.Generic;

namespace MCQPuzzleGame.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(LoginUser user, string ipAddress);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        bool RevokeToken(string token, string ipAddress);
        IEnumerable<Users> GetAll();
        Users GetById(int id);
        
    }
}
