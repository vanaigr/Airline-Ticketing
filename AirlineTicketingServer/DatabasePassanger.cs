using Communication;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using Validation;

namespace Server {
    static class ValidatePassanger {
        public static Validation.CheckResult validate(Passanger it) {
            var sb = new StringBuilder();
            var err = false;

            if(it.name == null || it.name.Length < 1) {
                err = true;
                sb.AC("имя должно быть заполнено");
            }
            if(it.surname == null || it.surname.Length < 1) {
                err = true;
                sb.AC("фамилия должна быть заполнена");
            }

            if(err) return  Validation.CheckResult.Err(sb.ToString());
            else return Validation.CheckResult.Ok();
        }
    }

    static class DatabasePassanger {
        public struct RawPassanger {
            public int index;
            public bool archived;
            public string name, surname, middleName;
            public DateTime birthday;
            public byte[] documentBin;
        }

        public static Dictionary<int, Passanger> getAll(SqlConnectionView cv, int customerId) {
            using(cv) {
            using(
            var command = new SqlCommand(@"
                select [pi].[Id], [pi].[Archived], [pi].[Name], [pi].[Surname], [pi].[MiddleName], [pi].[Birthday], [pi].[Document]
                from [Customers].[Passanger] as [pi]
                where [pi].[Customer] = @Customer
            ", cv.connection)) {
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.AddWithValue("@Customer", customerId);

            var rawPassangers = new List<RawPassanger>();

            cv.Open();
            using(
            var reader = command.ExecuteReader()){
            while(reader.Read()) rawPassangers.Add(new RawPassanger{
                index = (int) reader[0],
                archived = (bool) reader[1],
                name = (string) reader[2],
                surname = (string) reader[3],
                middleName = (string) (reader[4] is DBNull ? "" : reader[4]),
                birthday = (DateTime) reader[5],
                documentBin = (byte[]) reader[6]
            });
            reader.Close();
            command.Dispose();
            cv.Dispose();

            var passangers = new Dictionary<int, Passanger>(rawPassangers.Count);
            foreach(var rp in rawPassangers) {
                passangers.Add(rp.index, new Passanger{
                    archived = rp.archived,
                    name = rp.name, surname = rp.surname, middleName = rp.middleName,
                    birthday = rp.birthday, document = DatabaseDocument.fromBytes(rp.documentBin)
                });
            }
            return passangers;
            }}}
        }

        public static int? replace(SqlConnectionView cv, int customerId, int index, Passanger passanger) {
            using(cv) {
            var documentBin = DatabaseDocument.toBytes(passanger.document);

            using(
            var command = new SqlCommand("[Customers].[ReplacePassanger]", cv.connection)) {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Birthday", passanger.birthday);
            command.Parameters.AddWithValue("@Document", documentBin);
            command.Parameters.AddWithValue("@Name", passanger.name);
            command.Parameters.AddWithValue("@Surname", passanger.surname);
            command.Parameters.AddWithValue("@MiddleName", passanger.middleName == null ? "" : passanger.middleName);
            command.Parameters.AddWithValue("@Customer", customerId);
            command.Parameters.AddWithValue("@Id", index);
            var newIdParam = command.Parameters.Add("@NewId", System.Data.SqlDbType.Int);
            newIdParam.Direction = System.Data.ParameterDirection.Output;

            cv.Open();
            command.ExecuteNonQuery();
            if(newIdParam.Value.GetType() == typeof(DBNull)) return null;
            else return (int) newIdParam.Value;
            }}
        }

        public static int add(SqlConnectionView cv, int customerId, Passanger passanger) {
            var documentBin = DatabaseDocument.toBytes(passanger.document);

            using(
            var command = new SqlCommand("[Customers].[AddPassanger]",cv.connection)) {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Birthday", passanger.birthday);
            command.Parameters.AddWithValue("@Document", documentBin);
            command.Parameters.AddWithValue("@Name", passanger.name);
            command.Parameters.AddWithValue("@Surname", passanger.surname);
            command.Parameters.AddWithValue("@MiddleName", passanger.middleName == null ? "" : passanger.middleName);
            command.Parameters.AddWithValue("@CustomerId", customerId);
            var result = command.Parameters.Add("@NewId", System.Data.SqlDbType.Int);
            result.Direction = System.Data.ParameterDirection.Output;

            cv.Open();
            command.ExecuteNonQuery();
            return (int) result.Value;
            }
        }

        private static readonly Regex foreignKeyRegex = new Regex(
            "The (.+) statement conflicted with the (.+) constraint \"(.+)\"\\. The conflict occurred in database \"(.+)\", table \"(.+)\", column \'(.+)\'."
        );

        public static CheckResult remove(SqlConnectionView cv, int customerId, int index) {
            using(cv) {
            using(
            var command = new SqlCommand("[Flights].[RemovePassanger]", cv.connection)) {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Customer", customerId);
            command.Parameters.AddWithValue("@Id", index);
            var resultParam = command.Parameters.Add("@Deleted", System.Data.SqlDbType.Bit);
            resultParam.Direction = System.Data.ParameterDirection.Output;

            try {
                cv.Open();
                command.ExecuteNonQuery();
                if((bool) resultParam.Value == true) return new CheckResult{ ok = true };
                else return new CheckResult{ ok = false, errorMsg = "Ошибка удаления" };
            }
            catch(SqlException e) {
                command.Dispose();
                cv.Dispose();

                var match = foreignKeyRegex.Match(e.Message);
                if(match.Success && match.Groups[2].Value == "REFERENCE" && match.Groups[3].Value == "AvailableFlightsSeats_ForeignPassanger"
                    && match.Groups[5].Value == "Flights.AvailableFlightsSeats" && match.Groups[6].Value == "Passanger") {
                    return new CheckResult{ ok = false, errorMsg = "Данный пассажир не может быть удалён, так как он уже был выбран при покупке билета" };
                }
                else throw e;
            }

            }}
        }
    }
}
