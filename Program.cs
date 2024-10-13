using server.Domains;
using server.Libs;
using server.Middlewares;
using server.Repositories;
using server.Usecases;
using server.Utils.Error;

namespace Program
{
  public class Program
  {
    private static readonly string COR_POLICY = "CORS_COLICY";
    public static void Main(string[] args)
    {
      Run(Setup(args));
    }

    public static WebApplicationBuilder Setup(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      builder.Services.AddCors(c =>
      {
        c.AddPolicy(
          Program.COR_POLICY,
          p =>
        {
          p.WithOrigins("http://localhost:5555");
          p.AllowAnyMethod();
          p.AllowAnyHeader();
          p.AllowCredentials();
          p.SetIsOriginAllowed(_ => true);
        });
      });

      builder.Services.AddScoped(_ => new PostgreSQL(
        "127.0.0.1",
        "5432",
        "root",
        "THIS-IS-PG-PASSWORD@12345",
        "pet_system"
      ).GetConnection());

      // - Initalization of Repositories
      builder.Services.AddScoped<IUserRepository, UserRepository>();
      builder.Services.AddScoped<ISessionRepository, SessionRepository>();
      builder.Services.AddScoped<IPetRepository, PetRepository>();

      // - Initialization of Usecases
      builder.Services.AddScoped<IUserUsecases, UserUsecases>();
      builder.Services.AddScoped<ISessionUsecases, SessionUsecases>();
      builder.Services.AddScoped<IPetUsecases, PetUsecases>();
      builder.Services.AddScoped<IAuthUsecases, AuthUsecases>();

      builder.Services.AddScoped<AuthMiddleware>();
      builder.Services.AddExceptionHandler<GlobalExceptionFilter>();

      builder.Services.AddControllers();
      builder.Services.AddSwaggerGen();

      return builder;
    }

    public static void Run(WebApplicationBuilder builder)
    {
      var app = builder.Build();
      app.UseCors(Program.COR_POLICY);

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


