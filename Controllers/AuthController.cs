using Microsoft.AspNetCore.Mvc;

using server.Usecases;

namespace server.Controllers
{
	[ApiController]
	public class AuthController(AuthUsecases authUsecases) : Controller
	{
		private readonly AuthUsecases authUsecases = authUsecases;
	}
}
