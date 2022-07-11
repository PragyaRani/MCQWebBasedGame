using MCQPuzzleGame.Context;
using MCQPuzzleGame.Model;
using MCQPuzzleGame.Repositiories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCQPuzzleGame.Services
{
    public class UserService : IUserService
    {
        private readonly DbStoreContext _dbStoreContext;
        private readonly IUserRepository _userRepository;

        public UserService(DbStoreContext dbStoreContext, IUserRepository userRepository)
        {
            _dbStoreContext = dbStoreContext;
            _userRepository = userRepository;
        }
        public AuthenticateResponse Authenticate(LoginUser user, string ipAddress)
        {
            
            var userinfo = _userRepository.VerifyUser(user);
            // return null, if user is not found
           if(userinfo == null) return null;
            var token = _userRepository.GenerateUserToken(userinfo.Result);
            var refreshtoken = _userRepository.RefreshUserToken(ipAddress);

            // authentication sucessful so generate jwt and refresh tokens
            userinfo.Result.RefreshToken.Add(refreshtoken);
            // save refresh token
            _dbStoreContext.Update(userinfo.Result);
            _dbStoreContext.SaveChanges();
           return new AuthenticateResponse(userinfo.Result, token, refreshtoken.Token);
        }

        public IEnumerable<Users> GetAll()
        {
                return (IEnumerable<Users>)_dbStoreContext.UserInfo.ToListAsync();
        }

        public Users GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var user = _dbStoreContext.UserInfo.SingleOrDefault
                (u => u.RefreshToken.Any(t => t.Token == token));
            if (user == null) return null;

            var refreshtoken = user.RefreshToken.Single(x => x.Token == token);
            if ((bool)!refreshtoken.IsActive) return null;
            //var user = getUserData(token);
            //if (user == null)
            //    return null;
            //var refreshtoken = user.refreshtoken;
            var newrefreshtoken = _userRepository.RefreshUserToken(ipAddress);

            // refresh old token with new and save
            refreshtoken.Revoked = DateTime.UtcNow;
            refreshtoken.RevokedByIp = ipAddress;
            refreshtoken.ReplaceByToken = newrefreshtoken.Token;
            user.RefreshToken.Add(refreshtoken);
            _dbStoreContext.Update(user);
            _dbStoreContext.SaveChanges();
            var jwtToken = _userRepository.GenerateUserToken(user);
            return new AuthenticateResponse(user, jwtToken, newrefreshtoken.Token);
        }

        public bool RevokeToken(string token, string ipAddress)
        {
            var info = getUserData(token);
            if (info == null)
                return false;
            var rtoken = info.refreshtoken as RefreshTokens;
            rtoken.Revoked = DateTime.UtcNow;
            rtoken.RevokedByIp = ipAddress;
            _dbStoreContext.Remove(rtoken);
            _dbStoreContext.SaveChanges(); 
            return true;
        }

        private dynamic getUserData(string token)
        {
            var user = _dbStoreContext.UserInfo.SingleOrDefault
               (u => u.RefreshToken.Any(
                   t => (t.Token == token || t.ReplaceByToken == token)
                   ));
            if (user == null) return null;

            var refreshtoken = user.RefreshToken.Single(x => x.Token == token || x.ReplaceByToken == token);
            if ((bool)!refreshtoken.IsActive)
            {
                _dbStoreContext.Remove(refreshtoken);
                _dbStoreContext.SaveChanges();
                return null;
            }

            return new { user , refreshtoken };
        }
    }
}
