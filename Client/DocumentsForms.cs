using Communication;
using Documents;
using System;
using Validation;

namespace Common {
	public abstract class DocumentForm {
		public abstract Either<Document, ErrorString> toDocument(ErrorString e = new ErrorString());
	}
	

	public sealed class PassportForm : DocumentForm {
		public long? Number;

		public void updateNumber(string text) {
			try {
				long res;
				var success = long.TryParse(text, out res);
				if(!success) throw new IncorrectValue("Номер паспорта дожлен включать только цифры");
				PassportValidation.checkNumber(res).exception((msg) => new IncorrectValue(msg));
				Number = res;
			}
			catch(Exception e) {
				Number = null;
				throw e;
			}
		}

		public static PassportForm fromDocument(Passport it) {
			return new PassportForm { Number = it.Number };
		}

		public Either<Passport, ErrorString> toPassport(ErrorString e = new ErrorString()) {
			if(Number == null) e.ac("номер должен быть заполнен");

			if(!e.Error) {
				var it = new Passport(Number.Value);
				PassportValidation.validate(it, e);
				if(!e.Error) return Either<Passport, ErrorString>.Success(it);
			}

			return Either<Passport, ErrorString>.Failure(e);
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

		public void updateNumber(string text) {
			try {
				int res;
				var success = int.TryParse(text, out res);
				if(!success) throw new IncorrectValue("Номер заграничного паспорта дожлен включать только цифры");
				InternationalPassportValidation.checkNumber(res).exception((msg) => new IncorrectValue(msg));
				Number = res;
			}
			catch(Exception e) {
				Number = null;
				throw e;
			}
		}

		public void updateExpirationDate(DateTime date) {
			var res = InternationalPassportValidation.checkExpirationDate(date);
			if(res.Error) {
				ExpirationDate = null;
				throw new IncorrectValue(res.Message);
			}
			else ExpirationDate = date;
		}

		public void updateName(string text) {
			var res = InternationalPassportValidation.checkName(text);
			if(res.Error) {
				Name = null;
				throw new IncorrectValue(res.Message);
			}
			else Name = text;
		}

		public void updateSurname(string text) {
			var res = InternationalPassportValidation.checkSurname(text);
			if(res.Error) {
				Surname = null;
				throw new IncorrectValue(res.Message);
			}
			else Surname = text;
		}

		public void updateMiddleName(string text) {
			var res = InternationalPassportValidation.checkMiddleName(text);
			if(res.Error) {
				MiddleName = null;
				throw new IncorrectValue(res.Message);
			}
			else MiddleName = text;
		}

		public static InternationalPassportForm fromDocument(InternationalPassport it) {
			return new InternationalPassportForm {
				Number = it.Number,
				ExpirationDate = it.ExpirationDate,
				Name = it.Name,
				Surname = it.Surname,
				MiddleName = it.MiddleName
			};
		}

		public Either<InternationalPassport, ErrorString> toInternationalPassport(ErrorString e = new ErrorString()) {
			if(Number == null) e.ac("номер должен быть заполнен");
			if(ExpirationDate == null) e.ac("дата окончания срока действия должна быть заполнена");
			if(Name == null) e.ac("Имя должно быть заполнено");
			if(Surname == null) e.ac("Фамилия должна быть заполнена");

			if(!e.Error) {
				var it = new InternationalPassport(
					Number.Value, ExpirationDate.Value, Name, Surname, MiddleName == null ? "" : MiddleName
				);
				InternationalPassportValidation.validate(it, e);
				if(!e.Error) return Either<InternationalPassport, ErrorString>.Success(it);
			}

			return Either<InternationalPassport, ErrorString>.Failure(e);
		}

		public override Either<Document, ErrorString> toDocument(ErrorString e) {
			var res = toInternationalPassport(e);
			if(res) return Either<Document, ErrorString>.Success(res.s);
			else return Either<Document, ErrorString>.Failure(res.f);
		}
	}
}
