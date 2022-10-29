using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_net_core.Data {
	public class DataContext : DbContext {
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		public DbSet<Todo> Todos { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options) {
			options.UseSqlServer("Data Source=DESKTOP-UCAC4K4\\SQLEXPRESS;Initial Catalog=MinimalApi; Integrated Security=true;");
		}
	}
}