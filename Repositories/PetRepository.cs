using Npgsql;
using server.Domains;
using server.Libs;

namespace server.Repositories
{
  public class PetRepository(NpgsqlConnection connection) : IPetRepository
  {
    private readonly NpgsqlConnection Npgsql = connection;
    public async Task<Pet> GetPetById(string id)
    {
      try
      {
        await using var cmd = new NpgsqlCommand("SELECT pets.* FROM pets WHERE id = ($1)", this.Npgsql);
        cmd.Parameters.Add(new() { Value = id });

        NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        Pet? pet = null;

        while (await reader.ReadAsync())
        {
          pet = new()
          {
            Id = reader.GetString(0),
            UserId = reader.GetString(1),
            Name = reader.GetString(2),
            Type = reader.GetString(3),
            Gene = reader.GetString(4),
            CreatedAt = reader.GetDateTime(5),
            UpdatedAt = reader.GetDateTime(6)
          };
        }

        return pet!;
      }
      catch (Exception e)
      {
        throw new Exception("Can't query to get pet from PostgreSQL", e);
      }
    }

    public async Task<List<Pet>> GetPetsByUserId(string userId)
    {
      try
      {
        await using var cmd = new NpgsqlCommand(
          "SELECT pets.* FROM pets INNER JOIN users ON pets.user_id = users.id WHERE users.id = ($1)",
          this.Npgsql
        );
        cmd.Parameters.Add(new() { Value = userId });
        List<Pet> pets = [];

        NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
          pets.Add(new()
          {
            Id = reader.IsDBNull(0) ? "" : reader.GetString(0),
            UserId = reader.IsDBNull(1) ? "" : reader.GetString(1),
            Name = reader.IsDBNull(2) ? "" : reader.GetString(2),
            Type = reader.IsDBNull(3) ? "" : reader.GetString(3),
            Gene = reader.IsDBNull(4) ? "" : reader.GetString(4),
            CreatedAt = reader.IsDBNull(5) ? default : reader.GetDateTime(5),
            UpdatedAt = reader.IsDBNull(6) ? default : reader.GetDateTime(6)
          });
        }

        return pets;
      }
      catch (Exception e)
      {
        throw new Exception("Can't query to get pets from PostgreSQL", e);
      }
    }

    public async Task CreatePetToUser(Pet pet)
    {
      try
      {
        await using var cmd = new NpgsqlCommand(
          "INSERT INTO pets (id, user_id, name, type, gene, created_at, updated_at) VALUES (($1), ($2), ($3), ($4), ($5), ($6), ($7))",
          this.Npgsql
        );
        cmd.Parameters.Add(new() { Value = pet.Id });
        cmd.Parameters.Add(new() { Value = pet.UserId });
        cmd.Parameters.Add(new() { Value = pet.Name });
        cmd.Parameters.Add(new() { Value = pet.Type });
        cmd.Parameters.Add(new() { Value = pet.Gene });
        cmd.Parameters.Add(new() { Value = pet.CreatedAt });
        cmd.Parameters.Add(new() { Value = pet.UpdatedAt });

        await cmd.ExecuteNonQueryAsync();
      }
      catch (Exception e)
      {
        throw new Exception("Can't execute to create pet to user", e);
      }
    }
  }
}
