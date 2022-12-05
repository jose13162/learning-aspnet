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
	public class TodoController : Controller {
		private readonly ITodoRepository _todoRepository;
		private readonly IUserRepository _userRepository;

		public TodoController(ITodoRepository todoRepository, IUserRepository userRepository) {
			this._todoRepository = todoRepository;
			this._userRepository = userRepository;
		}

		[HttpGet("user/{userId}")]
		public IActionResult GetTodos(Guid userId) {
			if (!this._userRepository.UserExists(userId)) {
				return NotFound();
			}

			var user = this._userRepository.GetUser(userId);
			var todos = this._todoRepository.GetTodos(user);

			return Ok(todos);
		}

		[HttpGet("{todoId}")]
		public IActionResult GetTodo(Guid todoId) {
			if (!this._todoRepository.TodoExists(todoId)) {
				return NotFound();
			}

			var todo = this._todoRepository.GetTodo(todoId);

			return Ok(todo);
		}

		[HttpPost]
		public IActionResult CreateTodo([FromBody] Todo todo) {
			if (todo == null) {
				return BadRequest();
			}

			var succeeded = this._todoRepository.CreateTodo(todo);

			if (!this._todoRepository.CreateTodo(todo)) {
				return BadRequest();
			}

			return Ok();
		}
	}
}