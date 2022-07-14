using System;
using System.Collections.Generic;

namespace ProjectManagmentSoftware
{
    static public class Project
    {
        static public string projectName = "";
        static public DateTime startDate;
        static public DateTime endDate;
        static public List<Card> cards = new List<Card>();
        static public KanbanBoard currentBoard;

        //public Project(string projectName, DateTime startDate, DateTime endDate)
        //{
        //    this.projectName = projectName;
        //    this.startDate = startDate;
        //    this.endDate = endDate;

        //}


    }
}
