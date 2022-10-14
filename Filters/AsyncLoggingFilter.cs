using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace asp_net_core.Filters {
	public class AsyncLoggingFilter : Attribute, IAsyncActionFilter, IOrderedFilter {
		public int Order { get; set; }

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
			Console.WriteLine("Before the action (async)");
			await next();
			Console.WriteLine("After the action (async)");
		}
	}
}