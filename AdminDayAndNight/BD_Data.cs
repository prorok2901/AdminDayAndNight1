using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDayAndNight
{
    internal class BD_Data
    {
        static BD.DayAndnightEntities basa = new BD.DayAndnightEntities();

        static BD.status_user status = new BD.status_user();
        static BD.user user = new BD.user();
        static BD.booking_history booking = new BD.booking_history();
        static BD.borrow_room borrow = new BD.borrow_room();
        static BD.info_room room = new BD.info_room();
        static BD.role role = new BD.role();

        public static BD.DayAndnightEntities DataBase()
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
        public static Registration_S_User registration = new Registration_S_User();
        public static Authorization authorization = new Authorization();
        public static Home HomE = new Home();

    }
}
