using AdminDayAndNight.LittleThings;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AdminDayAndNight
{
    public partial class Home : Page
    {
        private BD_Data dataBase = new BD_Data();

        //работа со статусами пользователя:
        private string prevStatusName;

        //работа с ролями пользователя
        private string prevRoleName;

        //работа с пользователем:
        private bool IsLoginChange;
        private bool IsPasswordChange;
        private bool IsNameChange;

        private bool IsRoleChange;
        private bool IsStatusChange;

        private BD.user PrevUser;

        //работа с номерами отеля
        private bool IsTypeRoom;
        private bool IsStatusRoom;

        private bool IsCapacityRoom;
        private bool IsCountRoom;
        private bool IsNumberRoom;
        private bool IsPriceRoom;
        private bool IsShortDescription;


        private BD.info_room PrevRoom;

        //работа со статусом комнаты
        private string PrevStatusNameRoom;

        //работа с типами номеров
        private string PrevTypeNameRoom;

        public Home()
        {
            InitializeComponent();
        }

        private void Button_ClickAddStatusList(object _sender, RoutedEventArgs _e)
        {
            StatusNameUser.Text = "Введите название нового статуса";

            prevStatusName = StatusNameUser.Text;

            DeleteStatusUser.Content = "Отменить";
            ChangeStatusUser.Content = "Сохранить";

            DataStatusUser.Visibility = Visibility.Visible;
        }
        private void Button_ClickRoleAddList(object _sender, RoutedEventArgs _e)
        {
            RoleUserBox.Text = "Введите название нового статуса";

            prevRoleName = TypeRoomName.Text;

            DeleteRoleUser.Content = "Отменить";
            ChangeRoleUser.Content = "Сохранить";

            DataRoleUser.Visibility = Visibility.Visible;

        }

        private void Button_ClickStatus(object _sender, RoutedEventArgs _e)
        {
            string buttonContent = (_sender as Button).Content.ToString();

            StatusNameUser.Text = buttonContent;

            prevStatusName = buttonContent;

            DeleteStatusUser.Content = "Удалить";
            ChangeStatusUser.Content = "Изменить";

            DataStatusUser.Visibility = Visibility.Visible;
        }

        private void Button_ClickRole(object _sender, RoutedEventArgs _e)
        {
            string buttonContent = (_sender as Button).Content.ToString();

            RoleUserBox.Text = buttonContent;

            prevRoleName = buttonContent;

            DeleteRoleUser.Content = "Удалить";
            ChangeRoleUser.Content = "Изменить";

            DataRoleUser.Visibility = Visibility.Visible;
        }

        private void DeleteStatusUser_Click(object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;

            BD.status_user status = dataBase.DataBase().status_user.FirstOrDefault(a => a.name == prevStatusName);
            BD.user user;
            BD.borrow_room borrowRoom;
            BD.booking_history bookingHistory;
            BD.blocking blocking;

            if ((_sender as Button).Content.ToString() == "Удалить")
            {
                if (status != null)
                {
                    user = dataBase.DataBase().user.FirstOrDefault(a => a.status_user == status);

                    if (user != null)
                    {
                        borrowRoom = dataBase.DataBase().borrow_room.FirstOrDefault(a => a.administrator == user.id);
                        blocking = dataBase.DataBase().blocking.FirstOrDefault(a => a.block_user == user.id);

                        if (blocking != null)
                        {
                            dataBase.DataBase().blocking.Remove(blocking);
                        }
                        if (borrowRoom != null)
                        {
                            bookingHistory = dataBase.DataBase().booking_history.FirstOrDefault(a => a.administrator == user.id);

                            if (bookingHistory != null)
                            {
                                dataBase.DataBase().booking_history.Remove(bookingHistory);
                            }
                            dataBase.DataBase().borrow_room.Remove(borrowRoom);
                        }
                        dataBase.DataBase().user.Remove(user);
                    }
                    dataBase.DataBase().status_user.Remove(status);
                    dataBase.DataBase().SaveChanges();
                }
            }
            DataStatusUser.Visibility = Visibility.Hidden;
            UpdataListStatus();
        }

        private void ChangeStatusUser_Click(object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;
            if (button.Opacity != 0.5)
            {
                BD.status_user status = dataBase.DataBase().status_user.FirstOrDefault(a => a.name == prevStatusName);

                if (button.Content.ToString() == "Изменить")
                {
                    if (dataBase.DataBase().status_user.Any(a => a.ID == status.ID))
                    {
                        status.name = RoleUserBox.Text;
                    }
                }
                else if (button.Content.ToString() == "Сохранить" && button.Opacity != 0.5)
                {
                    if (!dataBase.DataBase().status_user.Any(a => a.name == StatusNameUser.Text))
                    {
                        BD.status_user newRoleUser = new BD.status_user
                        {
                            name = StatusNameUser.Text
                        };
                        dataBase.DataBase().status_user.Add(newRoleUser);
                    }
                    else
                    {
                        MessageBox.Show("Такой статус уже есть в базе данных");
                    }
                }
                dataBase.DataBase().SaveChanges();
                DataStatusUser.Visibility = Visibility.Hidden;
                ChangeStatusUser.Opacity = 0.5;
                UpdataListStatus();
            }
        }

        private void StatusNameUser_KeyUp(object _sender, KeyEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if (box.Text != "")
            {
                if (prevStatusName != box.Text && "Введите название нового статуса" != box.Text)
                {
                    ChangeStatusRoom.Opacity = 1;
                }
                else
                {
                    ChangeStatusRoom.Opacity = 0.5;
                }
            }
            else
            {
                ChangeStatusRoom.Opacity = 0.5;
            }

        }
        private void UpdataListStatus()
        {
            StatusList.Items.Clear();

            if (dataBase.DataBase().status_user.Any(a => a.name != "Super_User"))
            {
                foreach (BD.status_user r in dataBase.DataBase().status_user.ToList())
                {
                    Button nameStatus = new Button
                    {
                        Width = 160,
                        Height = 20,
                        Content = r.name,
                    };
                    nameStatus.Style = (Style)nameStatus.FindResource("ButtonK");
                    nameStatus.Click += Button_ClickStatus;

                    StatusList.Items.Add(nameStatus);
                }
            }
        }

        private void UpdateRoleList()
        {
            RoleList.Items.Clear();

            if (dataBase.DataBase().role.Any())
            {
                foreach (BD.role r in dataBase.DataBase().role.ToList())
                {
                    Button nameRole = new Button
                    {
                        Width = 160,
                        Height = 20,
                        Content = r.name,
                    };
                    nameRole.Style = (Style)nameRole.FindResource("ButtonK");
                    nameRole.Click += Button_ClickRole;

                    RoleList.Items.Add(nameRole);
                }
            }
        }

        private void TextBoxRole_KeyUp(object _sender, KeyEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if (box.Text != "")
            {
                if (PrevTypeNameRoom != box.Text && "Введите название новой роли пользователя:" != box.Text)
                {
                    ChangeRoleUser.Opacity = 1;
                    return;
                }
            }
            ChangeRoleUser.Opacity = 0.5;
        }

        private void TabItem_MouseUpStatus(object _sender, MouseButtonEventArgs _e)
        {
            StatusUserControl.Visibility = Visibility.Visible;

            UpdataListStatus();
        }
        private void TabItem_MouseUpUser(object _sender, MouseButtonEventArgs _e)
        {
            UserControl.Visibility = Visibility.Visible;

            UpdateUserList();
        }
        private void TabItem_MouseUpRole(object _sender, MouseButtonEventArgs _e)
        {
            UserRoleData.Visibility = Visibility.Visible;

            UpdateRoleList();
        }

        private void ChangeRoleUser_Click(object _sender, RoutedEventArgs _e)
        {

            Button button = _sender as Button;
            if (button.Opacity != 0.5)
            {
                BD.role role = dataBase.DataBase().role.FirstOrDefault(a => a.name == prevRoleName);

                if (button.Content.ToString() == "Изменить")
                {
                    if (dataBase.DataBase().role.Any(a => a.ID == role.ID))
                    {
                        role.name = RoleUserBox.Text;
                    }
                }
                else if (button.Content.ToString() == "Сохранить" && button.Opacity != 0.5)
                {
                    if (!dataBase.DataBase().role.Any(a => a.name == RoleUserBox.Text))
                    {
                        BD.role newRoleUser = new BD.role
                        {
                            name = RoleUserBox.Text
                        };
                        dataBase.DataBase().role.Add(newRoleUser);
                    }
                    else
                    {
                        MessageBox.Show("Такая роль уже есть в базе данных");
                    }
                }
                dataBase.DataBase().SaveChanges();

                DataRoleUser.Visibility = Visibility.Hidden;

                ChangeRoleUser.Opacity = 0.5;
                UpdateTypeRoomList();
            }
            
        }

        private void DeleteRoleUser_Click(object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;

            BD.role role = dataBase.DataBase().role.FirstOrDefault(a => a.name == prevRoleName);
            BD.user user;
            BD.borrow_room borrowRoom;
            BD.booking_history bookingHistory;
            BD.blocking blocking;

            if ((_sender as Button).Content.ToString() == "Удалить")
            {
                if (role != null)
                {
                    user = dataBase.DataBase().user.FirstOrDefault(a => a.role == role.ID);
                    if (user != null)
                    {
                        borrowRoom = dataBase.DataBase().borrow_room.FirstOrDefault(a => a.administrator == user.id);
                        blocking = dataBase.DataBase().blocking.FirstOrDefault(a => a.block_user == user.id);

                        if (blocking != null)
                        {
                            dataBase.DataBase().blocking.Remove(blocking);
                        }
                        if (borrowRoom != null)
                        {
                            bookingHistory = dataBase.DataBase().booking_history.FirstOrDefault(a => a.administrator == user.id);

                            if (bookingHistory != null)
                            {
                                dataBase.DataBase().booking_history.Remove(bookingHistory);
                            }
                            dataBase.DataBase().borrow_room.Remove(borrowRoom);
                        }
                        dataBase.DataBase().user.Remove(user);
                    }
                    dataBase.DataBase().role.Remove(role);
                    dataBase.DataBase().SaveChanges();
                }
            }
            DataRoleUser.Visibility = Visibility.Hidden;
            UpdateRoleList();
        }

        private void ButtonClickUserList(object _sender, RoutedEventArgs _e)
        {
            string login = (_sender as Button).Content.ToString();

            BD.role role = dataBase.DataBase().role.FirstOrDefault(a => a.name == "Super_User");

            PrevUser = dataBase.DataBase().user.FirstOrDefault(a => a.login == login && a.role != role.ID);

            UpdateComboBoxUser();

            if (PrevUser != null)
            {
                LoginEmployee.Text = PrevUser.login;
                NameEmployee.Text = PrevUser.name;
                PasswordEmployee.Text = PrevUser.password;

                RoleEmployee.Text = PrevUser.role1.name;
                StatusEmployee.Text = PrevUser.status_user.name;

                DeleteUser.Content = "Уволить";
                ChangeUser.Content = "Изменить";

                PanelEmployee.Visibility = Visibility.Visible;
            }
        }

        private void Button_AddEmployee(object sender, RoutedEventArgs e)
        {
            LoginEmployee.Text = "Логин Работника";
            NameEmployee.Text = "Имя";
            PasswordEmployee.Text = "Пароль работника";

            UpdateComboBoxUser();

            RoleEmployee.SelectedIndex = -1;
            StatusEmployee.SelectedIndex = -1;

            DeleteUser.Content = "Отменить";
            ChangeUser.Content = "Нанять";

            PanelEmployee.Visibility = Visibility.Visible;
        }

        private void UpdateComboBoxUser()
        {
            RoleEmployee.Items.Clear();
            StatusEmployee.Items.Clear();

            foreach (BD.role r in dataBase.DataBase().role.ToList())
            {
                if (r.name != "Super_User")
                {
                    RoleEmployee.Items.Add(r.name);
                }
            }
            foreach (BD.status_user r in dataBase.DataBase().status_user.ToList())
            {
                StatusEmployee.Items.Add(r.name);
            }
        }

        private void UpdateUserList()
        {
            UserList.Items.Clear();

            if (dataBase.DataBase().user.Any())
            {
                foreach (BD.user r in dataBase.DataBase().user.ToList())
                {
                    if (r.role1.name != "Super_User")
                    {
                        Button loginUser = new Button
                        {
                            Width = 140,
                            Height = 20,
                            Content = r.login,
                        };
                        loginUser.Style = (Style)loginUser.FindResource("ButtonK");
                        loginUser.Click += ButtonClickUserList;

                        UserList.Items.Add(loginUser);
                    }

                }
            }
        }


        private void ChangeUser_Click(object _sender, RoutedEventArgs _e)
        {
            string buttonContent = (_sender as Button).Content.ToString();
            if (ChangeUser.Opacity == 1)
            {
                BD.role role = dataBase.DataBase().role.FirstOrDefault(a => a.name == RoleEmployee.Text);
                BD.status_user status = dataBase.DataBase().status_user.FirstOrDefault(a => a.name == StatusEmployee.Text);

                if (buttonContent == "Изменить")
                {
                    BD.user user = dataBase.DataBase().user.FirstOrDefault(a => a.id == PrevUser.id);

                    if (user != null)
                    {
                        if (!dataBase.DataBase().user.Any(a => a.login == LoginEmployee.Text))
                        {
                            user.login = LoginEmployee.Text;
                        }
                        else if (PrevUser.login == LoginEmployee.Text)
                        {
                            user.login = LoginEmployee.Text;
                        }
                        else
                        {
                            MessageBox.Show("Логин занят");
                        }
                        user.password = PasswordEmployee.Text;
                        user.name = NameEmployee.Text;
                        user.role = role.ID;
                        user.status_user = status;
                    }

                }
                else if (buttonContent == "Нанять" && ChangeUser.Opacity == 1)
                {
                    if (dataBase.DataBase().user.Any(a => a.login == LoginEmployee.Text))
                    {
                        MessageBox.Show("Логин занят");
                    }
                    else
                    {
                        BD.user newUser = new BD.user
                        {
                            password = PasswordEmployee.Text,
                            name = NameEmployee.Text,
                            role = role.ID,
                            status_user = status,
                            login = LoginEmployee.Text
                        };
                        dataBase.DataBase().user.Add(newUser);
                    }
                }
                UpdateUserList();

                ChangeUser.Opacity = 0.5;

                dataBase.DataBase().SaveChanges();
                PanelEmployee.Visibility = Visibility.Hidden;
            }
        }

        private void DeleteUser_Click(object _sender, RoutedEventArgs _e)
        {
            string buttonContent = (_sender as Button).Content.ToString();

            BD.user user;
            BD.borrow_room borrowRoom;
            BD.booking_history bookingHistory;
            BD.blocking blocking;

            if ((_sender as Button).Content.ToString() == "Удалить")
            {
                user = dataBase.DataBase().user.FirstOrDefault(a => a.login == buttonContent);

                if (user != null)
                {
                    borrowRoom = dataBase.DataBase().borrow_room.FirstOrDefault(a => a.administrator == user.id);
                    blocking = dataBase.DataBase().blocking.FirstOrDefault(a => a.block_user == user.id);

                    if (blocking != null)
                    {
                        dataBase.DataBase().blocking.Remove(blocking);
                    }
                    if (borrowRoom != null)
                    {
                        bookingHistory = dataBase.DataBase().booking_history.FirstOrDefault(a => a.administrator == user.id);


                        if (bookingHistory != null)
                        {
                            dataBase.DataBase().booking_history.Remove(bookingHistory);
                        }
                        dataBase.DataBase().borrow_room.Remove(borrowRoom);
                    }
                    dataBase.DataBase().user.Remove(user);
                    dataBase.DataBase().SaveChanges();
                }
            }
            PanelEmployee.Visibility = Visibility.Hidden;
            UpdateUserList();
        }

        private void RoleUserBox_GotFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Введите название новой роли пользователя:", RoleUserBox);
        }
        private void RoleUserBox_LostFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Введите название новой роли пользователя:", RoleUserBox);
        }
        private void StatusNameUser_LostFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Введите название нового статуса пользователя:", StatusNameUser);
        }
        private void StatusNameUser_GotFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Введите название нового статуса пользователя:", StatusNameUser);
        }
        private void NameEmployee_GotFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Имя", NameEmployee);
        }
        private void NameEmployee_LostFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Имя", NameEmployee);
        }
        private void NameEmployee_KeyUp(object _sender, KeyEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if (box.Text != "")
            {

                if (PrevUser.name != NameEmployee.Text && NameEmployee.Text != "Имя")
                {
                    IsNameChange = true;
                }
                else
                {
                    IsNameChange = false;
                }

            }
        }

        private void LoginEmployee_LostFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Логин Работника", LoginEmployee);
        }
        private void LoginEmployee_KeyUp(object _sender, KeyEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if (box.Text != "")
            {

                if (PrevUser.login != LoginEmployee.Text && LoginEmployee.Text != "Логин Работника")
                {
                    IsLoginChange = true;

                }
                else
                {
                    IsLoginChange = false;
                }
            }
            else
            {
                IsLoginChange = false;
            }
        }

        private void LoginEmployee_GotFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Логин Работника", LoginEmployee);
        }

        private void PasswordEmployee_GotFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Пароль работника", PasswordEmployee);
        }
        private void PasswordEmployee_KeyUp(object _sender, KeyEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if (box.Text != "")
            {
                if (PrevUser.password != box.Text && PasswordEmployee.Text != "Пароль работника")
                {
                    IsPasswordChange = true;
                }
                else
                {
                    IsPasswordChange = false;
                }
            }
            else
            {
                IsPasswordChange = false;
            }
        }
        private void PasswordEmployee_LostFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Пароль работника", PasswordEmployee);
        }

        private void Grid_MouseEnter(object _sender, MouseEventArgs _e)
        {
            if (IsRoleChange || IsPasswordChange || IsNameChange || IsStatusChange || IsLoginChange)
            {
                ChangeUser.Opacity = 1;
            }
            else
            {
                ChangeUser.Opacity = 0.5;
            }
        }


        private void ComboRoleUser_LostFocus(object _sender, RoutedEventArgs _e)
        {
            if (RoleEmployee.SelectedIndex > -1)
            {
                if (PrevUser.role1.name != RoleEmployee.Text)
                {
                    IsPasswordChange = true;
                }
                else
                {
                    IsLoginChange = false;
                }
            }
            else
            {
                IsLoginChange = false;
            }
        }
        private void ComboStatusUser_LostFocus(object _sender, RoutedEventArgs _e)
        {
            if (StatusEmployee.SelectedIndex > -1)
            {
                if (PrevUser.status_user.name != StatusEmployee.Text)
                {
                    IsStatusChange = true;
                }
                else
                {
                    IsStatusChange = false;
                }
            }
            else
            {
                IsStatusChange = false;
            }
        }

        private void UpdateComboBoxRoom()
        {
            TypeRoom.Items.Clear();
            StatusRoom.Items.Clear();

            foreach (BD.type_room r in dataBase.DataBase().type_room.ToList())
            {
                TypeRoom.Items.Add(r.name);
            }
            foreach (BD.status_room r in dataBase.DataBase().status_room.ToList())
            {
                StatusRoom.Items.Add(r.name);
            }
        }

        private void RoomListButton_Click(object _sender, RoutedEventArgs _e)
        {
            string numberRoom = (_sender as Button).Content.ToString();

            PrevRoom = dataBase.DataBase().info_room.FirstOrDefault(a => a.num_room.ToString() == numberRoom);

            UpdateComboBoxRoom();

            if (PrevRoom != null)
            {
                NumberRoom.Text = PrevRoom.num_room.ToString();
                NumberRoom.IsReadOnly = true;
                NumberRoom.Cursor = Cursors.Arrow;

                CapacityRoom.Text = PrevRoom.capacity.ToString();
                CountRoom.Text = PrevRoom.count_room.ToString();


                TypeRoom.Text = PrevRoom.type_room1.name;
                StatusRoom.Text = PrevRoom.status_room1.name;

                CauseRoom.Text = PrevRoom.chort_description;
                Price.Text = PrevRoom.price.ToString();

                DeleteUser.Content = "Удалить";
                ChangeUser.Content = "Изменить";

                ChangeRoom.Opacity = 0.5;
                DataRoom.Visibility = Visibility.Visible;
            }
        }

        private void UpdateRoomList()
        {
            RoomList.Items.Clear();

            if (dataBase.DataBase().info_room.Any())
            {
                foreach (BD.info_room r in dataBase.DataBase().info_room.ToList())
                {
                    Button roomNumber = new Button
                    {
                        Width = 50,
                        Height = 20,
                        Content = r.num_room,
                    };
                    roomNumber.Style = (Style)roomNumber.FindResource("ButtonK");
                    roomNumber.Click += RoomListButton_Click;

                    RoomList.Items.Add(roomNumber);

                }
            }
        }

        private void ButtonAddRoom_Click(object _sender, RoutedEventArgs _e)
        {
            NumberRoom.IsReadOnly = false;
            NumberRoom.Cursor = Cursors.IBeam;

            CauseRoom.Text = "Краткое описание";
            NumberRoom.Text = "";
            CapacityRoom.Text = "";
            CountRoom.Text = "";
            Price.Text = "";

            UpdateComboBoxRoom();

            TypeRoom.SelectedIndex = 0;
            StatusRoom.SelectedIndex = 0;

            DeleteRoom.Content = "Отменить";
            ChangeRoom.Content = "Сохранить";

            ChangeRoom.Opacity = 0.5;

            DataRoom.Visibility = Visibility.Visible;
        }

        private void TabItem_MouseUp(object _sender, MouseButtonEventArgs _e)
        {
            PanelRoomData.Visibility = Visibility.Visible;

            UpdateRoomList();
        }

        private void ChangeRoom_Click(object _sender, RoutedEventArgs _e)
        {
            if((_sender as Button).Opacity == 1)
            {
                string buttonContent = (_sender as Button).Content.ToString();

                BD.type_room typeRoom = dataBase.DataBase().type_room.FirstOrDefault(a => a.name == TypeRoom.Text);
                BD.status_room statusRoom = dataBase.DataBase().status_room.FirstOrDefault(a => a.name == StatusRoom.Text);

                if (buttonContent == "Изменить")
                {
                    BD.info_room room = dataBase.DataBase().info_room.FirstOrDefault(a => a.num_room == PrevRoom.num_room);

                    if (room != null)
                    {
                        room.capacity = int.Parse(CapacityRoom.Text);
                        room.count_room = int.Parse(CountRoom.Text);
                        room.type_room = typeRoom.ID;
                        room.status_room = statusRoom.ID;
                        room.chort_description = CauseRoom.Text;
                        room.price = decimal.Parse(Price.Text);
                    }
                }
                else if (buttonContent == "Сохранить")
                {
                    int number = int.Parse(NumberRoom.Text);
                    if (dataBase.DataBase().info_room.Any(a => a.num_room == number))
                    {
                        BD.info_room newRoom = new BD.info_room
                        {
                            num_room = int.Parse(NumberRoom.Text),
                            capacity = int.Parse(CapacityRoom.Text),
                            type_room = typeRoom.ID,
                            status_room = statusRoom.ID,
                            chort_description = CauseRoom.Text,
                            price = decimal.Parse(Price.Text),
                        };

                        dataBase.DataBase().info_room.Add(newRoom);
                    }
                    else
                    {
                        MessageBox.Show("Такой номер уже есть в базе данных");
                    }
                }
                UpdateRoomList();

                ChangeRoom.Opacity = 0.5;

                dataBase.DataBase().SaveChanges();
                DataRoom.Visibility = Visibility.Hidden;
            }
        }

        private void DeleteRoom_Click(object _sender, RoutedEventArgs _e)
        {
            string buttonContent = (_sender as Button).Content.ToString();

            BD.info_room room;
            BD.borrow_room borrowRoom;
            BD.booking_history bookingHistory;

            if (buttonContent.ToString() == "Удалить")
            {
                room = dataBase.DataBase().info_room.FirstOrDefault(a => a.num_room == int.Parse(NumberRoom.Text));

                if (room != null)
                {
                    borrowRoom = dataBase.DataBase().borrow_room.FirstOrDefault(a => a.administrator == room.num_room);

                    if (borrowRoom != null)
                    {
                        bookingHistory = dataBase.DataBase().booking_history.FirstOrDefault(a => a.borrow_room == borrowRoom.id);

                        if (bookingHistory != null)
                        {
                            dataBase.DataBase().booking_history.Remove(bookingHistory);
                        }
                        dataBase.DataBase().borrow_room.Remove(borrowRoom);
                    }
                    dataBase.DataBase().info_room.Remove(room);
                    dataBase.DataBase().SaveChanges();
                }
            }
            DataRoom.Visibility = Visibility.Hidden;
            UpdateUserList();
        }

        private void CapacityRoom_KeyUp(object _sender, KeyEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if (box.Text != "" && PrevRoom.capacity != int.Parse(box.Text))
            {
                IsCapacityRoom = true;
            }
            else
            {
                IsCapacityRoom = false;
            }
        }
        private void CountRoom_KeyUp(object _sender, KeyEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if (box.Text != "" && PrevRoom.count_room != int.Parse(box.Text))
            {
                IsCountRoom = true;
            }
            else
            {
                IsCountRoom = false;
            }
        }
        private void NumberRoom_KeyUp(object _sender, KeyEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if (box.Text != "" && PrevRoom.num_room != int.Parse(box.Text))
            {
                IsNumberRoom = true;
            }
            else
            {
                IsNumberRoom = false;
            }
        }
        private void CauseRoom_KeyUp(object _sender, KeyEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if (box.Text != "" && PrevRoom.chort_description != box.Text)
            {
                IsShortDescription = true;
            }
            else
            {
                IsShortDescription = false;
            }
        }
        private void Price_KeyUp(object _sender, KeyEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if (box.Text != "" && PrevRoom.price != decimal.Parse(box.Text))
            {
                IsPriceRoom = true;
            }
            else
            {
                IsPriceRoom = false;
            }

        }
        private void StatusRoom_LostFocus(object _sender, RoutedEventArgs _e)
        {
            if (StatusRoom.SelectedIndex > -1)
            {
                if (PrevRoom.status_room1.name != StatusRoom.Text)
                {
                    IsStatusRoom = true;
                }

            }
            else
            {
                IsStatusRoom = false;
            }
        }
        private void TypeRoom_LostFocus(object _sender, RoutedEventArgs _e)
        {
            if (StatusEmployee.SelectedIndex > -1 && PrevUser.status_user.name != StatusEmployee.Text)
            {
                IsTypeRoom = true;
            }
            else
            {
                IsTypeRoom = false;
            }
        }
        private void Grid_MouseEnter_1(object _sender, MouseEventArgs _e)
        {
            if (IsCapacityRoom || IsCountRoom || IsShortDescription || IsPriceRoom || IsNumberRoom || IsStatusChange || IsTypeRoom)
            {
                ChangeRoom.Opacity = 1;
            }
            ChangeRoom.Opacity = 0.5;
        }

        private void StatusRoomName_KeyUp(object _sender, KeyEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if (box.Text != "")
            {
                if (PrevStatusNameRoom != box.Text && box.Text != "Введите название нового статуса")
                {
                    ChangeStatusRoom.Opacity = 1;
                    return;
                }
            }
            ChangeStatusRoom.Opacity = 0.5;
        }

        private void StatusRoomName_GotFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Введите название нового статуса номера", StatusRoomName);
        }
        private void StatusRoomName_LostFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Введите название нового статуса номера", StatusRoomName);
        }
        private void Button_ClickAddStatusRoomList(object _sender, RoutedEventArgs _e)
        {
            StatusRoomName.Text = "Введите название нового статуса номера";

            PrevStatusNameRoom = StatusRoomName.Text;

            ChangeStatusRoom.Content = "Сохранить";
            DeleteStatusRoom.Content = "Отменить";

            StatusRoomData.Visibility = Visibility.Visible;
        }

        private void UpdateStatusRoomList()
        {
            StatusRoomList.Items.Clear();

            if (dataBase.DataBase().status_room.Any())
            {
                foreach (BD.status_room r in dataBase.DataBase().status_room.ToList())
                {
                    Button nameStatusRoom = new Button
                    {
                        Width = 160,
                        Height = 20,
                        Content = r.name,
                    };
                    nameStatusRoom.Style = (Style)nameStatusRoom.FindResource("ButtonK");
                    nameStatusRoom.Click += Button_ClickStatusRoomList;

                    StatusRoomList.Items.Add(nameStatusRoom);
                }
            }
        }

        private void Button_ClickStatusRoomList(object _sender, RoutedEventArgs _e)
        {
            StatusRoomName.Text = (_sender as Button).Content.ToString();

            PrevStatusNameRoom = StatusRoomName.Text;

            ChangeStatusRoom.Content = "Изменить";
            DeleteStatusRoom.Content = "Удалить";

            StatusRoomData.Visibility = Visibility.Visible;
        }

        private void TabItem_MouseUp_1(object _sender, MouseButtonEventArgs _e)
        {
            UpdateStatusRoomList();

            StatusRoomPanel.Visibility = Visibility.Visible;
        }

        private void ChangeStatusRoom_Click(object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;
            if(button.Opacity != 0.5)
            {
                BD.status_room statusRoom = dataBase.DataBase().status_room.FirstOrDefault(a => a.name == PrevStatusNameRoom);

                if (ChangeStatusRoom.Content.ToString() == "Изменить")
                {
                    if (dataBase.DataBase().status_room.Any(a => a.ID == statusRoom.ID))
                    {
                        statusRoom.name = StatusRoomName.Text;
                    }
                }
                else if (ChangeStatusRoom.Content.ToString() == "Сохранить")
                {
                    if (!dataBase.DataBase().status_room.Any(a => a.name == StatusRoomName.Text))
                    {
                        BD.status_room newStatusRoom = new BD.status_room
                        {
                            name = StatusRoomName.Text
                        };
                        dataBase.DataBase().status_room.Add(newStatusRoom);
                    }
                    else
                    {
                        MessageBox.Show("Такой статус номера уже есть в базе данных");
                    }
                }
                dataBase.DataBase().SaveChanges();
                StatusRoomData.Visibility = Visibility.Hidden;
                ChangeStatusRoom.Opacity = 0.5;
                UpdateStatusRoomList();
            }
        }

        private void DeleteStatusRoom_Click(object _sender, RoutedEventArgs _e)
        {
            string buttonContent = (_sender as Button).Content.ToString();

            BD.status_room statusRoom;
            BD.info_room room;
            BD.borrow_room borrowRoom;
            BD.booking_history bookingHistory;

            if ((_sender as Button).Content.ToString() == "Удалить")
            {
                statusRoom = dataBase.DataBase().status_room.FirstOrDefault(a => a.name == PrevStatusNameRoom);

                if (statusRoom != null)
                {
                    room = dataBase.DataBase().info_room.FirstOrDefault(a => a.status_room == statusRoom.ID);

                    if (room != null)
                    {
                        borrowRoom = dataBase.DataBase().borrow_room.FirstOrDefault(a => a.room == room.num_room);

                        if (borrowRoom != null)
                        {
                            bookingHistory = dataBase.DataBase().booking_history.FirstOrDefault(a => a.borrow_room == borrowRoom.id);

                            if (bookingHistory != null)
                            {
                                dataBase.DataBase().booking_history.Remove(bookingHistory);
                            }
                            dataBase.DataBase().borrow_room.Remove(borrowRoom);
                        }

                        dataBase.DataBase().info_room.Remove(room);
                    }
                }
                dataBase.DataBase().status_room.Remove(statusRoom);
                dataBase.DataBase().SaveChanges();
            }

            PanelEmployee.Visibility = Visibility.Hidden;
            UpdateUserList();
        }

        private void TypeRoomName_KeyUp(object _sender, KeyEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if (box.Text != "")
            {
                if (PrevTypeNameRoom != box.Text && "Введите название типа номера" != box.Text)
                {
                    ChangeTypeRoom.Opacity = 1;
                    return;
                }
            }
            ChangeTypeRoom.Opacity = 0.5;
        }

        private void TypeRoomName_GotFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Введите название типа номера", TypeRoomName);
        }
        private void TypeRoomName_LostFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Введите название типа номера", TypeRoomName);
        }
        private void Button_ClickAddTypeRoomList(object _sender, RoutedEventArgs _e)
        {
            TypeRoomName.Text = "Введите название типа номера";

            PrevTypeNameRoom = TypeRoomName.Text;

            ChangeTypeRoom.Content = "Сохранить";
            DeleteRoomType.Content = "Отменить";

            TypeData.Visibility = Visibility.Visible;
        }

        private void UpdateTypeRoomList()
        {
            TypeRoomList.Items.Clear();

            if (dataBase.DataBase().type_room.Any())
            {
                foreach (BD.type_room r in dataBase.DataBase().type_room.ToList())
                {
                    Button nameTypeRoom = new Button
                    {
                        Width = 160,
                        Height = 20,
                        Content = r.name,
                    };
                    nameTypeRoom.Style = (Style)nameTypeRoom.FindResource("ButtonK");
                    nameTypeRoom.Click += Button_ClickTypeRoomList;

                    TypeRoomList.Items.Add(nameTypeRoom);
                }
            }
        }

        private void Button_ClickTypeRoomList(object _sender, RoutedEventArgs _e)
        {
            TypeRoomName.Text = (_sender as Button).Content.ToString();

            PrevTypeNameRoom = TypeRoomName.Text;

            ChangeTypeRoom.Content = "Изменить";
            DeleteRoomType.Content = "Удалить";

            TypeData.Visibility = Visibility.Visible;
        }

        private void TabItem_MouseUp_2(object _sender, MouseButtonEventArgs _e)
        {
            UpdateTypeRoomList();

            StatusRoomPanel.Visibility = Visibility.Visible;
        }

        private void ChangeTypeRoom_Click(object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;
            if (button.Opacity != 0.5)
            {
                BD.type_room typeRoom = dataBase.DataBase().type_room.FirstOrDefault(a => a.name == PrevTypeNameRoom);

                if (button.Content.ToString() == "Изменить")
                {
                    if (dataBase.DataBase().type_room.Any(a => a.ID == typeRoom.ID))
                    {
                        typeRoom.name = TypeRoomName.Text;
                    }
                }
                else if (button.Content.ToString() == "Сохранить")
                {
                    if (!dataBase.DataBase().type_room.Any(a => a.name == TypeRoomName.Text))
                    {
                        BD.type_room newTypeRoom = new BD.type_room
                        {
                            name = TypeRoomName.Text
                        };
                        dataBase.DataBase().type_room.Add(newTypeRoom);
                    }
                    else
                    {
                        MessageBox.Show("Такой тип уже есть в базе данных");
                    }
                }
                dataBase.DataBase().SaveChanges();
                TypeData.Visibility = Visibility.Hidden;
                ChangeTypeRoom.Opacity = 0.5;
                UpdateTypeRoomList();
            }
        }

        private void DeleteTypeRoom_Click(object _sender, RoutedEventArgs _e)
        {
            string buttonContent = (_sender as Button).Content.ToString();

            BD.type_room typeRoom;
            BD.info_room room;
            BD.borrow_room borrowRoom;
            BD.booking_history bookingHistory;

            if ((_sender as Button).Content.ToString() == "Удалить")
            {
                typeRoom = dataBase.DataBase().type_room.FirstOrDefault(a => a.name == PrevStatusNameRoom);
                if (typeRoom != null)
                {
                    room = dataBase.DataBase().info_room.FirstOrDefault(a => a.type_room == typeRoom.ID);
                    if (room != null)
                    {
                        borrowRoom = dataBase.DataBase().borrow_room.FirstOrDefault(a => a.room == room.num_room);

                        if (borrowRoom != null)
                        {
                            bookingHistory = dataBase.DataBase().booking_history.FirstOrDefault(a => a.borrow_room == borrowRoom.id);

                            if (bookingHistory != null)
                            {
                                dataBase.DataBase().booking_history.Remove(bookingHistory);
                            }
                            dataBase.DataBase().borrow_room.Remove(borrowRoom);
                        }
                        dataBase.DataBase().info_room.Remove(room);
                    }
                    dataBase.DataBase().type_room.Remove(typeRoom);
                    dataBase.DataBase().SaveChanges();
                }
            }
            PanelTypeRoomData.Visibility = Visibility.Hidden;
            UpdateUserList();
        }

    }
}

