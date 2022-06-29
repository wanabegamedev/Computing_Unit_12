using System.Collections.Generic;
using System.Windows;

namespace ProjectManagmentSoftware
{
    /// <summary>
    /// Interaction logic for KanbanBoard.xaml
    /// </summary>
    public partial class KanbanBoard : Window
    {
        public List<Card> cardsOnBoard = new List<Card>();

        public Project currentProject;

        public KanbanBoard()
        {
            InitializeComponent();

        }



        public void LoadProjectDetails(Project project)
        {

            currentProject = project;
            cardsOnBoard = currentProject.cards;


            this.Title = project.projectName;



            for (int i = 0; i < 3; i++)
            {
                //create new states                
            }

            if (cardsOnBoard.Count != 0)
            {
                for (int i = 0; i < cardsOnBoard.Count; i++)
                {
                    //create new visual cards and place them in each state
                }

            }




        }


    }
}
