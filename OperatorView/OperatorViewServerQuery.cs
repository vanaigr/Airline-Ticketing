using Common;
using OperatorViewCommunication;


namespace OperatorView {

	public sealed class OperatorViewServerQuery {
		public static MessageService Create() {
			return ServerQuery<MessageService>.Create("net.tcp://localhost:8080/operator-view");
		}
	}
}
