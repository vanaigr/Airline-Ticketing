using System.Collections.Generic;
using System.IO;
using Documents;

namespace Server {
    static class DatabaseDocument {
        public static byte[] toBytes(Documents.Document document) {
            using(var ms = new MemoryStream()) {
            using(var s = new BinaryWriter(ms)) {

            var id = document.Id;

            s.Write((byte) 1);
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
                s.Write(it.MiddleName);
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

            var version = s.ReadByte();
            Common.Debug2.AssertPersistent(version == 0 || version == 1);

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
                if(version == 0) {
                    var hasMiddleName = s.ReadBoolean();
                    if(hasMiddleName) middleName = s.ReadString();
                    else middleName = "";
                }
                else {
                    middleName = s.ReadString();
                }

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
