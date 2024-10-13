using Microsoft.AspNetCore.Diagnostics;

using server.Domains;

namespace server.Utils.Error
{
  public class GlobalExceptionFilter : IExceptionHandler
  {
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
      if (exception is HttpRequestException e)
      {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        if (e.StatusCode.HasValue)
        {
          httpContext.Response.StatusCode = (int)e.StatusCode;
        }

        await httpContext.Response.WriteAsJsonAsync(new Response()
        {
          Success = false,
          Error = new()
          {
            Message = e.Message
          }
        }, cancellationToken: cancellationToken);
        return true;
      }

      return false;
    }
  }
}
