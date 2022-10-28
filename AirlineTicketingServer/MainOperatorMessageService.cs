using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Communication;
using OperatorViewCommunication;

namespace AirlineTicketingServer {

[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = false)]
class MainOperatorMessageService : MessageService {
	AvailableOptionsResponse MessageService.availableOptions() {
		return new AvailableOptionsResponse {
			flightClasses = Program.flightClasses,
			cities = Program.cities
		};
	}

	Either<List<AvailableFlight>, InputError> MessageService.matchingFlights(MatchingFlightsParams p) {
		var err = Validation.ErrorString.Create();
		if(p.when == null) err.ac("дата вылета должа быть заполнена");

		if(err) return Either<List<AvailableFlight>, InputError>.Failure(new InputError(err.Message));
		else return Either<List<AvailableFlight>, InputError>.Success(Flights.matchingFlights(p, mustBeAbeToBook: false));
	}
}

}
