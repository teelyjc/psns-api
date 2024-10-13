namespace server.Domains
{
  public interface IAuthUsecases
  {
    Task<SignInOutputs> SignIn(SignInInputs inputs);
    Task<SignOutOutputs> SignOut(SignOutInputs inputs);
    Task<User> Authenticate(AuthenticateInputs inputs);
  }

  public class SignInCredentials
  {
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
  }
  public class SignInInputs
  {
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
  }
  public class SignInOutputs
  {
    public string CookieHeader { get; set; } = string.Empty;
    public CookieOptions CookieOptions { get; set; } = new CookieOptions();
  }
  public class SignOutInputs
  {
    public string CookieHeader { get; set; } = string.Empty;
  }
  public class SignOutOutputs
  {
    public string CookieHeader { get; set; } = string.Empty;
    public CookieOptions CookieOptions { get; set; } = new CookieOptions();
  }

  public class AuthenticateInputs
  {
    public string CookieHeader { get; set; } = string.Empty;
    public CookieOptions CookieOptions { get; set; } = new CookieOptions();
  }
}
