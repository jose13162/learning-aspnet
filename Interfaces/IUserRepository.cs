using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.Models;

namespace asp_net_core.Interfaces {
	public interface IUserRepository {
		Task<IEnumerable<User>> GetUsers();
	}
}