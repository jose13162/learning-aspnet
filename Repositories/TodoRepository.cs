using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.Data;
using asp_net_core.Interfaces;
using asp_net_core.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace asp_net_core.Repositories {
	public class TodoRepository : ITodoRepository {
		private readonly DapperContext _context;
		private readonly SqlConnection _connection;

		public TodoRepository(DapperContext context) {
			this._context = context;
			this._connection = this._context.CreateConnection();
		}
	}
}