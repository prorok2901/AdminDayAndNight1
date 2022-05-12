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
    public partial class Registration_S_User : Page
    {
        Grid grid = new Grid();
        ImageBrush image = new ImageBrush();
        public Registration_S_User()
        {
            InitializeComponent();
        }
        public Registration_S_User(Grid _grid, ImageBrush startImage)
        {
            InitializeComponent();
            grid = _grid;
            image = startImage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (grid.Visibility != Visibility.Hidden)
            {
                grid.Visibility = Visibility.Hidden;
                image.ImageSource = new BitmapImage(new Uri(@"C:\Users\proro\Source\Repos\prorok2901\AdminDayAndNight1\AdminDayAndNight\fsdfgh1.png")); ;
            }

            NavigationService.Navigate(Pages.AutorizationUser(LoginUser.Text, PasswordUser.Text));
        }
    }
}
