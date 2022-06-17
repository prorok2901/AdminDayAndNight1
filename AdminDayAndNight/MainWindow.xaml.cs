using AdminDayAndNight.Connector;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AdminDayAndNight
{
    public partial class MainWindow : Window
    {
        BD_Data basaBD = new BD_Data();

        public MainWindow()
        {
            InitializeComponent();
            chekSuperUser();
        }

        private void chekSuperUser()
        {
            if (basaBD.DataBase().user.Any(a => a.role1.name == "Super_User" && a.status_user.name == "Работает"))
            {
                Opupet.Navigate(Pages.AutorizationUser());
            }
            else
            {
                Opupet.Navigate(Pages.RegistrationUser(FirstStart, StartImage, false));
                FirstStart.Visibility = Visibility.Visible;
                StartImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Image/First_Start.png"));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Windows.Close();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Windows.DragMove();
        }
    }
}
