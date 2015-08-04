namespace TimeOverlay {
	 /// <summary>
	 /// SettingsInfo is a holder for the Settings Window so the Main Window can be edited.
	 /// This holds not only the saved values, but also the default settings.
	 /// </summary>
	public class SettingsInfo {
		private const int TimeFontSizeDefault = 30;
		private const int DateFontSizeDefault = 15;
		private const string TimeTextColorDefault = "ffffff";
		private const string DateTextColorDefault = "808080";
		private const int WindowOpacityDefault = 50;
		private const bool ClickThroughDefault = true;

		public SettingsInfo() {
			RestoreDefault();
			CloseApplication = false;
		}

		public string TimeTextColor { get; set; }
		public string DateTextColor { get; set; }
		public int TimeFontSize { get; set; }
		public int DateFontSize { get; set; }
		public bool CloseApplication { get; set; }
		public int WindowOpacityPercentage { get; set; }
		public bool ClickThrough { get; set; }

		public void DefaultTimeColor() {
			TimeTextColor = TimeTextColorDefault;
		}

		public void DefaultDateColor() {
			DateTextColor = DateTextColorDefault;
		}

		public void DefaultTimeFontSize() {
			TimeFontSize = TimeFontSizeDefault;
		}

		public void DefaultDateFontSize() {
			DateFontSize = DateFontSizeDefault;
		}

		public void DefaultOpacityPercentage() {
			WindowOpacityPercentage = WindowOpacityDefault;
		}

		 private void DefaultClickThrough() {
			ClickThrough = ClickThroughDefault;
		}

		public void RestoreDefault() {
			DefaultDateColor();
			DefaultTimeColor();
			DefaultDateFontSize();
			DefaultTimeFontSize();
			DefaultOpacityPercentage();
			DefaultClickThrough();
		}
	}
}