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
		Task CreateSession(Session session);
		Task DestroySessionById(string id);
	}
	public interface ISessionUsecases
	{
		Task<Session> GetSessionById(string id);
		Task<string> CreateSession(SessionCreateInputs inputs);
		Task DestroySessionById(string id);
		Task<Session> ValidateSession(string id);
	}

	public class SessionCreateInputs
	{
		public string UserId { get; set; } = string.Empty;
		public string UserAgent { get; set; } = string.Empty;
		public string IpAddress { get; set; } = string.Empty;
	}
}
