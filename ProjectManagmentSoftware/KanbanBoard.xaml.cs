using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
                Kanban_Grid.ColumnDefinitions.Add(new ColumnDefinition());
                var header = new TextBlock();
                header.Text = Enum.GetName(typeof(State), i);
                header.FontSize = 20;
                header.VerticalAlignment = VerticalAlignment.Top;
                header.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetColumn(header, i);
                Kanban_Grid.Children.Add(header);
                
                
               
            }

            //if (cardsOnBoard.Count != 0)
            //{
                for (int i = 0; i < 3; i++)
                {
                    
                    var tempRect = new Rectangle();
                    tempRect.Width = 10;
                    tempRect.Height = 5;
                    tempRect.Fill = new SolidColorBrush(Colors.Black);
                    Grid.SetColumn(tempRect, i);
                    Kanban_Grid.Children.Add(tempRect);
                    //still need to add text





                }

            //}




        }


    }
}
