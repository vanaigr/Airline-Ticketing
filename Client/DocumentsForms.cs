using Communication;
using Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Validation;

namespace Client {
	public abstract class DocumentForm {
		public abstract Either<Document, ErrorString> toDocument(ErrorString e = new ErrorString());
	}

	public sealed class PassportForm : DocumentForm {
		public long? Number;

		public static PassportForm fromDocument(Passport it) {
			return new PassportForm{ Number = it.Number };
		}
		
		public Communication.Either<Passport, ErrorString> toPassport(ErrorString e = new ErrorString()) {
			if(Number == null) e.ac("номер должен быть заполнен");

			if(!e.Error) {
				var it = new Passport(Number.Value);
				PassportValidation.validate(it, e);
				if(!e.Error) return Communication.Either<Passport, ErrorString>.Success(it);
			}

			return Communication.Either<Passport, ErrorString>.Failure(e);
		}

		public override Either<Document, ErrorString> toDocument(ErrorString e) {
			var res = toPassport(e);
			if(res) return Either<Document, ErrorString>.Success(res.s);
			else return Either<Document, ErrorString>.Failure(res.f);
		}
	}

	public sealed class InternationalPassportForm : DocumentForm {
		public int? Number;
		public DateTime? ExpirationDate;
		public string Name, Surname, MiddleName;

		public static InternationalPassportForm fromDocument(InternationalPassport it) {
			return new InternationalPassportForm{
				Number = it.Number, ExpirationDate = it.ExpirationDate,
				Name = it.Name, Surname = it.Surname, MiddleName = it.MiddleName
			};
		}

		public Communication.Either<InternationalPassport, ErrorString> toInternationalPassport(ErrorString e = new ErrorString()) {
			if(Number == null) e.ac("номер должен быть заполнен");
			if(ExpirationDate == null) e.ac("дата окончания срока действия должна быть заполнена");
			if(Name == null) e.ac("Имя должно быть заполнено");
			if(Surname == null) e.ac("Фамилия должна быть заполнена");

			if(!e.Error) {
				var it = new InternationalPassport(
					Number.Value, ExpirationDate.Value, Name, Surname, MiddleName
				);
				InternationalPassportValidation.validate(it, e);
				if(!e.Error) return Communication.Either<InternationalPassport, ErrorString>.Success(it);
			}

			return Communication.Either<InternationalPassport, ErrorString>.Failure(e);
		}

		public override Either<Document, ErrorString> toDocument(ErrorString e) {
			var res = toInternationalPassport(e);
			if(res) return Either<Document, ErrorString>.Success(res.s);
			else return Either<Document, ErrorString>.Failure(res.f);
		}
	}
}
