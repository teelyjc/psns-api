namespace server.Domains
{
  class Response
  {
    public bool Success
    {
      get;
      set;
    }

    public object? Data
    {
      get;
      set;
    }

    public Error? Error
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
