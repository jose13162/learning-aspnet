using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace asp_net_core.Filters {
	public class GlobalFilter : IActionFilter {
		public void OnActionExecuted(ActionExecutedContext context) {
			Console.WriteLine("Before the action (global)");
		}

		public void OnActionExecuting(ActionExecutingContext context) {
			Console.WriteLine("After the action (global)");
		}
	}
}