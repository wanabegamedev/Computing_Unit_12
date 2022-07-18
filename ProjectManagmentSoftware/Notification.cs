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
                int daysTillStartDate = ( card.GetStartDate() - DateTime.Today).Days;

                if (card.GetNotificationEnabled() == true && card.GetState() != State.done & daysTillEndDate == 1)
                {
                    currentCard = card;
                    EndDateNotification();
                   
                }
                if (card.GetNotificationEnabled() == true && card.GetState() != State.done & daysTillStartDate == 1)
                {
                    currentCard = card;
                    StartDateNotification();
                }




            }


            NotificationLoop();
           
            
        }
       
        void EndDateNotification()
        {

            title = currentCard.GetTitle() + " is about to expire on " + currentCard.GetEndDate().ToString();
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


        void StartDateNotification()
        {
            title = currentCard.GetTitle() + " is to start on " + currentCard.GetStartDate().Date.ToString();
            new ToastContentBuilder()
            .AddText(title).AddText(message).Show();
         
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
