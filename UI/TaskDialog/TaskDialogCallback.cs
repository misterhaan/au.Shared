namespace au.UI.TaskDialog {
	/// <summary>
	/// The signature of the callback that recieves notificaitons from the Task Dialog.
	/// </summary>
	/// <param name="taskDialog">The active task dialog which has methods that can be performed on an active Task Dialog.</param>
	/// <param name="args">The notification arguments including the type of notification and information for the notification.</param>
	/// <param name="callbackData">The value set on TaskDialog.CallbackData</param>
	/// <returns>Return value meaning varies depending on the Notification member of args.</returns>
	public delegate bool TaskDialogCallback(ActiveTaskDialog taskDialog, TaskDialogNotificationArgs args, object callbackData);
}
