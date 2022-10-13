using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineTicketingServer {
	public struct CheckResult {
		public bool ok;
		public string errorMsg;
		public bool error{ get{ return !ok; } }

		public static CheckResult Err(string msg) { return new CheckResult{ ok = false, errorMsg = msg }; }
		public static CheckResult Ok() { return new CheckResult{ ok = true }; }
	}

	public struct ErrorString {
		private bool error;
		private StringBuilder sb;

		public string Message{ get{ return sb.ToString(); } }

		public delegate void Appender(StringBuilder it);

		public static ErrorString Create() {
			return new ErrorString{
				error = false, sb = new StringBuilder()
			};
		}

		public void ac(String msg) {
			error = true;
			sb.AC(msg);
		}

		public void append(Appender a) {
			error = true;
			a(sb);
		}

		public bool Error{ get{ return error; } }

		public static bool operator true(ErrorString it) {
			return it.Error;
		}

		public static bool operator false(ErrorString it) {
			return !it.Error;
		}
	}

	public static class StringBuilderExt {
		//append capitalized if first or else add comma
		public static StringBuilder AC(this StringBuilder sb, string param) {
			if(sb.Length == 0) sb.Append(char.ToUpper(param[0]));
			else sb.Append(", ").Append(param[0]);
			sb.Append(param.Substring(1));
			return sb;
		}
	}
}
