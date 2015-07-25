using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace TimeOverlay
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private readonly Timer _updateTime;
		private DateTime _currentDateTime;
		public MainWindow()
		{

			InitializeComponent();
			_updateTime = new Timer {Interval = 1000};
			_currentDateTime = new DateTime();
			_updateTime.Elapsed += UpdateTime_Elapsed;
			_updateTime.Start();


		}

		private void UpdateTime_Elapsed(object sender, ElapsedEventArgs elapsedEventArgs)
		{
			_updateTime.Stop();
			_currentDateTime = DateTime.Now;
			Application.Current.Dispatcher.BeginInvoke((Action) delegate()
			{
				LblDate.Content = _currentDateTime.DayOfWeek + ", " + _currentDateTime.Day + "/" + _currentDateTime.Month + "/" +
				                  _currentDateTime.Year;
				LblTime.Content = _currentDateTime.Hour + ":" + _currentDateTime.Minute + "." + _currentDateTime.Second;
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

		//TODO: Add context menu items to change font and time/date styles
	}
}
