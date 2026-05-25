using ProjectOfMedicin_Server.Model;
using ProjectOfMedicine_Client;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public User user { get; set; }
        public Register(User user)
        {
            InitializeComponent();
            this.user = user;
            this.DataContext = this.user;

        }


        private void BackToLogin_Click(object sender, MouseButtonEventArgs e)
        {
            if(NavigationService.CanGoBack)
            { NavigationService.GoBack(); }
        }
        private void BtnFinishRegister_Click(object sender, RoutedEventArgs e)
        {

            
            MessageBox.Show("Got User:\nUserName is: " + user.UserName + "\n" + "Address is: " + user.FullAddress +
                "\nPhone Number is: " + user.PhoneNumber + "\nGmail is: " + user.Gmail +
                "\nPassword is: " + RegPass.Password);
        }
    }
}
