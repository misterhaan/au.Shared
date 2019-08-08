using System.Diagnostics.CodeAnalysis;

namespace au.UI.TaskDialog {
	/// <summary>
	/// The System icons the TaskDialog supports.
	/// </summary>
	[SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32")] // Type comes from CommCtrl.h
	public enum TaskDialogIcon : uint {
		/// <summary>
		/// No Icon.
		/// </summary>
		None = 0,

		/// <summary>
		/// System warning icon.
		/// </summary>
		Warning = 0xFFFF, // MAKEINTRESOURCEW(-1)

		/// <summary>
		/// System Error icon.
		/// </summary>
		Error = 0xFFFE, // MAKEINTRESOURCEW(-2)

		/// <summary>
		/// System Information icon.
		/// </summary>
		Information = 0xFFFD, // MAKEINTRESOURCEW(-3)

		/// <summary>
		/// Shield icon.
		/// </summary>
		Shield = 0xFFFC, // MAKEINTRESOURCEW(-4)
	}
}
