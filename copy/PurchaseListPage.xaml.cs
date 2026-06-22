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
//using Purchase = MediProject_Client.ServiceReference1.Purchase;

namespace MediProject_Client.GUI
{
    /// <summary>
    /// Interaction logic for PurchasesList.xaml
    /// </summary>
    public partial class PurchaseListPage : Page
    {
        
        private ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
        private ServiceReference1.User user;

        public PurchaseListPage(ServiceReference1.User loggedInUser)
        {
            InitializeComponent();
            this.user = loggedInUser;
            LoadPurchasesList();
        }

        private void LoadPurchasesList()
        {
            listView.ItemsSource = service.GetPurchasesByUser(user);
        }




        

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(listView.ItemsSource);

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
                        Purchase Purchase = item as Purchase;
                        if (Purchase == null) return false;

                        return (!string.IsNullOrEmpty(Purchase.medication.OriginalName) && Purchase.medication.OriginalName.ToLower().Contains(filterText));
                    };
                }
            }
        }
    }
}
