using System;
using System.Collections.Generic;

namespace au.IO.Web.API.GitHub.Types {
	/// <summary>
	/// Information about a release.  Subset of properties from the GitHub API.
	/// </summary>
	public interface IRelease {
#pragma warning disable IDE1006 // Naming Styles
		/// <summary>
		/// Release name
		/// </summary>
		string name { get; }

		/// <summary>
		/// Name of the tag in the repo that corresponds to this release
		/// </summary>
		string tag_name { get; }

		/// <summary>
		/// When the release was published
		/// </summary>
		DateTime published_at { get; }

		/// <summary>
		/// Release description
		/// </summary>
		string body { get; }

		/// <summary>
		/// Files included in the release
		/// </summary>
		IReadOnlyList<IAsset> assets { get; }
#pragma warning restore IDE1006 // Naming Styles
	}
}
