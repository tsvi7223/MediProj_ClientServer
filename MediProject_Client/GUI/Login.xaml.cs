using MediProject_Client;
using MediProject_Client.ServiceReference1;
using MediProject_Server.Model;
using MyUser = MediProject_Client.ServiceReference1.User;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Service1Client service1 = new Service1Client();
        // Service1Client service = new Service1Client();
        public MyUser user { get; set; }
        public Login()
        {
            InitializeComponent();
        }
        public Login(MyUser user)
        {
            InitializeComponent();
            this.user = user;
            this.DataContext = this.user;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference1.UserList users = service1.GetAllUsers();
            MyUser DBUser = users.Find(u => u.UserName.ToString() == this.LoginUser.Text);

            if (DBUser == null)
            {
                MessageBox.Show("משתמש לא קיים אנא הרשם");
            }
            else if (DBUser.Password == this.LoginPass.Password)
            {
                this.user = DBUser;

                // פיצול הניווט לפי הרשאות המשתמש
                if (this.user.IsAdmin)
                {
                    
                    this.NavigationService.Navigate(new AdminAreaPage(this.user));
                }
                else
                {
                    // אם הוא משתמש רגיל, נשלח אותו לאזור האישי שלו
                    this.NavigationService.Navigate(new PersonalArea(user));
                }
            }
            else
            {
                MessageBox.Show("סיסמה אינה תקינה, אנא נסה שוב");
            }
        }

        private void ShowRegister_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Register(user));
        }


    }
}
