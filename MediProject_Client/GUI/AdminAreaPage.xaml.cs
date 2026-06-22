using MediProject_Client.ServiceReference1; 
using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using User = MediProject_Client.ServiceReference1.User;

namespace MediProject_Client.GUI
{
    public partial class AdminAreaPage : Page
    {
        
        Service1Client service = new Service1Client();
        public User user;


        public AdminAreaPage(object user)
        {
            InitializeComponent();

            
            MediProject_Client.ServiceReference1.User clientUser = user as MediProject_Client.ServiceReference1.User;

            if (clientUser != null)
            {
                
                this.user = new User();
                this.user.UserName = clientUser.UserName;
                this.user.Password = clientUser.Password;
                this.user.IsAdmin = clientUser.IsAdmin;

                this.DataContext = this.user;
            }
            else
            {
                MessageBox.Show("שגיאה: המשתמש לא הועבר בהצלחה.");
            }
        }
        private void BtnLoadUsers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                MediProject_Client.ServiceReference1.UserList allUsers = service.GetAllUsers();

                dgUsers.ItemsSource = allUsers;
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה בטעינת המשתמשים: " + ex.Message);
            }
        }


        private void BtnAddMed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtSubstanceId.Text, out int substanceId))
                {
                    MessageBox.Show("שגיאה: קוד חומר פעיל חייב להיות מספר שלם.", "שגיאת קלט", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                ServiceReference1.Medication medication = new ServiceReference1.Medication();
                medication.OriginalName = txtMedName.Text;
                medication.InHealthBox = chkInHealthBox.IsChecked == true;
                medication.AvailableInIsrael = chkAvailableInIsr.IsChecked == true;
                medication.MainSubstanceId = substanceId;
                bool isSuccess = service.AddMedi(medication);

                if (isSuccess)
                {
                    MessageBox.Show("!התרופה נוספה בהצלחה");
                }
                else
                {
                    MessageBox.Show("שגיאה!: קוד החומר הפעיל לא נמצא במערכת", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("קרתה תקלה בעת הוספת התרופה: " + ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BtnDeleteMed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtDeleteMedName.Text, out int medId))
                {
                    MessageBox.Show("אנא הכנס קוד תרופה (מספר) תקין למחיקה");
                    return;
                }
                var allMeds = service.GetAllMedications(); 
                bool exists = false;
                foreach (var m in allMeds)
                {
                    if (m.ID == medId)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    MessageBox.Show("!שגיאה: קוד תרופה לא נמצא במערכת", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                ServiceReference1.Medication medToDelete = new ServiceReference1.Medication();
                medToDelete.ID = medId;
                service.DeleteMedication(medToDelete);

                MessageBox.Show("!התרופה נמחקה מהמאגר בהצלחה");
                txtDeleteMedName.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה במחיקת התרופה: " + ex.Message);
            }
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            
            this.user = new User();

           
            this.NavigationService.Navigate(new Login(this.user));

           
            while (this.NavigationService.RemoveBackEntry() != null) ;
        }
    }

    }
