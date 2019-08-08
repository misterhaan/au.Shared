using System.Diagnostics.CodeAnalysis;

namespace au.UI.TaskDialog {
	/// <summary>
	/// Progress bar state.
	/// </summary>
	[SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]  // Comes from CommCtrl.h PBST_* values which don't have a zero.
	public enum ProgressBarState {
		/// <summary>
		/// Normal.
		/// </summary>
		Normal = 1,

		/// <summary>
		/// Error state.
		/// </summary>
		Error = 2,

		/// <summary>
		/// Paused state.
		/// </summary>
		Paused = 3
	}
}
