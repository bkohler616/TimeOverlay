using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Navigation;

namespace TimeOverlay
{
	public class SettingsInfo
	{
		private string _timeTextColor;
		private string _dateTextColor;
		private int _timeFontSize;
		private int _dateFontSize;

		public SettingsInfo()
		{
			_timeTextColor = "ffffff";
			_dateTextColor = "808080";
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

		public void DefaultTimeColor()
		{
			_timeTextColor = "ffffff";
		}

		public void DefaultDateColor()
		{
			_dateTextColor = "808080";
		}

		public void DefaultTimeFontSize()
		{
			_timeFontSize = 30;
		}

		public void DefaultDateFontSize()
		{
			_dateFontSize = 15;
		}

		public void RestoreDefault()
		{
			DefaultDateColor();
			DefaultTimeColor();
			DefaultDateFontSize();
			DefaultTimeFontSize();
		}
	}
}
