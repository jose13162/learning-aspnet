using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.Interfaces;

namespace asp_net_core.Services {
	public class EmailTransporter : IEmailTransporter {
		public void Send(string content) {
			Console.WriteLine($"Email sent: {content}");
		}
	}
}