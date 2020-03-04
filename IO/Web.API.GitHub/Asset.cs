using au.IO.Web.API.GitHub.Types;

namespace au.IO.Web.API.GitHub {
	/// <summary>
	/// Information about a file associated with a release.  Subset of properties
	/// from the GitHub API.
	/// </summary>
	internal class Asset : IAsset {
		/// <inheritdoc />
		public string name { get; set; }

		/// <inheritdoc />
		public long size { get; set; }

		/// <inheritdoc />
		public string browser_download_url { get; set; }
	}
}
