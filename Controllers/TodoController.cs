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
	public class TodoController : Controller {
		private readonly IMapper _mapper;
		private readonly ITodoRepository _todoRepository;
		private readonly IUserRepository _userRepository;

		public TodoController(IMapper mapper, ITodoRepository todoRepository, IUserRepository userRepository) {
			this._mapper = mapper;
			this._todoRepository = todoRepository;
			this._userRepository = userRepository;
		}

		[HttpGet("user/{userId}")]
		public IActionResult GetTodosFromUser(Guid userId) {
			if (!this._userRepository.UserExists(userId)) {
				return NotFound();
			}

			var user = this._userRepository.GetUser(userId);
			var todos = this._mapper.Map<ICollection<TodoDto>>(this._todoRepository.GetTodos(user));

			return Ok(todos);
		}

		[HttpGet("{todoId}")]
		public IActionResult GetTodo(Guid todoId) {
			if (!this._todoRepository.TodoExists(todoId)) {
				return NotFound();
			}

			var todo = this._mapper.Map<TodoDto>(this._todoRepository.GetTodo(todoId));

			return Ok(todo);
		}

		[HttpPost]
		public IActionResult CreateTodo(Guid ownerId, [FromBody] TodoDto todo) {
			var mappedTodo = this._mapper.Map<Todo>(todo);

			mappedTodo.Owner = this._userRepository.GetUser(ownerId);

			if (!this._todoRepository.CreateTodo(ownerId, mappedTodo)) {
				return BadRequest();
			}

			return Ok();
		}

		[HttpPut]
		public IActionResult UpdateTodo(Guid ownerId, [FromBody] TodoDto todo) {
			if (!this._todoRepository.TodoExists(todo.Id)) {
				return NotFound();
			}

			var mappedTodo = this._mapper.Map<Todo>(todo);

			if (!this._todoRepository.UpdateTodo(ownerId, mappedTodo)) {
				return BadRequest();
			}

			return Ok();
		}

		[HttpDelete("{todoId}")]
		public IActionResult DeleteTodo(Guid todoId) {
			var todo = this._todoRepository.GetTodo(todoId);

			if (todo == null) {
				return NotFound();
			}

			if (!this._todoRepository.DeleteTodo(todo)) {
				return BadRequest();
			}

			return Ok();
		}
	}
}