using System;

namespace au.UI.TaskDialog {
	/// <summary>
	/// The TaskDialog common button flags used to specify the builtin bottons to show in the TaskDialog.
	/// </summary>
	[Flags]
	public enum TaskDialogCommonButtons {
		/// <summary>
		/// No common buttons.
		/// </summary>
		None = 0,

		/// <summary>
		/// OK common button. If selected Task Dialog will return DialogResult.OK.
		/// </summary>
		Ok = 0x0001,

		/// <summary>
		/// Yes common button. If selected Task Dialog will return DialogResult.Yes.
		/// </summary>
		Yes = 0x0002,

		/// <summary>
		/// No common button. If selected Task Dialog will return DialogResult.No.
		/// </summary>
		No = 0x0004,

		/// <summary>
		/// Cancel common button. If selected Task Dialog will return DialogResult.Cancel.
		/// If this button is specified, the dialog box will respond to typical cancel actions (Alt-F4 and Escape).
		/// </summary>
		Cancel = 0x0008,

		/// <summary>
		/// Retry common button. If selected Task Dialog will return DialogResult.Retry.
		/// </summary>
		Retry = 0x0010,

		/// <summary>
		/// Close common button. If selected Task Dialog will return this value.
		/// </summary>
		Close = 0x0020,
	}
}
