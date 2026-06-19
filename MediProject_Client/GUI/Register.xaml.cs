using MediProject_Client;
using MediProject_Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions; // נוסף לצורך בדיקת מייל
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
using MediProject_Server.Utilities;
namespace MediProject_Client.GUI
{
    public partial class Register : Page
    {
        public Service1Client service { get; set; } = new Service1Client();
        public User user { get; set; }
        public Register(User user)
        {
            InitializeComponent();
            this.user = user;
            this.DataContext = this.user;
            ServiceReference1.KupatHolimList kupas = service.GetAllKupas();
            this.RegHMO.ItemsSource = kupas;
            this.RegHMO.SelectedItem = kupas.FirstOrDefault();

        }


        private void BackToLogin_Click(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Login(this.user));
        }
        private void BtnFinishRegister_Click(object sender, RoutedEventArgs e)
        {
            // בדיקת תקינות לפני שליחה
            if (string.IsNullOrWhiteSpace(FirstNameTB.Text) || string.IsNullOrWhiteSpace(LastNameTB.Text) ||
                string.IsNullOrWhiteSpace(RegUser.Text) || string.IsNullOrWhiteSpace(RegAddress.Text))
            {
                MessageBox.Show(".נא למלא את כל השדות");
                return;
            }

            if (!RegMail.Text.Contains("@") || !RegMail.Text.Contains("."))
            {
                MessageBox.Show(".כתובת מייל לא תקינה");
                return;
            }

            if (RegPhone.Text.Length < 10 || !RegPhone.Text.All(char.IsDigit))
            {
                MessageBox.Show("(מספר טלפון לא תקין (חייב להכיל רק ספרות באורך מינימלי של 10 ספרות");
                return;
            }

            if (RegPass.Password.Length < 6)
            {
                MessageBox.Show(".סיסמה חייבת להכיל לפחות 6 תווים");
                return;
            }

            user.Kupa = RegHMO.SelectedItem as KupatHolim;
            user.Password = Encription.ComputeSha256Hash(RegPass.Password);
            service.InsertUser(user);
            this.NavigationService.Navigate(new Login(this.user));
        }

        private void BackDoor_Click(object sender, RoutedEventArgs e)
        {
            this.FirstNameTB.Text = user.FirstName = "דניאל";
            this.LastNameTB.Text = user.LastName = "אסולין";
            this.DateOfBirth.Text = DateTime.Now.ToString();
            user.DateOfBirth = DateTime.Now;
            this.RegUser.Text = user.UserName = "assulindani";
            this.RegPhone.Text = user.PhoneNumber = "0555555555";
            this.RegMail.Text = user.Gmail = "assulindani@gmail.com";
            this.RegPass.Password = user.Password = "1234567890";
            this.RegAddress.Text = user.FullAddress = "הפסגה 8";

        }
    }
}
