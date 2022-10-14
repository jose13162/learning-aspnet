using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.Filters;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_core.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[AsyncLoggingFilter(Order = -1)]
	public class AuthController : Controller {
		[LoggingFilter(Order = -1)]
		[ResourceFilter]
		[HttpGet]
		[ProducesResponseType(200)]
		public IActionResult Index() {
			Console.WriteLine("Route accessed");

			return Ok();
		}
	}
}