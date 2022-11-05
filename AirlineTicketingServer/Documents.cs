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

	[KnownType(typeof(Documents.Passport))]
	[KnownType(typeof(Documents.InternationalPassport))]
	[Serializable] public abstract class Document {
		public abstract int Id{ get; }

		public abstract Validation.ErrorString validate();
	}

	public static class PassportValidation {
		public static ErrorString checkNumber(Passport it, long? value, ErrorString e = new ErrorString()) {
			//value is not 10 digits long
			if(value != null && !(value >= 1000000000L && value < 10000000000L)) 
				e.ac("номер паспорта должен состоять из 10 цифр");
			return e;
		}

		public static ErrorString validate(Passport it, ErrorString e = new ErrorString()) {
			if(it.Number == null) e.ac("номер должен быть заполнен");
			else checkNumber(it, it.Number, e);

			return e;
		}
	}

	[Serializable] public sealed class Passport : Document {
		public static readonly int id = 0;


		public long? Number{ get; set; }
	

		public override int Id{ get{ return id; } }

		public override ErrorString validate() {
			return PassportValidation.validate(this);
		}


		public override bool Equals(object o) {
			if(o == null || !(o is Passport)) return false;
			var s = (Passport) o;
			return Equals(Number, s.Number);
		}

		public override int GetHashCode() {
			return 187193536+Number.GetHashCode();
		}

		public override string ToString() {
			return "Паспорт: " + ((long) Number).ToString("00 00 000000");
		}
	}

	public static class InternationalPassportValidation {
		public static ErrorString checkNumber(InternationalPassport it, int? value, ErrorString e = new ErrorString()) {
			//value is not 9 digits long
			if(value != null) {
				if(!(value >= 100000000 && value < 1000000000)) 
					e.ac("номер заграничного паспорта должен состоять из 9 цифр");
			}
			return e;
		}

		public static ErrorString checkExpirationDate(InternationalPassport it, DateTime? value, ErrorString e = new ErrorString()) {
			return e;
		}

		public static ErrorString checkName(InternationalPassport it, string value, ErrorString e = new ErrorString()) {
			if(value != null) {
				if(value.Length == 0) e.ac("имя должно быть заполнено");
				else foreach(var ch in value) if(!(Misc.isLatin(ch) || ch == '-')) { 
					e.ac("имя должно содержать только латинские буквы или символ дефиса");
					break;
				}
			}
			return e;
		}

		public static ErrorString checkSurname(InternationalPassport it, string value, ErrorString e = new ErrorString()) {
			if(value != null) {
				if(value.Length == 0) e.ac("фамилия должна быть заполнена");
				else foreach(var ch in value) if(!(Misc.isLatin(ch) || ch == '-')) { 
					e.ac("фамилия должна содержать только латинские буквы или символ дефиса");
					break;
				}
			}
			return e;
		}

		public static ErrorString checkMiddleName(InternationalPassport it, string value, ErrorString e = new ErrorString()) {
			if(value != null) {
				if(value.Length == 0) e.ac("отчество должно быть заполнена");
				else foreach(var ch in value) if(!(Misc.isLatin(ch) || ch == '-')) { 
					e.ac("отчество должно содержать только латинские буквы или символ дефиса");
					break;
				}
			}
			return e;
		}

		public static ErrorString validate(InternationalPassport it, ErrorString e = new ErrorString()) {
			if(it.Number == null) e.ac("Номер должен быть заполнен");
			else checkNumber(it, it.Number, e);

			if(it.ExpirationDate == null) e.ac("дата окончания срока действия должна быть заполнена");
			checkExpirationDate(it, it.ExpirationDate, e);

			if(it.Name == null) e.ac("Имя должно быть заполнено");
			else checkName(it, it.Name, e);

			if(it.Surname == null) e.ac("Фамилия должна быть заполнена");
			else checkSurname(it, it.Surname, e);

			if(it.MiddleName == null); //null middle name is fine
			else checkMiddleName(it, it.Name, e);

			return e;
		}
	}
		
	[Serializable] public sealed class InternationalPassport : Document {
		public static readonly int id = 1;


		public int? Number;
		private DateTime? expirationDate;
		public string Name, Surname, MiddleName;

		public DateTime? ExpirationDate{ 
			get{ return expirationDate; } 
			set{ expirationDate = value?.Date; }
		}


		public override int Id{ get{ return id; } }

		public override ErrorString validate() {
			return InternationalPassportValidation.validate(this);
		}
		

		public override bool Equals(object o) {
			if(o == null || !(o is InternationalPassport)) return false;
			var s = (InternationalPassport) o;
			return Equals(Number, s.Number) && Equals(expirationDate, s.expirationDate)
				&& Equals(Name, s.Name) && Equals(Surname, s.Surname) && Equals(MiddleName, s.MiddleName);
		}

		public override int GetHashCode() {
			int hashCode = -1848562201;
			hashCode=hashCode*-1521134295+Number.GetHashCode();
			hashCode=hashCode*-1521134295+expirationDate.GetHashCode();
			hashCode=hashCode*-1521134295+EqualityComparer<string>.Default.GetHashCode(Name);
			hashCode=hashCode*-1521134295+EqualityComparer<string>.Default.GetHashCode(Surname);
			hashCode=hashCode*-1521134295+EqualityComparer<string>.Default.GetHashCode(MiddleName);
			hashCode=hashCode*-1521134295+ExpirationDate.GetHashCode();
			return hashCode;
		}

		public override string ToString() {
			return String.Format(
				"Заграничный паспорт: номер - {0}, дата окончания срока действия - {1}, фамилия: `{2}" +
				", имя - {3}, отчество - {4}",
				((long) Number).ToString("00 0000000"), ((DateTime) ExpirationDate).ToString("dd.MM.yyyy"),
				Surname, Name, MiddleName != null ? "`" + MiddleName + "`" : "нет"
			);
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
