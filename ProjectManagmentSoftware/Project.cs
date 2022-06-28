using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSoftware
{
    class Project
    {
        public string projectName;
        public DateTime startDate;
        public DateTime endDate;
        public List<Card> cards = null;
        public List<State> states;

        public Project(string projectName, DateTime startDate, DateTime endDate)
        {
            this.projectName = projectName;
            this.startDate = startDate;
            this.endDate = endDate;

        }
        

    }
}
