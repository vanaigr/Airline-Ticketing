using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineTicketingServer {
	public struct CheckResult {
		public bool ok;
		public string errorMsg;
		public bool error{ get => !ok; }

		public static CheckResult Err(string msg) { return new CheckResult{ ok = false, errorMsg = msg }; }
		public static CheckResult Ok() { return new CheckResult{ ok = true }; }
	}

	public static class StringBuildexExt {
		//append capitalized if first or else add comma
		public static StringBuilder AC(this StringBuilder sb, string param) {
			if(sb.Length == 0) sb.Append(char.ToUpper(param[0]));
			else sb.Append(", ").Append(param[0]);
			sb.Append(param.Substring(1));
			return sb;
		}
	}
}
