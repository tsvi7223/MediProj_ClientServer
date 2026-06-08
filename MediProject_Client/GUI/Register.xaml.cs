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
            KupatHolimList kupas = service.GetAllKupas();
            this.RegHMO.ItemsSource = kupas;
            this.RegHMO.SelectedItem = kupas.FirstOrDefault();

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

        private void BackDoor_Click(object sender, RoutedEventArgs e)
        {
            this.FirstNameTB.Text = user.FirstName =  "דניאל";
            this.LastNameTB.Text = user.LastName =  "אסולין";
            this.DateOfBirth.Text =  DateTime.Now.ToString();
            user.DateOfBirth = DateTime.Now;
            this.RegUser.Text = user.UserName =  "assulindani";
            this.RegPhone.Text = user.PhoneNumber = "0555555555";
            this.RegMail.Text = user.Gmail =  "assulindani@gmail.com";
            this.RegPass.Password = user.Password =  "1234567890";
            this.RegAddress.Text = user.FullAddress  = "הפסגה 8";

        }
    }
}
