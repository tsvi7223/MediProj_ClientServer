using MediProject_Client.ServiceReference1;
using System;
using System.Collections.Generic;
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

            List<MedicationExpiration> filteredList = new List<MedicationExpiration>();
            if (list != null)
            {
                foreach (MedicationExpiration item in list)
                { 
                    if (item.ExpirationDate >= DateTime.Today)
                    {
                        filteredList.Add(item);
                    }
                }
            }
            medicationExpirationList.ItemsSource = filteredList;
        }
    }
}