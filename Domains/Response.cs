namespace server.Domains
{
  public class Response<T>()
  {
    public bool Success { get; set; }

    public T? Data { get; set; }

    public Error? Error { get; set; }
  }

  public class Response : Response<object> { }

  public class Error
  {
    public string? Message { get; set; }
    public int? Status { get; set; }
  }

  public class Success
  {
    public string? Message { get; set; }
  }
}
