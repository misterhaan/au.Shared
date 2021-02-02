﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace au.IO.Files.FileOperation {
	internal class ShellItemFactory {
		private static Guid _shellItemGuid = typeof(IShellItem).GUID;

		internal virtual ComDisposer<IShellItem> Create(string path)
			=> new ComDisposer<IShellItem>((IShellItem)SHCreateItemFromParsingName(path, null, ref _shellItemGuid));

		[DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Unicode, PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		private static extern object SHCreateItemFromParsingName([MarshalAs(UnmanagedType.LPWStr)] string pszPath, IBindCtx pbc, ref Guid riid);
	}
}
