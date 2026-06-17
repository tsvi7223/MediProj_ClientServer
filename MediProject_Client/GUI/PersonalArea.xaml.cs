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
    /// Interaction logic for PersonalArea.xaml
    /// </summary>
    public partial class PersonalArea : Page
    {
         Service1Client service = new Service1Client();
        public MyUser user;



        public PersonalArea(MyUser user)
        {
            InitializeComponent();
            this.user = user;
            ServiceReference1.MedicationsList list = service.GetUserMedications(user);
            this.PersonalMediListFrame.Navigate(new MedicationsListPage(user));
            this.DataContext =user;
        }

        private void AddMedication_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddMedicationPage(user));
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            
            this.user = null;
            this.NavigationService.Navigate(new Login());
            while (this.NavigationService.RemoveBackEntry() != null) ;
        }
    }
}
