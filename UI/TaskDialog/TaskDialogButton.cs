using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace au.UI.TaskDialog {
	/// <summary>
	/// A custom button for the TaskDialog.
	/// </summary>
	[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")] // Would be unused code as not required for usage.
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
	public struct TaskDialogButton {
		/// <summary>
		/// The ID of the button. This value is returned by TaskDialog.Show when the button is clicked.
		/// </summary>
		private int buttonId;

		/// <summary>
		/// The string that appears on the button.
		/// </summary>
		[MarshalAs(UnmanagedType.LPWStr)]
		private string buttonText;

		/// <summary>
		/// Initialize the custom button.
		/// </summary>
		/// <param name="id">The ID of the button. This value is returned by TaskDialog.Show when
		/// the button is clicked. Typically this will be a value in the DialogResult enum.</param>
		/// <param name="text">The string that appears on the button.</param>
		public TaskDialogButton(int id, string text) {
			buttonId = id;
			buttonText = text;
		}

		/// <summary>
		/// Initialize the custom button.
		/// </summary>
		/// <param name="id">The ID of the button. This value is returned by TaskDialog.Show when
		/// the button is clicked. Typically this will be a value in the DialogResult enum.</param>
		/// <param name="title">The title string that appears on the button (larger font).</param>
		/// <param name="description">The description string that appears on the button (smaller font).</param>
		public TaskDialogButton(int id, string title, string description)
			: this(id, title + "\n" + description) { }

		/// <summary>
		/// The ID of the button. This value is returned by TaskDialog.Show when the button is clicked.
		/// </summary>
		public int ButtonId {
			get { return buttonId; }
			set { buttonId = value; }
		}

		/// <summary>
		/// The string that appears on the button.
		/// </summary>
		public string ButtonText {
			get { return buttonText; }
			set { buttonText = value; }
		}
	}
}
