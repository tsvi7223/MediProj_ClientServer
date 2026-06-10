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
    /// Interaction logic for MedicationsList.xaml
    /// </summary>
    public partial class MedicationsList : Page
    {
        Service1Client service1 = new Service1Client();

        public MedicationsList()
        {
            InitializeComponent();
            this.listView.ItemsSource = service1.GetAllMedications();
        }

       
        private void ShowAlternatives_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            // קבלת האובייקט של השורה שעליה לחצו
            Medication medication = (Medication)button.DataContext;
            this.NavigationService.Navigate(new MedicationPage(medication.ID));


        }
    }
}
