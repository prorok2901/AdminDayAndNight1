using AdminDayAndNight.Connector;
using AdminDayAndNight.LittleThings;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace AdminDayAndNight
{
    public partial class Registration_S_User : Page
    {
        private BD_Data dataBase = new BD_Data();
        private Grid grid = new Grid();
        private ImageBrush image = new ImageBrush();

        public Registration_S_User()
        {
            InitializeComponent();

            Canvas.SetZIndex(PasswordBoxUser, -1);

            GoBack.Visibility = Visibility.Visible;
        }
        public Registration_S_User(Grid _grid, ImageBrush _startImage, bool _isUsers)
        {
            InitializeComponent();

            if (!_isUsers)
            {
                GoBack.Visibility = Visibility.Hidden;
            }

            grid = _grid;
            image = _startImage;

            Canvas.SetZIndex(PasswordBoxUser, -1);
        }

        private void Button_Click(object _sender, RoutedEventArgs _e)
        {
            BD.user SuperUser;
            BD.role role;
            BD.status_user status;

            if (LoginUser != null && PasswordUser != null && NameUser != null)
            {
                BD.role roleUser = dataBase.DataBase().role.FirstOrDefault(a => a.name == "Super_User");
                BD.status_user statusUser = dataBase.DataBase().status_user.FirstOrDefault(a => a.name == "Работает");

                BD.user user = dataBase.DataBase().user.FirstOrDefault(a => a.login == LoginUser.Text);

                role = new BD.role
                {
                    name = "Super_User"
                };

                status = new BD.status_user
                {
                    name = "Работает"
                };

                if (roleUser == null)
                {
                    dataBase.DataBase().role.Add(role);
                    dataBase.DataBase().SaveChanges();

                    roleUser = dataBase.DataBase().role.FirstOrDefault(a => a.name == "Super_User");
                }

                if (statusUser == null)
                {
                    dataBase.DataBase().status_user.Add(status);
                    dataBase.DataBase().SaveChanges();

                    statusUser = dataBase.DataBase().status_user.FirstOrDefault(a => a.name == "Работает");
                }

                if (user == null)
                {
                    SuperUser = new BD.user
                    {
                        name = NameUser.Text,
                        role = roleUser.ID,
                        user_status = statusUser.ID,
                        password = PasswordBoxUser.Password,
                        login = LoginUser.Text,
                    };

                    dataBase.DataBase().user.Add(SuperUser);
                    dataBase.DataBase().SaveChanges();

                    NavigationService.Navigate(Pages.AutorizationUser(LoginUser.Text, PasswordBoxUser.Password));
                }
                else
                {
                    MessageBox.Show("Логин занят");
                }

                if (grid.Visibility != Visibility.Hidden)
                {
                    grid.Visibility = Visibility.Hidden;
                    image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Image/BackgroundImage.png"));
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены");
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

        private void Name_GotFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Введите имя", NameUser);
        }
        private void Name_LostFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Введите имя", NameUser);
        }

        private void Password_GotFocus(object _sender, RoutedEventArgs _e)
        {
            Canvas.SetZIndex(PasswordBoxUser, 1);
            PasswordBoxUser.Focus();
        }
        private void Password_LostFocus(object _sender, RoutedEventArgs _e)
        {
            if (PasswordBoxUser.Password == "")
            {
                Canvas.SetZIndex(PasswordBoxUser, -1);
            }
        }

        private void Inkognito_MouseEnter(object _sender, MouseEventArgs _e)
        {
            FillEmptyBox.FillEmptyPassword(PasswordBoxUser, PasswordUser, Inkognito, new BitmapImage(new Uri("pack://application:,,,/Image/Open.png")), 0, false);
        }
        private void Inkognito_MouseLeave(object _sender, MouseEventArgs _e)
        {
            FillEmptyBox.FillEmptyPassword(PasswordBoxUser, PasswordUser, Inkognito, new BitmapImage(new Uri("pack://application:,,,/Image/Closed.png")), 1, true);
        }

        private void Button_Click_1(object _sender, RoutedEventArgs _e)
        {
            NavigationService.GoBack();
        }
    }
}
