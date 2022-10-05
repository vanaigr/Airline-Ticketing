using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FlightsOptions {

[Serializable] public struct Size3 {
	public short x, y, z;

	public static bool operator==(Size3 f, Size3 s) { 
		return f.x == s.x && f.y == s.y && f.z == s.z;
	}

	public static bool operator!=(Size3 f, Size3 s) {  return !(f == s); }
}

[Serializable] public class Baggage {
	internal short costRub; //times count
	internal short count;
	internal short maxWeightKg; //per baggage
	internal Size3 maxDim; //per baggare

	public Baggage(short count, short costRub = 0, short maxWeightKg = 0, Size3 maxDim = new Size3()) {
		Debug.Assert(
			costRub >= 0 && count > 0 && maxWeightKg >= 0
			&& ((maxDim.x > 0 && maxDim.y > 0 && maxDim.z > 0) || (maxDim == new Size3()))
		);

		this.costRub = costRub;
		this.count = count;
		this.maxWeightKg = maxWeightKg;
		this.maxDim = maxDim;
	}

	public bool IsFree{ get{ return costRub == 0; } }
	public short TotalCostRub{ get{ Debug.Assert(!IsFree); return costRub; } }

	public short Count{ get{ return count; } }

	public bool RestrictionWeight{ get{ return maxWeightKg != 0; } }
	public bool RestrictionSize{ get{ return maxDim != new Size3(); } }

	public short WeightKgRestrictionPerSingle{ get{ Debug.Assert(RestrictionWeight); return maxWeightKg; } }
	public Size3 SizeRestrictionPerSingle{ get{ Debug.Assert(RestrictionSize); return maxDim; } }
}

[Serializable] public class BaggageOptions {
	public List<Baggage> baggage;
	public List<Baggage> handLuggage;
	public Size3 e;
}

[Serializable] public struct TermsOptions {
	public short changeFlightCostRub;
	public short refundCostRub;

	public bool CanChangeFlights { get { return changeFlightCostRub >= 0; } }
	public bool Cefundable { get { return refundCostRub >= 0; } }
}

[Serializable] public struct ServicesOptions {
	public short seatChoiceCostRub;
}

[Serializable] public class Options {
	public BaggageOptions baggageOptions;
	public TermsOptions termsOptions;
	public ServicesOptions servicesOptions;
}

}

