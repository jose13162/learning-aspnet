using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.Models;

namespace asp_net_core.Interfaces {
	public interface ITodoRepository {
		IEnumerable<Todo> GetTodos(User user);
		Todo GetTodo(Guid id);
		bool TodoExists(Guid id);
		bool CreateTodo(Todo todo);
		bool UpdateTodo(Todo todo);
		bool DeleteTodo(Todo todo);
		bool Save();
	}
}