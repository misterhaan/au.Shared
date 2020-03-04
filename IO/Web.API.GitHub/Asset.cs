using au.IO.Web.API.GitHub.Types;

namespace au.IO.Web.API.GitHub {
	/// <summary>
	/// Information about a file associated with a release.  Subset of propertie
	/// from the GitHub API.
	/// </summary>
	internal class Asset : IAsset {
		public string name { get; set; }

		public long size { get; set; }

		public string url { get; set; }

		public string browser_download_url { get; set; }
	}
}
