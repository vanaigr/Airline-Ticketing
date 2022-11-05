using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Server {
	public static class SqlConnectionOpen {
		public static void Open2(this SqlConnection connection) {
			if(connection != null && connection.State == System.Data.ConnectionState.Closed) 
				connection.Open();
		}
	}
	public struct SqlConnectionView : IDisposable {
		public SqlConnection connection;
		private bool canClose, canDispose;

		public SqlConnectionView(SqlConnection connection, bool allowClose/*misleading naming*/) {
			this.connection = connection;
			this.canDispose = allowClose;
			this.canClose = this.canDispose;
		}

		public SqlConnectionView(SqlConnection connection, bool canClose, bool canDispose) {
			this.connection = connection;
			this.canClose = canClose;
			this.canDispose = canDispose;
		}

		public void Open() {
			connection.Open2();
		}

		public void Close() { if(canClose && connection != null) connection.Close(); }

		public void Dispose() { 
			if(canDispose && connection != null) connection.Dispose();
			else Close();
		}
	}
}
