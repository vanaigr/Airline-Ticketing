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
				s.Write(it.ExpirationDate.Ticks);
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
				var number = s.ReadInt64();
				var it = new Passport(number);
				document = it;
			}
			else if(id == InternationalPassport.id) {
				var number = s.ReadInt32();
				var expirationDate = new System.DateTime(s.ReadInt64());
				var name = s.ReadString();
				var surname = s.ReadString();
				string middleName;
				var hasMiddleName = s.ReadBoolean();
				if(hasMiddleName) middleName = s.ReadString();
				else middleName = null;

				var it = new InternationalPassport(number, expirationDate, name, surname, middleName);
				document = it;
			}
			else throw new System.InvalidOperationException();

			Common.Debug2.AssertPersistent(stream.Position == stream.Length);
			Common.Debug2.AssertPersistent(document.validate().Error == false);
			return document;
			}}
		}
	}
}
