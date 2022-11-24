using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.Models;

namespace asp_net_core.Interfaces {
	public interface IUserRepository {
		IEnumerable<User> GetUsers();
		User GetUser(Guid id);
		bool UserExists(Guid id);
		bool CreateUser(User user);
		bool UpdateUser(User user);
		bool DeleteUser(User user);
		bool Save();
	}
}