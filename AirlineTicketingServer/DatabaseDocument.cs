using System.Collections.Generic;
using System.IO;
using Documents;

namespace Server {
	static class DatabaseDocument {
		public static byte[] toBytes(Documents.Document document) {
			using(var ms = new MemoryStream()) { 
			using(var s = new BinaryWriter(ms)) {

			var id = document.Id;

			s.Write((byte) 0);
			s.Write(id);

			if(id == Passport.id) {
				var it = (Passport) document;
				s.Write((long) it.Number);
			}
			else if(id == InternationalPassport.id) {
				var it = (InternationalPassport) document;
				s.Write((int) it.Number);
				s.Write(it.ExpirationDate.Value.Ticks);
				s.Write(it.Name);
				s.Write(it.Surname);
				if(it.MiddleName == null) s.Write(false);
				else {
					s.Write(true);
					s.Write(it.MiddleName);
				}
			}
			s.Close();
			return ms.ToArray();
			}}
		}

		public static Document fromBytes(byte[] bytes) {
			using(
			var stream = new MemoryStream(bytes, false)) { 
			using(
			var s = new BinaryReader(stream)) {

			Common.Debug2.AssertPersistent(s.ReadByte() == 0);

			Document document;
			var id = s.ReadInt32();

			if(id == Passport.id) {
				var it = new Passport();
				it.Number = s.ReadInt64();
				document = it;
			}
			else if(id == InternationalPassport.id) {
				var it = new InternationalPassport();
				it.Number = s.ReadInt32();
				it.ExpirationDate = new System.DateTime(s.ReadInt64());
				it.Name = s.ReadString();
				it.Surname = s.ReadString();
				var hasMiddleName = s.ReadBoolean();
				if(hasMiddleName) it.MiddleName = s.ReadString();
				else it.MiddleName = null;
				document = it;
			}
			else throw new System.InvalidOperationException();

			Common.Debug2.AssertPersistent(stream.Position == stream.Length);
			return document;
			}}
		}
	}
}
