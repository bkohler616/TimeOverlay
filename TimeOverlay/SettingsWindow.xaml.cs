using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;

namespace TimeOverlay {
	/// <summary>
	///    Interaction logic for SettingsWindow.xaml
	/// </summary>
	public partial class SettingsWindow {
		private readonly SettingsInfo _settings;

		public SettingsWindow() {
			InitializeComponent();
			_settings = new SettingsInfo();
			NotifyIcon nIcon = new NotifyIcon {Icon = new Icon("/TimeOverlay;Component/ImageIcons/TimeOverlayClock.ico")};
			nIcon.MouseClick += NIconOnTrayLeftMouseDown;
			nIcon.Visible = true;
			SetSettingsWindowContent();
		}

		  /// <summary>
		  /// Notify icon on click.
		  /// Show the settings window.
		  /// (Mainly used when hit-check is disabled)
		  /// </summary>
		  /// <param name="sender"></param>
		  /// <param name="routedEventArgs"></param>
		private void NIconOnTrayLeftMouseDown(object sender, EventArgs routedEventArgs) {
			this.Show();
		}

		  /// <summary>
		  /// Constructor to retrieve the saved settings of the on-disk serialized XML.
		  /// </summary>
		  /// <param name="savedSettings"></param>
		public SettingsWindow(SettingsInfo savedSettings) {
			InitializeComponent();
			_settings = savedSettings;
			NotifyIcon nIcon = new NotifyIcon {Icon = new Icon(System.Windows.Application.GetResourceStream(new Uri("ImageIcons/TimeOverlayClock.ico", UriKind.Relative)).Stream) };
			nIcon.MouseClick += NIconOnTrayLeftMouseDown;
			nIcon.Visible = true;
			SetSettingsWindowContent();
		}


		  /// <summary>
		  /// Accessor for the settings, so the main window can get the settings information.
		  /// </summary>
		  public SettingsInfo Settings => _settings;

		  /// <summary>
		  /// Set the content of the settings to the currently saved settings
		  /// </summary>
		private void SetSettingsWindowContent() {
			TbDateColor.Text = _settings.DateTextColor;
			TbTimeColor.Text = _settings.TimeTextColor;
			TbDateFontSize.Text = _settings.DateFontSize.ToString();
			TbTimeFontSize.Text = _settings.TimeFontSize.ToString();
			TbBackgroundOpacity.Text = _settings.WindowOpacityPercentage.ToString();
			CbClickThrough.IsChecked = _settings.ClickThrough;
		}

		  /// <summary>
		  /// On close application close, legitamatly close.
		  /// On close this window, set the settings to the save, set the content, then hide the window and cancel the close.
		  /// </summary>
		  /// <param name="sender"></param>
		  /// <param name="e"></param>
		private void SettingsWindow_OnClosing(object sender, CancelEventArgs e) {
			if (_settings.CloseApplication) return;

			SetSettings();

			SetSettingsWindowContent();
			Hide();
			e.Cancel = true;

		}

		  /// <summary>
		  /// Check if the input settings from the settings window is proper.
		  /// Build error list if there are any improper values.
		  /// Output error list in the end and save the settings.
		  /// </summary>
		private void SetSettings() {
			#region vars

			var errorList = "Error(s):";
			var printErrorIfTrue = false;

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
				if(!CheckInt(TbDateFontSize.Text))
					throw new Exception("\n-Date font size is not a proper value. Please only use whole numbers.");
				int fontValue;
				var parseSuccess = int.TryParse(TbDateFontSize.Text, out fontValue);
				if(!parseSuccess)
					throw new Exception("\n-Date font size is not a proper value. Please only use whole numbers.");
				if(!(fontValue < 201 && fontValue > 4))
					throw new Exception("\n-Date font size is out of range. Please use numbers in between 5 and 200");
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
				if(!CheckInt(TbTimeFontSize.Text))
					throw new Exception("\n-Time font size is not a proper value. Please only use whole numbers.");
				int fontValue;
				var parseSuccess = int.TryParse(TbTimeFontSize.Text, out fontValue);
				if(!parseSuccess)
					throw new Exception("\n-Time font size is not a proper value. Please only use whole numbers.");
				if(!(fontValue < 201 && fontValue > 4))
					throw new Exception("\n-Time font size is out of range. Please use numbers in between 5 and 400");
				_settings.TimeFontSize = fontValue;
			} catch(Exception ex)
			{
				errorList += ex.Message;
				printErrorIfTrue = true;
				_settings.DefaultTimeFontSize();
			}
			#endregion

