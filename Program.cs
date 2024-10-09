using Npgsql.Replication;
using server.Libs;
using server.Repositories;
using server.Usecases;
using server.Utils.Error;

namespace Program
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var app = Program.Create(args);
			Program.Run(app);
		}

		public static WebApplicationBuilder Create(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddScoped(p => new PgConnection());

			builder.Services.AddScoped<UsersRepository>();
			builder.Services.AddScoped<UsersUsecases>();

			builder.Services.AddScoped<PetsRepository>();
			builder.Services.AddScoped<PetsUsecases>();

			builder.Services.AddExceptionHandler<GlobalExceptionFilter>();

			builder.Services.AddControllers();
			builder.Services.AddSwaggerGen();

			return builder;
		}

		public static void Run(WebApplicationBuilder builder)
		{
			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseExceptionHandler(_ => { });

			app.MapControllers();
			app.Run();
		}
	}
}


