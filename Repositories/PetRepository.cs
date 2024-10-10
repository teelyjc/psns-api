using Npgsql;
using server.Domains;
using server.Libs;

namespace server.Repositories
{
  public class PetRepository(NpgsqlConnection connection) : IPetRepository
  {
    private readonly NpgsqlConnection Npgsql = connection;
  }
}
