using System;
using System.Diagnostics;

namespace Common {
	public static class Debug2 {
		public static void AssertPersistent(bool condition) {
			Debug.Assert(condition);
		}
	}
}
