using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.Dto;
using asp_net_core.Interfaces;
using asp_net_core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_core.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : Controller {
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;

		public UserController(IMapper mapper, IUserRepository userRepository) {
			this._mapper = mapper;
			this._userRepository = userRepository;
		}

		[HttpGet]
		public IActionResult GetUsers() {
			var users = this._mapper.Map<ICollection<UserDto>>(this._userRepository.GetUsers());

			return Ok(users);
		}

		[HttpGet("{userId}")]
		public IActionResult GetUser(Guid userId) {
			if (!this._userRepository.UserExists(userId)) {
				return NotFound();
			}

			var user = this._mapper.Map<UserDto>(this._userRepository.GetUser(userId));

			return Ok(user);
		}

		[HttpPost]
		public IActionResult CreateUser([FromBody] UserDto user) {
			var mappedUser = this._mapper.Map<User>(user);

			if (!this._userRepository.CreateUser(mappedUser)) {
				return BadRequest();
			}

			return Ok();
		}

		[HttpPut]
		public IActionResult UpdateUser([FromBody] UserDto user) {
			if (!this._userRepository.UserExists(user.Id)) {
				return NotFound();
			}

			var mappedUser = this._mapper.Map<User>(user);

			if (!this._userRepository.UpdateUser(mappedUser)) {
				return BadRequest();
			}

			return Ok();
		}

		[HttpDelete("{userId}")]
		public IActionResult DeleteUser(Guid userId) {
			var user = this._userRepository.GetUser(userId);

			if (user == null) {
				return NotFound();
			}

			if (!this._userRepository.DeleteUser(user)) {
				return BadRequest();
			}

			return Ok();
		}
	}
}