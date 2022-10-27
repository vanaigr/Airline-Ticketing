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

	Either<List<AvailableFlight>, InputError> MessageService.matchingFlights(MatchingFlightsParams matchingFlightsParams) {
		throw new NotImplementedException();
	}
}

}
