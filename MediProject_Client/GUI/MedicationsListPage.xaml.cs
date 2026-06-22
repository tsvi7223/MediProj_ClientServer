using MediProject_Client.ServiceReference1;
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
//using User = MediProject_Client.ServiceReference1.User;
//using Medication = MediProject_Client.ServiceReference1.Medication;

namespace MediProject_Client.GUI
{
    /// <summary>
    /// Interaction logic for MedicationsList.xaml
    /// </summary>
    public partial class MedicationsListPage : Page
    {

        private ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
        private ServiceReference1.User user;

        public MedicationsListPage(ServiceReference1.User loggedInUser)
        {
            InitializeComponent();
            this.user = loggedInUser;
            LoadMedicationsList();
        }

        private void LoadMedicationsList()
        {
            listView.ItemsSource = service.GetMedicationsByUser(user.ID);
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            ServiceReference1.Medication selectedMed = (ServiceReference1.Medication)btn.DataContext;

            MessageBoxResult answer = MessageBox.Show(
                "מחיקת התרופה תביא למחיקתה מעמודת התרופות שלי. האם להמשיך?",
                "אישור מחיקה",
                MessageBoxButton.YesNo);

            if (answer == MessageBoxResult.Yes)
            {
                service.RemoveFromUserList(user.ID, selectedMed.ID);
                LoadMedicationsList();
            }
        }


        private void ShowAlternatives_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Medication medication = (Medication)button.DataContext;


            Window mainWindow = Window.GetWindow(this);


            Frame mainFrame = mainWindow?.FindName("MainFrame") as Frame;

            if (mainFrame != null)
            {
                mainFrame.Navigate(new MedicationPage(medication.MainSubstanceId));
            }
            else
            {

                NavigationService nav = NavigationService.GetNavigationService(this);
                nav?.Navigate(new MedicationPage(medication.ID));
            }
        }


        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICollectionView view = System.Windows.Data.CollectionViewSource.GetDefaultView(listView.ItemsSource);

            if (view != null)
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    view.Filter = null; 
                }
                else
                {
                    string filterText = txtSearch.Text.ToLower();
                    view.Filter = item =>
                    {

                        Medication medication = item as Medication;
                        if (medication == null) return false;

                        return (!string.IsNullOrEmpty(medication.OriginalName) && medication.OriginalName.ToLower().Contains(filterText));
                    };
                }
            }
        }
    }
}
