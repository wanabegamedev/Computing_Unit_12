using System;
namespace ProjectManagmentSoftware
{
    //all possible states the card can be in
    public enum State { todo, inprogress, done }
    public class Card
    {

        string cardTitle;
        string cardDescription;
        DateTime cardStartDate;
        DateTime cardEndDate;
        State currentState;
        int cardIndex;
        bool notificationEnabled;


        //change from sudo code to add a card index of position in state
        public Card(string cardTitle, DateTime cardStartDate, DateTime cardEndDate, string cardDescription, bool notificationEnabled)
        {
            this.cardTitle = cardTitle;
            this.cardStartDate = cardStartDate;
            this.cardEndDate = cardEndDate;
            this.cardDescription = cardDescription;
            this.notificationEnabled = notificationEnabled;

            
        }


        //getter and setter functions for all the variables 
        public void SetState(State newState)
        {
            currentState = newState;
        }
        public State GetState()
        {
            return currentState;
        }

        public void SetTitle(string newTitle)
        {
           cardTitle = newTitle;
        }

        public string GetTitle()
        {
            return cardTitle;
        }

        public void SetDescription(string newCardDescription)
        {
            cardDescription = newCardDescription;
        }

        public string GetDescription()
        {
            return cardDescription;
        }

        public void SetIndex(int newCardIndex)
        {
            cardIndex = newCardIndex;
        }

        public int GetIndex()
        {
            return cardIndex; 
        }

        public void SetStartDate(DateTime newStartDate)
        {
            cardStartDate = newStartDate;
            
        }

        public DateTime GetStartDate()
        {
            return cardStartDate;
        }

        public void SetEndDate(DateTime newEndDate)
        {
            cardEndDate = newEndDate;

        }

        public DateTime GetEndDate()
        {
            return cardEndDate;
        }

        public bool GetNotificationEnabled()
        {
            return notificationEnabled;
        }

        public void SetNotificationEnabled(bool enabled)
        {
            notificationEnabled = enabled;
        }

    }
}
