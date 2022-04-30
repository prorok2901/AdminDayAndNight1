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

    public partial class MainWindow : Window
    {
        BD.group363_06Entities appapa;
        Registration_S_User user;
        Authorization authorization;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void chekSuperUser()
        {
            BD.user super_admin =appapa.user.FirstOrDefault(a => a.role != null && a.role == "Super_User");
            if(super_admin == null)
            {
                user = new Registration_S_User();
                Opupet.Navigate(user);
            }
            else
            {
                authorization = new Authorization();
                Opupet.Navigate(authorization);
            }
        }
    }
}

