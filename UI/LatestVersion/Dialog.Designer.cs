﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace au.UI.LatestVersion {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Dialog {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Dialog() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("au.UI.LatestVersion.Dialog", typeof(Dialog).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Download and install
        ///Automatically download the update, install it, and remove the file when completed..
        /// </summary>
        internal static string DownloadAndInstallUpdate {
            get {
                return ResourceManager.GetString("DownloadAndInstallUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Download installer.
        /// </summary>
        internal static string DownloadInstallerTitle {
            get {
                return ResourceManager.GetString("DownloadInstallerTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Download only
        ///Just download the update.  It will need to be installed manually..
        /// </summary>
        internal static string DownloadUpdateOnly {
            get {
                return ResourceManager.GetString("DownloadUpdateOnly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Remind me later
        ///Don’t do anything about this update right now.  You will be reminded again next time..
        /// </summary>
        internal static string IgnoreUpdate {
            get {
                return ResourceManager.GetString("IgnoreUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is available.
        /// </summary>
        internal static string UpdateAvailableDescription {
            get {
                return ResourceManager.GetString("UpdateAvailableDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Update Available.
        /// </summary>
        internal static string UpdateAvailableTitle {
            get {
                return ResourceManager.GetString("UpdateAvailableTitle", resourceCulture);
            }
        }
    }
}