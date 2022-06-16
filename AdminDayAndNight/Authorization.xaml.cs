using AdminDayAndNight.Connector;
using AdminDayAndNight.LittleThings;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace AdminDayAndNight
{
    public partial class Authorization : Page
    {
        private BD_Data dataBase = new BD_Data();

        private static string allowChar = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
        
        public Authorization(string _login, string _password)
        {
            InitializeComponent();
            LoginUser.Text = _login;
            PasswordBoxUser.Password = _password;
        }
        public Authorization()
        {
            InitializeComponent();
            Canvas.SetZIndex(PasswordBoxUser, -1);
        }


        private void CreateCaptcha()
        {
            ValidateCaptcha.Text = "";
            string pwd = "";
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                pwd += allowChar.Substring(random.Next(0, allowChar.Length), 1);
            }
            captcha.Text = pwd;
        }

        private void TextBox_KeyDown(object _sender, KeyEventArgs _e)
        {
            if (_e.Key == Key.Enter)
            {
                //if (captcha.Text == ValidateCaptcha.Text)
                //{
                    NavigationService.Navigate(Pages.HomeUser());
                //}
                //else
                //{
                //    CreateCaptcha();
                //}
            }
        }

        private void Button_Click(object _sender, RoutedEventArgs _e)
        {
            if(LoginUser !=null && PasswordUser != null)
            {
                if (dataBase.DataBase().user.Any(a => a.login == LoginUser.Text && a.password == PasswordBoxUser.Password))
                {
                    CreateCaptcha();
                    CapTcha.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Такого пользователя нет, либо допущена ошибка при вводе данных");
                }
            }
            return;
        }

        private void Button_Click_1(object _sender, RoutedEventArgs _e)
        {
            NavigationService.Navigate(Pages.RegistrationUser());
        }

        private void Inkognito_MouseEnter(object _sender, MouseEventArgs _e)
        {
            FillEmptyBox.FillEmptyPassword(PasswordBoxUser, PasswordUser, Inkognito, new BitmapImage(new Uri("pack://application:,,,/Image/Open.png")),0, false);
        }
        private void Inkognito_MouseLeave(object _sender, MouseEventArgs _e)
        {
            FillEmptyBox.FillEmptyPassword(PasswordBoxUser, PasswordUser, Inkognito, new BitmapImage(new Uri("pack://application:,,,/Image/Closed.png")), 1, true);
        }

        private void Password_GotFocus(object _sender, RoutedEventArgs _e)
        {
            Canvas.SetZIndex(PasswordBoxUser, 1);
            PasswordBoxUser.Focus();
        }
        private void Password_LostFocus(object _sender, KeyboardFocusChangedEventArgs _e)
        {
            if (PasswordBoxUser.Password == "")
            {
                Canvas.SetZIndex(PasswordBoxUser, -1);
            }
        }

        private void Login_GotFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Введите логин", LoginUser);
        }
        private void Login_LostFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Введите логин", LoginUser);
        }
    }
}
