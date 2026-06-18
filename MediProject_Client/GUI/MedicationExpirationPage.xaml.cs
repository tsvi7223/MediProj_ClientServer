using MediProject_Client.ServiceReference1;
using System.Windows.Controls;

namespace MediProject_Client.GUI
{
    public partial class MedicationExpirationPage : Page
    {
        private Service1Client service = new Service1Client();
        private User user;

        public MedicationExpirationPage(User user)
        {
            InitializeComponent();
            this.user = user;
            LoadExpirationData();
        }

        private void LoadExpirationData()
        {
            
            MedicationExpiration[] list = service.GetMedicationExpirationList(user);
            medicationExpirationList.ItemsSource = list;
        }
    }
}