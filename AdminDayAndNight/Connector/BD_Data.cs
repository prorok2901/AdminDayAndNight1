using System.Windows.Controls;

namespace AdminDayAndNight
{
    internal class BD_Data
    {
        private BD.DayAndNightEntities basa = new BD.DayAndNightEntities();

        private BD.status_user status = new BD.status_user();
        private BD.user user = new BD.user();
        private BD.booking_history booking = new BD.booking_history();
        private BD.borrow_room borrow = new BD.borrow_room();
        private BD.info_room room = new BD.info_room();
        private BD.role role = new BD.role();
        private BD.blocking blocking = new BD.blocking();

        public BD.DayAndNightEntities DataBase()
        {
            return basa;
        }

        public BD.status_user Status()
        {
            return status;
        }

        public BD.user UserData()
        {
            return user;
        }

        public BD.booking_history BookongData()
        {
            return booking;
        }

        public BD.borrow_room BorrowData()
        {
            return borrow;
        }

        public BD.info_room RoomData()
        {
            return room;
        }

        public BD.role RoleUser()
        {
            return role;
        }

        public BD.blocking BlockingData()
        {
            return blocking;
        }
    }
}
