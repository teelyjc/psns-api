namespace server.Domains
{
  public class User
  {
    public string Id
    {
      get;
      set;
    } = string.Empty;
    public string Username
    {
      get;
      set;
    } = string.Empty;
    public string Password
    {
      get;
      set;
    } = string.Empty;
    public string Firstname
    {
      get;
      set;
    } = string.Empty;
    public string Lastname
    {
      get;
      set;
    } = string.Empty;
    public DateTime CreatedAt
    {
      get;
      set;
    }
    public DateTime UpdatedAt
    {
      get;
      set;
    }
  }

  public class SignUpUser
  {
    public string Username
    {
      get;
      set;
    } = string.Empty;
    public string Password
    {
      get;
      set;
    } = string.Empty;
    public string ConfirmPassword
    {
      get;
      set;
    } = string.Empty;

    public string Firstname
    {
      get;
      set;
    } = string.Empty;
    public string Lastname
    {
      get;
      set;
    } = string.Empty;
  }
}
