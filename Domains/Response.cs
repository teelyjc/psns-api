namespace server.Domains
{
  class Response
  {
    public bool success
    {
      get;
      set;
    }

    public object? data
    {
      get;
      set;
    }

    public Error? error
    {
      get;
      set;
    }
  }

  class Error
  {
    public string? message
    {
      get;
      set;
    }

    public int? status
    {
      get;
      set;
    }
  }
}
