using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AdminDayAndNight.Connector
{
    internal class Pages
    {
        private static Registration_S_User registration = new Registration_S_User();
        private static Authorization authorization = new Authorization();
        private static Home home = new Home();

        public static Registration_S_User RegistrationUser()
        {
            return registration;
        }

        public static Registration_S_User RegistrationUser(Grid _grid, System.Windows.Media.ImageBrush _startImage, bool _isUsers)
        {
            return registration = new Registration_S_User(_grid, _startImage, _isUsers);
        }

        public static Authorization AutorizationUser()
        {
            return authorization;
        }

        public static Authorization AutorizationUser(string _login, string _password)
        {
            return authorization = new Authorization(_login, _password);
        }

        public static Home HomeUser()
        {
            return home;
        }
    }
}
