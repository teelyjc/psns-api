using Npgsql;
using server.Libs;

namespace server.Repositories
{
  public class PetsRepository
  {
    private readonly NpgsqlConnection npgsql;
    public PetsRepository(PgConnection pg)
    {
      this.npgsql = pg.GetConnection();
    }

  }
}
