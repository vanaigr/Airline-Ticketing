using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client {
	public class CustomerData {
		public Communication.Customer? customer;
		public Dictionary<int, Communication.Passanger> passangers; 

		public CustomerData() { }

		public CustomerData(string login, string password) {
			this.customer = new Communication.Customer{
				login = login,
				password = password
			};
		}

		public void unlogin() {
			customer = null;
		}

		public void setFrom(Communication.Customer o) {
			this.customer = o;
		}

		public bool LoggedIn{
			get{ return customer != null; }
		}
	}
}
