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
using System.Windows.Shapes;

namespace MediProject_Client.GUI
{
    /// <summary>
    /// Interaction logic for MedicationsPopupWindow.xaml
    /// </summary>
    public partial class MedicationsPopupWindow : Window
    {
        Service1Client service1 = new Service1Client();
        public Medication SelectedMedication { get; private set; }

        public MedicationsPopupWindow()
        {
            InitializeComponent();

            this.listView.ItemsSource = service1.GetAllMedications();

        }

       
        private void SelectMedicationRow_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

          
            Medication selectedMedication = button?.DataContext as Medication;

            if (selectedMedication != null)
            {
               
                SelectedMedication = selectedMedication;

               
                this.DialogResult = true;
                this.Close();
            }
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(listView.ItemsSource);

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
                        Medication medication = item as Medication;
                        if (medication == null) return false;

                        return (!string.IsNullOrEmpty(medication.OriginalName) &&
                                medication.OriginalName.ToLower().Contains(filterText));
                    };
                }
            }
        }
    }
}
