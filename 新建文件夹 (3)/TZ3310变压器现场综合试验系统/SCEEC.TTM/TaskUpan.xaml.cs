using SCEEC.MI.TZ3310;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SCEEC.TTM
{
    /// <summary>
    /// TaskUpan.xaml 的交互逻辑
    /// </summary>
    public partial class TaskUpan : Window
    {
        public string[] Tasknames;
        public TaskUpan(string[] tasknames)
        {
            InitializeComponent();
            Tasknames = tasknames;

        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MinimumButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximumButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                maximumButtonImage.Source = new BitmapImage(new Uri("Resources/maximum.png", UriKind.Relative));
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                maximumButtonImage.Source = new BitmapImage(new Uri("Resources/maximum2.png", UriKind.Relative));
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

        }


        private void ProgressBegin()
        {

            Thread thread = new Thread(new ThreadStart(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    this.LaunchProgressBar.Dispatcher.BeginInvoke((ThreadStart)delegate { this.LaunchProgressBar.Value = i; });
                    this.Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        IsMessage.Text = i.ToString() + " %";
                    });
                    Thread.Sleep(20);
                    if (i == 100)
                    {
                        this.Dispatcher.BeginInvoke((Action)delegate ()
                        {
                            IsMessage.Text = "100%";
                        });

                    }
                }

            }));
            thread.Start();

        }
        private void ProgressEnd()
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                if (LaunchProgressBar != null)
                {
                    this.LaunchProgressBar.Dispatcher.BeginInvoke((ThreadStart)delegate { this.LaunchProgressBar.Value = 0; });
                    this.Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        if (IsMessage != null)
                            IsMessage.Text = "";
                    });
                }
            }));
            thread.Start();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobListBox.SelectedIndex > -1)
            {
                Insertdatabyupan inser = new Insertdatabyupan(Drivenames[Drivenames.Length - 1] + Tasknames[JobListBox.SelectedIndex]);
                inser.InsertUpandatatodatabase();
                ProgressBegin();
            }
            else
            {
                IsMessage.Text = "NoFiles";
            }
        }

        private void JobListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.LaunchProgressBar.Dispatcher.BeginInvoke((ThreadStart)delegate { this.LaunchProgressBar.Value = 0; });
            IsMessage.Text = "";
        }

        private void JobListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (JobListBox.SelectedIndex > -1)
            {
                Insertdatabyupan inser = new Insertdatabyupan(Drivenames[Drivenames.Length - 1] + Tasknames[JobListBox.SelectedIndex]);
                inser.InsertUpandatatodatabase();
                ProgressBegin();

                // this.Close();
            }
        }
        string[] Drivenames = Environment.GetLogicalDrives();

        private void UpanWindows_loaded(object sender, RoutedEventArgs e)
        {
            Drivenames = Environment.GetLogicalDrives();
            string[] testTime = new string[Tasknames.Length];
            for (int i = 0; i < Tasknames.Length; i++)
            {
                INIFiLE ini = new INIFiLE(Drivenames[Drivenames.Length - 1] + Tasknames[i] + "\\Information.ini");
                testTime[i] = "Name:   " + Tasknames[i] + "(" + ini.ReadString("information", "测试时间", "") + ")";
            }
            JobListBox.ItemsSource = testTime;
            try { JobListBox.SelectedItem = 0; } catch { }
        }
    }
}
