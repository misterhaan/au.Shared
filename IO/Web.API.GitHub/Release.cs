using System;
using System.Collections.Generic;
using System.Linq;
using au.IO.Web.API.GitHub.Types;

namespace au.IO.Web.API.GitHub {
	/// <summary>
	/// Information about a release.  Subset of properties from the GitHub API.
	/// </summary>
	internal class Release : IRelease {
		public string name { get; set; }

		public string tag_name { get; set; }

		public DateTime published_at { get; set; }

		public string body { get; set; }

		IReadOnlyList<IAsset> IRelease.assets
			=> assets.Select(a => a as IAsset).ToList().AsReadOnly();

		public Asset[] assets { get; set; }
	}
}
