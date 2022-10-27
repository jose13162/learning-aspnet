using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace asp_net_core.Identity {
	public class Authorize : AuthorizeAttribute {
		public Authorize() : base() {
			base.AuthenticationSchemes = "Bearer";
		}
	}
}