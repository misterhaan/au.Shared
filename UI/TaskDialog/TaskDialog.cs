using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace au.UI.TaskDialog {
	/// <summary>
	/// A Task Dialog. This is like a MessageBox but with many more features. TaskDialog requires Windows Vista or later.
	/// </summary>
	public class TaskDialog {
		/// <summary>
		/// The flags passed to TaskDialogIndirect.
		/// </summary>
		private UnsafeNativeMethods.TASKDIALOG_FLAGS _flags;

		/// <summary>
		/// Creates a default Task Dialog.
		/// </summary>
		public TaskDialog() {
			Reset();
		}

		/// <summary>
		/// Returns true if the current operating system supports TaskDialog. If false TaskDialog.Show should not
		/// be called as the results are undefined but often results in a crash.
		/// </summary>
		public static bool IsAvailableOnThisOS {
			get {
				OperatingSystem os = Environment.OSVersion;
				return os.Platform == PlatformID.Win32NT && os.Version.CompareTo(TaskDialog.RequiredOSVersion) >= 0;
			}
		}

		/// <summary>
		/// The minimum Windows version needed to support TaskDialog.
		/// </summary>
		public static Version RequiredOSVersion {
			get { return new Version(6, 0, 5243); }
		}

		/// <summary>
		/// The string to be used for the dialog box title. If this parameter is NULL, the filename of the executable program is used.
		/// </summary>
		public string WindowTitle { get; set; }

		/// <summary>
		/// The string to be used for the main instruction.
		/// </summary>
		public string MainInstruction { get; set; }

		/// <summary>
		/// The string to be used for the dialog’s primary content. If the EnableHyperlinks member is true,
		/// then this string may contain hyperlinks in the form: <A HREF="executablestring">Hyperlink Text</A>. 
		/// WARNING: Enabling hyperlinks when using content from an unsafe source may cause security vulnerabilities.
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Specifies the push buttons displayed in the dialog box. This parameter may be a combination of flags.
		/// If no common buttons are specified and no custom buttons are specified using the Buttons member, the
		/// dialog box will contain the OK button by default.
		/// </summary>
		public TaskDialogCommonButtons CommonButtons { get; set; }

		/// <summary>
		/// Specifies a built in icon for the main icon in the dialog. If this is set to none
		/// and the CustomMainIcon is null then no main icon will be displayed.
		/// </summary>
		public TaskDialogIcon MainIcon { get; set; }

		/// <summary>
		/// Specifies a custom in icon for the main icon in the dialog. If this is set to none
		/// and the CustomMainIcon member is null then no main icon will be displayed.
		/// </summary>
		public Icon CustomMainIcon { get; set; }

		/// <summary>
		/// Specifies a built in icon for the icon to be displayed in the footer area of the
		/// dialog box. If this is set to none and the CustomFooterIcon member is null then no
		/// footer icon will be displayed.
		/// </summary>
		public TaskDialogIcon FooterIcon { get; set; }

		/// <summary>
		/// Specifies a custom icon for the icon to be displayed in the footer area of the
		/// dialog box. If this is set to none and the CustomFooterIcon member is null then no
		/// footer icon will be displayed.
		/// </summary>
		public Icon CustomFooterIcon { get; set; }

		/// <summary>
		/// Specifies the custom push buttons to display in the dialog. Use CommonButtons member for
		/// common buttons; OK, Yes, No, Retry and Cancel, and Buttons when you want different text
		/// on the push buttons.
		/// </summary>
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")] // Style of use is like single value. Array is of value types.
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")] // Returns a reference, not a copy.
		public TaskDialogButton[] Buttons { get; set; }

		/// <summary>
		/// Specifies the radio buttons to display in the dialog.
		/// </summary>
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")] // Style of use is like single value. Array is of value types.
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")] // Returns a reference, not a copy.
		public TaskDialogButton[] RadioButtons { get; set; }

		/// <summary>
		/// Enables hyperlink processing for the strings specified in the Content, ExpandedInformation
		/// and FooterText members. When enabled, these members may be strings that contain hyperlinks
		/// in the form: <A HREF="executablestring">Hyperlink Text</A>. 
		/// WARNING: Enabling hyperlinks when using content from an unsafe source may cause security vulnerabilities.
		/// Note: Task Dialog will not actually execute any hyperlinks. Hyperlink execution must be handled
		/// in the callback function specified by Callback member.
		/// </summary>
		public bool EnableHyperlinks {
			get { return HasFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_ENABLE_HYPERLINKS); }
			set { SetFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_ENABLE_HYPERLINKS, value); }
		}

		/// <summary>
		/// Indicates that the dialog should be able to be closed using Alt-F4, Escape and the title bar’s
		/// close button even if no cancel button is specified in either the CommonButtons or Buttons members.
		/// </summary>
		public bool AllowDialogCancellation {
			get { return HasFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_ALLOW_DIALOG_CANCELLATION); }
			set { SetFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_ALLOW_DIALOG_CANCELLATION, value); }
		}

		/// <summary>
		/// Indicates that the buttons specified in the Buttons member should be displayed as command links
		/// (using a standard task dialog glyph) instead of push buttons.  When using command links, all
		/// characters up to the first new line character in the ButtonText member (of the TaskDialogButton
		/// structure) will be treated as the command link’s main text, and the remainder will be treated
		/// as the command link’s note. This flag is ignored if the Buttons member has no entires.
		/// </summary>
		public bool UseCommandLinks {
			get { return HasFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_USE_COMMAND_LINKS); }
			set { SetFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_USE_COMMAND_LINKS, value); }
		}

		/// <summary>
		/// Indicates that the buttons specified in the Buttons member should be displayed as command links
		/// (without a glyph) instead of push buttons. When using command links, all characters up to the
		/// first new line character in the ButtonText member (of the TaskDialogButton structure) will be
		/// treated as the command link’s main text, and the remainder will be treated as the command link’s
		/// note. This flag is ignored if the Buttons member has no entires.
		/// </summary>
		public bool UseCommandLinksNoIcon {
			get { return HasFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_USE_COMMAND_LINKS_NO_ICON); }
			set { SetFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_USE_COMMAND_LINKS_NO_ICON, value); }
		}

		/// <summary>
		/// Indicates that the string specified by the ExpandedInformation member should be displayed at the
		/// bottom of the dialog’s footer area instead of immediately after the dialog’s content. This flag
		/// is ignored if the ExpandedInformation member is null.
		/// </summary>
		public bool ExpandFooterArea {
			get { return HasFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_EXPAND_FOOTER_AREA); }
			set { SetFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_EXPAND_FOOTER_AREA, value); }
		}

		/// <summary>
		/// Indicates that the string specified by the ExpandedInformation member should be displayed
		/// when the dialog is initially displayed. This flag is ignored if the ExpandedInformation member
		/// is null.
		/// </summary>
		public bool ExpandedByDefault {
			get { return HasFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_EXPANDED_BY_DEFAULT); }
			set { SetFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_EXPANDED_BY_DEFAULT, value); }
		}

		/// <summary>
		/// Indicates that the verification checkbox in the dialog should be checked when the dialog is
		/// initially displayed. This flag is ignored if the VerificationText parameter is null.
		/// </summary>
		public bool VerificationFlagChecked {
			get { return HasFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_VERIFICATION_FLAG_CHECKED); }
			set { SetFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_VERIFICATION_FLAG_CHECKED, value); }
		}

		/// <summary>
		/// Indicates that a Progress Bar should be displayed.
		/// </summary>
		public bool ShowProgressBar {
			get { return HasFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_SHOW_PROGRESS_BAR); }
			set { SetFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_SHOW_PROGRESS_BAR, value); }
		}

		/// <summary>
		/// Indicates that an Marquee Progress Bar should be displayed.
		/// </summary>
		public bool ShowMarqueeProgressBar {
			get { return HasFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_SHOW_MARQUEE_PROGRESS_BAR); }
			set { SetFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_SHOW_MARQUEE_PROGRESS_BAR, value); }
		}

		/// <summary>
		/// Indicates that the TaskDialog’s callback should be called approximately every 200 milliseconds.
		/// </summary>
		public bool CallbackTimer {
			get { return HasFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_CALLBACK_TIMER); }
			set { SetFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_CALLBACK_TIMER, value); }
		}

		/// <summary>
		/// Indicates that the TaskDialog should be positioned (centered) relative to the owner window
		/// passed when calling Show. If not set (or no owner window is passed), the TaskDialog is
		/// positioned (centered) relative to the monitor.
		/// </summary>
		public bool PositionRelativeToWindow {
			get { return HasFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_POSITION_RELATIVE_TO_WINDOW); }
			set { SetFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_POSITION_RELATIVE_TO_WINDOW, value); }
		}

		/// <summary>
		/// Indicates that the TaskDialog should have right to left layout.
		/// </summary>
		public bool RightToLeftLayout {
			get { return HasFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_RTL_LAYOUT); }
			set { SetFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_RTL_LAYOUT, value); }
		}

		/// <summary>
		/// Indicates that the TaskDialog should have no default radio button.
		/// </summary>
		public bool NoDefaultRadioButton {
			get { return HasFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_NO_DEFAULT_RADIO_BUTTON); }
			set { SetFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_NO_DEFAULT_RADIO_BUTTON, value); }
		}

		/// <summary>
		/// Indicates that the TaskDialog can be minimised. Works only if there if parent window is null. Will enable cancellation also.
		/// </summary>
		public bool CanBeMinimized {
			get { return HasFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_CAN_BE_MINIMIZED); }
			set { SetFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_CAN_BE_MINIMIZED, value); }
		}

		/// <summary>
		/// Indicates the default button for the dialog. This may be any of the values specified
		/// in ButtonId members of one of the TaskDialogButton structures in the Buttons array,
		/// or one a DialogResult value that corresponds to a buttons specified in the CommonButtons Member.
		/// If this member is zero or its value does not correspond to any button ID in the dialog,
		/// then the first button in the dialog will be the default. 
		/// </summary>
		public int DefaultButton { get; set; }

		/// <summary>
		/// Indicates the default radio button for the dialog. This may be any of the values specified
		/// in ButtonId members of one of the TaskDialogButton structures in the RadioButtons array.
		/// If this member is zero or its value does not correspond to any radio button ID in the dialog,
		/// then the first button in RadioButtons will be the default.
		/// The property NoDefaultRadioButton can be set to have no default.
		/// </summary>
		public int DefaultRadioButton { get; set; }

		/// <summary>
		/// The string to be used to label the verification checkbox. If this member is null, the
		/// verification checkbox is not displayed in the dialog box.
		/// </summary>
		public string VerificationText { get; set; }

		/// <summary>
		/// The string to be used for displaying additional information. The additional information is
		/// displayed either immediately below the content or below the footer text depending on whether
		/// the ExpandFooterArea member is true. If the EnameHyperlinks member is true, then this string
		/// may contain hyperlinks in the form: <A HREF="executablestring">Hyperlink Text</A>.
		/// WARNING: Enabling hyperlinks when using content from an unsafe source may cause security vulnerabilities.
		/// </summary>
		public string ExpandedInformation { get; set; }

		/// <summary>
		/// The string to be used to label the button for collapsing the expanded information. This
		/// member is ignored when the ExpandedInformation member is null. If this member is null
		/// and the CollapsedControlText is specified, then the CollapsedControlText value will be
		/// used for this member as well.
		/// </summary>
		public string ExpandedControlText { get; set; }

		/// <summary>
		/// The string to be used to label the button for expanding the expanded information. This
		/// member is ignored when the ExpandedInformation member is null.  If this member is null
		/// and the ExpandedControlText is specified, then the ExpandedControlText value will be
		/// used for this member as well.
		/// </summary>
		public string CollapsedControlText { get; set; }

		/// <summary>
		/// The string to be used in the footer area of the dialog box. If the EnableHyperlinks member
		/// is true, then this string may contain hyperlinks in the form: <A HREF="executablestring">
		/// Hyperlink Text</A>.
		/// WARNING: Enabling hyperlinks when using content from an unsafe source may cause security vulnerabilities.
		/// </summary>
		public string Footer { get; set; }

		/// <summary>
		/// width of the Task Dialog's client area in DLU's. If 0, Task Dialog will calculate the ideal width.
		/// </summary>
		public uint Width { get; set; }

		/// <summary>
		/// The callback that receives messages from the Task Dialog when various events occur.
		/// </summary>
		public TaskDialogCallback Callback { get; set; }

		/// <summary>
		/// Reference that is passed to the callback.
		/// </summary>
		public object CallbackData { get; set; }

		/// <summary>
		/// Resets the Task Dialog to the state when first constructed, all properties set to their default value.
		/// </summary>
		public void Reset() {
			WindowTitle = null;
			MainInstruction = null;
			Content = null;
			CommonButtons = 0;
			MainIcon = TaskDialogIcon.None;
			CustomMainIcon = null;
			FooterIcon = TaskDialogIcon.None;
			CustomFooterIcon = null;
			Buttons = new TaskDialogButton[0];
			RadioButtons = new TaskDialogButton[0];
			_flags = 0;
			DefaultButton = 0;
			DefaultRadioButton = 0;
			VerificationText = null;
			ExpandedInformation = null;
			ExpandedControlText = null;
			CollapsedControlText = null;
			Footer = null;
			Callback = null;
			CallbackData = null;
			Width = 0;
		}

		/// <summary>
		/// Creates, displays, and operates a task dialog. The task dialog contains application-defined messages, title,
		/// verification check box, command links and push buttons, plus any combination of predefined icons and push buttons
		/// as specified on the other members of the class before calling Show.
		/// </summary>
		/// <returns>The result of the dialog, either a DialogResult value for common push buttons set in the CommonButtons
		/// member or the ButtonID from a TaskDialogButton structure set on the Buttons member.</returns>
		public int Show() {
			return Show(IntPtr.Zero, out bool verificationFlagChecked, out int radioButtonResult);
		}

		/// <summary>
		/// Creates, displays, and operates a task dialog. The task dialog contains application-defined messages, title,
		/// verification check box, command links and push buttons, plus any combination of predefined icons and push buttons
		/// as specified on the other members of the class before calling Show.
		/// </summary>
		/// <param name="owner">Owner window the task Dialog will modal to.</param>
		/// <returns>The result of the dialog, either a DialogResult value for common push buttons set in the CommonButtons
		/// member or the ButtonID from a TaskDialogButton structure set on the Buttons member.</returns>
		public int Show(IWin32Window owner) {
			return Show(owner?.Handle ?? IntPtr.Zero, out bool verificationFlagChecked, out int radioButtonResult);
		}

		/// <summary>
		/// Creates, displays, and operates a task dialog. The task dialog contains application-defined messages, title,
		/// verification check box, command links and push buttons, plus any combination of predefined icons and push buttons
		/// as specified on the other members of the class before calling Show.
		/// </summary>
		/// <param name="hwndOwner">Owner window the task Dialog will modal to.</param>
		/// <returns>The result of the dialog, either a DialogResult value for common push buttons set in the CommonButtons
		/// member or the ButtonID from a TaskDialogButton structure set on the Buttons member.</returns>
		public int Show(IntPtr hwndOwner) {
			return Show(hwndOwner, out bool verificationFlagChecked, out int radioButtonResult);
		}

		/// <summary>
		/// Creates, displays, and operates a task dialog. The task dialog contains application-defined messages, title,
		/// verification check box, command links and push buttons, plus any combination of predefined icons and push buttons
		/// as specified on the other members of the class before calling Show.
		/// </summary>
		/// <param name="owner">Owner window the task Dialog will modal to.</param>
		/// <param name="verificationFlagChecked">Returns true if the verification checkbox was checked when the dialog
		/// was dismissed.</param>
		/// <returns>The result of the dialog, either a DialogResult value for common push buttons set in the CommonButtons
		/// member or the ButtonID from a TaskDialogButton structure set on the Buttons member.</returns>
		public int Show(IWin32Window owner, out bool verificationFlagChecked) {
			return Show(owner?.Handle ?? IntPtr.Zero, out verificationFlagChecked, out int radioButtonResult);
		}

		/// <summary>
		/// Creates, displays, and operates a task dialog. The task dialog contains application-defined messages, title,
		/// verification check box, command links and push buttons, plus any combination of predefined icons and push buttons
		/// as specified on the other members of the class before calling Show.
		/// </summary>
		/// <param name="hwndOwner">Owner window the task Dialog will modal to.</param>
		/// <param name="verificationFlagChecked">Returns true if the verification checkbox was checked when the dialog
		/// was dismissed.</param>
		/// <returns>The result of the dialog, either a DialogResult value for common push buttons set in the CommonButtons
		/// member or the ButtonID from a TaskDialogButton structure set on the Buttons member.</returns>
		public int Show(IntPtr hwndOwner, out bool verificationFlagChecked) {
			// We have to call a private version or PreSharp gets upset about a unsafe
			// block in a public method. (PreSharp error 56505)
			return PrivateShow(hwndOwner, out verificationFlagChecked, out int radioButtonResult);
		}

		/// <summary>
		/// Creates, displays, and operates a task dialog. The task dialog contains application-defined messages, title,
		/// verification check box, command links and push buttons, plus any combination of predefined icons and push buttons
		/// as specified on the other members of the class before calling Show.
		/// </summary>
		/// <param name="owner">Owner window the task Dialog will modal to.</param>
		/// <param name="verificationFlagChecked">Returns true if the verification checkbox was checked when the dialog
		/// was dismissed.</param>
		/// <param name="radioButtonResult">The radio botton selected by the user.</param>
		/// <returns>The result of the dialog, either a DialogResult value for common push buttons set in the CommonButtons
		/// member or the ButtonID from a TaskDialogButton structure set on the Buttons member.</returns>
		public int Show(IWin32Window owner, out bool verificationFlagChecked, out int radioButtonResult) {
			return Show(owner?.Handle ?? IntPtr.Zero, out verificationFlagChecked, out radioButtonResult);
		}

		/// <summary>
		/// Creates, displays, and operates a task dialog. The task dialog contains application-defined messages, title,
		/// verification check box, command links and push buttons, plus any combination of predefined icons and push buttons
		/// as specified on the other members of the class before calling Show.
		/// </summary>
		/// <param name="hwndOwner">Owner window the task Dialog will modal to.</param>
		/// <param name="verificationFlagChecked">Returns true if the verification checkbox was checked when the dialog
		/// was dismissed.</param>
		/// <param name="radioButtonResult">The radio botton selected by the user.</param>
		/// <returns>The result of the dialog, either a DialogResult value for common push buttons set in the CommonButtons
		/// member or the ButtonID from a TaskDialogButton structure set on the Buttons member.</returns>
		public int Show(IntPtr hwndOwner, out bool verificationFlagChecked, out int radioButtonResult) {
			// We have to call a private version or PreSharp gets upset about a unsafe
			// block in a public method. (PreSharp error 56505)
			return PrivateShow(hwndOwner, out verificationFlagChecked, out radioButtonResult);
		}

		/// <summary>
		/// Creates, displays, and operates a task dialog. The task dialog contains application-defined messages, title,
		/// verification check box, command links and push buttons, plus any combination of predefined icons and push buttons
		/// as specified on the other members of the class before calling Show.
		/// </summary>
		/// <param name="hwndOwner">Owner window the task Dialog will modal to.</param>
		/// <param name="verificationFlagChecked">Returns true if the verification checkbox was checked when the dialog
		/// was dismissed.</param>
		/// <param name="radioButtonResult">The radio botton selected by the user.</param>
		/// <returns>The result of the dialog, either a DialogResult value for common push buttons set in the CommonButtons
		/// member or the ButtonID from a TaskDialogButton structure set on the Buttons member.</returns>
		private int PrivateShow(IntPtr hwndOwner, out bool verificationFlagChecked, out int radioButtonResult) {
			verificationFlagChecked = false;
			radioButtonResult = 0;
			int result = 0;
			UnsafeNativeMethods.TASKDIALOGCONFIG config = new UnsafeNativeMethods.TASKDIALOGCONFIG();

			try {
				config.cbSize = (uint)Marshal.SizeOf(typeof(UnsafeNativeMethods.TASKDIALOGCONFIG));
				config.hwndParent = hwndOwner;
				config.dwFlags = this._flags;
				config.dwCommonButtons = CommonButtons;

				if(!string.IsNullOrEmpty(WindowTitle))
					config.pszWindowTitle = WindowTitle;

				config.MainIcon = (IntPtr)MainIcon;
				if(CustomMainIcon != null) {
					config.dwFlags |= UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_USE_HICON_MAIN;
					config.MainIcon = CustomMainIcon.Handle;
				}

				if(!string.IsNullOrEmpty(MainInstruction))
					config.pszMainInstruction = MainInstruction;

				if(!string.IsNullOrEmpty(Content))
					config.pszContent = Content;

				TaskDialogButton[] customButtons = Buttons ?? new TaskDialogButton[0];
				if(customButtons.Length > 0) {
					// Hand marshal the buttons array.
					int elementSize = Marshal.SizeOf(typeof(TaskDialogButton));
					config.pButtons = Marshal.AllocHGlobal(elementSize * customButtons.Length);
					for(int i = 0; i < customButtons.Length; i++) {
						unsafe {  // Unsafe because of pointer arithmatic.
							byte* p = (byte*)config.pButtons;
							Marshal.StructureToPtr(customButtons[i], (IntPtr)(p + elementSize * i), false);
						}
						config.cButtons++;
					}
				}

				TaskDialogButton[] customRadioButtons = RadioButtons ?? new TaskDialogButton[0];
				if(customRadioButtons.Length > 0) {
					// Hand-marshal the buttons array.
					int elementSize = Marshal.SizeOf(typeof(TaskDialogButton));
					config.pRadioButtons = Marshal.AllocHGlobal(elementSize * customRadioButtons.Length);
					for(int i = 0; i < customRadioButtons.Length; i++) {
						unsafe {  // Unsafe because of pointer arithmatic.
							byte* p = (byte*)config.pRadioButtons;
							Marshal.StructureToPtr(customRadioButtons[i], (IntPtr)(p + elementSize * i), false);
						}
						config.cRadioButtons++;
					}
				}

				config.nDefaultButton = DefaultButton;
				config.nDefaultRadioButton = DefaultRadioButton;

				if(!string.IsNullOrEmpty(VerificationText))
					config.pszVerificationText = VerificationText;

				if(!string.IsNullOrEmpty(ExpandedInformation))
					config.pszExpandedInformation = ExpandedInformation;

				if(!string.IsNullOrEmpty(ExpandedControlText))
					config.pszExpandedControlText = ExpandedControlText;

				if(!string.IsNullOrEmpty(CollapsedControlText))
					config.pszCollapsedControlText = CollapsedControlText;

				config.FooterIcon = (IntPtr)FooterIcon;
				if(CustomFooterIcon != null) {
					config.dwFlags |= UnsafeNativeMethods.TASKDIALOG_FLAGS.TDF_USE_HICON_FOOTER;
					config.FooterIcon = CustomFooterIcon.Handle;
				}

				if(!string.IsNullOrEmpty(Footer))
					config.pszFooter = Footer;

				// If our user has asked for a callback then we need to ask for one to
				// translate to the friendly version.
				if(Callback != null)
					config.pfCallback = new UnsafeNativeMethods.TaskDialogCallback(PrivateCallback);

				////config.lpCallbackData = this.callbackData; // How do you do this? Need to pin the ref?
				config.cxWidth = Width;

				// The call all this mucking about is here for.
				UnsafeNativeMethods.TaskDialogIndirect(ref config, out result, out radioButtonResult, out verificationFlagChecked);
			} finally {
				// Free the unmanged memory needed for the button arrays.
				// There is the possiblity of leaking memory if the app-domain is destroyed in a non clean way
				// and the hosting OS process is kept alive but fixing this would require using hardening techniques
				// that are not required for the users of this class.
				if(config.pButtons != IntPtr.Zero) {
					int elementSize = Marshal.SizeOf(typeof(TaskDialogButton));
					for(int i = 0; i < config.cButtons; i++)
						unsafe {
							byte* p = (byte*)config.pButtons;
							Marshal.DestroyStructure((IntPtr)(p + elementSize * i), typeof(TaskDialogButton));
						}

					Marshal.FreeHGlobal(config.pButtons);
				}

				if(config.pRadioButtons != IntPtr.Zero) {
					int elementSize = Marshal.SizeOf(typeof(TaskDialogButton));
					for(int i = 0; i < config.cRadioButtons; i++)
						unsafe {
							byte* p = (byte*)config.pRadioButtons;
							Marshal.DestroyStructure((IntPtr)(p + elementSize * i), typeof(TaskDialogButton));
						}

					Marshal.FreeHGlobal(config.pRadioButtons);
				}
			}
			return result;
		}

		/// <summary>
		/// The callback from the native Task Dialog. This prepares the friendlier arguments and calls the simplier callback.
		/// </summary>
		/// <param name="hwnd">The window handle of the Task Dialog that is active.</param>
		/// <param name="msg">The notification. A TaskDialogNotification value.</param>
		/// <param name="wparam">Specifies additional noitification information.  The contents of this parameter depends on the value of the msg parameter.</param>
		/// <param name="lparam">Specifies additional noitification information.  The contents of this parameter depends on the value of the msg parameter.</param>
		/// <param name="refData">Specifies the application-defined value given in the call to TaskDialogIndirect.</param>
		/// <returns>A HRESULT. It's not clear in the spec what a failed result will do.</returns>
		private int PrivateCallback([In] IntPtr hwnd, [In] uint msg, [In] UIntPtr wparam, [In] IntPtr lparam, [In] IntPtr refData) {
			TaskDialogCallback callback = Callback;
			if(callback != null) {
				// Prepare arguments for the callback to the user we are insulating from Interop casting sillyness.

				// Future: Consider reusing a single ActiveTaskDialog object and mark it as destroyed on the destry notification.
				ActiveTaskDialog activeDialog = new ActiveTaskDialog(hwnd);
				TaskDialogNotificationArgs args = new TaskDialogNotificationArgs {
					Notification = (TaskDialogNotification)msg
				};
				switch(args.Notification) {
					case TaskDialogNotification.ButtonClicked:
					case TaskDialogNotification.RadioButtonClicked:
						args.ButtonId = (int)wparam;
						break;
					case TaskDialogNotification.HyperlinkClicked:
						args.Hyperlink = Marshal.PtrToStringUni(lparam);
						break;
					case TaskDialogNotification.Timer:
						args.TimerTickCount = (uint)wparam;
						break;
					case TaskDialogNotification.VerificationClicked:
						args.VerificationFlagChecked = wparam != UIntPtr.Zero;
						break;
					case TaskDialogNotification.ExpandoButtonClicked:
						args.Expanded = wparam != UIntPtr.Zero;
						break;
				}

				return callback(activeDialog, args, CallbackData) ? 1 : 0;
			}

			return 0;
		}

		/// <summary>
		/// Helper function to set or clear a bit in the flags field.
		/// </summary>
		/// <param name="flag">The flag bit to set or clear.</param>
		/// <param name="value">True to set, false to clear the bit in the flags field.</param>
		private void SetFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS flag, bool value) {
			if(value)
				_flags |= flag;
			else
				_flags &= ~flag;
		}

		/// <summary>
		/// Helper function to check if a flag is set.
		/// </summary>
		/// <param name="flag">The flag bit to check.</param>
		/// <returns>True if the flag is set.</returns>
		private bool HasFlag(UnsafeNativeMethods.TASKDIALOG_FLAGS flag) {
			return (_flags & flag) != 0;
		}
	}
}
