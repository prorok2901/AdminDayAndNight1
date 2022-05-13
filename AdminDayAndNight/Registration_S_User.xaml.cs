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
            BD.user SuperUser;
            BD.role role;
            BD.status_user status;

            if (LoginUser != null && PasswordUser != null && NameUser != null)
            {
                var roleUser = BD_Data.DataBase().role.FirstOrDefault(a => a.name == "Super_User");
                var statusUser = BD_Data.DataBase().role.FirstOrDefault(a => a.name == "Работает");

                role = new BD.role
                {
                    name = "Super_User"
                };

                status = new BD.status_user
                {
                    name = "Работает"
                };

                if(roleUser == null && statusUser == null)
                {
                    BD_Data.DataBase().role.Add(role);
                    BD_Data.DataBase().status_user.Add(status);
                }

                SuperUser = new BD.user
                {
                    name = NameUser.Text,
                    role = role.name,
                    user_status = status.name,
                    password = PasswordUser.Text,
                    login = LoginUser.Text,
                };

                if (grid.Visibility != Visibility.Hidden)
                {
                    grid.Visibility = Visibility.Hidden;
                    image.ImageSource = new BitmapImage(new Uri(@"C:\Users\proro\Source\Repos\prorok2901\AdminDayAndNight1\AdminDayAndNight\Image\fsdfgh1.png"));
                }

                BD_Data.DataBase().user.Add(SuperUser);
                BD_Data.DataBase().SaveChanges();

                NavigationService.Navigate(Pages.AutorizationUser(LoginUser.Text, PasswordUser.Text));
            }
            else
            {
                MessageBox.Show("Не все поля заполнены");
            }
        }
    }
}
