using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AdminDayAndNight
{
    internal class BD_Data
    {
        private static BD.DayAndNightEntities basa = new BD.DayAndNightEntities();

        private static BD.status_user status = new BD.status_user();
        private static BD.user user = new BD.user();
        private static BD.booking_history booking = new BD.booking_history();
        private static BD.borrow_room borrow = new BD.borrow_room();
        private static BD.info_room room = new BD.info_room();
        private static BD.role role = new BD.role();

        public static BD.DayAndNightEntities DataBase()
        {
            return basa;
        }
        public static BD.status_user Status()
        {
            return status;
        }
        public static BD.user UserData()
        {
            return user;
        }
        public static BD.booking_history BookongData()
        {
            return booking;
        }
        public static BD.borrow_room BorrowData()
        {
            return borrow;
        }
        public static BD.info_room RoomData()
        {
            return room;
        }
        public static BD.role RoleUser()
        {
            return role;
        }
    }
    class Pages
    {
        private static Registration_S_User registration = new Registration_S_User();
        private static Authorization authorization = new Authorization();
        private static Home HomE = new Home();

        public static Registration_S_User RegistrationUser()
        {
            return registration;
        }
        public static Registration_S_User RegistrationUser(Grid grid, System.Windows.Media.ImageBrush startImage)
        {
            return registration = new Registration_S_User(grid, startImage);
        }
        public static Authorization AutorizationUser()
        {
            return authorization;
        }
        public static Authorization AutorizationUser(string login, string password)
        {
            return authorization = new Authorization(login,password);
        }
        public static Home HomeUser()
        {
            return HomE;
        }

    }
}
