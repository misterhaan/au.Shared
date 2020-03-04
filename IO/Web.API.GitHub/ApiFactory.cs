using au.IO.Web.API.GitHub.Types;

namespace au.IO.Web.API.GitHub {
	/// <summary>
	/// Factory for building clients for GitHub APIs.
	/// </summary>
	public class ApiFactory : IApiFactory {
		/// <inheritdoc />
		public IReposApi BuildReposApi(string username, string repoName)
			=> new ReposApi(username, repoName);
	}
}
