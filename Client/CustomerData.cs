using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Communication;

namespace Client {
	public class CustomerData {
		public Customer? customer_;
		public int newLocalPassangerId;
		public Dictionary<int, Passanger> localPassangers;
		public Dictionary<int, Passanger> databasePassangers;

		public CustomerData() { 
			customer_ = null;
			newLocalPassangerId = 0;
			localPassangers = new Dictionary<int, Passanger>();
			databasePassangers = new Dictionary<int, Passanger>();
		}

		public CustomerData(string login, string password) : this() {
			this.customer_ = new Customer{
				login = login,
				password = password
			};
		}

		public Customer Get() { return (Customer) customer_; }

		public bool tryGetPassangerAt(PassangerId i, out Passanger it) {
			if(!i.isValid) throw new InvalidOperationException();
			else if(i.IsLocal) return localPassangers.TryGetValue(i.Index, out it);
			else return databasePassangers.TryGetValue(i.Index, out it);
		}

		public Passanger passangerAt(PassangerId i) {
			if(!i.isValid) throw new InvalidOperationException();
			else if(i.IsLocal) return localPassangers[i.Index];
			else return databasePassangers[i.Index];
		}

		public void unlogin() {
			customer_ = null;
		}

		public void setFrom(Customer o) {
			this.customer_ = o;
		}

		public bool LoggedIn{
			get{ return customer_ != null; }
		}
	}

	public struct PassangerId {
		private bool? isLocal;
		private int index;

		public static PassangerId fromLocalIndex(int value) {
			return new PassangerId{ isLocal = true, index = value };
		}

		public static PassangerId fromDatabaseIndex(int value) {
			return new PassangerId{ isLocal = false, index = value };
		}

		public bool isValid{ get{ return isLocal != null; } }
		public bool IsLocal{ get{ if(!isValid) throw new InvalidOperationException(); return (bool) isLocal; } }

		public int Index{ get{ if(!isValid) throw new InvalidOperationException(); return index; } }

		public override bool Equals(object obj) {
			if(obj == null || !(obj is PassangerId)) return false;
			var o = (PassangerId) obj;
			return isLocal == o.isLocal && index == o.index;
		}

		public override int GetHashCode() {
			return index * (isLocal == null ? 0 : (isLocal == true ? 1 : -1));
		}
	}
}
