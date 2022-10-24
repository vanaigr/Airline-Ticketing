using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace AirlineTicketingServer {
	static class BinaryDocument {
		public static byte[] toBytes(Documents.Document document) {
			using(
			var ms = new MemoryStream()) {
			new BinaryFormatter().Serialize(ms, document);
			return ms.ToArray();
			}
		}

		public static Documents.Document fromBytes(byte[] documentBin) {
			using(
			var ms = new MemoryStream(documentBin, false)) {
			return (Documents.Document) new BinaryFormatter().Deserialize(ms);
			}
		}
	}
}
