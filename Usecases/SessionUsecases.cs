using System.Net;

using server.Domains;

namespace server.Usecases
{
	public class SessionUsecases(ISessionRepository sessionRepository) : ISessionUsecases
	{
		private readonly ISessionRepository SessionRepository = sessionRepository;

		public async Task<Session> GetSessionById(string id)
		{
			Session? session = await this.SessionRepository.GetSessionById(id);
			return session ?? throw new HttpRequestException("session was not found with this id", null, HttpStatusCode.NotFound);
		}
	}
}
