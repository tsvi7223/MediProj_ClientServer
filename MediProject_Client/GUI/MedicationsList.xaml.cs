using MediProject_Client.ServiceReference1;
using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class MedicationsListPage : Page
    {
        Service1Client service1 = new Service1Client();

        public MedicationsListPage(ServiceReference1.MedicationsList list)
        {
            InitializeComponent();
            this.listView.ItemsSource = list;
        }

       
        private void ShowAlternatives_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            // קבלת האובייקט של השורה שעליה לחצו
            Medication medication = (Medication)button.DataContext;
            this.NavigationService.Navigate(new MedicationPage(medication.ID));


        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICollectionView view = System.Windows.Data.CollectionViewSource.GetDefaultView(listView.ItemsSource);

            if (view != null)
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    view.Filter = null; // מציג הכל כשהתיבה ריקה
                }
                else
                {
                    string filterText = txtSearch.Text.ToLower();
                    view.Filter = item =>
                    {
                        // החלף את YourModelClass בשם ה-Class של האובייקטים שלך ברשימה
                        Medication medication = item as Medication;
                        if (medication == null) return false;

                        return (!string.IsNullOrEmpty(medication.OriginalName) && medication.OriginalName.ToLower().Contains(filterText));
                    };
                }
            }
        }
    }
}
