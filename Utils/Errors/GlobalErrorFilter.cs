using Microsoft.AspNetCore.Diagnostics;
using Npgsql.Replication;
using server.Domains;

namespace server.Utils.Error
{
	public static class ExceptionExtension
	{
		public static bool IsHttpRequestException(this Exception exception)
		{
			return (exception.GetType().FullName == "System.Net.Http.Http.HttpRequestException");
		}
	}

	public class GlobalExceptionFilter : IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			if (exception.IsHttpRequestException())
			{
				Response response = new()
				{
					success = false,
					error = new Domains.Error()
					{
						message = exception.Message,
					},
				};

				await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
			}

			return true;
		}
	}
}
