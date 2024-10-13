
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using server.Domains;

namespace server.Middlewares
{
  public class AuthMiddleware(IAuthUsecases authUsecases) : IAsyncActionFilter
  {
    private readonly IAuthUsecases authUsecases = authUsecases;
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      string? sid = context.HttpContext.Request.Cookies["sid"];
      if (sid == null || sid == "")
      {
        throw new HttpRequestException("session not found", null, HttpStatusCode.Unauthorized);
      }

      User user = await authUsecases.Authenticate(new() { CookieHeader = sid });
      User.SetUserToContext(context.HttpContext, user);

      await next();
    }
  }
}
