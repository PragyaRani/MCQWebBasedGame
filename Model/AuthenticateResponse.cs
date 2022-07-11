using System.Text.Json.Serialization;

namespace MCQPuzzleGame.Model
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string JwtToken { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }    
        public AuthenticateResponse(Users users, string jwt,string refreshToken)
        {
            Id = users.Id;
            FirstName = users.FirstName;
            LastName = users.LastName;
            UserName = users.Email;
            JwtToken = jwt;
            RefreshToken = refreshToken;
        }
    }
}
