using Npgsql;
using server.Domains;
using server.Libs;

namespace server.Repositories
{
	public class UsersRepository
	{
		private readonly NpgsqlConnection pgsql;
		public UsersRepository(PgConnection pg)
		{
			this.pgsql = pg.GetConnection();
		}

		public async Task<User> GetUserById(string id)
		{
			try
			{
				await using var cmd = new NpgsqlCommand(
					"SELECT users.* FROM users WHERE id = ($1)",
					this.pgsql
				);
				cmd.Parameters.Add(new() { Value = id });

				NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
				User? user = null;

				while (await reader.ReadAsync())
				{
					user = new User()
					{
						Id = reader.GetString(0),
						Username = reader.GetString(1),
						Password = reader.GetString(2),
						Firstname = reader.GetString(3),
						Lastname = reader.GetString(4),
						CreatedAt = reader.GetDateTime(5),
						UpdatedAt = reader.GetDateTime(6),
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
					this.pgsql
				);
				cmd.Parameters.Add(new() { Value = username });

				NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
				User? user = null;

				while (await reader.ReadAsync())
				{
					user = new User()
					{
						Id = reader.GetString(0),
						Username = reader.GetString(1),
						Password = reader.GetString(2),
						Firstname = reader.GetString(3),
						Lastname = reader.GetString(4),
						CreatedAt = reader.GetDateTime(5),
						UpdatedAt = reader.GetDateTime(6),
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
					"INSERT INTO users (id, username, password, created_at, updated_at) VALUES (($1), ($2), ($3), ($4), ($5))",
					this.pgsql
				);

				cmd.Parameters.Add(new() { Value = user.Id });
				cmd.Parameters.Add(new() { Value = user.Username });
				cmd.Parameters.Add(new() { Value = user.Password });
				cmd.Parameters.Add(new() { Value = user.CreatedAt });
				cmd.Parameters.Add(new() { Value = user.UpdatedAt });

				await cmd.ExecuteNonQueryAsync();
			}
			catch (Exception e)
			{
				throw new Exception("Can't execute to create users to PostgreSQL", e);
			}
		}
	}
}


