﻿using System;
using System.Collections.Generic;
using System.Linq;
using au.IO.Web.API.GitHub.Types;

namespace au.IO.Web.API.GitHub {
	/// <summary>
	/// Information about a release.  Subset of properties from the GitHub API.
	/// </summary>
	internal class Release : IRelease {
		/// <inheritdoc />
		public string name { get; set; }

		/// <inheritdoc />
		public string tag_name { get; set; }

		/// <inheritdoc />
		public DateTime published_at { get; set; }

		/// <inheritdoc />
		public string body { get; set; }

		/// <inheritdoc />
		IReadOnlyList<IAsset> IRelease.assets
			=> _externalAssets;
		private IReadOnlyList<IAsset> _externalAssets = Array.Empty<IAsset>();

		/// <inheritdoc />
		public Asset[] assets {
			get => _assetsArray;
			set {
				_assetsArray = value ?? Array.Empty<Asset>();
				_externalAssets = _assetsArray.Select(a => a as IAsset).ToList().AsReadOnly();
			}
		}
		private Asset[] _assetsArray;
	}
}
