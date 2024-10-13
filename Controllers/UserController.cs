using Microsoft.AspNetCore.Mvc;

using server.Domains;

namespace server.Controllers
{
  [ApiController]
  [Route("users")]
  public class UserController(IUserUsecases usersUsecases) : Controller
  {
    private readonly IUserUsecases UserUsecases = usersUsecases;

    [HttpPost("/signup")]
    public async Task<ActionResult<Response>> SignUp(SignUpUser signUp)
    {
      await this.UserUsecases.SignUp(signUp);
      return new Response()
      {
        Success = true,
        Data = new Success() { Message = "signed up successfully" }
      };
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<Response<User>>> GetUserByUsername(string username)
    {
      User user = await this.UserUsecases.GetUserByUsername(username);
      return new Response<User>() { Success = true, Data = user };
    }

    [HttpPatch("{userId}")]
    public async Task<ActionResult<Response<Success>>> UpdateUserById(string userId, UserUpdateInputs inputs)
    {
      await this.UserUsecases.UpdateUserById(userId, inputs);
      return new Response<Success>()
      {
        Success = true,
        Data = new Success() { Message = "user was updated successfully" }
      };
    }
  }
}
