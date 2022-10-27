using Communication;
using System.Collections.Generic;
using System.ServiceModel;

namespace OperatorViewCommunication {

	[ServiceContract]
	public interface MessageService {
		[OperationContract] AvailableOptionsResponse availableOptions();
		[OperationContract] Either<List<AvailableFlight>, InputError> matchingFlights(MatchingFlightsParams matchingFlightsParams);
	}
}
