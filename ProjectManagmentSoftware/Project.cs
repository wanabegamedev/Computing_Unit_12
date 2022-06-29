using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManagmentSoftware
{
   public class Project
    {
        public string projectName;
        public DateTime startDate;
        public DateTime endDate;
        public List<Card> cards;

        public Project(string projectName, DateTime startDate, DateTime endDate)
        {
            this.projectName = projectName;
            this.startDate = startDate;
            this.endDate = endDate;

        }
        
       
    }
}
