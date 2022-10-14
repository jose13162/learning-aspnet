using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace asp_net_core.Filters {
	public class ResourceFilter : Attribute, IResourceFilter {
		public void OnResourceExecuted(ResourceExecutedContext context) {
			throw new NotImplementedException();
		}

		public void OnResourceExecuting(ResourceExecutingContext context) {
			context.Result = new ContentResult() {
				Content = "Resource unavailable - undefined header"
			};
		}
	}
}