using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_net_core.Interfaces {
	public interface IEmailTransporter {
		void Send(string content);
	}
}