﻿using System;
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


    }
}
