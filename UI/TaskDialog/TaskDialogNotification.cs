namespace au.UI.TaskDialog {
	/// <summary>
	/// Task Dialog callback notifications. 
	/// </summary>
	public enum TaskDialogNotification {
		/// <summary>
		/// Sent by the Task Dialog once the dialog has been created and before it is displayed.
		/// The value returned by the callback is ignored.
		/// </summary>
		Created = 0,

		//// Spec is not clear what this is so not supporting it.
		///// <summary>
		///// Sent by the Task Dialog when a navigation has occurred.
		///// The value returned by the callback is ignored.
		///// </summary>   
		// Navigated = 1,

		/// <summary>
		/// Sent by the Task Dialog when the user selects a button or command link in the task dialog.
		/// The button ID corresponding to the button selected will be available in the
		/// TaskDialogNotificationArgs. To prevent the Task Dialog from closing, the application must
		/// return true, otherwise the Task Dialog will be closed and the button ID returned to via
		/// the original application call.
		/// </summary>
		ButtonClicked = 2,            // wParam = Button ID

		/// <summary>
		/// Sent by the Task Dialog when the user clicks on a hyperlink in the Task Dialog’s content.
		/// The string containing the HREF of the hyperlink will be available in the
		/// TaskDialogNotificationArgs. To prevent the TaskDialog from shell executing the hyperlink,
		/// the application must return TRUE, otherwise ShellExecute will be called.
		/// </summary>
		HyperlinkClicked = 3,            // lParam = (LPCWSTR)pszHREF

		/// <summary>
		/// Sent by the Task Dialog approximately every 200 milliseconds when TaskDialog.CallbackTimer
		/// has been set to true. The number of milliseconds since the dialog was created or the
		/// notification returned true is available on the TaskDialogNotificationArgs. To reset
		/// the tickcount, the application must return true, otherwise the tickcount will continue to
		/// increment.
		/// </summary>
		Timer = 4,            // wParam = Milliseconds since dialog created or timer reset

		/// <summary>
		/// Sent by the Task Dialog when it is destroyed and its window handle no longer valid.
		/// The value returned by the callback is ignored.
		/// </summary>
		Destroyed = 5,

		/// <summary>
		/// Sent by the Task Dialog when the user selects a radio button in the task dialog.
		/// The button ID corresponding to the button selected will be available in the
		/// TaskDialogNotificationArgs.
		/// The value returned by the callback is ignored.
		/// </summary>
		RadioButtonClicked = 6,            // wParam = Radio Button ID

		/// <summary>
		/// Sent by the Task Dialog once the dialog has been constructed and before it is displayed.
		/// The value returned by the callback is ignored.
		/// </summary>
		DialogConstructed = 7,

		/// <summary>
		/// Sent by the Task Dialog when the user checks or unchecks the verification checkbox.
		/// The verificationFlagChecked value is available on the TaskDialogNotificationArgs.
		/// The value returned by the callback is ignored.
		/// </summary>
		VerificationClicked = 8,             // wParam = 1 if checkbox checked, 0 if not, lParam is unused and always 0

		/// <summary>
		/// Sent by the Task Dialog when the user presses F1 on the keyboard while the dialog has focus.
		/// The value returned by the callback is ignored.
		/// </summary>
		Help = 9,

		/// <summary>
		/// Sent by the task dialog when the user clicks on the dialog's expando button.
		/// The expanded value is available on the TaskDialogNotificationArgs.
		/// The value returned by the callback is ignored.
		/// </summary>
		ExpandoButtonClicked = 10            // wParam = 0 (dialog is now collapsed), wParam != 0 (dialog is now expanded)
	}
}
