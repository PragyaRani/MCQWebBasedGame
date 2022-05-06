using System.ComponentModel.DataAnnotations;

namespace MCQPuzzleGame.Model
{
    public class RegisterUser
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "first name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "email is required")]
        [EmailAddress(ErrorMessage = "email format is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password is required")]
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

    public class LoginUser
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email format is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
