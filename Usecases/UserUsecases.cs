using System.Net;

using server.Domains;

namespace server.Usecases
{
  public class UserUsecases(IUserRepository userUsecases) : IUserUsecases
  {
    private readonly IUserRepository userUsecases = userUsecases;

    public async Task<User> GetUserByUsername(string username)
    {
      User? user = await this.userUsecases.GetUserByUsername(username);
      user.Password = "";

      return user ?? throw new HttpRequestException("user was not found with this username", null, HttpStatusCode.NotFound);
    }

    public async Task<User> GetUserById(string id)
    {
      User? user = await this.userUsecases.GetUserById(id);
      user.Password = "";

      return user ?? throw new HttpRequestException("user was not found with this id", null, HttpStatusCode.NotFound);
    }

    public async Task SignUpUser(SignUpUser signUp)
    {
      DateTime timestamp = DateTime.Now;

      User? user = await this.GetUserByUsername(signUp.Username.ToString());
      if (user != null)
      {
        throw new HttpRequestException("username is already in use", null, HttpStatusCode.Conflict);
      }

      await this.userUsecases.CreateUser(
        new()
        {
          Id = Guid.NewGuid().ToString(),
          Username = signUp.Username,
          Password = BCrypt.Net.BCrypt.EnhancedHashPassword(signUp.Password, 10),
          CreatedAt = timestamp,
          UpdatedAt = timestamp,
        }
      );
    }

    public async Task UpdateUser(UpdateUser updateUser)
    {
      User user = await this.GetUserById(updateUser.Id);

      await this.userUsecases.UpdateUserInfo(
        new()
        {
          Id = user.Id,
          Firstname = updateUser.Firstname,
          Lastname = updateUser.Lastname
        }
      );
    }
  }
}
