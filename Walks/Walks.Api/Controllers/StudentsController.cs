using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Walks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStudents()
        {
            string[] lists = new String[] { "Rameez", "Khalil", "Ahmed" }; 
            return Ok(lists);
        }
    }
}
