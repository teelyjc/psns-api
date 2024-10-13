namespace server.Utils.Error
{
  public class SystemException
  {
    private StatusCode StatusCode { get; set; }

    private string Message { get; set; }

    public SystemException(StatusCode StatusCode, string Message)
    {
      this.StatusCode = StatusCode;
      this.Message = Message;
    }
  }
  public enum StatusCode
  {
    ErrUserNotFound = 2000,
    ErrUserConflict = 2001
  }
}
