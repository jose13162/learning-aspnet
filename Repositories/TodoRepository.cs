using asp_net_core.Data;
using asp_net_core.Interfaces;
using asp_net_core.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_net_core.Repositories {
	public class TodoRepository : ITodoRepository {
		private readonly DataContext _context;

		public TodoRepository(DataContext context) {
			this._context = context;
		}

		public IEnumerable<Todo> GetTodos(User user) {
			var todos = this._context.Todos
				.Include((todo) => todo.Owner)
				.Where((todo) => todo.OwnerId == user.Id)
				.ToList();

			return todos;
		}

		public Todo GetTodo(Guid id) {
			return this._context.Todos
				.Where((todo) => todo.Id == id)
				.FirstOrDefault();
		}

		public bool TodoExists(Guid id) {
			return this._context.Todos
				.Any((todo) => todo.Id == id);
		}

		public bool CreateTodo(Guid ownerId, Todo todo) {
			var owner = this._context.Users
				.Where((user) => user.Id == ownerId)
				.FirstOrDefault()!;

			todo.Owner = owner;

			this._context.Todos.Add(todo);

			return this.Save();
		}

		public bool UpdateTodo(Guid ownerId, Todo todo) {
			var owner = this._context.Users
				.Where((user) => user.Id == ownerId)
				.FirstOrDefault()!;

			todo.Owner = owner;

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