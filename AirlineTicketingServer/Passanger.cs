using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineTicketingServer {
	class Passanger {
		class PassangerDocument {
			public string name;
		}
		static readonly List<PassangerDocument> documents = new List<PassangerDocument> {
			new PassangerDocument{ name = "Паспорт РФ" },
			new PassangerDocument{ name = "Заграничный паспорт РФ" },
			new PassangerDocument{ name = "Иностранный документ" },
			new PassangerDocument{ name = "Вид на жительство" },
			new PassangerDocument{ name = "Паспорт СССР" },
			new PassangerDocument{ name = "Удостоверение личности моряка" },
			new PassangerDocument{ name = "Свидетельство о рождении" },
			new PassangerDocument{ name = "Медицинское свидетельство о рождении" },
			new PassangerDocument{ name = "Военный билет для военнослужащих срочной службы, по контракту и курсантов" },
			new PassangerDocument{ name = "Удостоверение личности военнослужащего" },
			new PassangerDocument{ name = "Удостоверение личности лица без гражданства" },
			new PassangerDocument{ name = "Справка об освобождении из мест лишения свободы" },
			new PassangerDocument{ name = "Дипломатический паспорт" },
			new PassangerDocument{ name = "Служебный паспорт" },
			new PassangerDocument{ name = "Свидетельство на возвращение" },
			new PassangerDocument{ name = "Справка об утере паспорта" },
			new PassangerDocument{ name = "Свидетельство беженца" },
			new PassangerDocument{ name = "Удостоверение беженца" }
		};
	}
}
