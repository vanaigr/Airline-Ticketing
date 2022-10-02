using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Text;

namespace Client {
	public delegate void Action(Communication.MessageService serivce);
	
	public class ServerQuery : System.Runtime.Remoting.Proxies.RealProxy {
	    private ChannelFactory<Communication.MessageService> factory;
	
	    private ServerQuery() : base(typeof(Communication.MessageService)) {
	        string adress = "net.tcp://localhost:8080/client-query";
			var endpoint = new EndpointAddress(adress);
			var binding = new NetTcpBinding();
			factory = new ChannelFactory<Communication.MessageService>(binding, endpoint);
	    }
	
	    public static Communication.MessageService Create() {
	        return (Communication.MessageService) new ServerQuery().GetTransparentProxy();
	    }
	
	    public override IMessage Invoke(IMessage msg) {
			var methodCall = (IMethodCallMessage) msg;
	        
			try {
				var method = (MethodInfo) methodCall.MethodBase;
				var service = factory.CreateChannel();
	            var result = method.Invoke(service, methodCall.InArgs);
				((IClientChannel) service).Dispose();
	            return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
	        }
	        catch (Exception e) {
	            if (e is TargetInvocationException && e.InnerException != null) {
	                return new ReturnMessage(e.InnerException, msg as IMethodCallMessage);
				} 
				else throw e;
	        }
	    }
	}
}
