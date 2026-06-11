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
using System.Windows.Shapes;

namespace MediProject_Client.GUI
{
    /// <summary>
    /// Interaction logic for MedicationsPopupWindow.xaml
    /// </summary>
    public partial class MedicationsPopupWindow : Window
    {
        Service1Client service1 = new Service1Client();
        // תכונה ציבורית שתאפשר לדף הקורא לקבל את שם התרופה שנבחרה
        public string SelectedMedicationName { get; private set; }

        public MedicationsPopupWindow()
        {
            InitializeComponent();

            this.listView.ItemsSource = service1.GetAllMedications();

        }

        // אירוע לחיצה על כפתור "בחר תרופה" בשורה
        private void SelectMedicationRow_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            // שליפת האובייקט שמוצג בשורה הנוכחית
            // החלף את YourMedicationClass בסוג ה-Class האמיתי של התרופה שלך
            var selectedItem = button?.DataContext as dynamic;

            if (selectedItem != null)
            {
                // שמירת השם של התרופה שנבחרה (לפי השדה הקיים אצלך באובייקט)
                SelectedMedicationName = selectedItem.OriginalName;

                // סימון שהבחירה הצליחה וסגירת החלון
                this.DialogResult = true;
                this.Close();
            }
        }

        // מנגנון הסינון הקיים שלך עבור תיבת החיפוש
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(listView.ItemsSource);
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
                        var medication = item as dynamic; // החלף ב-Class האמיתי במידת הצורך
                        if (medication == null) return false;
                        return (!string.IsNullOrEmpty(medication.OriginalName) && medication.OriginalName.ToLower().Contains(filterText));
                    };
                }
            }
        }
    }
}
