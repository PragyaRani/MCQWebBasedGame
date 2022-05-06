using System.ComponentModel.DataAnnotations;

namespace MCQPuzzleGame.Model
{
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? MobileNo { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PinCode { get; set; }
        public string? DateOfBirth { get; set; }
        public string? UserRole { get; set; }

    }
}
