using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_core.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : Controller {
		[HttpGet]
		[ProducesResponseType(200)]
		public IActionResult Index() {
			Console.WriteLine("Route accessed");

			return Ok();
		}

		[HttpGet("foo/s")]
		[ProducesResponseType(200)]
		public IActionResult FooIndex() {
			Console.WriteLine("Foo route accessed");

			return Ok();
		}

		[HttpGet("bar")]
		[ProducesResponseType(200)]
		public IActionResult BarIndex() {
			Console.WriteLine("Bar route accessed");

			return Ok();
		}
	}
}