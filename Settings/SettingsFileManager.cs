using System.IO;
using System.Xml.Serialization;

namespace au.Settings {
	/// <summary>
	/// Save and load settings from a file.
	/// </summary>
	/// <typeparam name="TSettings">Settings type.</typeparam>
	public class SettingsFileManager<TSettings> where TSettings : new() {
		/// <summary>
		/// File to save and load from.
		/// </summary>
		private readonly FileInfo _file;

		/// <summary>
		/// FileInfo constructor.
		/// </summary>
		/// <param name="file">Settings file.</param>
		public SettingsFileManager(FileInfo file) {
			_file = file;
		}

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
				if(_settings == null)
					_settings = Load();
				return _settings;
			}
		}
		private TSettings _settings;

		/// <summary>
		/// Save settings to file.
		/// </summary>
		public void Save() {
			using(StreamWriter writer = _file.CreateText())
				new XmlSerializer(typeof(TSettings)).Serialize(writer, Settings);
		}

		/// <summary>
		/// Load settings from file, or create default settings if the file doesn't exist yet.
		/// </summary>
		/// <returns>Settings.</returns>
		protected TSettings Load() {
			_file.Refresh();
			if(_file.Exists)
				using(StreamReader reader = _file.OpenText())
					if(new XmlSerializer(typeof(TSettings)).Deserialize(reader) is TSettings settings)
						return settings;
			return new TSettings();
		}
	}
}
