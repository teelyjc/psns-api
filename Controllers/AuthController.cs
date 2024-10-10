using Microsoft.AspNetCore.Mvc;

using server.Usecases;

namespace server.Controllers
{
	[ApiController]
	public class AuthController(AuthUsecases authUsecases) : Controller
	{
		private readonly AuthUsecases authUsecases = authUsecases;

		[HttpPost]
		[Route("signin")]
		public void SignIn() { }

		[HttpDelete]
		[Route("signout")]
		public void SignOut() { }

		[HttpGet]
		[Route("me")]
		public void Authenticate() { }
	}
}
