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

		public async Task CreateSession(Session session)
		{
			try
			{
				await using var cmd = new NpgsqlCommand(
					"INSERT INTO sessions (id, user_id, user_agent, ip_address, created_at, expired_at) VALUES (($1), ($2), ($3), ($4), ($5), ($6))",
					this.Npgsql
				);
				cmd.Parameters.Add(new() { Value = session.Id });
				cmd.Parameters.Add(new() { Value = session.UserId });
				cmd.Parameters.Add(new() { Value = session.UserAgent });
				cmd.Parameters.Add(new() { Value = session.IpAddress });
				cmd.Parameters.Add(new() { Value = session.CreatedAt });
				cmd.Parameters.Add(new() { Value = session.ExpiredAt });

				await cmd.ExecuteNonQueryAsync();
			}
			catch (Exception e)
			{
				throw new Exception("Can't execute to create session to PostgreSQL", e);
			}
		}

		public async Task DestroySessionById(string id)
		{
			try
			{
				await using var cmd = new NpgsqlCommand(
					"DELETE FROM sessions WHERE id = ($1)",
					this.Npgsql
				);
				cmd.Parameters.Add(new() { Value = id });
				await cmd.ExecuteNonQueryAsync();
			}
			catch (Exception e)
			{
				throw new Exception("Can't execute to destroy session from PostgreSQL", e);
			}
		}
	}
}
