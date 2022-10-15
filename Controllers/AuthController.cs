using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.Helper;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_core.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : Controller {
		private IConfiguration _configuration;

		public AuthController(IConfiguration configuration) {
			this._configuration = configuration;
		}

		[HttpGet]
		[ProducesResponseType(200)]
		public IActionResult Index() {
			var authData = this._configuration.GetSection("Auth").Get<AuthenticationData>();

			return Ok(authData);
		}
	}
}