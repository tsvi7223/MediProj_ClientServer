using ProjectOfMedicin_Server.DB;
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

namespace ProjectOfMedicine_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Service1Client service = new Service1Client();

            PeopleDB personDB = PeopleDB.GetInstance();
            PeopleList peopleList = new PeopleList();
            peopleList = personDB.SelectAll();
            peopleList.ForEach((person) => { Console.WriteLine(person.FName.ToString()); });


        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("מנסה להתחבר למערכת בית המרקחת...");
        }

        private void ShowRegister_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LoginPanel.Visibility = Visibility.Collapsed;
            RegisterPanel.Visibility = Visibility.Visible;
        }

        private void ShowLogin_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RegisterPanel.Visibility = Visibility.Collapsed;
            LoginPanel.Visibility = Visibility.Visible;
        }

        private void BtnFinishRegister_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("נרשמת בהצלחה למערכת!");
        }

    }
}
