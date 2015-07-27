using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeOverlay
{
	/// <summary>
	/// Interaction logic for About.xaml
	/// </summary>
	public partial class About: Window
	{
		public About()
		{
			InitializeComponent();
			this.Hide();
			PreNameText.Text = "Application Developer: ";
			AppDevName.Text = "Benjamin Kohler";
			VersionNumber.Text = "Version: " + Assembly.GetExecutingAssembly().GetName().Version;
			GithubLink.Text = "See this TimeOverlay project on my github!";
			ApplicationDesc.Text =
				"This project is a template for other systems. The project is licensed under the GNU GPL 2 License. Please only use this application and the source code as the license states.";
			//TODO: Fix clock image. For some reason the image path goes to null on runtime.
			//ImageDisplay.Source = "pack://application:,,,,/Resources/TimeOverlayClock.png";
		}

		private void GithubLink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			System.Diagnostics.Process.Start("http://github.com/riku12124/TimeOverlay");
		}


		private void About_OnClosing(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			this.Hide();

		}

		private void BenWebsite_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			System.Diagnostics.Process.Start("http://BenKohler.com");
		}
	}
}
