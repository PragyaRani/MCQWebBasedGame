using MCQPuzzleGame.Context;
using MCQPuzzleGame.Model;
using System.Linq;
using System.Threading.Tasks;
using MCQPuzzleGame.Helpers;
namespace MCQPuzzleGame.Repositiories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbStoreContext dbStoreContext;
        private readonly IJwtHelper jwtHelper;
        public UserRepository(DbStoreContext _dbStoreContext, IJwtHelper _jwtHelper)
        {
            dbStoreContext = _dbStoreContext;
            jwtHelper = _jwtHelper;

        }
        public async Task<int> AddUser(RegisterUser user)
        {

            var userData = new Users()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = PasswordHash.Hash(user.Password),
                UserRole = user.UserRole
        };
            dbStoreContext.Users.Add(userData);
            await dbStoreContext.SaveChangesAsync();
            return userData.Id;
        }

        public async Task<Users> GetUserById(string Email)
        {
            return await dbStoreContext.Users.FindAsync(Email);
        }

        public async Task<Users> VerifyUser(LoginUser user)
        {
            var userdata =  dbStoreContext.Users.Where(x => x.Email == user.Email)
                .FirstOrDefault();
            if (userdata != null)
            { 
                return PasswordHash.IsVerify(user.Password, userdata.Password) ? userdata : null;
            }
            return null;

        }

        public async Task<string> sendToken(Users user)
        {
            return await jwtHelper.GenerateTokens(user);
        }
    }
}
