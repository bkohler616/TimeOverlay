using System;
using System.Collections.Generic;
using System.Linq;
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
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow: Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object o, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if(e.ChangedButton == MouseButton.Left)
				this.DragMove();
		}

		private void CloseOverlay_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
		//TODO: Add timer to update lblTimer on what time it is.


	}
}
