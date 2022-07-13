using System;
using System.Windows;
using System.Windows.Threading;

namespace GameOfLife
{
	public partial class MainWindow : Window
	{
		private Grid mainGrid;
		DispatcherTimer timer;   //  Generation timer
		private int genCounter;
		private AdWindow[] adWindow;


		public MainWindow()
		{
			InitializeComponent();

			Closing += MainWindow_Closing;
			Closed += MainWindow_Closed;
			mainGrid = new Grid(MainCanvas);

			timer = new DispatcherTimer();
			timer.Tick += OnTimer;
			timer.Interval = TimeSpan.FromMilliseconds(200);
		}

		private void MainWindow_Closing(object sender, EventArgs e)
		{
			timer.Stop();
			timer.Tick -= OnTimer;
		}

		private void MainWindow_Closed(object sender, EventArgs e)
		{
			mainGrid.Clear();
			timer = null;
		}

		private void StartAd()
		{
			adWindow = new AdWindow[2];
			for (int i = 0; i < 2; i++)
			{
				if (adWindow[i] == null)
				{
					adWindow[i] = new AdWindow(this);
					adWindow[i].Closed += AdWindowOnClosed;
					adWindow[i].Top = this.Top + (330 * i) + 70;
					adWindow[i].Left = this.Left + 240;
					adWindow[i].Show();
				}
			}
		}

		private void AdWindowOnClosed(object sender, EventArgs eventArgs)
		{
			for (int i = 0; i < 2; i++)
			{
				adWindow[i].Closed -= AdWindowOnClosed;
				adWindow[i] = null;
			}
		}


		private void Button_OnClick(object sender, EventArgs e)
		{
			if (!timer.IsEnabled)
			{
				timer.Start();
				ButtonStart.Content = "Stop";
				StartAd();
			}
			else
			{
				timer.Stop();
				ButtonStart.Content = "Start";
			}
		}

		private void OnTimer(object sender, EventArgs e)
		{
			mainGrid.Update();
			genCounter++;
			lblGenCount.Content = genCounter;
		}

		private void ButtonClear_Click(object sender, RoutedEventArgs e)
		{
			mainGrid.Clear();
		}

	}
}
