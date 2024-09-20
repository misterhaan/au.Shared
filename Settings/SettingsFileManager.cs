using System.IO;
using System.Xml.Serialization;

namespace au.Settings {
	/// <summary>
	/// Save and load settings from a file.
	/// </summary>
	/// <typeparam name="TSettings">Settings type.</typeparam>
	/// <param name="file">Settings file.</param>
	public class SettingsFileManager<TSettings>(FileInfo file) where TSettings : new() {

		/// <summary>
		/// File path string constructor.
		/// </summary>
		/// <param name="filePath">Absolute path to settings file.</param>
		public SettingsFileManager(string filePath) : this(new FileInfo(filePath)) { }

		/// <summary>
		/// Settings object to manage.
		/// </summary>
		public TSettings Settings {
			get {
				_settings ??= Load();
				return _settings;
			}
		}
		private TSettings _settings;

		/// <summary>
		/// Save settings to file.
		/// </summary>
		public void Save() {
			using StreamWriter writer = file.CreateText();
			new XmlSerializer(typeof(TSettings)).Serialize(writer, Settings);
		}

		/// <summary>
		/// Load settings from file, or create default settings if the file doesn't exist yet.
		/// </summary>
		/// <returns>Settings.</returns>
		protected TSettings Load() {
			file.Refresh();
			if(file.Exists)
				using(StreamReader reader = file.OpenText())
					if(new XmlSerializer(typeof(TSettings)).Deserialize(reader) is TSettings settings)
						return settings;
			return new TSettings();
		}
	}
}
