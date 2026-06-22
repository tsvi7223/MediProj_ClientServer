using System.Windows;
using System.Windows.Controls;
using MediProject_Client.ServiceReference1;

namespace MediProject_Client.GUI
{
    public partial class PurchaseHistoryPage : Page
    {
        private User user;
        Service1Client service = new Service1Client();

        public PurchaseHistoryPage(User user)
        {
            InitializeComponent();
             this.user = user;
            LoadData();
        }

        private void LoadData()
        {
            PurchaseList purchaseList = service.GetPurchasesByUser(user);
            purchaseListView.ItemsSource = purchaseList;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PersonalArea(user));
        }
    }
}