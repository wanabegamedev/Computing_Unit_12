using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Media;
using System.Windows;
using System.Windows.Threading;




namespace ProjectManagmentSoftware
{

    public class Notification
    {
        
         string title;
         string message;
        Card currentCard;

        

       


        public async Task NotificationLoop()
        {

            await Task.Delay(5000);

            foreach (Card card in Project.cards)
            {

                //added check to make sure state is not set to done
                int daysTillEndDate = (DateTime.Today - card.GetEndDate()).Days;
                if (card.GetNotificationEnabled() == true && card.GetState() != State.done)
                {
                    currentCard = card;
                    NewNotification();
                   
                }
            }


            NotificationLoop();
           
            
        }
       
        void NewNotification()
        {

            title = currentCard.GetTitle() + " is about to expire on " + currentCard.GetEndDate().Date.ToString();
            new ToastContentBuilder()
                .AddButton(new ToastButton()
                .SetContent("delay")
                .AddArgument("action", "delay")
                .SetBackgroundActivation())
                .AddButton(new ToastButton()
                .SetContent("mark as done")
                .AddArgument("action", "complete")
                .SetBackgroundActivation())
            .AddText(title).AddText(message).Show();
            //SystemSounds.Exclamation.Play();

            ToastNotificationManagerCompat.OnActivated += ToastNotificationManagerCompat_OnActivated;
            
            

        }

        private void ToastNotificationManagerCompat_OnActivated(ToastNotificationActivatedEventArgsCompat e)
        {

            if (e.Argument == "action=delay")
            {
                DelayEndDate();
            }
            if (e.Argument == "action=complete")
            {
                currentCard.SetState(State.done);

                Project.currentBoard.ReloadProjectDetails();
                
              
            }
            
        }


        void DelayEndDate()
        {
          currentCard.SetEndDate(currentCard.GetEndDate().AddDays(3));
        }
    }

   
}
