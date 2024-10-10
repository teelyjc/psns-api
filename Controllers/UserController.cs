using Microsoft.AspNetCore.Mvc;

using server.Domains;

namespace server.Controllers
{
  [ApiController]
  public class UserController(IUserUsecases usersUsecases) : Controller
  {
    private readonly IUserUsecases UserUsecases = usersUsecases;

    [HttpPost("signup")]
    public async Task SignUpAsync(Domains.SignUpUser signUp)
    {
      await this.UserUsecases.SignUpUser(signUp);
      return;
    }

    [HttpGet("users/{username}")]
    public async Task<User> GetUserByUsernameAsync(string username)
    {
      return await this.UserUsecases.GetUserByUsername(username);
    }

    [HttpPatch("users")]
    public async Task UpdateUserAsync(Domains.UpdateUser user)
    {
      await this.UserUsecases.UpdateUser(user);
      return;
    }
  }
}
