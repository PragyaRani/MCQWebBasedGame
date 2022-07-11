using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MCQPuzzleGame.Model
{
    public class McqQuestions
    {
       
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Question is required")]
        public string Questions { get; set; }
        [Required(ErrorMessage = "Option1 is required")]
        public string Option1 { get; set; }
        [Required(ErrorMessage = "Option2 is required")]
        public string Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
        [Required(ErrorMessage = "Answer is required")]
        public string Answer { get; set; }
        public Image Image { get; set; }
    }

    public class Image
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public IList<McqQuestions> McqQuestions { get; set; }
    }
}
