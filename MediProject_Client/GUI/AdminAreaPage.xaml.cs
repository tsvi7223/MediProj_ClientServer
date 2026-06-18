using MediProject_Client.ServiceReference1; // מוודא גישה ישירה לשרות
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

         
            if (user != null)
            {
               
                var clientUser = (MediProject_Client.ServiceReference1.User)user;

                // בניית האובייקט של השרת
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
                // הגדרה מפורשת מאיזה ריפרנס מגיע ה-UserList כדי לפתור את השגיאה השלישית
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
                // שימוש בשם המפורש של המחלקה מתוך ה-ServiceReference
                ServiceReference1.Medication medication = new ServiceReference1.Medication();

                medication.OriginalName = txtMedName.Text;
                medication.InHealthBox = chkInHealthBox.IsChecked == true;
                medication.AvailableInIsrael = chkAvailableInIsr.IsChecked == true;

                medication.ActiveSubstance = new ServiceReference1.Substance();
                medication.ActiveSubstance.ID = int.Parse(txtSubstanceId.Text);

             
                service.AddMedi(medication);

                MessageBox.Show("התרופה נוספה בהצלחה!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה בהוספה: " + ex.Message);
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

                // יצירת אובייקט זמני עם ה-ID שרוצים למחוק
                MediProject_Client.ServiceReference1.Medication medToDelete = new MediProject_Client.ServiceReference1.Medication()
                {
                    ID = medId
                };

                //  קריאה לפונקציית המחיקה בשרת
                // (תוודא אם קראת לה Delete או DeleteMedication ב-Service)
                service.DeleteMedication(medToDelete);

                MessageBox.Show("הפקודה נשלחה: התרופה נמחקה מהמאגר!");
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
