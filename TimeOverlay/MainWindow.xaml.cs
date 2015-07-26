using System;
using System.ComponentModel;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace TimeOverlay
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private readonly Timer _updateTime;
		private DateTime _currentDateTime;
		private SettingsWindow settingsWindowAccess;
		private static string _path;

		public MainWindow()
		{

			InitializeComponent();
			_updateTime = new Timer {Interval = 1000};
			_currentDateTime = new DateTime();
			_updateTime.Elapsed += UpdateTime_Elapsed;
			_updateTime.Start();

			_path = (new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location)).Directory + "//TimeOverlaySettings.xml";

			//If settings save exists, deserialize it to SettingsWindow
			if(File.Exists(_path))
			{
				XmlSerializer reader = new XmlSerializer(typeof(SettingsInfo));
				StreamReader file = new StreamReader(_path);
				settingsWindowAccess = new SettingsWindow((SettingsInfo)reader.Deserialize(file));
			}
			settingsWindowAccess = new SettingsWindow();
			settingsWindowAccess.Hide();
		}

		private void UpdateTime_Elapsed(object sender, ElapsedEventArgs elapsedEventArgs)
		{
			_updateTime.Stop();
			_currentDateTime = DateTime.Now;
			//Invoke another thread to input content
			Application.Current.Dispatcher.BeginInvoke((Action) delegate()
			{
				LblDate.Content = _currentDateTime.DayOfWeek + ", " + _currentDateTime.Month + "/" + _currentDateTime.Day + "/" + _currentDateTime.Year;
				LblTime.Content = _currentDateTime.Hour%12 + ":" + _currentDateTime.Minute + "." + _currentDateTime.Second;
			});
			_updateTime.Start();
		}

		
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				DragMove();
			}
		}

		private void CloseOverlay_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void LblDate_OnLoaded(object sender, RoutedEventArgs e)
		{
			LblDate.Content = "Getting Date...";
		}

		private void LblTime_OnLoaded(object sender, RoutedEventArgs e)
		{
			LblTime.Content = "Getting Time...";
		}

		private void ShowSettings_Click(object sender, RoutedEventArgs e)
		{
			settingsWindowAccess.Show();
		}

		//TODO: Add context menu items to change font and time/date styles
		private void ShowAbout_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		//TODO: Send and recieve settings info to and from the settings window to save into XML Serilizable
		private void MainWindow_OnClosing(object sender, CancelEventArgs e)
		{
			MessageBox.Show("Saving settings...");
			SettingsInfo saveSettings = settingsWindowAccess.Settings;
			XmlSerializer writer = new XmlSerializer(typeof(SettingsInfo));
			FileStream file = File.Create(_path);

			try
			{
				writer.Serialize(file, saveSettings);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error writing settings to document:" + ex.Message + "..... " + ex.StackTrace);
			}
			finally
			{
				file.Close();
			}
			
			
		}
	}
}
