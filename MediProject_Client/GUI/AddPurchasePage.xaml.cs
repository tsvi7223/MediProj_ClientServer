using MediProject_Client.ServiceReference1;
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
//using User = MediProject_Client.ServiceReference1.User;
//using Medication = MediProject_Client.ServiceReference1.Medication;

namespace MediProject_Client.GUI
{
    /// <summary>
    /// Interaction logic for AddMedicationPage.xaml
    /// </summary>
    public partial class AddPurchasePage : Page
    {
        Medication selectedMedication;
        public Purchase purchase = new Purchase();
        Service1Client service = new Service1Client();
        public User user = new User();
        public AddPurchasePage(User user)
        {
            InitializeComponent();
            this.user = user;
            this.DataContext = purchase;
        }
        private void SelectMedication_Click(object sender, RoutedEventArgs e)
        {


            MedicationsPopupWindow popup = new MedicationsPopupWindow();
            if (popup.ShowDialog() == true)
            {
                selectedMedication = popup.SelectedMedication;
                txtMedicationName.Text = selectedMedication.OriginalName;
            }

        }

        // 2. לחיצה על כפתור שמירה
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // בדיקת תקינות בסיסית (ולנציות)
            if ( selectedMedication == null)
            {
                MessageBox.Show("אנא בחר תרופה מהרשימה.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtPillsAmount.Text, out int pillsAmount))
            {
                MessageBox.Show("אנא הזן מספר כדורים תקין.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(txtPillsPerDay.Text, out int pillsPerDay))
            {
                MessageBox.Show("אנא הזן מספר כדורים ליום תקין.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime? purchaseDate = dpPurchaseDate.SelectedDate;
       

            if (purchaseDate == null )
            {
                MessageBox.Show("אנא מלא את תאריך.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            purchase.medication = selectedMedication;
            purchase.PillsPerDay = int.Parse(txtPillsPerDay.Text);
            purchase.PillAmount =int.Parse( txtPillsAmount.Text);
            purchase.user = this.user;
            purchase.PurchaseDate = DateTime.Parse(this.dpPurchaseDate.ToString());

           service.AddPurchase(purchase);
            MessageBox.Show("הקניה נשמרה בהצלחה במערכת!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
            this.NavigationService.Navigate(new PersonalArea(user));

        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {        

            this.NavigationService.Navigate(new PersonalArea(user));

        }
    }

}
