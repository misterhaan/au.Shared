using System;
using System.Collections.Generic;

namespace au.IO.Web.API.GitHub.Types {
	/// <summary>
	/// Information about a release.  Subset of properties from the GitHub API.
	/// </summary>
	public interface IRelease {
		string name { get; }
		string tag_name { get; }
		DateTime published_at { get; }
		string body { get; }
		IReadOnlyList<IAsset> assets { get; }
	}
}
