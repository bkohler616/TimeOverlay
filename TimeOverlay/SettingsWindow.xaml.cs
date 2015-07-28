﻿using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace TimeOverlay {
	/// <summary>
	///    Interaction logic for SettingsWindow.xaml
	/// </summary>
	public partial class SettingsWindow {
		private readonly SettingsInfo _settings;

		public SettingsWindow() {
			InitializeComponent();
			_settings = new SettingsInfo();
			SetSettings();
		}

		public SettingsWindow(SettingsInfo savedSettings) {
			InitializeComponent();
			_settings = savedSettings;
			SetSettings();
		}

		public SettingsInfo Settings {
			get { return _settings; }
		}

		private void SetSettings() {
			TbDateColor.Text = _settings.DateTextColor;
			TbTimeColor.Text = _settings.TimeTextColor;
			TbDateFontSize.Text = _settings.DateFontSize.ToString();
			TbTimeFontSize.Text = _settings.TimeFontSize.ToString();
			TbBackgroundOpacity.Text = _settings.WindowOpacityPercentage.ToString();
		}

		private void SettingsWindow_OnClosing(object sender, CancelEventArgs e) {
			if (_settings.CloseApplication) return;

			#region vars

			var errorList = "Error(s):";
			var printErrorIfTrue = false;

			#endregion

			/*Check for errors before closing.*/

			#region hexColors

			try {
				if (TbDateColor.Text.Length < 6)
					throw new Exception("\n-Date hex code is not proper length. (please use RGB without # symbol)");
				if (!CheckHex(TbDateColor.Text))
					throw new Exception("\n-Date hex code is not in proper format. Please use only hex characters");
				_settings.DateTextColor = TbDateColor.Text;
			}
			catch (Exception ex) {
				errorList += ex.Message;
				printErrorIfTrue = true;
				_settings.DefaultDateColor();
			}

			try {
				if (TbTimeColor.Text.Length < 6)
					throw new Exception("\n-Time hex code is not proper length. (please use RGB without # symbol)");
				if (!CheckHex(TbTimeColor.Text))
					throw new Exception("\n-Time hex code is not in proper format. Please use only hex characters");
				_settings.TimeTextColor = TbTimeColor.Text;
			}
			catch (Exception ex) {
				errorList += ex.Message;
				printErrorIfTrue = true;
				_settings.DefaultTimeColor();
			}

			#endregion

			#region fontIntegers

			//Date Font Check
			try {
				if (!CheckInt(TbDateFontSize.Text))
					throw new Exception("\n-Date font size is not a proper value. Please only use whole numbers.");
				int fontValue;
				var parseSuccess = int.TryParse(TbDateFontSize.Text, out fontValue);
				if (!parseSuccess)
					throw new Exception("\n-Date font size is not a proper value. Please only use whole numbers.");
				if (!(fontValue < 201 && fontValue > 4))
					throw new Exception("\n-Date font size is out of range. Please use numbers in between 5 and 200");
				_settings.DateFontSize = fontValue;
			}
			catch (Exception ex) {
				errorList += ex.Message;
				printErrorIfTrue = true;
				_settings.DefaultDateFontSize();
			}

			//Time Font Check
			try {
				if (!CheckInt(TbTimeFontSize.Text))
					throw new Exception("\n-Time font size is not a proper value. Please only use whole numbers.");
				int fontValue;
				var parseSuccess = int.TryParse(TbTimeFontSize.Text, out fontValue);
				if (!parseSuccess)
					throw new Exception("\n-Time font size is not a proper value. Please only use whole numbers.");
				if (!(fontValue < 201 && fontValue > 4))
					throw new Exception("\n-Time font size is out of range. Please use numbers in between 5 and 400");
				_settings.TimeFontSize = fontValue;
			}
			catch (Exception ex) {
				errorList += ex.Message;
				printErrorIfTrue = true;
				_settings.DefaultTimeFontSize();
			}

			//Opacity Check
			try {
				if (!CheckInt(TbBackgroundOpacity.Text))
					throw new Exception("\n-Opacity is not a proper value. Please only use whole numbers.");
				int opacityPercent;
				var parseSuccess = int.TryParse(TbBackgroundOpacity.Text, out opacityPercent);
				if (!parseSuccess)
					throw new Exception("\n-Opacity is not a proper value. Please only use whole numbers.");
				if (!(opacityPercent > -1 && opacityPercent < 101))
					throw new Exception("\n-Opacity percentage is out of range. Please use numbers in between 1 and 100");
				_settings.WindowOpacityPercentage = opacityPercent;
			}
			catch (Exception ex) {
				errorList += ex.Message;
				printErrorIfTrue = true;
				_settings.DefaultOpacityPercentage();
			}

			#endregion

			#region output

			//Output errors
			if (printErrorIfTrue)
				MessageBox.Show(errorList + "\n\nAll inproper content will be reverted to defaults.");
			SetSettings();
			Hide();
			e.Cancel = true;

			#endregion
		}

		private void ResetDefault_OnClick(object sender, RoutedEventArgs e) {
			_settings.RestoreDefault();
			SetSettings();
		}

		private bool CheckHex(string hexString) {
			var m = Regex.Match(hexString, @"([A-F0-9])+", RegexOptions.IgnoreCase);
			var rgxTest = m.Value.Equals(hexString);
			if (rgxTest)
				rgxTest = m.Success;
			return rgxTest;
		}

		private bool CheckInt(string intString) {
			var m = Regex.Match(intString, @"([0-9])+");
			var rgxTest = m.Value.Equals(intString);
			if (rgxTest)
				rgxTest = m.Success;
			return rgxTest;
		}
	}
}