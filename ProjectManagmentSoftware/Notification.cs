using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Media;



namespace ProjectManagmentSoftware
{

    public class Notification
    {
        
         string title;
         string message;        

        Notification(string title, string message)
        {
            this.title = title;
            this.message = message;
        }

        public async void notificationLoop()
        {

            

            foreach (Card card in Project.cards)
            {
                int daysTillEndDate = (DateTime.Today - card.GetEndDate()).Days;
                if (card.GetNotificationEnabled() == true & daysTillEndDate == 1)
                {
                   
                }
            }
            
            
           
            
        }
       
        void NewNotification()
        {
            new ToastContentBuilder()
            .AddText(title).AddText(message).Show();
            SystemSounds.Exclamation.Play();
            notificationLoop();

        }
    }

   
}
