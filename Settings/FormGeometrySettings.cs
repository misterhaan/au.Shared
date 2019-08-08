using System.Drawing;
using System.Windows.Forms;
using au.Settings.Types;

namespace au.Settings {
	/// <summary>
	/// Settings for how a form is displayed.
	/// </summary>
	public class FormGeometrySettings : IFormGeometrySettings {
		/// <summary>
		/// State of the form.
		/// </summary>
		public FormWindowState WindowState { get; set; } = FormWindowState.Normal;
		/// <summary>
		/// Location of the form when not maximized or minimized.
		/// </summary>
		public Point? Location { get; set; } = null;
		/// <summary>
		/// Size of the form when not maximized or minimized.
		/// </summary>
		public Size? Size { get; set; } = null;
	}
}
