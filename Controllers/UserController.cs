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
		public IActionResult GetUsers() {
			var users = this._userRepository.GetUsers();

			return Ok(users);
		}

		[HttpGet("{id}")]
		public IActionResult GetUser(Guid id) {
			var user = this._userRepository.GetUser(id);

			if (user == null) {
				return NotFound();
			}

			return Ok(user.Todos);
		}

		[HttpPost]
		public IActionResult CreateUser([FromBody] User user) {
			var succeeded = this._userRepository.CreateUser(user);

			if (!succeeded) {
				return BadRequest();
			}

			return Ok(new {
				succeeded
			});
		}

		[HttpPut("{id}")]
		public IActionResult UpdateUser(Guid id, [FromBody] User user) {
			if (id != user.Id) {
				return BadRequest();
			}

			if (!this._userRepository.UserExists(id)) {
				return NotFound();
			}

			var succeeded = this._userRepository.UpdateUser(user);

			if (!succeeded) {
				return BadRequest();
			}

			return Ok(new {
				succeeded
			});
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteUser(Guid id) {
			var user = this._userRepository.GetUser(id);

			if (user == null) {
				return NotFound();
			}

			var succeeded = this._userRepository.DeleteUser(user);

			if (!succeeded) {
				return BadRequest();
			}

			return Ok(new {
				succeeded
			});
		}
	}
}