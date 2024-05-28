using HttpServer.RequestHandling;
using System.Net;
using System.Text;

namespace HttpServer;
public static class Server
{
	public static HttpListener? Listener { get; set; }
	private static readonly string _url = "http://localhost:8000/"; // TODO: make constant; no hardcoded url
	private static Dictionary<string, RequestHandler> _routes = [];

	public static void Init()
	{
		InitialiseRoutes();
		// TODO: set url to constant maybe? idk if necessary
	}

	public static void Run()
	{
		Listener = new HttpListener();
		Listener.Prefixes.Add(_url);
		Listener.Start();
		Console.WriteLine("Listening for connections on {0}", _url);

		Task listenTask = HandleIncomingConnections();
		listenTask.GetAwaiter().GetResult();

		Listener.Close();
	}

	public static void Shutdown()
	{
		// TODO: implement server shutdown
	}

	public static async Task HandleIncomingConnections()
	{
		bool runServer = true;

		while (runServer)
		{
			HttpListenerContext context = await Listener.GetContextAsync();

			HttpListenerRequest request = context.Request;
			HttpListenerResponse response = context.Response;

			Console.WriteLine(request.Url.ToString());
			Console.WriteLine(request.HttpMethod);
			Console.WriteLine(request.UserHostName);
			Console.WriteLine(request.UserAgent);
			Console.WriteLine();

			await RouteRequest(request, response);

			// TODO: check if placement here is fine
			if (IsShutdownRequested(request))
			{
				Console.WriteLine("Server shutdown requested");
				runServer = false;
			}
		}
	}

	private static void InitialiseRoutes()
	{
		_routes["/tours"] = new TourHandler();
	}

	private static async Task RouteRequest(HttpListenerRequest request, HttpListenerResponse response)
	{
		if (request.Url is null)
		{
			throw new ArgumentNullException(nameof(request.Url)); // TODO: use different (custom) exception
		}

		if (!_routes.TryGetValue(request.Url.AbsolutePath, out RequestHandler? handler)) // TODO: RouteNotExistsGuard
		{
			// TODO: create method
			response.StatusCode = (int)HttpStatusCode.NotFound;
			byte[] data = Encoding.UTF8.GetBytes("404 - Not Found");
			await response.OutputStream.WriteAsync(data, 0, data.Length);
			response.Close();
			return;
		}

		handler.HandleRequest(request);
	}

	private static bool IsShutdownRequested(HttpListenerRequest request)
	{
		return request.HttpMethod == "POST" && request.Url.AbsolutePath == "/shutdown";
	}
}
