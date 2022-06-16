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
        private string statusName;
        private string prevStatusName;

        //работа с ролями пользователя
        private string prevRoleName;
        private string roleName;

        //работа с пользователем:
        private bool IsChangeUser;
        private BD.user PrevUser;

        //работа с номерами отеля
        private BD.info_room PrevRoom;
        private bool IsChangeRoom;

        public Home()
        {
            InitializeComponent();
            DataStatusUser.Visibility = Visibility.Hidden;
            DataRoleUser.Visibility = Visibility.Hidden;
        }

        private void Button_ClickAddStatusList(object _sender, RoutedEventArgs _e)
        {
            DeleteDataTextBox("Введите название нового статуса", StatusNameUser, true);

            DeleteStatusUser.Content = "Отменить";
            ChangeStatusUser.Content = "Сохранить";
        }
        private void Button_ClickRoleAddList(object _sender, RoutedEventArgs _e)
        {
            DeleteDataTextBox("Введите название нового статуса", RoleUserBox, false);

            DeleteRoleUser.Content = "Отменить";
            ChangeRoleUser.Content = "Сохранить";
        }

        private void Button_ClickStatus(object _sender, RoutedEventArgs _e)
        {
            string buttonContent = (_sender as Button).Content.ToString();
            DeleteDataTextBox(buttonContent, StatusNameUser, true);

            prevStatusName = buttonContent;

            DeleteStatusUser.Content = "Удалить";
            ChangeStatusUser.Content = "Изменить";
        }

        private void Button_ClickRole(object _sender, RoutedEventArgs _e)
        {
            string buttonContent = (_sender as Button).Content.ToString();
            DeleteDataTextBox(buttonContent, RoleUserBox, false);

            prevRoleName = buttonContent;

            DeleteStatusUser.Content = "Удалить";
            ChangeStatusUser.Content = "Изменить";
        }

        private void DeleteDataTextBox(string _text, TextBox _textBox, bool _currentLine)
        {
            _textBox.Text = _text;
            if (_currentLine)
            {
                statusName = _text;
            }
            else
            {
                roleName = _text;
            }
            DataStatusUser.Visibility = Visibility.Visible;
        }

        private void DeleteStatusUser_Click(object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;

            BD.status_user status = dataBase.DataBase().status_user.FirstOrDefault(a => a.name == statusName);
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

            BD.status_user status = dataBase.DataBase().status_user.FirstOrDefault(a => a.name == statusName);

            if (button.Content.ToString() == "Изменить" && button.Opacity != 0.5)
            {
                if (status != null)
                {
                    status.name = StatusNameUser.Text;
                }
            }
            else if (button.Content.ToString() == "Сохранить" && button.Opacity != 0.5)
            {
                if (status == null)
                {
                    BD.status_user newStatus = new BD.status_user
                    {
                        name = StatusNameUser.Text
                    };
                    dataBase.DataBase().status_user.Add(newStatus);
                }
            }
            dataBase.DataBase().SaveChanges();
            DataStatusUser.Visibility = Visibility.Hidden;
            ChangeStatusUser.Opacity = 0.5;
            UpdataListStatus();
        }

        private void StatusNameUser_KeyUp(object _sender, KeyEventArgs _e)
        {
            if (prevStatusName == StatusNameUser.Text || "Введите название нового статуса пользователя:" == StatusNameUser.Text)
            {
                ChangeStatusUser.Opacity = 1;
            }
            else
            {
                ChangeStatusUser.Opacity = 0.5;
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
            if (prevRoleName == RoleUserBox.Text || "Введите название новой роли пользователя:" == RoleUserBox.Text)
            {
                ChangeRoleUser.Opacity = 1;
            }
            else
            {
                ChangeRoleUser.Opacity = 0.5;
            }
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

            BD.role role = dataBase.DataBase().role.FirstOrDefault(a => a.name == roleName);

            if (button.Content.ToString() == "Изменить" && button.Opacity != 0.5)
            {
                if (dataBase.DataBase().role.Any(a => a.name == roleName))
                {
                    role.name = RoleUserBox.Text;
                }
            }
            else if (button.Content.ToString() == "Сохранить" && button.Opacity != 0.5)
            {
                if (!dataBase.DataBase().role.Any(a => a.name == roleName))
                {
                    BD.role newRole = new BD.role
                    {
                        name = RoleUserBox.Text
                    };
                    dataBase.DataBase().role.Add(newRole);
                }
            }
            dataBase.DataBase().SaveChanges();
            DataRoleUser.Visibility = Visibility.Hidden;
            ChangeRoleUser.Opacity = 0.5;
            UpdateRoleList();
        }

        private void DeleteRoleUser_Click(object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;

            BD.role role = dataBase.DataBase().role.FirstOrDefault(a => a.name == roleName);
            BD.user user = dataBase.DataBase().user.FirstOrDefault(a => a.role == role.ID);
            BD.borrow_room borrowRoom;
            BD.booking_history bookingHistory;
            BD.blocking blocking;

            if ((_sender as Button).Content.ToString() == "Удалить")
            {
                if (dataBase.DataBase().role.Any(a => a.name == roleName))
                {
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

            RoleEmployee.SelectedIndex = 0;
            StatusEmployee.SelectedIndex = 0;

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

            BD.role role = dataBase.DataBase().role.FirstOrDefault(a => a.name == RoleEmployee.Text);
            BD.status_user status = dataBase.DataBase().status_user.FirstOrDefault(a => a.name == StatusEmployee.Text);

            if (buttonContent == "Изменить" && ChangeUser.Opacity == 1)
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

            if ((PrevUser.login != LoginEmployee.Text || LoginEmployee.Text != "Логин Работника") || IsChangeUser)
            {
                ChangeUser.Opacity = 1;
            }
            else
            {
                ChangeUser.Opacity = 0.5;
            }
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
                if (IsChangeUser)
                {
                    if (PrevUser.name != NameEmployee.Text && NameEmployee.Text != "Имя")
                    {
                        ChangeUser.Opacity = 1;
                    }
                }
                if (IsChangeUser == false) IsChangeUser = true;
                return;
            }
            ChangeUser.Opacity = 0.5;
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
                if (IsChangeUser)
                {
                    if (PrevUser.login != LoginEmployee.Text && LoginEmployee.Text != "Логин Работника")
                    {
                        ChangeUser.Opacity = 1;
                        return;
                    }
                }
                if (IsChangeUser == false) IsChangeUser = true;
            }
            ChangeUser.Opacity = 0.5;
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
                if (IsChangeUser)
                {
                    if (PrevUser.password != box.Text && PasswordEmployee.Text != "Пароль работника")
                    {
                        ChangeUser.Opacity = 1;
                    }
                }
                if (IsChangeUser == false) IsChangeUser = true;
            }
            ChangeUser.Opacity = 0.5;
        }
        private void PasswordEmployee_LostFocus(object _sender, RoutedEventArgs _e)
        {
            FillEmptyBox.FillEmptyTextBox("Пароль работника", PasswordEmployee);
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
            string buttonContent = (_sender as Button).Content.ToString();

            BD.type_room typeRoom = dataBase.DataBase().type_room.FirstOrDefault(a => a.name == TypeRoom.Text);
            BD.status_room statusRoom = dataBase.DataBase().status_room.FirstOrDefault(a => a.name == StatusRoom.Text);

            if (buttonContent == "Изменить" && ChangeRoom.Opacity == 1)
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
            else if (buttonContent == "Сохранить" && ChangeRoom.Opacity == 1)
            {

                if (dataBase.DataBase().info_room.Any(a => a.num_room == int.Parse(NumberRoom.Text)))
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

        private void CapacityRoom_LostFocus(object _sender, RoutedEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if(box.Text != "")
            {
                if (IsChangeRoom)
                {
                    if (PrevRoom.capacity != int.Parse(box.Text))
                    {
                        ChangeRoom.Opacity = 1;
                    }
                }
                if (IsChangeRoom == false) IsChangeRoom = true;
                return;
            }
            ChangeRoom.Opacity = 0.5;
        }
        private void CountRoom_LostFocus(object _sender, RoutedEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if (box.Text != "")
            {
                if (IsChangeRoom)
                {
                    if (PrevRoom.count_room != int.Parse(box.Text))
                    {
                        ChangeRoom.Opacity = 1;
                    }
                }
                if (IsChangeRoom == false) IsChangeRoom = true;
                return;
            }
            ChangeRoom.Opacity = 0.5;
        }
        private void NumberRoom_LostFocus(object _sender, RoutedEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if (box.Text != "")
            {
                if (IsChangeRoom)
                {
                    if (PrevRoom.num_room != int.Parse(box.Text))
                    {
                        ChangeRoom.Opacity = 1;
                    }
                }
                if (IsChangeRoom == false) IsChangeRoom = true;
                return;
            }
            ChangeRoom.Opacity = 0.5;
        }
        private void CauseRoom_LostFocus(object _sender, RoutedEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if (box.Text != "")
            {
                if (IsChangeRoom)
                {
                    if (PrevRoom.chort_description != box.Text)
                    {
                        ChangeRoom.Opacity = 1;
                    }
                }
                if (IsChangeRoom == false) IsChangeRoom = true;
                return;
            }
            ChangeRoom.Opacity = 0.5;
        }
        private void Price_LostFocus(object _sender, RoutedEventArgs _e)
        {
            TextBox box = _sender as TextBox;

            if (box.Text != "")
            {
                if (IsChangeRoom)
                {
                    if (PrevRoom.price != decimal.Parse(box.Text))
                    {
                        ChangeRoom.Opacity = 1;
                    }
                }
                if (IsChangeRoom == false) IsChangeRoom = true;
                return;
            }
            ChangeRoom.Opacity = 0.5;
        }


    }

}
