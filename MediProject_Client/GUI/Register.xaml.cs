using MediProject_Client;
using MediProject_Client.ServiceReference1;
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

namespace MediProject_Client.GUI
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Service1Client service { get; set; }  = new Service1Client();
        public User user { get; set; }
        public Register(User user)
        {
            InitializeComponent();
            this.user = user;
            this.DataContext = this.user;
            this.RegHMO.ItemsSource = service.GetAllKupas();

        }


        private void BackToLogin_Click(object sender, MouseButtonEventArgs e)
        {
            if(NavigationService.CanGoBack)
            { NavigationService.GoBack(); }
        }
        private void BtnFinishRegister_Click(object sender, RoutedEventArgs e)
        {
            user.Kupa = RegHMO.SelectedItem as KupatHolim;
            service.InsertUser(user);
            
            
            
        }


    }
}
