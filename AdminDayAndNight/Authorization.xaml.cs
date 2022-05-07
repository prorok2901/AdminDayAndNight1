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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminDayAndNight
{
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void CreateCaptcha()
        {
            string allowChar = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
            string pwd = "";

            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                pwd += allowChar.Substring(random.Next(0, allowChar.Length), 1);
            }
            captcha.Text = pwd;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(captcha.Text == ValidateCaptcha.Text)
                {
                    Home home = Pages.HomeUser();
                    NavigationService.Navigate(home);
                }
                else
                {
                    CreateCaptcha();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateCaptcha();
            CapTcha.Visibility = Visibility.Visible;
        }
    }
}
