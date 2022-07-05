using System;
namespace ProjectManagmentSoftware
{
    public enum State { todo, inprogress, done }
    public class Card
    {

        string cardTitle;
        string cardDescription;
        DateTime cardStartDate;
        DateTime cardEndDate;
        State currentState = State.todo;

        //need to update date strigs to dates
        int cardIndex;
        //change from sudo code to add a card index of position
        public Card(string cardTitle, DateTime cardStartDate, DateTime cardEndDate, string cardDescription)
        {
            this.cardTitle = cardTitle;
            this.cardStartDate = cardStartDate;
            this.cardEndDate = cardEndDate;
            this.cardDescription = cardDescription;

        }

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

        public void SetCardDescription(string newCardDescription)
        {
            cardDescription = newCardDescription;
        }

        public string GetCardDescription()
        {
            return cardDescription;
        }

    }
}
