using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineTicketingServer {
	public class IncorrectPasswordLength : Exception {
		public IncorrectPasswordLength() {
		}

		public IncorrectPasswordLength(string message)
			: base(message) {
		}

		public IncorrectPasswordLength(string message, Exception inner)
			: base(message, inner) {
		}
	}

	class PasswordHashing {
		static readonly byte[] salt2 = new byte[] { 151, 232, 178, 30, 118, 115, 133, 45, 198, 114 };
		public static readonly int maxLengh = (448/8) - salt2.Length;

		public static byte[] computePasswordHash(byte[] salt, byte[] password) {
			var arrLength = salt.Length + salt2.Length + password.Length;
			if(arrLength > (448/8) || password.Length == 0) {
				throw new IncorrectPasswordLength();
			}

			var passBytes = new byte[arrLength];
			salt.CopyTo(passBytes, 0);
			salt2.CopyTo(passBytes, salt.Length);
			password.CopyTo(passBytes, salt.Length + salt2.Length);

			return new System.Security.Cryptography.SHA512Managed().ComputeHash(passBytes);
		}
	}
}
