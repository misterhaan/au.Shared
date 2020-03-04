namespace au.IO.Web.API.GitHub.Types {
	/// <summary>
	/// Factory for building clients for GitHub APIs.
	/// </summary>
	public interface IApiFactory {
		/// <summary>
		/// Build a Repos API client for the specified repository.
		/// </summary>
		/// <param name="username">GitHub username that owns the repository</param>
		/// <param name="repoName">Name of the repository</param>
		/// <returns>Repos API client</returns>
		IReposApi BuildReposApi(string username, string repoName);
	}
}
