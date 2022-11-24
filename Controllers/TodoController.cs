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

		public TodoController(ITodoRepository todoRepository) {
			this._todoRepository = todoRepository;
		}

		[HttpPost]
		public IActionResult CreateTodo([FromBody] Todo todo) {
			var succeeded = this._todoRepository.CreateTodo(todo);

			return Ok(new {
				succeeded
			});
		}
	}
}