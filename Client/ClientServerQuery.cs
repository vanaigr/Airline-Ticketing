using ClientCommunication;
using Common;

namespace Client {

	public sealed class ClientServerQuery {
		public static MessageService Create() {
			return ServerQuery<MessageService>.Create("net.tcp://localhost:8080/client-query");
		}
	}
}
