using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MediProject_Client.GUI
{
    public partial class MedicationPage : Page
    {
        private ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();

        public MedicationPage(int mediId)
        {
            InitializeComponent();
            LoadData(mediId);
        }

        private void LoadData(int mediId)
        {
            // קריאה לפונקציה החדשה בשרת
            ServiceReference1.Substance substance = service.GetSubstanceDetails(mediId);

            if (substance != null)
            {
                // הזרקת הנתון לתוך ה-Grid
                List<ServiceReference1.Substance> dataList = new List<ServiceReference1.Substance>();
                dataList.Add(substance);
                MedicationGrid.ItemsSource = dataList;
            }
            else
            {
                // אם הגענו לפה והנתון הוא null, זה אומר שהשאילתה ב-SubstanceDB לא מצאה את ה-ID
                MessageBox.Show("לא נמצאו פרטים עבור תרופה זו (ID: " + mediId + "). בדוק שהחומר קיים ב-DB.");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }
    }
}