using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ClientCommunication;
using Communication;

namespace ClientCommunication {
	public class Customer {
		public Account? customer;

		public int newPassangerIndex = 0;
		public Dictionary<int, PassangerIdData> passangerIds;
		public Dictionary<int, Passanger> passangers;

		public int newBookedFlightIndex = 0;
		public Dictionary<int, BookedFlightDetails> bookedFlightsDetails;
		public Dictionary<int, BookedFlight> flightsBooked;

		public Customer() { 
			customer = null;
			newPassangerIndex = 0;
			passangerIds = new Dictionary<int, PassangerIdData>();
			passangers = new Dictionary<int, Passanger>();
			bookedFlightsDetails = new Dictionary<int, BookedFlightDetails>();
			flightsBooked = new Dictionary<int, BookedFlight>();
		}

		public Customer(string login, string password) : this() {
			this.customer = new Account{
				login = login,
				password = password
			};
		}

		public Account Get() { return (Account) customer; }
		
		public void unlogin() {
			customer = null;
			newPassangerIndex = 0;
			passangerIds.Clear();
			passangers.Clear();
			bookedFlightsDetails.Clear();
			flightsBooked.Clear();
		}

		public void setFrom(Account o) {
			unlogin();
			this.customer = o;
		}

		public bool LoggedIn{
			get{ return customer != null; }
		}

		public int? findPasangerIndexByDatabaseId(int databaseId) {
			foreach(var pair in passangerIds) {
				if(!pair.Value.IsLocal && pair.Value.DatabaseId == databaseId) return pair.Key;
			}
			return null;
		}
	}

	public struct PassangerIdData {
		private int? databaseId;

		public PassangerIdData(int? databaseId) {
			this.databaseId = databaseId;
		}

		public bool IsLocal{ get{ return databaseId == null; } }

		public int DatabaseId{ get{
			if(IsLocal) throw new InvalidOperationException();
			else return (int) databaseId;
		}}
	}
}
