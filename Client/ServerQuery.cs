using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;

namespace Common {
    public class ServerQuery<T> : System.Runtime.Remoting.Proxies.RealProxy, IDisposable {
        private ChannelFactory<T> factory;

        private ServerQuery(string adress) : base(typeof(T)) {
            var endpoint = new EndpointAddress(adress);
            var binding = new NetTcpBinding();
            factory = new ChannelFactory<T>(binding, endpoint);
        }

        public static T Create(string adress) {
            return (T)new ServerQuery<T>(adress).GetTransparentProxy();
        }

        public void Dispose() {
            factory.Close();
        }

        public override IMessage Invoke(IMessage msg) {
            var methodCall = (IMethodCallMessage)msg;

            try {
                var method = (MethodInfo)methodCall.MethodBase;

                //https://stackoverflow.com/a/10833801/18704284
                var service = factory.CreateChannel();
                var ar = ((System.ServiceModel.Channels.IChannel)service).BeginOpen(null, null);
                if(!ar.AsyncWaitHandle.WaitOne(new TimeSpan(0, 0, 0, 0, 500), true)) throw new TimeoutException("Service is not available");
                ((System.ServiceModel.Channels.IChannel)service).EndOpen(ar);

                var result = method.Invoke(service, methodCall.InArgs);
                ((IClientChannel)service).Dispose();
                return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
            }
            catch(Exception e) {
                if(e is TargetInvocationException && e.InnerException != null) {
                    return new ReturnMessage(e.InnerException, msg as IMethodCallMessage);
                }
                else throw e;
            }
        }
    }
}
