using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.Data;
using asp_net_core.Interfaces;
using asp_net_core.Models;
using Microsoft.Data.SqlClient;

namespace asp_net_core.Repositories {
	public class TodoRepository : ITodoRepository {
		private readonly DataContext _context;
		private readonly SqlConnection _connection;

		public TodoRepository(DataContext context) {
			this._context = context;
		}

		public IEnumerable<Todo> GetTodos(User user) {
			var todos = this._context
				.Entry(user)
				.Collection((user) => user.Todos)
				.Query();
 
			return todos;
		}

		public Todo GetTodo(Guid id) {
			var todo = this._context.Todos
				.Where((todo) => todo.Id == id)
				.FirstOrDefault()!;

			this._context.Entry(todo).Reference((todo) => todo.Owner).Load();

			return todo;
		}

		public bool TodoExists(Guid id) {
			return this._context.Todos
				.Any((todo) => todo.Id == id);
		}

		public bool CreateTodo(Todo todo) {
			this._context.Todos.Add(todo);

			return this.Save();
		}

		public bool UpdateTodo(Todo todo) {
			this._context.Todos.Update(todo);

			return this.Save();
		}

		public bool DeleteTodo(Todo todo) {
			this._context.Todos.Remove(todo);

			return this.Save();
		}

		public bool Save() {
			var saved = this._context.SaveChanges();

			return saved > 0;
		}
	}
}