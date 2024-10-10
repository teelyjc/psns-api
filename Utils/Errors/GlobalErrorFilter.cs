using Microsoft.AspNetCore.Diagnostics;

using server.Domains;

namespace server.Utils.Error
{
  public class GlobalExceptionFilter : IExceptionHandler
  {
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
      Response response = new()
      {
        Success = false,
        Error = new Domains.Error()
        {
          Message = exception.Message,
        },
      };

      await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
      return true;
    }
  }
}
