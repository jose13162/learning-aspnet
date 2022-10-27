using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace asp_net_core.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class PostController : Controller {
		[HttpGet("anonymous")]
		[ProducesResponseType(200)]
		[AllowAnonymous]
		public IActionResult GetPostsAnonymous() {
			return Ok("anonymous - get posts");
		}

		[HttpGet("authorized")]
		[ProducesResponseType(200)]
		[Identity.Authorize(AuthenticationSchemes = "Bearer")]
		public IActionResult GetPostsAuthorized() {
			return Ok(User.Claims);
		}

		[HttpGet("authorized-user")]
		[ProducesResponseType(200)]
		[Identity.Authorize(Roles = "User")]
		public IActionResult GetPostsUser() {
			return Ok(User.Claims);
		}

		[HttpGet("authorized-admin")]
		[ProducesResponseType(200)]
		[Identity.Authorize(Roles = "Admin")]
		public IActionResult GetPostsAdmin() {
			return Ok(User.Claims);
		}
	}
}