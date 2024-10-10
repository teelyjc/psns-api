using Npgsql;
using server.Domains;
using server.Libs;

namespace server.Repositories
{
  public class SessionRepository(NpgsqlConnection connection) : ISessionRepository
  {
    private readonly NpgsqlConnection Npgsql = connection;

    public async Task<Session> GetSessionById(string id)
    {
      try
      {
        await using var cmd = new NpgsqlCommand(
        "SELECT sessions.* FROM sessions WHERE id = ($1)",
        this.Npgsql
      );
        cmd.Parameters.Add(new() { Value = id });
        NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

        Session? session = null;
        while (await reader.ReadAsync())
        {
          session = new()
          {
            Id = reader.GetString(0),
            UserId = reader.GetString(1),
            UserAgent = reader.GetString(2),
            IpAddress = reader.GetString(3),
            CreatedAt = reader.GetDateTime(4),
            ExpiredAt = reader.GetDateTime(5)
          };
        }

        await reader.CloseAsync();
        return session!;
      }
      catch (Exception e)
      {
        throw new Exception("Can't query to get session from PostgreSQL", e);
      }
    }
  }
}
