using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController1 : ControllerBase
    {
        // GET: api/Hi
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Hi";
        }
    }
}
