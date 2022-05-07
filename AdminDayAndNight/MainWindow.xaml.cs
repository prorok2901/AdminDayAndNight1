using System;
using System.Linq;
using System.Windows;

namespace AdminDayAndNight
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            chekSuperUser();
        }
        private void chekSuperUser()
        {
            BD.user super_admin = BD_Data.DataBase().user.FirstOrDefault(a => a.role == "Super_User");
            if (super_admin == null)
            {
                
                Opupet.Navigate(Pages.registration);
            }
            else
            {
                
                Opupet.Navigate(Pages.authorization);
            }
        }
    }
}
