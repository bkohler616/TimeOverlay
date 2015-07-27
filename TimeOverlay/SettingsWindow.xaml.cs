using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace TimeOverlay
{
	/// <summary>
	/// Interaction logic for SettingsWindow.xaml
	/// </summary>
	public partial class SettingsWindow
	{
		private readonly SettingsInfo _settings;
		public SettingsWindow()
		{
			InitializeComponent();
			_settings = new SettingsInfo();
			SetSettings();
		}

		public SettingsWindow(SettingsInfo savedSettings)
		{
			InitializeComponent();
			_settings = savedSettings;
			SetSettings();
		}

		private void SetSettings()
		{
			TbDateColor.Text = _settings.DateTextColor;
			TbTimeColor.Text = _settings.TimeTextColor;
			TbDateFontSize.Text = _settings.DateFontSize.ToString();
			TbTimeFontSize.Text = _settings.TimeFontSize.ToString();
		}

		private void SettingsWindow_OnClosing(object sender, CancelEventArgs e)
		{
			if (_settings.CloseApplication) return;

			#region vars

			string errorList = "Error(s):";
			bool printErrorIfTrue = false;

			#endregion

			/*Check for errors before closing.*/

			#region hexColors

			try
			{
				if(TbDateColor.Text.Length < 6)
					throw new Exception("\n-Date hex code is not proper length. (please use RGB without # symbol)");
				if(!CheckHex(TbDateColor.Text))
					throw new Exception("\n-Date hex code is not in proper format. Please use only hex characters");
				_settings.DateTextColor = TbDateColor.Text;
			} catch(Exception ex)
			{
				errorList += ex.Message;
				printErrorIfTrue = true;
				_settings.DefaultDateColor();
			}

			try
			{
				if(TbTimeColor.Text.Length < 6)
					throw new Exception("\n-Time hex code is not proper length. (please use RGB without # symbol)");
				if(!CheckHex(TbTimeColor.Text))
					throw new Exception("\n-Time hex code is not in proper format. Please use only hex characters");
				_settings.TimeTextColor = TbTimeColor.Text;
			} catch(Exception ex)
			{
				errorList += ex.Message;
				printErrorIfTrue = true;
				_settings.DefaultTimeColor();
			}
			

			#endregion

			#region fontIntegers

			//Date Font Check
			try
			{
				if(!CheckHex(TbDateFontSize.Text))
					throw new Exception("\n-Date font size is not a proper value. Please only use whole numbers.");
				int fontValue;
				bool parseSuccess = Int32.TryParse(TbDateFontSize.Text, out fontValue);
				if(!parseSuccess)
					throw new Exception("\n-Date font size is not a proper value. Please only use whole numbers.");
				if(!(fontValue < 101 && fontValue > 4))
					throw new Exception("\n-Date font size is out of range. Please use numbers in between 5 and 100");
				_settings.DateFontSize = fontValue;
			} catch(Exception ex)
			{
				errorList += ex.Message;
				printErrorIfTrue = true;
				_settings.DefaultDateFontSize();
			}

			//Time Font Check
			try
			{
				if(!CheckHex(TbTimeFontSize.Text))
					throw new Exception("\n-Time font size is not a proper value. Please only use whole numbers.");
				int fontValue;
				bool parseSuccess = Int32.TryParse(TbTimeFontSize.Text, out fontValue);
				if(!parseSuccess)
					throw new Exception("\n-Time font size is not a proper value. Please only use whole numbers.");
				if(!(fontValue < 101 && fontValue > 4))
					throw new Exception("\n-Time font size is out of range. Please use numbers in between 5 and 100");
				_settings.TimeFontSize = fontValue;
			} catch(Exception ex)
			{
				errorList += ex.Message;
				printErrorIfTrue = true;
				_settings.DefaultTimeFontSize();
			}
			#endregion

			#region output

			//Output errors
			if (printErrorIfTrue)
				MessageBox.Show(errorList + "\n\nAll inproper content will be reverted to defaults.");
			SetSettings();
			this.Hide();
			e.Cancel = true;

			#endregion
		}

		public SettingsInfo Settings
		{
			get { return _settings; }
		}

		//TODO: Check if the settings info rejected to hex code and defaulted to another on close.
		private void ResetDefault_OnClick(object sender, RoutedEventArgs e)
		{
			_settings.RestoreDefault();
			SetSettings();

		}

		private bool CheckHex(string hexString)
		{

			Match m = Regex.Match(hexString, @"([A-F0-9])+", RegexOptions.IgnoreCase);
			bool rgxTest = m.Value.Equals(hexString);
			if (rgxTest)
				rgxTest = m.Success;
			return rgxTest;
		}

		private bool CheckInt(string intString)
		{
			Match m = Regex.Match(intString, @"([0-9])+");
			bool rgxTest = m.Value.Equals(intString);
			if(rgxTest)
				rgxTest = m.Success;
			return rgxTest;
		}
	}
}
