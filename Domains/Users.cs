namespace server.Domains
{
  public class User
  {
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }

  public interface IUserRepository
  {
    Task<User> GetUserById(string id);
    Task<User> GetUserByUsername(string username);
    Task CreateUser(User user);
    Task UpdateUserInfo(UpdateUser user);
  }
  public interface IUserUsecases
  {
    Task<User> GetUserByUsername(string username);
    Task<User> GetUserById(string id);
    Task SignUpUser(SignUpUser user);
    Task UpdateUser(UpdateUser user);
  }

  public class SignUpUser
  {
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
  }

  public class UpdateUser
  {
    public string Id { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
  }
}
