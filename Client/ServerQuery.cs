using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Client {
	public class ServerQuery {
		private ChannelFactory<Communication.MessageService> factory;

		public ServerQuery() {
			string adress = "net.tcp://localhost:8080/client-query";
			var endpoint = new EndpointAddress(adress);
			var binding = new NetTcpBinding();
			factory = new ChannelFactory<Communication.MessageService>(binding, endpoint);
		}

		public Communication.Response query(Communication.Params parameter) {
			var service = factory.CreateChannel();
			var result = service.execute(parameter);
			((IClientChannel) service).Close();
			return result;
		}

		public List<Communication.Response> queryMultiple(List<Communication.Params> parameters) {
			var service = factory.CreateChannel();
			var results = new List<Communication.Response>();
			foreach(var parameter in parameters) {
				var result = service.execute(parameter);
				results.Add(result);
			}
			((IClientChannel)service).Close();
			return results;
		}
	}
}
