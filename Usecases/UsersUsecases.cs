using System.Net;

using server.Domains;
using server.Repositories;

namespace server.Usecases
{
	public class UsersUsecases
	{
		private UsersRepository usersRepository;
		public UsersUsecases(UsersRepository usersRepository)
		{
			this.usersRepository = usersRepository;
		}

		public async Task<User> GetUserByUsername(string username)
		{
			User? user = await this.usersRepository.GetUserByUsername(username);
			return user ?? throw new HttpRequestException("Error user was not found with this username", null, HttpStatusCode.NotFound);
		}

		public async Task<User> GetUserById(string id)
		{
			User? user = await this.usersRepository.GetUserById(id);
			return user ?? throw new HttpRequestException("Error user was not found with this id", null, HttpStatusCode.NotFound);
		}

		public async Task SignUpUser(SignUpUser signUp)
		{
			DateTime timestamp = DateTime.Now;

			User? user = await this.GetUserByUsername(signUp.Username.ToString());
			if (user != null)
			{
				throw new HttpRequestException("username is already in use", null, HttpStatusCode.Conflict);
			}

			await this.usersRepository.CreateUser(
				new()
				{
					Id = Guid.NewGuid().ToString(),
					Username = signUp.Username,
					Password = BCrypt.Net.BCrypt.EnhancedHashPassword(signUp.Password, 10),
					CreatedAt = timestamp,
					UpdatedAt = timestamp,
				}
			);
		}
	}
}
