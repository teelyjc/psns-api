using Npgsql;

using server.Domains;

namespace server.Repositories
{
  public class UserRepository(NpgsqlConnection connection) : IUserRepository
  {
    private readonly NpgsqlConnection Npgsql = connection;

    public async Task<User> GetUserById(string id)
    {
      try
      {
        await using var cmd = new NpgsqlCommand(
          "SELECT users.* FROM users WHERE id = ($1)",
          this.Npgsql
        );
        cmd.Parameters.Add(new() { Value = id });

        NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        User? user = null;

        while (await reader.ReadAsync())
        {
          user = new User()
          {
            Id = reader.IsDBNull(0) ? "" : reader.GetString(0),
            Username = reader.IsDBNull(1) ? "" : reader.GetString(1),
            Password = reader.IsDBNull(2) ? "" : reader.GetString(2),
            Firstname = reader.IsDBNull(3) ? "" : reader.GetString(3),
            Lastname = reader.IsDBNull(4) ? "" : reader.GetString(4),
            Position = reader.IsDBNull(5) ? Position.CUSTOMER : reader.GetFieldValue<Position>(5),
            CreatedAt = reader.IsDBNull(6) ? default : reader.GetDateTime(6),
            UpdatedAt = reader.IsDBNull(7) ? default : reader.GetDateTime(7),
          };
        }

        await reader.CloseAsync();
        return user!;
      }
      catch (Exception e)
      {
        throw new Exception("Can't query to get users from PostgreSQL", e);
      }
    }

    public async Task<User> GetUserByUsername(string username)
    {
      try
      {
        await using var cmd = new NpgsqlCommand(
          "SELECT users.* FROM users WHERE username = ($1)",
          this.Npgsql
        );
        cmd.Parameters.Add(new() { Value = username });

        NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        User? user = null;

        while (await reader.ReadAsync())
        {
          user = new User()
          {
            Id = reader.IsDBNull(0) ? "" : reader.GetString(0),
            Username = reader.IsDBNull(1) ? "" : reader.GetString(1),
            Password = reader.IsDBNull(2) ? "" : reader.GetString(2),
            Firstname = reader.IsDBNull(3) ? "" : reader.GetString(3),
            Lastname = reader.IsDBNull(4) ? "" : reader.GetString(4),
            Position = reader.IsDBNull(5) ? Position.CUSTOMER : reader.GetFieldValue<Position>(5),
            CreatedAt = reader.IsDBNull(5) ? default : reader.GetDateTime(6),
            UpdatedAt = reader.IsDBNull(6) ? default : reader.GetDateTime(7),
          };
        }

        await reader.CloseAsync();
        return user!;
      }
      catch (Exception e)
      {
        throw new Exception("Can't query to get users from PostgreSQL", e);
      }
    }

    public async Task CreateUser(User user)
    {
      try
      {
        await using var cmd = new NpgsqlCommand(
          "INSERT INTO users (id, username, password, position, created_at, updated_at) VALUES (($1), ($2), ($3), ($4), ($5), ($6))",
          this.Npgsql
        );

        cmd.Parameters.Add(new() { Value = user.Id });
        cmd.Parameters.Add(new() { Value = user.Username });
        cmd.Parameters.Add(new() { Value = user.Password });
        cmd.Parameters.Add(new() { Value = Position.CUSTOMER });
        cmd.Parameters.Add(new() { Value = user.CreatedAt });
        cmd.Parameters.Add(new() { Value = user.UpdatedAt });

        await cmd.ExecuteNonQueryAsync();
      }
      catch (Exception e)
      {
        throw new Exception("Can't execute to create users to PostgreSQL", e);
      }
    }

    public async Task UpdateUserById(string userId, UserUpdateInputs inputs)
    {
      try
      {
        await using var cmd = new NpgsqlCommand(
          "UPDATE users SET firstname = ($1), lastname = ($2), updated_at = ($3) WHERE id = ($4)",
          this.Npgsql
        );

        cmd.Parameters.Add(new() { Value = inputs.Firstname });
        cmd.Parameters.Add(new() { Value = inputs.Lastname });
        cmd.Parameters.Add(new() { Value = DateTime.Now });
        cmd.Parameters.Add(new() { Value = userId });

        await cmd.ExecuteNonQueryAsync();
      }
      catch (Exception e)
      {
        throw new Exception("Can't execute to update users to PostgreSQL", e);
      }
    }

    public async Task<User> GetUserBySessionId(string id)
    {
      try
      {
        await using var cmd = new NpgsqlCommand(
          "SELECT users.* FROM users INNER JOIN sessions ON users.id = sessions.user_id WHERE sessions.id = ($1)",
          this.Npgsql
        );
        cmd.Parameters.Add(new() { Value = id });

        User? user = null;
        var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
          user = new User()
          {
            Id = reader.IsDBNull(0) ? "" : reader.GetString(0),
            Username = reader.IsDBNull(1) ? "" : reader.GetString(1),
            Password = reader.IsDBNull(2) ? "" : reader.GetString(2),
            Firstname = reader.IsDBNull(3) ? "" : reader.GetString(3),
            Lastname = reader.IsDBNull(4) ? "" : reader.GetString(4),
            Position = reader.IsDBNull(5) ? Position.CUSTOMER : reader.GetFieldValue<Position>(5),
            CreatedAt = reader.IsDBNull(6) ? default : reader.GetDateTime(6),
            UpdatedAt = reader.IsDBNull(7) ? default : reader.GetDateTime(7),
          };
        }

        await reader.CloseAsync();
        return user!;
      }
      catch (Exception e)
      {
        throw new Exception("Can't query to get users from PostgreSQL", e);
      }
    }
    public async Task<List<User>> GetUsers(int? offset = 0, int? limit = 50)
    {
      try
      {
        await using var cmd = new NpgsqlCommand("SELECT users.* FROM users LIMIT ($1) OFFSET ($2)", this.Npgsql);
        cmd.Parameters.Add(new() { Value = limit });
        cmd.Parameters.Add(new() { Value = offset });

        List<User> users = [];
        NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
          users.Add(new()
          {
            Id = reader.IsDBNull(0) ? "" : reader.GetString(0),
            Username = reader.IsDBNull(1) ? "" : reader.GetString(1),
            Password = reader.IsDBNull(2) ? "" : reader.GetString(2),
            Firstname = reader.IsDBNull(3) ? "" : reader.GetString(3),
            Lastname = reader.IsDBNull(4) ? "" : reader.GetString(4),
            Position = reader.IsDBNull(5) ? Position.CUSTOMER : reader.GetFieldValue<Position>(5),
            CreatedAt = reader.IsDBNull(6) ? default : reader.GetDateTime(6),
            UpdatedAt = reader.IsDBNull(7) ? default : reader.GetDateTime(7),
          });
        }

        return users;
      }
      catch (Exception e)
      {
        throw new Exception("Can't query to get users from PostgreSQL", e);
      }
    }
  }
}


