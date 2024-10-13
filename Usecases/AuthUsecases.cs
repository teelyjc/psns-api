using System.Net;
using BCrypt.Net;
using server.Domains;

namespace server.Usecases
{
	public class AuthUsecases(IUserUsecases userUsecases, ISessionUsecases sessionUsecases) : IAuthUsecases
	{
		private readonly IUserUsecases userUsecases = userUsecases;
		private readonly ISessionUsecases sessionUsecases = sessionUsecases;

		private static readonly CookieOptions COOKIE_OPTIONS = new()
		{
			HttpOnly = true,
			Secure = true,
			Path = "/"
		};
		public async Task<SignInOutputs> SignIn(SignInInputs inputs)
		{
			User user = await this.userUsecases.GetUserByUsername(inputs.Username);
			if (!BCrypt.Net.BCrypt.Verify(inputs.Password, user.Password))
			{
				throw new HttpRequestException("password not correct", null, HttpStatusCode.Unauthorized);
			}

			string CookieHeader = await this.sessionUsecases.CreateSession(new()
			{
				UserId = user.Id,
				UserAgent = inputs.UserAgent,
				IpAddress = inputs.IpAddress
			});

			return new()
			{
				CookieHeader = CookieHeader,
				CookieOptions = AuthUsecases.COOKIE_OPTIONS
			};
		}

		public async Task<SignOutOutputs> SignOut(SignOutInputs inputs)
		{
			Session session = await this.sessionUsecases.ValidateSession(inputs.CookieHeader);
			await this.sessionUsecases.DestroySessionById(session.Id);

			return new()
			{
				CookieHeader = "",
				CookieOptions = AuthUsecases.COOKIE_OPTIONS
			};
		}
		public async Task<User> Authenticate(AuthenticateInputs inputs)
		{
			Session session = await this.sessionUsecases.ValidateSession(inputs.CookieHeader);
			User user = await this.userUsecases.GetUserBySessionId(session.Id);

			return user;
		}
	}
}
