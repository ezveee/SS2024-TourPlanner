using System.Net;

namespace HttpServer.RequestHandling;
public abstract class RequestHandler
{
	public abstract void HandleRequest(HttpListenerRequest request);
}
