using System;
using System.Diagnostics.Tracing;

namespace au.IO.Web.API.GitHub.Types {
	/// <summary>
	/// Result of checking for an update.
	/// </summary>
	public interface IUpdateCheckResult {
		/// <summary>
		/// Whether an update is available
		/// </summary>
		bool Available { get; }

		/// <summary>
		/// Severity level of this result
		/// </summary>
		EventLevel Level { get; }

		/// <summary>
		/// Name of the latest release or title of why we can't upgrade
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Description of the latest release or details of why we can't upgrade
		/// </summary>
		string Description { get; }

		/// <summary>
		/// File size of the latest update (zero if Available is false)
		/// </summary>
		long Size { get; }

		/// <summary>
		/// Download URL for the lastest update (null if Available is false)
		/// </summary>
		Uri Url { get; }
	}
}
