using MediProject_Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MediProject_Client.GUI
{
    public partial class PersonalArea : Page
    {
        Service1Client service = new Service1Client();
        public User user;
        private Medication selectedMedication;

        public PersonalArea(User user)
        {
            InitializeComponent();
            this.user = user;


            this.PersonalMediListFrame.Navigate(new MedicationsListPage(user));

            this.PersonalExpirationListFrame.Navigate(new MedicationExpirationPage(user));

            this.DataContext = user;
        }

        private void AddMedication_Click(object sender, RoutedEventArgs e)
        {
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
            MessageBox.Show("התרופה נשמרה בהצלחה במערכת!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
            this.NavigationService.Navigate(new PersonalArea(user));
        }

        private void AddPurchase_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddPurchasePage(user));
        }

        private void PurchaseHistory_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PurchaseHistoryPage(user));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.user = new User();
            this.NavigationService.Navigate(new Login(this.user));
            while (this.NavigationService.RemoveBackEntry() != null) ;
        }
    }
}
