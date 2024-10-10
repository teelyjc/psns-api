namespace server.Domains
{
  public class Session
  {
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiredAt { get; set; }
  }

  public interface ISessionRepository
  {
    Task<Session> GetSessionById(string id);
  }
  public interface ISessionUsecases
  {
    Task<Session> GetSessionById(string id);
  }
}
