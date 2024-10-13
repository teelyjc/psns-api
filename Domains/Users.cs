using System.Net;
using Npgsql.Replication;
using NpgsqlTypes;

namespace server.Domains
{
  public class User
  {
    public static User GetUserFromContext(HttpContext context)
    {
      if (context.Items.TryGetValue("user", out var incomingUser))
      {
        if (incomingUser is not User user)
        {
          throw new HttpRequestException("user was not found in context", null, HttpStatusCode.InternalServerError);
        }

        return user;
      }

      throw new HttpRequestException("user was not set to context", null, HttpStatusCode.InternalServerError);
    }
    public static void SetUserToContext(HttpContext context, User user)
    {
      context.Items["user"] = user;
    }
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    [PgName("position")]
    public Position Position { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }

  public enum Position
  {
    [PgName("CUSTOMER")]
    CUSTOMER,
    [PgName("VETERINARIAN")]
    VETERINARIAN,
    [PgName("EXECUTIVE")]
    EXECUTIVE,
    [PgName("ADMINISTRATOR")]
    ADMINISTRATOR
  }

  public interface IUserRepository
  {
    Task<User> GetUserById(string id);
    Task<User> GetUserByUsername(string username);
    Task CreateUser(User user);
    Task UpdateUserById(string userId, UserUpdateInputs user);
    Task<User> GetUserBySessionId(string id);
  }
  public interface IUserUsecases
  {
    Task<User> GetUserByUsername(string username);
    Task<User> GetUserById(string id);
    Task SignUp(SignUpUser user);
    Task UpdateUserById(string userId, UserUpdateInputs user);
    Task<User> GetUserBySessionId(string id);
  }

  public class SignUpUser
  {
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
  }

  public class UserUpdateInputs
  {
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
  }
}
