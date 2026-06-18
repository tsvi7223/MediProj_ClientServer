using MediProject_Client.ServiceReference1;
//using User = MediProject_Client.ServiceReference1.User;
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
        public User user;
        private Medication selectedMedication;

        public PersonalArea(User user)
        {
            InitializeComponent();
            this.user = user;
            //ServiceReference1.MedicationsList list = service.GetUserMedications(user);
            this.PersonalMediListFrame.Navigate(new MedicationsListPage(user));
            this.PersonalPurchaseListFrame.Navigate(new PurchaseListPage(user));
            this.DataContext =user;
        }

        private void AddMedication_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new MedicationsPopupWindow());
            MedicationsPopupWindow popup = new MedicationsPopupWindow();
            if (popup.ShowDialog() == true)
            {
                selectedMedication = popup.SelectedMedication;
            }
            if (selectedMedication == null)
            {
                MessageBox.Show("אנא בחר תרופה מהרשימה.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            service.AddMedication(user, selectedMedication);
            // אם הכל תקין - כאן אתה שולח את הנתונים ל-DB או לשרת שלך
            MessageBox.Show("התרופה נשמרה בהצלחה במערכת!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);

            // חזרה לדף הקודם (למשל לרשימת התרופות)
            this.NavigationService.Navigate(new PersonalArea(user));


        }

        private void AddPurchase_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddPurchasePage(user));
            


        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            
            this.user = new User();
            this.NavigationService.Navigate(new Login(this.user));
            while (this.NavigationService.RemoveBackEntry() != null) ;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
