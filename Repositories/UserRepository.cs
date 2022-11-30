using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.Interfaces;
using asp_net_core.Models;
using Microsoft.Data.SqlClient;
using asp_net_core.Data;
using Microsoft.EntityFrameworkCore;

namespace asp_net_core.Repositories {
	public class UserRepository : IUserRepository {
		private readonly DataContext _context;
		private readonly SqlConnection _connection;

		public UserRepository(DataContext context) {
			this._context = context;
		}

		public IEnumerable<User> GetUsers() {
			return this._context.Users.ToList();
		}

		public User GetUser(Guid id) {
			return this._context.Users
				.Include((user) => user.Todos)
				.Where((user) => user.Id == id)
				.FirstOrDefault();
		}

		public bool UserExists(Guid id) {
			return this._context.Users.Any((user) => user.Id == id);
		}

		public bool CreateUser(User user) {
			this._context.Users.Add(user);

			return this.Save();
		}

		public bool UpdateUser(User user) {
			this._context.Users.Update(user);

			return this.Save();
		}

		public bool DeleteUser(User user) {
			this._context.Users.Remove(user);

			return this.Save();
		}

		public bool Save() {
			var saved = this._context.SaveChanges();

			return saved > 0;
		}
	}
}