			//Opacity Check
			try
			{
				if(!CheckInt(TbBackgroundOpacity.Text))
					throw new Exception("\n-Opacity is not a proper value. Please only use whole numbers.");
				int opacityPercent;
				var parseSuccess = int.TryParse(TbBackgroundOpacity.Text, out opacityPercent);
				if(!parseSuccess)
					throw new Exception("\n-Opacity is not a proper value. Please only use whole numbers.");
				if(!(opacityPercent > -1 && opacityPercent < 101))
					throw new Exception("\n-Opacity percentage is out of range. Please use numbers in between 1 and 100");
				_settings.WindowOpacityPercentage = opacityPercent;
			} catch(Exception ex)
			{
				errorList += ex.Message;
				printErrorIfTrue = true;
				_settings.DefaultOpacityPercentage();
			}

			if(CbClickThrough.IsChecked != null)
				_settings.ClickThrough = (bool) CbClickThrough.IsChecked;
			

			#region output

			//Output errors
			if (printErrorIfTrue)
				System.Windows.MessageBox.Show(errorList + "\n\nAll inproper content will be reverted to defaults.");
			#endregion
		}

		  /// <summary>
		  /// Reset button on click
		  /// Reset the settings to default and set the window content to show.
		  /// </summary>
		  /// <param name="sender"></param>
		  /// <param name="e"></param>
		private void ResetDefault_OnClick(object sender, RoutedEventArgs e) {
			_settings.RestoreDefault();
			SetSettingsWindowContent();
		}

		  /// <summary>
		  /// Regex check if the hex string is properly formatted
		  /// </summary>
		  /// <param name="hexString"></param>
		  /// <returns></returns>
		private bool CheckHex(string hexString) {
			var m = Regex.Match(hexString, @"([A-F0-9])+", RegexOptions.IgnoreCase);
			var rgxTest = m.Value.Equals(hexString);
			if (rgxTest)
				rgxTest = m.Success;
			return rgxTest;
		}

		  /// <summary>
		  /// Regex check if the int string is properly formatted
		  /// </summary>
		  /// <param name="intString"></param>
		  /// <returns></returns>
		private bool CheckInt(string intString) {
			var m = Regex.Match(intString, @"([0-9])+");
			var rgxTest = m.Value.Equals(intString);
			if (rgxTest)
				rgxTest = m.Success;
			return rgxTest;
		}

		  /// <summary>
		  /// Window Clickable Check Box on Uncheck.
		  /// Set the Textbox opacity text to 0.
		  /// (Used in settings and main window the make the window clickable)
		  /// </summary>
		  /// <param name="sender"></param>
		  /// <param name="e"></param>
		private void CbWindowClickable_OnUnchecked(object sender, RoutedEventArgs e) {
			TbBackgroundOpacity.Text = "0";
		}

		  /// <summary>
		  /// Window Clickable Check Box on Check
		  /// Set the Textbox opacity text to 50.
		  /// (Used in settings and main window to make the window clickable)
		  /// </summary>
		  /// <param name="sender"></param>
		  /// <param name="e"></param>
		private void CbClickThrough_OnChecked(object sender, RoutedEventArgs e) {
			TbBackgroundOpacity.Text = "50";
		}

		  /// <summary>
		  /// Apply button On Click
		  /// Save settings to Settings Info.
		  /// </summary>
		  /// <param name="sender"></param>
		  /// <param name="e"></param>
		private void Apply_OnClick(object sender, RoutedEventArgs e) {
			SetSettings();
		}
	}
}