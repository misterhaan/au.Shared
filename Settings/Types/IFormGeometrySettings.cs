using System.Drawing;
using System.Windows.Forms;

namespace au.Settings.Types {
	/// <summary>
	/// Settings for how a form is displayed.
	/// </summary>
	public interface IFormGeometrySettings {
		/// <summary>
		/// State of the form.
		/// </summary>
		FormWindowState WindowState { get; set; }
		/// <summary>
		/// Location of the form when not maximized or minimized.
		/// </summary>
		Point? Location { get; set; }
		/// <summary>
		/// Size of the form when not maximized or minimized.
		/// </summary>
		Size? Size { get; set; }
	}
}
