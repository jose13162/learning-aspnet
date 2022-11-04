using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.Models;

namespace asp_net_core.Interfaces {
	public interface IUserRepository {
		Task<IEnumerable<User>> GetUsers();
		Task<User> GetUser(Guid id);
		Task<bool> CreateUser(User user);
		Task<bool> UpdateUser(Guid id, User user);
		Task<bool> DeleteUser(Guid id);
	}
}