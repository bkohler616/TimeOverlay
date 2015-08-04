using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace TimeOverlay {
	/// <summary>
	///    Interaction logic for About.xaml
	/// </summary>
	public partial class About : Window {
		public About() {
			InitializeComponent();
			Hide();
			PreNameText.Text = "Application Developer: ";
			AppDevName.Text = "Benjamin Kohler";
			VersionNumber.Text = "Version: " + Assembly.GetExecutingAssembly().GetName().Version;
			GithubLink.Text = "See this TimeOverlay project on my github!";
			ApplicationDesc.Text =
				"This project is a template for other systems. The project is licensed under the GNU GPL 2 License. Please only use this application and the source code as the license states.";
			//TODO: Fix clock image. For some reason the image path goes to null on runtime.
			//ImageDisplay.Source = "pack://application:,,,,/Resources/TimeOverlayClock.png";
		}

		  /// <summary>
		  /// On GitHub Link Click
		  /// Start process to open browser to the TimeOverlay project on Github.
		  /// </summary>
		  /// <param name="sender"></param>
		  /// <param name="e"></param>
		private void GithubLink_OnRequestNavigate(object sender, RequestNavigateEventArgs e) {
			Process.Start("http://github.com/riku12124/TimeOverlay");
		}

		  /// <summary>
		  /// On Window Closing Hide window.
		  /// </summary>
		  /// <param name="sender"></param>
		  /// <param name="e"></param>
		private void About_OnClosing(object sender, CancelEventArgs e) {
			e.Cancel = true;
			Hide();
		}

		  /// <summary>
		  /// On Developer Name Click Open link to his website.
		  /// </summary>
		  /// <param name="sender"></param>
		  /// <param name="e"></param>
		private void BenWebsite_OnRequestNavigate(object sender, RequestNavigateEventArgs e) {
			Process.Start("http://BenKohler.com");
		}
	}
}