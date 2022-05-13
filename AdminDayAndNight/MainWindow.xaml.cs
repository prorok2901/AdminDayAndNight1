using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AdminDayAndNight
{

    public partial class MainWindow : Window
    {
        BD.DayAndNightEntities basaBD = new BD.DayAndNightEntities();
        public MainWindow()
        {
            InitializeComponent();
            chekSuperUser();
        }
        private void chekSuperUser()
        {
            BD.user super_admin = basaBD.user.FirstOrDefault(a => a.role == "Super_User");
            if (super_admin == null)
            {
                Opupet.Navigate(Pages.RegistrationUser(FirstStart, StartImage));
                FirstStart.Visibility = Visibility.Visible;
                StartImage.ImageSource = new BitmapImage(new Uri(@"C:\Users\proro\Source\Repos\prorok2901\AdminDayAndNight1\AdminDayAndNight\Image\First_Start.png"));
            }
            else
            {
                Opupet.Navigate(Pages.AutorizationUser());
            }
        }
    }
}
