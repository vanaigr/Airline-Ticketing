using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Validation;

namespace Documents {
	static class Misc {
		//https://stackoverflow.com/a/9082843/18704284
		public static bool isLatin(char c) {
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
		}
	}

	public class IncorrectValue : Exception {
		public IncorrectValue() {}

		public IncorrectValue(string message) : base(message) {}

		public IncorrectValue(string message, Exception innerException) : base(message, innerException) {}

		protected IncorrectValue(SerializationInfo info, StreamingContext context) : base(info, context) {}
	}

	[Serializable] public abstract class Document {
		public abstract int Id{ get; }

		public abstract Validation.ErrorString validate();
	}

	[Serializable] public sealed class Passport : Document {
		public static readonly int id = 0;
		private long? number;
		public long? Number{ 
			get{ return number; } 
			set{
				if(value == null) number = value;
				else if(value >= 1000000000L && value < 10000000000L) //value is 10 digits long
					number = value;
				else throw new IncorrectValue("Номер паспорта должен состоять из 10 цифр");
			}
		}

		public override int Id{ get{ return Passport.id; } }

		public void setNumber(string text) {
			long res;
			var success = long.TryParse(text, out res);
			if(!success) throw new IncorrectValue("Номер паспорта дожлен включать только цифры");
			Number = res;
		}

		public override bool Equals(object o) {
			if(o == null || !(o is Passport)) return false;
			var s = (Passport) o;
			return Equals(number, s.number);
		}

		public override int GetHashCode() {
			int hashCode = -1882778528;
			hashCode=hashCode*-1521134295+number.GetHashCode();
			hashCode=hashCode*-1521134295+Number.GetHashCode();
			return hashCode;
		}

		public override string ToString() {
			return "Паспорт: " + ((long) number).ToString("00 00 000000");
		}

		public override ErrorString validate() {
			var it = ErrorString.Create();
			if(Number == null) {
				it.ac("номер должен быть заполнен");
			}

			return it;
		}
	}
		
