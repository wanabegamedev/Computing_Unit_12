using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input; 


namespace ProjectManagmentSoftware
{
    /// <summary>
    /// Interaction logic for KanbanBoard.xaml
    /// </summary>
    public partial class KanbanBoard : Window
    {

        int rowCount = 0;


        public KanbanBoard()
        {
            InitializeComponent();

        }



        public void LoadProjectDetails()
        {


            //sets window name to project name
            this.Title = Project.projectName;



            //creates column headers
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

            if (Project.cards.Count > 0)
            {
                

                //debug amount for testing
                for (int i = 0; i < Project.cards.Count; i++)
                {

                    var tempRect = new Rectangle();
                    tempRect.Width = 10;
                    tempRect.Height = 5;
                    tempRect.Fill = new SolidColorBrush(Colors.White);
                    Grid.SetColumn(tempRect, i);
                    Kanban_Grid.Children.Add(tempRect);
                    //need to repalce with button code


                    //need to make it so it can place card in the right state



                }

            }




        }
        //create visual representation of card
        public void CreateVisualCard()
        {
            Card tempCard = (Card)Project.cards[Project.cards.Count - 1];
            var tempButton = new Button();
            Grid.SetColumn(tempButton, 0);
            Grid.SetRow(tempButton, rowCount);
            Kanban_Grid.Children.Add(tempButton);
            tempButton.HorizontalAlignment = HorizontalAlignment.Center;
            tempButton.Width = 60;
            tempButton.Height = 40;
            tempButton.Content = tempCard.GetTitle();
            tempButton.MouseDoubleClick += CardDoubleClick;
            tempButton.MouseLeftButtonDown += OnMouseDown;

            //sets card's positon in current state
            tempCard.SetIndex(rowCount);

            //makes the tag of the button hold all the data of the card
            tempButton.Tag = tempCard;

            RowDefinition newRow = new RowDefinition();

            Kanban_Grid.RowDefinitions.Add(newRow);
            rowCount += 1;




        }


        void CardDoubleClick(object sender, RoutedEventArgs e)
        {

            //makes object access button functionality
            Button b = sender as Button;

            new EditCardDetailsWindow((Card)b.Tag, b).Show();
            
        }

        void OnMouseDown(object sender, MouseEventArgs e)
        {
            Button selectedButton = sender as Button;
            Point mousePos = e.GetPosition(this);

            MessageBox.Show("text");
            selectedButton.Width = mousePos.X;
            selectedButton.Height = mousePos.Y;


            Kanban_Grid.Children.Remove(selectedButton);
            

            selectedButton.SetValue(Canvas.TopProperty, mousePos.Y);
            selectedButton.SetValue(Canvas.LeftProperty, mousePos.X);

        }
        

        //Loads create new card window
        private void LoadCreateCardWindow_Click(object sender, RoutedEventArgs e)
        {
            var cardWindow = new NewCardWindow(this);
            cardWindow.Show();
            
            


            
           
         

        }
    }
}
