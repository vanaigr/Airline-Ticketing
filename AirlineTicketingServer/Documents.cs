using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

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

	[Serializable] public class Document {}

	[Serializable] public class Passport : Document {
		public static readonly int id = 0;
		private long number;
		public long Number{ 
			get{ return number; } 
			set{
				if(value >= 1000000000L && value < 10000000000L) //value is 10 digits long
					number = value;
				else throw new IncorrectValue("Номер паспорта должен состоять из 10 цифр");
			}
		}
	}
		
	[Serializable] public class InternationalPassport : Document {
		public static readonly int id = 1;
		private int number;
		private DateTime expirationDate;
		private string name, surname, middleName;

		public int Number{ 
			get{ return number; } 
			set{
				if(value >= 100000000 && value < 1000000000) //value is 9 digits long
					number = value;
				else throw new IncorrectValue("Номер заграничного паспорта должен состоять из 9 цифр");
			}
		}
		public DateTime ExpirationDate{ 
			get{ return expirationDate; } 
			set{ expirationDate = value.Date; }
		}
		public string Name{ 
			get{ return name; } 
			set{
				if(value == null || value.Length == 0) throw new IncorrectValue("Имя должно быть заполнено");
				foreach(var ch in value) if(!(Misc.isLatin(ch) || ch == '-')) 
					throw new IncorrectValue("Имя должно содержать только латинские буквы или символ дефиса");
				name = value;
			}
		}
		public string Surname{ 
			get{ return surname; } 
			set{
				if(value == null || value.Length == 0) throw new IncorrectValue("Фамилия должна быть заполнена");
				foreach(var ch in value) if(!(Misc.isLatin(ch) || ch == '-')) 
					throw new IncorrectValue("Фамилия должна содержать только латинские буквы или символ дефиса");
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
	}

	static class DocumentsName {
		public static readonly Dictionary<int, string> documentsNames = new Dictionary<int, string>();

		static DocumentsName() {
			documentsNames.Add(Passport.id, "Паспорт РФ");
			documentsNames.Add(InternationalPassport.id, "Заграничный паспорт РФ");
			documentsNames.Add(2, "Иностранный документ");
			documentsNames.Add(3, "Вид на жительство");
			documentsNames.Add(4, "Паспорт СССР");
			documentsNames.Add(5, "Удостоверение личности моряка");
			documentsNames.Add(6, "Свидетельство о рождении");
			documentsNames.Add(7, "Медицинское свидетельство о рождении");
			documentsNames.Add(8, "Военный билет для военнослужащих срочной службы, по контракту и курсантов");
			documentsNames.Add(9, "Удостоверение личности военнослужащего");
			documentsNames.Add(10, "Удостоверение личности лица без гражданства");
			documentsNames.Add(11, "Справка об освобождении из мест лишения свободы");
			documentsNames.Add(12, "Дипломатический паспорт");
			documentsNames.Add(13, "Служебный паспорт");
			documentsNames.Add(14, "Свидетельство на возвращение");
			documentsNames.Add(15, "Справка об утере паспорта");
			documentsNames.Add(16, "Свидетельство беженца");
			documentsNames.Add(17, "Удостоверение беженца");
		}
	}
}
