using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AirlineTicketingServer {
	struct SqlConnectionView : IDisposable {
		public SqlConnection connection;
		private bool allowClose;

		public SqlConnectionView(SqlConnection connection, bool allowClose) {
			this.connection = connection;
			this.allowClose = allowClose;
		}

		public void Dispose() { if(allowClose) connection.Close(); }
	}
}
