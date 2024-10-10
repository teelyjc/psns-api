using Npgsql;

namespace server.Libs
{
  public class PgConnection
  {
    private readonly NpgsqlConnection Npgsql;
    public PgConnection()
    {
      String dsn = String.Format(
        "Host={0}; Port={1}; Database={2}; Username={3}; Password={4}",
        "127.0.0.1",
        "5432",
        "pet_system",
        "root",
        "THIS-IS-PG-PASSWORD@12345"
      );

      NpgsqlConnection npgsql = new NpgsqlConnection(dsn);
      npgsql.Open();

      this.Npgsql = npgsql;
    }

    public NpgsqlConnection GetConnection()
    {
      return this.Npgsql;
    }
  }
}
