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

namespace SCEEC.TTM
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        internal bool verified = false;

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        internal void LoginVerification()
        {   
            string username = this.UsernameTextBox.Text;
            string password = this.PasswordTextBox.Password;

            if (username == string.Empty)
            {
                MessageBox.Show("请输入用户名", "登录错误", MessageBoxButton.OK, MessageBoxImage.Error);
                this.UsernameTextBox.Focus();
                return;
            }
            if (password == string.Empty)
            {
                MessageBox.Show("请输入密码", "登录错误", MessageBoxButton.OK, MessageBoxImage.Error);
                this.PasswordTextBox.Focus();
                return;
            }

            GlobalSettings.currentUser.name = username;
            GlobalSettings.currentUser.password = password;

            string errorText;
            verified = SCEEC.Config.Users.userVerify(ref GlobalSettings.currentUser, out errorText);
            GlobalSettings.Startuped = verified;

            if (!verified)
            {
                MessageBox.Show(errorText, "登录错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (verified)
            {
                this.Close();
            }
        }

        public LoginWindow()
        {
            InitializeComponent();
            //if (GlobalSettings.Startuped)
            //    closeButton.Visibility = Visibility.Visible;
            //else
            //    closeButton.Visibility = Visibility.Hidden;
            UsernameTextBox.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginVerification();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginVerification();
            }
        }

        private void Clos_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.verified == false)
            {
                if (GlobalSettings.Startuped)
                    e.Cancel = true;
                else
                    Application.Current.Shutdown();
            }
        }
    }
}
