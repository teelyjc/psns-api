using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using server.Domains;
using server.Usecases;

namespace server.Controllers
{
  [ApiController]
  public class UsersController : Controller
  {
    private UsersUsecases usersUsecases;
    public UsersController(UsersUsecases usersUsecases)
    {
      this.usersUsecases = usersUsecases;
    }

    [HttpPost("signup")]
    public async Task<string> SignUpAsync(Domains.SignUpUser signUp)
    {
      await this.usersUsecases.SignUpUser(signUp);
      return "OK";
    }

    [HttpGet("users/{username}")]
    public async Task<User> GetUserByUsernameAsync(string username)
    {
      return await this.usersUsecases.GetUserByUsername(username);
    }
  }
}
