using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSoftware
{
    public enum State { todo, inprogress, done }
    public class Card
    {
        
        public string cardTitle;
        public string cardDescription;
        public string cardStartDate;
        public string cardEndDate;
        public State currentState;
        public int cardIndex;
        //change from sudo code to add a card index of position
        public Card(string cardTitle, string cardStartDate, string cardEndDate, string cardDescription)
        {
            this.cardTitle = cardTitle;
            this.cardStartDate = cardStartDate;
            this.cardEndDate = cardEndDate;
            this.cardDescription = cardDescription;
           
        }

    }
}
