using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_net_core.Models {
	public class User {
		public Guid Id { get; set; }
		public string Username { get; set; }
		public ICollection<Todo> Todos = new List<Todo>();
	}
}