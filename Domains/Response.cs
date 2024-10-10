namespace server.Domains
{
  class Response
  {
    public bool Success { get; set; }

    public object? Data { get; set; }

    public Error? Error { get; set; }
  }

  class Error
  {
    public string? Message { get; set; }
    public int? Status { get; set; }
  }
}
