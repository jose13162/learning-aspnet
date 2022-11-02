using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;

namespace asp_net_core.Data {
	public class DapperContext {
		private readonly IConfiguration _configuration;
		private readonly string _connectionString;

		public DapperContext(IConfiguration configuration) {
			this._configuration = configuration;
			this._connectionString = this._configuration.GetConnectionString("DefaultConnection");
		}

		public SqlConnection CreateConnection() {
			return new SqlConnection(this._connectionString);
		}

		public async Task CreateTables() {
			var connection = this.CreateConnection();
			var sqlQuery = @"
				IF OBJECT_ID('Users') IS NULL
					BEGIN
						CREATE TABLE Users (
							Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
							Username VARCHAR(32)
						)
					END;
				IF OBJECT_ID('Todos') IS NULL
					BEGIN
						CREATE TABLE Todos (
							Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
							UserId UNIQUEIDENTIFIER NOT NULL,
							Title VARCHAR(32),
							Done BIT DEFAULT 0,
							CONSTRAINT todo_user_fk FOREIGN KEY(UserId)
								REFERENCES Users(Id)
								ON UPDATE CASCADE
								ON DELETE CASCADE
						)
					END;
			";

			await connection.ExecuteAsync(sqlQuery);
		}

		public async Task RecreateTables() {
			var connection = this.CreateConnection();
			var sqlQuery = @"
				IF OBJECT_ID('Todos') IS NOT NULL
					BEGIN
						DROP TABLE Todos
					END;
				IF OBJECT_ID('Users') IS NOT NULL
					BEGIN
						DROP TABLE Users
					END;
			";

			await connection.ExecuteAsync(sqlQuery);
			await this.CreateTables();
		}
	}
}