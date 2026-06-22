using MediProject_Client;
using MediProject_Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            this.DateOfBirth.SelectedDate = DateTime.Now;
            this.user.DateOfBirth = DateTime.Now;

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
            if (string.IsNullOrWhiteSpace(FirstNameTB.Text) || string.IsNullOrWhiteSpace(LastNameTB.Text) ||
                string.IsNullOrWhiteSpace(RegUser.Text) || string.IsNullOrWhiteSpace(RegAddress.Text) ||
                string.IsNullOrWhiteSpace(RegPhone.Text) || string.IsNullOrWhiteSpace(RegMail.Text))
            {
                MessageBox.Show("נא למלא את כל השדות");
                return;
            }

            if (RegPhone.Text.Length != 10 || !RegPhone.Text.All(char.IsDigit))
            {
                MessageBox.Show("מספר טלפון חייב להכיל 10 ספרות בדיוק");
                return;
            }
            if (RegUser.Text.Length < 6)
            {
                MessageBox.Show("שם משתמש חייב להכיל לפחות 6 תווים");
                return;
            }
            string mail = RegMail.Text;
            if (!mail.Contains("@") || !mail.Contains("."))
            {
                MessageBox.Show("כתובת המייל אינה תקינה. וודא שקיימים '@' ונקודה ");
                return;
            }
            if (DateOfBirth.SelectedDate > DateTime.Now)
            {
                MessageBox.Show(".לא ניתן לבחור תאריך לידה עתידי");
                return;
            }
            user.FirstName = FirstNameTB.Text;
            user.LastName = LastNameTB.Text;
            user.UserName = RegUser.Text;
            user.FullAddress = RegAddress.Text;
            user.PhoneNumber = RegPhone.Text;
            user.Gmail = RegMail.Text;
            user.DateOfBirth = DateOfBirth.SelectedDate ?? DateTime.Now;
            user.Kupa = RegHMO.SelectedItem as KupatHolim;
            user.Password = Encription.ComputeSha256Hash(RegPass.Password);

            string message = ":אנא אשר את הפרטים שלך\n\n" +
                                 "שם מלא: " + user.FirstName + " " + user.LastName + "\n" +
                                 "שם משתמש: " + user.UserName + "\n" +
                                 "מייל: " + user.Gmail + "\n" +
                                 "טלפון: " + user.PhoneNumber + "\n" +
                                 "כתובת: " + user.FullAddress + "\n" +
                                 "קופת חולים: " + (user.Kupa != null ? user.Kupa.Name : "לא נבחרה") + "\n" +
                                 "תאריך לידה: " + user.DateOfBirth.ToShortDateString();

            MessageBoxResult result = MessageBox.Show(message, "אישור פרטי הרשמה", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                bool success = service.InsertUser(user);

                if (success == true)
                {
                    MessageBox.Show("!ההרשמה בוצעה בהצלחה");
                    this.NavigationService.Navigate(new Login(this.user));
                }
                else
                {
                    MessageBox.Show(".שגיאה: שם המשתמש כבר קיים במערכת, אנא בחר שם אחר");
                }
            }
        }
    }
}