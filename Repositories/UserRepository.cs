using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.Interfaces;
using asp_net_core.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using asp_net_core.Data;

namespace asp_net_core.Repositories {
	public class UserRepository : IUserRepository {
		private readonly DapperContext _context;
		private readonly SqlConnection _connection;

		public UserRepository(DapperContext context) {
			this._context = context;
			this._connection = this._context.CreateConnection();
		}

		public async Task<IEnumerable<User>> GetUsers() {
			var sqlSelectUsers = @"
				SELECT * FROM Users;
			";
			var users = await this._connection.QueryAsync<User>(sqlSelectUsers);

			return users;
		}

		public async Task<User> GetUser(Guid id) {
			var sqlSelectUsers = @"
				SELECT * FROM Users
					WHERE Id = @Id;
			";
			var user = await this._connection.QuerySingleAsync<User>(sqlSelectUsers, new {
				Id = id
			});

			return user;
		}

		public async Task<bool> CreateUser(User user) {
			var sqlCreateUser = @"
				INSERT INTO Users (Username) VALUES (@Username);
			";

			var affectedRows = await this._connection.ExecuteAsync(sqlCreateUser, user);

			return affectedRows > 0;
		}

		public async Task<bool> UpdateUser(Guid id, User user) {
			var sqlUpdateUser = @"
				UPDATE Users 
					SET Username = @Username
					WHERE Id = @Id;
			";

			var affectedRows = await this._connection.ExecuteAsync(sqlUpdateUser, new {
				Id = id,
				Username = user.Username
			});

			return affectedRows > 0;
		}

		public async Task<bool> DeleteUser(Guid id) {
			var sqlDeleteUser = @"
				DELETE FROM Users
					WHERE Id = @Id;
			";

			var affectedRows = await this._connection.ExecuteAsync(sqlDeleteUser, new { Id = id });

			return affectedRows > 0;
		}
	}
}