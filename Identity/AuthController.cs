using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using asp_net_core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace asp_net_core.Identity {
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : Controller {
		private readonly IConfiguration _configuration;
		private readonly UserManager<User> _userManager;

		public AuthController(IConfiguration configuration, UserManager<User> userManager) {
			this._configuration = configuration;
			this._userManager = userManager;
		}

		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Authenticate([FromBody] LoginProperties userCredentials) {
			if (userCredentials == null) {
				return BadRequest();
			}

			var user = await this._userManager.FindByNameAsync(userCredentials.Username);

			if (user == null) {
				return BadRequest("Either the username or the password are invalid");
			}

			var isCorrectPassword = await this._userManager.CheckPasswordAsync(user, userCredentials.Password);

			if (!isCorrectPassword) {
				return BadRequest("Either the username or the password are invalid");
			}

			var roles = await this._userManager.GetRolesAsync(user);
			var role = roles.FirstOrDefault()!;

			var claims = new ClaimsIdentity(
				new Claim[] {
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim(ClaimTypes.Role, role)
				}
			);
			var expires = DateTime.Now.AddDays(30);

			var key = this._configuration.GetValue<string>("Jwt:Secret");
			var keyToBytes = Encoding.UTF8.GetBytes(key);
			var tokenDescriptor = new SecurityTokenDescriptor() {
				Subject = claims,
				Expires = expires,
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(keyToBytes),
					SecurityAlgorithms.HmacSha512Signature
				),
				Issuer = this._configuration.GetValue<string>("Jwt:Issuer"),
				Audience = this._configuration.GetValue<string>("Jwt:Audience")
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			return Ok(new {
				Token = tokenString
			});
		}
	}
}