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

		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(User))]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetUser(Guid id) {
			var user = await this._userRepository.GetUser(id);

			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			if (user == null) {
				return NotFound();
			}

			return Ok(user);
		}

		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> CreateUser([FromBody] User user) {
			var succeeded = await this._userRepository.CreateUser(user);

			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			if (!succeeded) {
				return BadRequest();
			}

			return Ok(new {
				succeeded
			});
		}

		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> UpdateUser(Guid id, [FromBody]User user) {
			var succeeded = await this._userRepository.UpdateUser(id, user);

			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			if (!succeeded) {
				return BadRequest();
			}

			return Ok(new {
				succeeded
			});
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> DeleteUser(Guid id) {
			var succeeded = await this._userRepository.DeleteUser(id);

			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			if (!succeeded) {
				return BadRequest();
			}

			return Ok(new {
				succeeded
			});
		}
	}
}