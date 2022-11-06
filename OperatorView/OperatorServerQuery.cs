using Common;
using OperatorCommunication;


namespace Operator {

	public sealed class OperatorServerQuery {
		public static OperatorService Create() {
			return ServerQuery<OperatorService>.Create("net.tcp://localhost:8080/operator-view");
		}
	}
}
