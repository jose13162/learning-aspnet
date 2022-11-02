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

		public async Task<bool> CreateUser() {
			var sqlCreateUser = @"
				INSERT INTO Users (Username) values ('username_create');
			";

			var affectedRows = await this._connection.ExecuteAsync(sqlCreateUser);

			return affectedRows > 0;
		}

		public async Task<bool> UpdateUser() {
			var sqlUpdateUser = @"
				UPDATE Users 
					SET Username = 'username_update'
					WHERE Username = 'username_create';
			";

			var affectedRows = await this._connection.ExecuteAsync(sqlUpdateUser);

			return affectedRows > 0;
		}

		public async Task<bool> DeleteUser() {
			var sqlDeleteUser = @"
				DELETE FROM Users;
			";

			var affectedRows = await this._connection.ExecuteAsync(sqlDeleteUser);

			return affectedRows > 0;
		}
	}
}