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

		public SettingsInfo()
		{
			_timeTextColor = "ffffff";
			_dateTextColor = "808080";
		}
		public string TimeTextColor
		{
			get { return _timeTextColor; }
			set { _timeTextColor = CheckHex(value) ? value : "ffffff"; }
		}

		public string DateTextColor
		{
			get { return _dateTextColor; }
			set { _dateTextColor = CheckHex(value) ? value : "808080"; }
		}

		private bool CheckHex(String hexString)
		{
			Regex rgx = new Regex(@"/([A-Z]|[0-9])/g");
			return rgx.IsMatch(hexString);
		}
	}
}
