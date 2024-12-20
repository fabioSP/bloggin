using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace bloggin.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ErrorController : ControllerBase
  {
    [HttpGet("{code}")]
    public async Task<IActionResult> Get(int code)
    {
        return await Task.Run(() =>
        {
            return StatusCode(code, new ProblemDetails()
            {
                Detail = "See the errors property for details.",
                Instance = HttpContext.Request.Path,
                Status = code,
                Title = ((HttpStatusCode)code).ToString(),
                Type = "https://my.api.com/response"
            });
        });
    }
  }
}