namespace server.Domains
{
	public interface IAuthUsecases
	{
		void SignIn();
		void SignOut();
		void Authenticate();
	}
}
