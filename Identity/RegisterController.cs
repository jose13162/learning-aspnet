using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_core.Identity {
	[Route("api/[controller]")]
	[ApiController]
	public class RegisterController : Controller {
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public RegisterController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager) {
			this._userManager = userManager;
			this._roleManager = roleManager;
		}

		[HttpPost("user")]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> RegisterUser([FromBody] User user) {
			if (user == null) {
				return BadRequest();
			}

			var result = await this._userManager.CreateAsync(user, user.PasswordHash);

			if (!result.Succeeded) {
				return StatusCode(500, result.Errors);
			}

			var roleExists = await this._roleManager.RoleExistsAsync(UserRoles.User);

			if (!roleExists) {
				var identityRole = new IdentityRole() {
					Name = UserRoles.User
				};
				var roleCreationResult = await this._roleManager.CreateAsync(identityRole);

				if (!roleCreationResult.Succeeded) {
					return StatusCode(500, roleCreationResult);
				}
			}

			var addToRoleResult = await this._userManager.AddToRoleAsync(user, UserRoles.User);

			if (!addToRoleResult.Succeeded) {
				return StatusCode(500, addToRoleResult);
			}

			return StatusCode(201);
		}

		[HttpPost("admin")]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> RegisterAdmin([FromBody] User user) {
			if (user == null) {
				return BadRequest();
			}

			var result = await this._userManager.CreateAsync(user, user.PasswordHash);

			if (!result.Succeeded) {
				return StatusCode(500, result.Errors);
			}

			var roleExists = await this._roleManager.RoleExistsAsync(UserRoles.Admin);

			if (!roleExists) {
				var identityRole = new IdentityRole() {
					Name = UserRoles.Admin
				};
				var roleCreationResult = await this._roleManager.CreateAsync(identityRole);

				if (!roleCreationResult.Succeeded) {
					return StatusCode(500, roleCreationResult);
				}
			}

			var addToRoleResult = await this._userManager.AddToRoleAsync(user, UserRoles.Admin);

			if (!addToRoleResult.Succeeded) {
				return StatusCode(500, addToRoleResult);
			}

			return StatusCode(201);
		}
	}
}