using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_net_core.Dto {
	public class UserDto {
		public Guid Id { get; set; }
		public string Username { get; set; }
	}
}