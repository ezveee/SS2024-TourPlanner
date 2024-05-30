using System.Net.Http;

namespace UI.Http;

public sealed class HttpClientSingleton
{
	private static readonly Lazy<HttpClient> lazyHttpClient =
		new(() =>
		{
			HttpClient client = new()
			{
				BaseAddress = new Uri("https://localhost:7144/") // TODO: change to not hardcoded uri
			};
			return client;
		});

	public static HttpClient Instance => lazyHttpClient.Value;

	private HttpClientSingleton() { }
}
