using System.Data;
using asp_net_core.Interfaces;
using asp_net_core.Models;
using asp_net_core.Data;
using Microsoft.EntityFrameworkCore;

namespace asp_net_core.Repositories {
	public class UserRepository : IUserRepository {
		private readonly DataContext _context;

		public UserRepository(DataContext context) {
			this._context = context;
		}

		public IEnumerable<User> GetUsers() {
			return this._context.Users.ToList();
		}

		public User GetUser(Guid id) {
			return this._context.Users
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