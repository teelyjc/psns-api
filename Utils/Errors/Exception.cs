namespace server.Utils.Error
{
  public class SystemException
  {
    private StatusCode statusCode
    {
      get;
      set;
    }

    private string message
    {
      get;
      set;
    }

    public SystemException(StatusCode statusCode, string message)
    {
      this.statusCode = statusCode;
      this.message = message;
    }
  }
  public enum StatusCode
  {
    ErrUserNotFound = 2000,
    ErrUserConflict = 2001
  }
}
