using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_net_core.Models {
	public class Todo {
		public Guid Id { get; set; }
		public string Title { get; set; }
		public bool Done { get; set; }
	}
}