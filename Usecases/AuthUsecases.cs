using server.Domains;

namespace server.Usecases
{
	public class AuthUsecases(IUserUsecases userUsecases, ISessionUsecases sessionUsecases) : IAuthUsecases
	{
		private readonly IUserUsecases userUsecases = userUsecases;
		private readonly ISessionUsecases sessionUsecases = sessionUsecases;
		public void Authenticate() { }

		public void SignIn() { }

		public void SignOut() { }
	}
}
