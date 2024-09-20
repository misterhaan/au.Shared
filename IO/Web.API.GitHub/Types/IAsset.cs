namespace au.IO.Web.API.GitHub.Types {
	/// <summary>
	/// Information about a file associated with a release.  Subset of propertie
	/// from the GitHub API.
	/// </summary>
	public interface IAsset {
#pragma warning disable IDE1006 // Naming Styles
		/// <summary>
		/// Asset name
		/// </summary>
		string name { get; }

		/// <summary>
		/// Asset size
		/// </summary>
		long size { get; }

		/// <summary>
		/// Asset download URL
		/// </summary>
		string browser_download_url { get; }
#pragma warning restore IDE1006 // Naming Styles
	}
}
