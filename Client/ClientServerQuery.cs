using Client;
using ClientCommunication;
using Common;

namespace Client {

    public sealed class ClientServerQuery {
        public static ClientService Create() {
            return ServerQuery<ClientService>.Create("net.tcp://localhost:8080/client-query");
        }
    }
}
