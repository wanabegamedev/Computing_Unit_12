using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ProjectManagmentSoftware
{
    /// <summary>
    /// Interaction logic for KanbanBoard.xaml
    /// </summary>
    public partial class KanbanBoard : Window
    {




        public KanbanBoard()
        {
            InitializeComponent();

        }



        public void LoadProjectDetails()
        {


            this.Title = Project.projectName;



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

            //if (Project.cards.Count > 0)
            //{


            //debug amount for testing
            for (int i = 0; i < 3; i++)
            {

                var tempRect = new Rectangle();
                tempRect.Width = 10;
                tempRect.Height = 5;
                tempRect.Fill = new SolidColorBrush(Colors.White);
                Grid.SetColumn(tempRect, i);
                Kanban_Grid.Children.Add(tempRect);
                //still need to add text





            }

            //}




        }

        public void CreateVisualCard()
        {
            Card tempCard = (Card)Project.cards[Project.cards.Count - 1];
            var tempButton = new Button();
            tempButton.Width = 40;
            tempButton.Height = 20;
            tempButton.VerticalAlignment = VerticalAlignment.Center;
            tempButton.HorizontalAlignment = HorizontalAlignment.Center;
            tempButton.Padding = new Thickness(10);
            tempButton.Content = tempCard.GetTitle();
            tempButton.Foreground = new SolidColorBrush(Colors.White);
            Kanban_Grid.Children.Add(tempButton);
            


        }

        //Loads create new card window
        private void LoadCreateCardWindow_Click(object sender, RoutedEventArgs e)
        {
            var cardWindow = new NewCardWindow();
            cardWindow.Show();
            cardWindow.setKanbanBoard(this);

        }
    }
}
