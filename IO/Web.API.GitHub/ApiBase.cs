using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace au.IO.Web.API.GitHub {
	/// <summary>
	/// GitHub API base class
	/// </summary>
	internal abstract class ApiBase {
		private const string _hostname = "api.github.com";

		/// <summary>
		/// GitHub Web API base URL for this API
		/// </summary>
		protected readonly Uri _urlBase;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="path">URL path components common to all requests to this API.  Parameters are joined with a path separator.</param>
		protected ApiBase(params string[] path) {
			_urlBase = new UriBuilder(Uri.UriSchemeHttps, _hostname, -1, string.Join("/", path) + "/").Uri;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
		}

		/// <summary>
		/// Peform a basic GET request and deserialize the JSON result.
		/// </summary>
		/// <typeparam name="T">Deserialize JSON to this type</typeparam>
		/// <param name="relativeUrl">Request URL relative to the base URL built in the constructor</param>
		/// <returns>API response as the requested type</returns>
		protected async Task<T> GetRequestAsync<T>(string relativeUrl) {
			HttpWebRequest request = WebRequest.CreateHttp(new Uri(_urlBase, relativeUrl));
			request.UserAgent = "misterhaan/au.Shared";  // GitHub requires a useragent and requests that it contain the repo containing the code
			using WebResponse response = await request.GetResponseAsync().ConfigureAwait(false);
			using Stream responseStream = response.GetResponseStream();
			using StreamReader reader = new StreamReader(responseStream);
			using JsonReader json = new JsonTextReader(reader);
			return new JsonSerializer().Deserialize<T>(json);
		}
	}
}
