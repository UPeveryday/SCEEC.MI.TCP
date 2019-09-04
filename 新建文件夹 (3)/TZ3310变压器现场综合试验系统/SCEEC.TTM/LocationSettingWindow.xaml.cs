using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SCEEC.MI.TZ3310;


namespace SCEEC.TTM
{
    /// <summary>
    /// LocationSettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LocationSettingWindow : Window
    {
        private bool textChanged = false;
        private bool Changingconfirm = false;
        private string originName = string.Empty;
        private bool closingConfirm = false;

        public string name;

        public bool updateLocation()
        {
            string name = NameTextBox.Text.Trim();
            Location location = new Location();
            location.id = -1;
            if (Changingconfirm && (originName != name))
            {
                WorkingSets.local.deleteLocation(originName);
                if (!WorkingSets.local.LocalSQLClient.Connected)
                {
                    ErrorReporter.ErrorReport(30003, "位置管理器", WorkingSets.local.LocalSQLClient.ErrorText);
                    
                    return false;
                }
            }
            else
            {
                location = WorkingSets.local.getLocation(name);
            }
            if (!WorkingSets.local.LocalSQLClient.Connected)
            {
                ErrorReporter.ErrorReport(30002, "位置管理器", WorkingSets.local.LocalSQLClient.ErrorText);
                return false;
            }
            if (location.id > -1)
            {
                if (Changingconfirm)
                {
                    WorkingSets.local.deleteLocation(name);
                }
            }
            WorkingSets.local.addLocation(
                name: NameTextBox.Text, 
                company: CompanyTextBox.Text, 
                address: AddressTextBox.Text, 
                operatorName: OperatorTextBox.Text, 
                id: location.id);
            if (!WorkingSets.local.LocalSQLClient.Connected)
            {
                ErrorReporter.ErrorReport(30003, "位置管理器", WorkingSets.local.LocalSQLClient.ErrorText);
                return false;
            }
            return true;
        }

        public LocationSettingWindow(string name = "")
        {
            InitializeComponent();
            NameTextBox.Text = name;
            if (name != string.Empty)
            {
                this.name = name;
                Location location;
                location = WorkingSets.local.getLocation(name);
                if (!WorkingSets.local.LocalSQLClient.Connected)
                {
                    ErrorReporter.ErrorReport(30002, "位置管理器", WorkingSets.local.LocalSQLClient.ErrorText);
                }
                if (location.id > -1)
                {
                    TitleTextBlock.Text = "修改位置";
                    Changingconfirm = true;
                    originName = name;
                    CompanyTextBox.Text = location.company;
                    AddressTextBox.Text = location.address;
                    OperatorTextBox.Text = location.operatorName;
                    textChanged = false;
                }
                else
                {
                    TitleTextBlock.Text = "新位置";
                }
            }
            else
                TitleTextBlock.Text = "新位置";
            NameTextBox.Focus();
        }

        private void closeWithConfirm()
        {

            if (NameTextBox.Text == string.Empty)
            {
                MessageBox.Show("请输入位置名称!", "位置管理器", MessageBoxButton.OK, MessageBoxImage.Information);
                NameTextBox.Focus();
                return;
            }
            if (updateLocation())
            {
                name = NameTextBox.Text.Trim();
                closingConfirm = true;
                this.Close();
            }
        }

        private bool closeWithoutConfirm()
        {
            if (closingConfirm) return true;
            if (!textChanged)
            {
                return true;
            }
            switch (MessageBox.Show("位置已发生更改，是否进行保存?", "位置管理器", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
            {
                case MessageBoxResult.Yes:
                    closeWithConfirm();
                    return true;
                case MessageBoxResult.No:
                    return true;
                case MessageBoxResult.Cancel:
                    return false;
            }
            return false;
        }

        //private void Grid_KeyDown(object sender, KeyEventArgs e)
        //{
        //    switch (e.Key)
        //    {
        //        case Key.Enter:
        //            closeWithConfirm();
        //            break;
        //        case Key.Escape:
                    
        //            break;
        //    }
        //}

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            textChanged = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!closeWithoutConfirm()) e.Cancel = true;
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
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            closeWithConfirm();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Minimized:
                    break;
                default:
                    this.Show();
                    this.Activate();
                    break;
            }
        }

        private void NameTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            NameTextBox.Text = NameTextBox.Text.Trim();
            Location location = WorkingSets.local.getLocation(NameTextBox.Text);
            if (!WorkingSets.local.LocalSQLClient.Connected)
            {
                ErrorReporter.ErrorReport(30002, "位置管理器", WorkingSets.local.LocalSQLClient.ErrorText);
            }
            if (!Changingconfirm)
            {
                if (location.id > -1)
                {
                    switch (MessageBox.Show("该位置名称已存在，是否对该该位置信息进行修改?", "位置名称重复", MessageBoxButton.YesNo, MessageBoxImage.Exclamation))
                    {
                        case MessageBoxResult.Yes:
                            TitleTextBlock.Text = "修改位置";
                            originName = NameTextBox.Text;
                            CompanyTextBox.Text = location.company;
                            AddressTextBox.Text = location.address;
                            OperatorTextBox.Text = location.operatorName;
                            Changingconfirm = true;
                            NameTextBox.IsEnabled = false;
                            break;
                        default:
                            NameTextBox.Focus();

                            break;
                    }
                }
            }
            else
            {
                if ((location.id > -1) && (NameTextBox.Text != originName))
                {
                    MessageBox.Show("该位置名称与已存在的其他记录重复，请更换名称。", "位置名称重复", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    NameTextBox.Focus();
                }
            }
        }

        private void CompanyTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            CompanyTextBox.Text = CompanyTextBox.Text.Trim();
        }

        private void AddressTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            AddressTextBox.Text = AddressTextBox.Text.Trim();
        }

        private void OperatorTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            OperatorTextBox.Text = OperatorTextBox.Text.Trim();
        }

        private void NameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
            if (e.Key == Key.Enter)
            {
                NameTextBox.Text = NameTextBox.Text.Trim();
                Location location = WorkingSets.local.getLocation(NameTextBox.Text);
                if (!WorkingSets.local.LocalSQLClient.Connected)
                {
                    ErrorReporter.ErrorReport(30002, "位置管理器", WorkingSets.local.LocalSQLClient.ErrorText);
                }
                if (!Changingconfirm)
                {
                    if (location.id > -1)
                    {
                        switch (MessageBox.Show("该位置名称已存在，是否对该该位置信息进行修改?", "位置名称重复", MessageBoxButton.YesNo, MessageBoxImage.Exclamation))
                        {
                            case MessageBoxResult.Yes:
                                originName = NameTextBox.Text;
                                CompanyTextBox.Text = location.company;
                                AddressTextBox.Text = location.address;
                                OperatorTextBox.Text = location.operatorName;
                                Changingconfirm = true;
                                break;
                            default:
                                NameTextBox.Focus();
                                return;
                        }
                    }
                    closeWithConfirm();
                }
                else
                {
                    if ((location.id > -1) && (NameTextBox.Text != originName))
                    {
                        MessageBox.Show("该位置名称与已存在的其他记录重复，请更换名称。", "位置名称重复", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        NameTextBox.Focus();
                        return;
                    }
                    closeWithConfirm();
                }
            }
        }

        private void CompanyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) this.Close();
            if (e.Key == Key.Enter) closeWithConfirm();
        }

        private void AddressTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) this.Close();
            if (e.Key == Key.Enter) closeWithConfirm();
        }

        private void OperatorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) this.Close();
            if (e.Key == Key.Enter) closeWithConfirm();
        }
    }
}
