using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.Interfaces;
using asp_net_core.Models;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_core.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : Controller {
		private readonly IUserRepository _userRepository;

		public UserController(IUserRepository userRepository) {
			this._userRepository = userRepository;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetUsers() {
			var users = await this._userRepository.GetUsers();

			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			return Ok(users);
		}
	}
}