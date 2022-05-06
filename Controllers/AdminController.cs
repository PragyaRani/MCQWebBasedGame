using MCQPuzzleGame.Model;
using MCQPuzzleGame.Repositiories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MCQPuzzleGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMcqQuestionRepo mcqQuestion;
        public AdminController(IMcqQuestionRepo _mcqQuestion)
        {
            mcqQuestion = _mcqQuestion;
        }
        [HttpPost]
        public async Task<IActionResult> AddQuestions([FromBody] McqQuestions[] questions)
        {
            int val = await mcqQuestion.AddQuestion(questions);

            return val == 1 ? Ok() : Ok("SOmething wrong with server");
        }


    }
}
