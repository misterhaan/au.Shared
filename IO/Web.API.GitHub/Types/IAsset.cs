namespace au.IO.Web.API.GitHub.Types {
	/// <summary>
	/// Information about a file associated with a release.  Subset of propertie
	/// from the GitHub API.
	/// </summary>
	public interface IAsset {
		string name { get; }
		long size { get; }
		string url { get; }
		string browser_download_url { get; }
	}
}
