using System.Net;
using server.Domains;

namespace server.Usecases
{
  public class UserUsecases(IUserRepository userRepository) : IUserUsecases
  {
    private readonly IUserRepository userRepository = userRepository;

    public async Task<User> GetUserByUsername(string username)
    {
      User user = await this.userRepository.GetUserByUsername(username)
        ?? throw new HttpRequestException("user was not found with this username", null, HttpStatusCode.NotFound);

      return user;
    }

    public async Task<User> GetUserById(string id)
    {
      User? user = await this.userRepository.GetUserById(id)
        ?? throw new HttpRequestException("user was not found with this id", null, HttpStatusCode.NotFound);

      return user;
    }

    public async Task SignUp(SignUpUser signUp)
    {
      DateTime timestamp = DateTime.Now;

      User? user = await this.userRepository.GetUserByUsername(signUp.Username);
      if (user != null)
      {
        throw new HttpRequestException("username is already in use", null, HttpStatusCode.Conflict);
      }

      await this.userRepository.CreateUser(
        new()
        {
          Id = Guid.NewGuid().ToString(),
          Username = signUp.Username,
          Password = BCrypt.Net.BCrypt.HashPassword(signUp.Password, 13),
          CreatedAt = timestamp,
          UpdatedAt = timestamp,
        }
      );
    }

    public async Task UpdateUserById(string userId, UserUpdateInputs inputs)
    {
      User user = await this.GetUserById(userId);

      await this.userRepository.UpdateUserById(
        user.Id,
        new()
        {
          Firstname = inputs.Firstname,
          Lastname = inputs.Lastname
        }
      );
    }

    public async Task<User> GetUserBySessionId(string id)
    {
      User user = await this.userRepository.GetUserBySessionId(id)
        ?? throw new HttpRequestException("user was not found with this id", null, HttpStatusCode.NotFound);

      return user;
    }
  }
}
