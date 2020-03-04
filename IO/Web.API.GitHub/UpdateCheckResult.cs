using System;
using System.Diagnostics.Tracing;
using System.Linq;
using au.IO.Web.API.GitHub.Types;

namespace au.IO.Web.API.GitHub {
	/// <summary>
	/// Result object returned by UpdateChecker.
	/// </summary>
	internal class UpdateCheckResult : IUpdateCheckResult {
		/// <summary>
		/// Update available constructor
		/// </summary>
		/// <param name="name">Name of the latest release</param>
		/// <param name="description">Description of the latest release</param>
		/// <param name="size">File size of the latest update</param>
		/// <param name="url">Download URL for the lastest update</param>
		internal UpdateCheckResult(string name, string description, long size, string url)
			: this(name, description, size, new Uri(url)) { }

		/// <summary>
		/// Update available constructor
		/// </summary>
		/// <param name="name">Name of the latest release</param>
		/// <param name="description">Description of the latest release</param>
		/// <param name="size">File size of the latest update</param>
		/// <param name="url">Download URL for the lastest update</param>
		internal UpdateCheckResult(string name, string description, long size, Uri url) {
			Available = true;
			Level = EventLevel.Informational;
			Name = name;
			Description = description;
			Size = size;
			Url = url;
		}

		/// <summary>
		/// Update not available constructor
		/// </summary>
		/// <param name="level">Severity level of this result</param>
		/// <param name="title">Title of why we can't upgrade</param>
		/// <param name="details">Details of why we can't upgrade</param>
		internal UpdateCheckResult(EventLevel level, string title, string details) {
			Available = false;
			Level = level;
			Name = title;
			Description = details;
		}

		/// <inheritdoc />
		public bool Available { get; }

		/// <inheritdoc />
		public EventLevel Level { get; }

		/// <inheritdoc />
		public string Name { get; }

		/// <inheritdoc />
		public string Description { get; }

		/// <inheritdoc />
		public long Size { get; }

		/// <inheritdoc />
		public Uri Url { get; }

		/// <summary>
		/// Create a result from a release.  Use when there is a newer release to upgrade to.
		/// </summary>
		/// <param name="latest">Latest release</param>
		/// <returns>Result with update available</returns>
		internal static UpdateCheckResult FromLatestRelease(IRelease latest) {
			string bitnessFilter = Environment.Is64BitProcess
				? "_x64"
				: "_x86";
			IAsset bestAsset = latest.assets.FirstOrDefault(asset => asset.name.Contains(bitnessFilter)) ?? latest.assets.FirstOrDefault();
			return new UpdateCheckResult(latest.name, latest.body, bestAsset.size, bestAsset.browser_download_url);
		}
	}
}
