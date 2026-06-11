using MediProject_Client.ServiceReference1;
using MediProject_Server.Model;
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
        public User user;



        public PersonalArea(User user)
        {
            InitializeComponent();


            this.user = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // this.NavigationService.Navigate(new MedicationsList());
            this.NavigationService.Navigate(new AddMedicationPage());
        }
    }
}
