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
            ServiceReference1.Substance substance = service.GetSubstanceDetails(mediId);

            if (substance != null)
            {
                List<ServiceReference1.Substance> dataList = new List<ServiceReference1.Substance>();
                dataList.Add(substance);
                MedicationGrid.ItemsSource = dataList;
            }
            else
            {
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