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

		public async Task<int> CountUsers() {
			var sqlCountUsers = "SELECT COUNT(*) FROM Users";
			var count = await this._connection.ExecuteScalarAsync<int>(sqlCountUsers);

			return count;
		}

		public async Task<User> GetUser() {
			var sqlSelectUser = @"
				SELECT * FROM Users
					WHERE Id = 'E2E73C63-639C-44E9-B269-4FDAD5A7B201'
			";
			var user = await this._connection.QuerySingleAsync<User>(sqlSelectUser);

			return user;
		}

		public async Task<User> GetAdmin() {
			var sqlSelectUser = @"
				SELECT * FROM Users
					WHERE Id = 'E2E73C63-639C-44E9-B269-4FDAD5A7B201'
			";
			var user = await this._connection.QuerySingleOrDefaultAsync<User>(sqlSelectUser);

			return user;
		}

		public async Task<User> GetUserByName() {
			var sqlSelectUser = @"
				SELECT * FROM Users
					WHERE Name = 'name'
			";
			var user = await this._connection.QueryFirstAsync<User>(sqlSelectUser);

			return user;
		}
	}
}