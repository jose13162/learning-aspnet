using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace asp_net_core.Filters {
	public class LoggingFilter : Attribute, IActionFilter, IOrderedFilter {
		public int Order { get; set; }

		public void OnActionExecuted(ActionExecutedContext context) {
			Console.WriteLine("After the action");
		}

		public void OnActionExecuting(ActionExecutingContext context) {
			Console.WriteLine("Before the action");
		}
	}
}