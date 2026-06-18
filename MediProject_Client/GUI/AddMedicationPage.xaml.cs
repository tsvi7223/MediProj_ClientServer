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
    public partial class AddMedicationPage : Page
    {
        Medication selectedMedication;
        Service1Client service = new Service1Client();
        public User user = new User();
        public AddMedicationPage(User user)
        {
            InitializeComponent();
            this.user = user;
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

            if (!int.TryParse(txtPillsCount.Text, out int pillsCount))
            {
                MessageBox.Show("אנא הזן מספר כדורים תקין.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime? prescriptionDate = dpPrescriptionDate.SelectedDate;
            DateTime? startDate = dpStartDate.SelectedDate;
            DateTime? expiryDate = dpExpiryDate.SelectedDate;

            if (prescriptionDate == null || startDate == null || expiryDate == null)
            {
                MessageBox.Show("אנא מלא את כל התאריכים המבוקשים.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            service.AddMedication(user, selectedMedication);
            // אם הכל תקין - כאן אתה שולח את הנתונים ל-DB או לשרת שלך
            MessageBox.Show("התרופה נשמרה בהצלחה במערכת!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);

            // חזרה לדף הקודם (למשל לרשימת התרופות)
            this.NavigationService.Navigate(new PersonalArea(user));

        }

        // 3. לחיצה על ביטול
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // חזרה לדף הקודם ללא שמירה
            this.NavigationService.Navigate(new PersonalArea(user));

        }
    }

}
