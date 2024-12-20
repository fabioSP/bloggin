using System;
using System.Threading.Tasks;
using bloggin.API.Repositories;
using bloggin.API.Services;
using bloggin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bloggin.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HomeController : ControllerBase
  {
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserMO model)
    {
      var user = UserRepository.GetUser(model.UserName, model.Password);

      if(user == null)
        return NotFound(new { message = "Usuário ou senha inválidos" });

      var token = TokenService.GenerateToken(user);
      user.Password = "";

      return new {
        user = user,
        token = $"Bearer {token}"
      };
    }

    [HttpGet]
    [Route("anonymous")]
    [AllowAnonymous]
    public string Anonymous() => "Anônimo";

    [HttpGet]
    [Route("authenticated")]
    [Authorize]
    public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

    [HttpGet]
    [Route("manager")]
    [Authorize(Roles = "manager")]
    public string Manager() => "Gerente";
  }
}