
using System;

namespace TimeOverlay
{
	public class SettingsInfo
	{
		private string _timeTextColor;
		private string _dateTextColor;
		private int _windowOpacityPercentage;
		private int _timeFontSize;
		private int _dateFontSize;

		private const int TimeFontSizeDefault = 30;
		private const int DateFontSizeDefault = 15;

		private const string TimeTextColorDefault = "ffffff";
		private const string DateTextColorDefault = "808080";
		private const int WindowOpacityDefault = 50;

		public SettingsInfo()
		{
			RestoreDefault();
			CloseApplication = false;
		}
		public string TimeTextColor
		{
			get { return _timeTextColor; }
			set { _timeTextColor = value; }
		}

		public string DateTextColor
		{
			get { return _dateTextColor; }
			set { _dateTextColor = value; }
		}

		public int TimeFontSize
		{
			get { return _timeFontSize; }
			set { _timeFontSize = value; }
		}

		public int DateFontSize
		{
			get { return _dateFontSize; }
			set { _dateFontSize = value; }
		}

		public bool CloseApplication { get; set; }

		public int WindowOpacityPercentage
		{
			get { return _windowOpacityPercentage; }
			set { _windowOpacityPercentage = value; }
		}

		public void DefaultTimeColor()
		{
			_timeTextColor = TimeTextColorDefault;
		}

		public void DefaultDateColor()
		{
			_dateTextColor = DateTextColorDefault;
		}

		public void DefaultTimeFontSize()
		{
			_timeFontSize = TimeFontSizeDefault;
		}

		public void DefaultDateFontSize()
		{
			_dateFontSize = DateFontSizeDefault;
		}

		public void DefaultOpacityPercentage()
		{
			_windowOpacityPercentage = WindowOpacityDefault;
		}

		public void RestoreDefault()
		{
			DefaultDateColor();
			DefaultTimeColor();
			DefaultDateFontSize();
			DefaultTimeFontSize();
			DefaultOpacityPercentage();
		}
	}
}
