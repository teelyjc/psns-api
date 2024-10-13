using Npgsql;
using server.Domains;

namespace server.Libs
{
  public class PostgreSQL
  {
    private readonly NpgsqlConnection Npgsql;
    public PostgreSQL(string host, string port, string user, string password, string db)
    {
      String dsn = String.Format(
        "Host={0}; Port={1}; Database={2}; Username={3}; Password={4}; Pooling=false",
        host,
        port,
        db,
        user,
        password
      );

      NpgsqlDataSourceBuilder npgsqlDataSourceBuilder = new(dsn);
      npgsqlDataSourceBuilder.MapEnum<Position>("user_position");

      NpgsqlDataSource npgsqlDataSource = npgsqlDataSourceBuilder.Build();

      this.Npgsql = npgsqlDataSource.OpenConnection();
    }

    public NpgsqlConnection GetConnection()
    {
      return this.Npgsql;
    }
  }
}
