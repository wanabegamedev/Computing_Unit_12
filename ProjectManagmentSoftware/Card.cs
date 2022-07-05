using System;
namespace ProjectManagmentSoftware
{
    //all possible states the card can be in
    public enum State { todo, inprogress, done }
    public class Card
    {

        string cardTitle { get; set; }
        string cardDescription { get; set; }
        DateTime cardStartDate { get; set; }
        DateTime cardEndDate { get; set; }
        State currentState { get; set; }
        int cardIndex { get; set; }


        //change from sudo code to add a card index of position in state
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

        public void SetCardIndex(int newCardIndex)
        {
            cardIndex = newCardIndex;
        }

        public int GetCardIndex()
        {
            return cardIndex; 
        }

    }
}
