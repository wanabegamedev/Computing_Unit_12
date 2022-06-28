using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSoftware
{
    class Card
    {
        public string cardTitle;
        public string cardStartDate;
        public string cardEndDate;

        public Card(string cardTitle, string cardStartDate, string cardEndDate)
        {
            this.cardTitle = cardTitle;
            this.cardStartDate = cardStartDate;
            this.cardEndDate = cardEndDate;
        }
        //public State currentState; 
    }
}
