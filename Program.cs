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
      PgConnection connection = new();
      UsersRepository usersRepository = new(connection);

      WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

      builder.Services.AddScoped(p => new PgConnection());

      builder.Services.AddScoped<UsersRepository>();
      builder.Services.AddScoped<PetsRepository>();

      builder.Services.AddScoped<UsersUsecases>();
      builder.Services.AddScoped<PetsUsecases>();

      builder.Services.AddExceptionHandler<GlobalExceptionFilter>();

      builder.Services.AddControllers();
      builder.Services.AddSwaggerGen();

      WebApplication app = builder.Build();

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
