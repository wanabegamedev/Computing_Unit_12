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

        

        public async Task NotificationLoop()
        {

            await Task.Delay(3000);

            foreach (Card card in Project.cards)
            {
                int daysTillEndDate = (DateTime.Today - card.GetEndDate()).Days;
                if (card.GetNotificationEnabled() == true)
                {
                    
                    NewNotification(card);
                }
            }


            NotificationLoop();
           
            
        }
       
        void NewNotification(Card card)
        {
            title = card.GetTitle() + " is about to expire on " + card.GetEndDate().Date.ToString();
            new ToastContentBuilder() 
                .AddButton(new ToastButton()
                .SetContent("delay")
                .AddArgument("action", "delay")
                .SetBackgroundActivation())
            .AddText(title).AddText(message).Show();
            SystemSounds.Exclamation.Play();      
            
            

        }
        
       
    }

   
}
