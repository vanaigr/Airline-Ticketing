using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Validation {
	public struct CheckResult {
		public bool ok;
		public string errorMsg;
		public bool error{ get{ return !ok; } }

		public static CheckResult Err(string msg) { return new CheckResult{ ok = false, errorMsg = msg }; }
		public static CheckResult Ok() { return new CheckResult{ ok = true }; }
	}

	public delegate T Throw<T>(String msg);

	public struct ErrorString {
		private bool error;
		private StringBuilder sb_;
		private StringBuilder sb{ get{
			if(sb_ == null) sb_ = new StringBuilder();
			return sb_;
		} }

		public string Message{ get{ return sb.ToString(); } }

		public delegate void Appender(StringBuilder it);

		public static ErrorString Create() {
			return new ErrorString{
				error = false, sb_ = new StringBuilder()
			};
		}

		public ErrorString ac(String msg) {
			error = true;
			sb.AC(msg);
			return this;
		}

		public ErrorString append(string a) {
			error = true;
			sb.Append(a);
			return this;
		}

		public ErrorString append(int a) {
			error = true;
			sb.Append(a);
			return this;
		}

		public ErrorString append(Appender a) {
			error = true;
			a(sb);
			return this;
		}

		public bool Error{ get{ return error; } }

		public static bool operator true(ErrorString it) {
			return it.Error;
		}

		public static bool operator false(ErrorString it) {
			return !it.Error;
		}

		public void exception<T>(Throw<T> t) where T : Exception {
			if(Error) throw t(Message);
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
