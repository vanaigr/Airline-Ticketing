using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AirlineTicketingServer {
	public struct SqlConnectionView : IDisposable {
		public SqlConnection connection;
		private bool allowClose;

		public SqlConnectionView(SqlConnection connection, bool allowClose) {
			this.connection = connection;
			this.allowClose = allowClose;
		}

		public void Open() {
			if(connection.State != System.Data.ConnectionState.Open) 
				connection.Open();
		}

		public void Dispose() { if(allowClose && connection != null) connection.Dispose(); }
	}
}
