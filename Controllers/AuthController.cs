using System.Net;
using Microsoft.AspNetCore.Mvc;

using server.Domains;
using server.Middlewares;

namespace server.Controllers
{
	[ApiController]
	[Route("auth")]
	public class AuthController(IAuthUsecases authUsecases) : Controller
	{
		private readonly IAuthUsecases authUsecases = authUsecases;

		[HttpPost("/signin")]
		public async Task<ActionResult<Response>> SignIn(SignInCredentials credentials)
		{
			IPAddress? ip = this.HttpContext.Connection.RemoteIpAddress;
			string userAgent = this.Request.Headers.UserAgent.ToString();

			var header = await this.authUsecases.SignIn(new()
			{
				IpAddress = ip?.ToString() ?? "",
				UserAgent = userAgent,
				Username = credentials.Username,
				Password = credentials.Password
			});

			this.Response.Cookies.Append("sid", header.CookieHeader, header.CookieOptions);

			return new Response()
			{
				Success = true,
				Data = new Success() { Message = "signed in successfully" }
			};
		}

		[HttpDelete("/signout")]
		[ServiceFilter(typeof(AuthMiddleware))]
		public async Task<ActionResult<Response>> SignOut()
		{
			var header = await this.authUsecases.SignOut(new()
			{
				CookieHeader = this.HttpContext.Request.Cookies["sid"]!
			});

			this.Response.Cookies.Append("sid", header.CookieHeader, header.CookieOptions);

			return new Response()
			{
				Success = true,
				Data = new Success() { Message = "signed out successfully" }
			};
		}

		[HttpGet("me")]
		[ServiceFilter(typeof(AuthMiddleware))]
		public ActionResult<Response> Me()
		{
			User user = Domains.User.GetUserFromContext(this.HttpContext);
			return new Response()
			{
				Success = true,
				Data = user
			};
		}
	}
}
