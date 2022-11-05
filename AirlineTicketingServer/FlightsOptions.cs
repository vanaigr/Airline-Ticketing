using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FlightsOptions {

[Serializable] public struct Size3 {
	public short x, y, z;

	public override bool Equals(Object o) { 
		if(o == null || !(o is Size3)) return false;
		var s = (Size3) o;
		return x == s.x && y == s.y && z == s.z;
	}

}

[Serializable] public class Baggage {
	internal int costRub; //times count
	internal short count;
	internal short maxWeightKg; //per baggage
	internal Size3 maxDim; //per baggare

	public Baggage(short count, int costRub, short maxWeightKg = 0, Size3 maxDim = new Size3()) {
		Common.Debug2.AssertPersistent(
			costRub >= 0 && count >= 0 && maxWeightKg >= 0
			&& ((maxDim.x > 0 && maxDim.y > 0 && maxDim.z > 0) || Equals(maxDim, new Size3()))
		);

		this.costRub = costRub;
		this.count = count;
		this.maxWeightKg = maxWeightKg;
		this.maxDim = maxDim;
	}

	public bool IsFree{ get{ return costRub == 0; } }
	public bool RestrictionWeight{ get{ return maxWeightKg != 0; } }
	public bool RestrictionSize{ get{ return !Equals(maxDim, new Size3()); } }
}

[Serializable] public class BaggageOptions {
	public List<Baggage> baggage;
	public List<Baggage> handLuggage;
}

[Serializable] public struct TermsOptions {
	internal int changeFlightCostRub;
	internal int refundCostRub;

	public int ChangeFlightCostRub { get { Common.Debug2.AssertPersistent(CanChangeFlights); return changeFlightCostRub; } }
	public int RefundCostRub { get { Common.Debug2.AssertPersistent(Refundable); return refundCostRub; } }

	public bool CanChangeFlights { get { return changeFlightCostRub >= 0; } }
	public bool Refundable { get { return refundCostRub >= 0; } }
}

[Serializable] public struct ServicesOptions {
	public int basePriceRub;
	public int seatChoiceCostRub;
}

[Serializable] public class Options {
	public BaggageOptions baggageOptions;
	public TermsOptions termsOptions;
	public ServicesOptions servicesOptions;
}

[Serializable] public class SelectedBaggageOptions {
	public int baggageIndex;
	public int handLuggageIndex;

	public SelectedBaggageOptions(int baggageIndex, int handLuggageIndex) {
		this.baggageIndex = baggageIndex;
		this.handLuggageIndex = handLuggageIndex;
	}
}

[Serializable] public class SelectedServicesOptions {
	public bool seatSelected;

	public SelectedServicesOptions(bool seatSelected) {
		this.seatSelected = seatSelected;
	}
}

[Serializable] public class SelectedOptions {
	public SelectedBaggageOptions baggageOptions;
	public SelectedServicesOptions servicesOptions;

	public SelectedOptions(
		SelectedBaggageOptions baggageOptions,
		SelectedServicesOptions servicesOptions
	) {
		this.baggageOptions = baggageOptions;
		this.servicesOptions = servicesOptions;
	}
}

}