	[Serializable] public sealed class InternationalPassport : Document {
		public static readonly int id = 1;
		private int? number;
		private DateTime? expirationDate;
		private string name, surname, middleName;

		public int? Number{ 
			get{ return number; } 
			set{
				if(value == null) number = value;
				else if(value >= 100000000 && value < 1000000000) //value is 9 digits long
					number = value;
				else throw new IncorrectValue("Номер заграничного паспорта должен состоять из 9 цифр");
			}
		}
		public DateTime? ExpirationDate{ 
			get{ return expirationDate; } 
			set{ expirationDate = value?.Date; }
		}
		public string Name{ 
			get{ return name; } 
			set{
				if(value != null) {
					if(value.Length == 0) throw new IncorrectValue("Имя должно быть заполнено");
					foreach(var ch in value) if(!(Misc.isLatin(ch) || ch == '-')) 
						throw new IncorrectValue("Имя должно содержать только латинские буквы или символ дефиса");
				}
				name = value;
			}
		}
		public string Surname{ 
			get{ return surname; } 
			set{
				if(value != null) {
					if(value.Length == 0) throw new IncorrectValue("Фамилия должна быть заполнена");
					foreach(var ch in value) if(!(Misc.isLatin(ch) || ch == '-')) 
						throw new IncorrectValue("Фамилия должна содержать только латинские буквы или символ дефиса");
				}
				surname = value;
			}
		}

		public string MiddleName{ 
			get{ return middleName; } 
			set{
				if(value != null) foreach(var ch in value) if(!(Misc.isLatin(ch) || ch == '-')) 
					throw new IncorrectValue("Фамилия должна содержать только латинские буквы или символ дефиса");
				middleName = value;
			}
		}

		public override int Id{ get{ return InternationalPassport.id; } }

		public void setNumber(string text) {
			int res;
			var success = int.TryParse(text, out res);
			if(!success) throw new IncorrectValue("Номер заграничного паспорта дожлен включать только цифры");
			Number = res;
		}

		public void setExpirationDate(string text) {
			DateTime res;
			var success = DateTime.TryParseExact(text, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out res);
			if(!success) throw new IncorrectValue("Дата окончания срока действия должнеа быть в формате дд.мм.гггг");
			expirationDate = res;
		}

		public override bool Equals(object o) {
			if(o == null || !(o is InternationalPassport)) return false;
			var s = (InternationalPassport) o;
			return Equals(number, s.number) && Equals(expirationDate, s.expirationDate)
				&& Equals(name, s.name) && Equals(surname, s.surname) && Equals(middleName, s.middleName);
		}

		public override int GetHashCode() {
			int hashCode = 456306398;
			hashCode=hashCode*-1521134295+number.GetHashCode();
			hashCode=hashCode*-1521134295+expirationDate.GetHashCode();
			hashCode=hashCode*-1521134295+EqualityComparer<string>.Default.GetHashCode(name);
			hashCode=hashCode*-1521134295+EqualityComparer<string>.Default.GetHashCode(surname);
			hashCode=hashCode*-1521134295+EqualityComparer<string>.Default.GetHashCode(middleName);
			hashCode=hashCode*-1521134295+Number.GetHashCode();
			hashCode=hashCode*-1521134295+ExpirationDate.GetHashCode();
			hashCode=hashCode*-1521134295+EqualityComparer<string>.Default.GetHashCode(Name);
			hashCode=hashCode*-1521134295+EqualityComparer<string>.Default.GetHashCode(Surname);
			hashCode=hashCode*-1521134295+EqualityComparer<string>.Default.GetHashCode(MiddleName);
			return hashCode;
		}

		public override string ToString() {
			return String.Format(
				"Заграничный паспорт: номер - {0}, дата окончания срока действия - {1}, фамилия: `{2}" +
				", имя - {3}, отчество - {4}",
				((long) number).ToString("00 0000000"), ((DateTime) expirationDate).ToString("dd.MM.yyyy"),
				surname, name, middleName != null ? "`" + middleName + "`" : "нет"
			);
		}

		public override ErrorString validate() {
			var it = ErrorString.Create();

			if(Number == null) {
				it.ac("номер должен быть заполнен");
			}
			if(ExpirationDate == null) {
				it.ac("дата окончания срока действия должна быть заполнена");
			}
			if(name == null) it.ac("имя должно быть заполнено");
			if(surname == null) it.ac("фамилия должна быть заполнена");

			return it;
		}
	}

	static class DocumentsName {
		public static readonly Dictionary<int, string> documentsNames = new Dictionary<int, string>();

		static DocumentsName() {
			documentsNames.Add(Passport.id, "Паспорт РФ");
			documentsNames.Add(InternationalPassport.id, "Заграничный паспорт РФ");
			//documentsNames.Add(2, "Иностранный документ");
			//documentsNames.Add(3, "Вид на жительство");
			//documentsNames.Add(4, "Паспорт СССР");
			//documentsNames.Add(5, "Удостоверение личности моряка");
			//documentsNames.Add(6, "Свидетельство о рождении");
			//documentsNames.Add(7, "Медицинское свидетельство о рождении");
			//documentsNames.Add(8, "Военный билет для военнослужащих срочной службы, по контракту и курсантов");
			//documentsNames.Add(9, "Удостоверение личности военнослужащего");
			//documentsNames.Add(10, "Удостоверение личности лица без гражданства");
			//documentsNames.Add(11, "Справка об освобождении из мест лишения свободы");
			//documentsNames.Add(12, "Дипломатический паспорт");
			//documentsNames.Add(13, "Служебный паспорт");
			//documentsNames.Add(14, "Свидетельство на возвращение");
			//documentsNames.Add(15, "Справка об утере паспорта");
			//documentsNames.Add(16, "Свидетельство беженца");
			//documentsNames.Add(17, "Удостоверение беженца");
		}
	}
}
