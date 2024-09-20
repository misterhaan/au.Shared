using System;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using au.IO.Web.API.GitHub.Types;

namespace au.IO.Web.API.GitHub {
	/// <summary>
	/// Checks for an update to the running application.  Uses the GitHub API.
	/// </summary>
	public class UpdateChecker : IUpdateChecker {
		private readonly IApiFactory _apiFactory;
		private readonly string _username;
		private readonly string _repoName;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="username">GitHub username that owns the repository</param>
		/// <param name="repoName">Name of the repository</param>
		public UpdateChecker(string username, string repoName)
			: this(username, repoName, new ApiFactory()) { }

		/// <summary>
		/// Testing constructor
		/// </summary>
		/// <param name="username">GitHub username that owns the repository</param>
		/// <param name="repoName">Name of the repository</param>
		/// <param name="apiFactory">Object that can create API clients</param>
		internal UpdateChecker(string username, string repoName, IApiFactory apiFactory) {
			_username = username;
			_repoName = repoName;
			_apiFactory = apiFactory;
		}

		/// <inheritdoc />
		public async Task<IUpdateCheckResult> CheckAsync() {
			IReposApi api = _apiFactory.BuildReposApi(_username, _repoName);
			try {
				IRelease latest = await api.LatestRelease().ConfigureAwait(false);
				Version latestVersion = new(latest.tag_name.TrimStart('v'));
				return latestVersion > ApplicationVersion
					? latest.assets.Any()
						? UpdateCheckResult.FromLatestRelease(latest)
						: new UpdateCheckResult(EventLevel.Error, Messages.NoUpdateTitle, Messages.NoUpdateNoFiles)
					: new UpdateCheckResult(EventLevel.Informational, Messages.NoUpdateTitle, Messages.NoUpdateAlreadyLatest);
			} catch(WebException wex) {
				switch(wex.Status) {
					case WebExceptionStatus.NameResolutionFailure:
					case WebExceptionStatus.ConnectFailure:
						return new UpdateCheckResult(EventLevel.Error, Messages.NoUpdateTitle, Messages.NoUpdateCannotConnect);
					case WebExceptionStatus.ProtocolError:
						if(wex.Response is HttpWebResponse { StatusCode: HttpStatusCode.NotFound })
							return new UpdateCheckResult(EventLevel.Error, Messages.NoUpdateTitle, Messages.NoUpdateNoReleases);
						goto default;  // we only handled not found, so anything else is a random other error
					default:
						return new UpdateCheckResult(EventLevel.Error, Messages.NoUpdateTitle, string.Format(Messages.NoUpdateError, wex.Message));
				}
			}
		}

		/// <summary>
		/// Get the version of the application.
		/// </summary>
		private static Version ApplicationVersion
			=> Assembly.GetEntryAssembly().GetName().Version;
	}
}
