using System.Threading.Tasks;
using au.IO.Web.API.GitHub.Types;

namespace au.IO.Web.API.GitHub {
	/// <summary>
	/// A subset of commands from the GitHub repos API.
	/// </summary>
	internal class ReposApi : ApiBase, IReposApi {
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="username">Name of the user who owns the repository</param>
		/// <param name="repoName">Name of the repository</param>
		internal ReposApi(string username, string repoName)
			: base("repos", username, repoName) { }

		/// <inheritdoc />
		public async Task<IRelease> LatestRelease()
			=> await GetRequestAsync<Release>("releases/latest").ConfigureAwait(false);
	}
}
