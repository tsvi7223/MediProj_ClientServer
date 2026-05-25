using ProjectOfMedicine_Client;
using ProjectOfMedicin_Server.Model;
using ProjectOfMedicine_Client.ServiceReference1;
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

namespace ProjectOfMedicine_Client.GUI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        Service1Client service = new Service1Client();
        public User user { get; set; } 
        public Login(User user)
        {
            InitializeComponent();
            this.user = user;
            this.DataContext = this.user;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {

            
            MessageBox.Show("מנסה להתחבר למערכת בית המרקחת...");
        }

        private void ShowRegister_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Register(user));
        }


    }
}
