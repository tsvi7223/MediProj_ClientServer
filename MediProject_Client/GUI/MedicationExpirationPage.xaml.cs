using MediProject_Client.ServiceReference1;
using System;
using System.Collections.Generic; // נדרש עבור ה-List
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
            // קבלת כל הרשימה מהשרת
            MedicationExpiration[] list = service.GetMedicationExpirationList(user);

            // יצירת רשימה חדשה לסינון
            List<MedicationExpiration> filteredList = new List<MedicationExpiration>();

            // מעבר על הרשימה כדי לסנן רק את מה שרלוונטי מהיום והלאה
            if (list != null)
            {
                foreach (MedicationExpiration item in list)
                {
                    // תנאי: אם התאריך הוא היום או בעתיד - נוסיף לרשימה
                    if (item.ExpirationDate >= DateTime.Today)
                    {
                        filteredList.Add(item);
                    }
                }
            }

            // עדכון ה-ItemsSource רק עם הרשימה המסוננת
            medicationExpirationList.ItemsSource = filteredList;
        }
    }
}