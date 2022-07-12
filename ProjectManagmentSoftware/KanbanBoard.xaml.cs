using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;


namespace ProjectManagmentSoftware
{
    /// <summary>
    /// Interaction logic for KanbanBoard.xaml
    /// </summary>
    public partial class KanbanBoard : Window
    {

        Button buttonBeingDragged;

        int toDoRowCount = 0;



        public KanbanBoard()
        {
            InitializeComponent();
            Kanban_Grid.AllowDrop = true;

        }



        public void LoadProjectDetails()
        {


            //sets window name to project name
            this.Title = Project.projectName;



            //creates column headers
            for (int i = 0; i < 3; i++)
            {
                Kanban_Grid.ColumnDefinitions.Add(new ColumnDefinition());              
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
            for (int i = 0; i < 3; i++)
            {
                var tempButton = new Button();
                tempButton.HorizontalAlignment = HorizontalAlignment.Center;
                tempButton.Width = 60;
                tempButton.Height = 40;
                tempButton.PreviewMouseDown += DragIt;
                tempButton.Drop += DropIt;
                tempButton.AllowDrop = true;

                if (i == 0)
                {
                    //makes the tag of the button hold all the data of the card
                    tempButton.Tag = tempCard;

                    //sets card's positon in current state
                    tempCard.SetIndex(toDoRowCount);

                    tempButton.Content = tempCard.GetTitle();

                    tempButton.MouseDoubleClick += CardDoubleClick;
                }
                else
                {
                    tempButton.Content = "";
                }

                Grid.SetColumn(tempButton, i);
                Grid.SetRow(tempButton, toDoRowCount);
                Kanban_Grid.Children.Add(tempButton);
            }

            RowDefinition newRow = new RowDefinition();

            newRow.Height = new GridLength(70);

            Kanban_Grid.RowDefinitions.Add(newRow);
            toDoRowCount += 1;


        }


        void CardDoubleClick(object sender, RoutedEventArgs e)
        {

            //makes object access button functionality
            Button b = sender as Button;

            if (b.Tag != null)
                new EditCardDetailsWindow((Card)b.Tag, b).Show();

        }

        void DragIt(object sender, MouseEventArgs e)
        {

            buttonBeingDragged = sender as Button;
            Mouse.SetCursor(Cursors.Hand);

            //check to see if card data is not null
            if (buttonBeingDragged.Content.ToString() != "")
            {
                DragDrop.DoDragDrop(buttonBeingDragged, buttonBeingDragged, DragDropEffects.Copy);
            }

        }



        void DropIt(object sender, DragEventArgs e)
        {
            Button selectedButton = sender as Button;

            //makes sure you cant drag and drop onto the button you are dragging
            if (selectedButton == buttonBeingDragged)
            {
                return;
            }

            selectedButton.Content = buttonBeingDragged.Content;
            selectedButton.Tag = (Card)buttonBeingDragged.Tag;
            selectedButton.MouseDoubleClick += CardDoubleClick;

            Card buttonCard = selectedButton.Tag as Card;

            switch (Grid.GetColumn(selectedButton))
            {
                default:
                    break;
                case 0:
                    buttonCard.SetState(State.todo);
                    break;
                case 1:
                    buttonCard.SetState(State.inprogress);
                    break;
                case 2:
                    buttonCard.SetState(State.done);
                    break;

            }

            buttonBeingDragged.Content = "";
            buttonBeingDragged.Tag = null;
            buttonBeingDragged.MouseDoubleClick -= CardDoubleClick;



        }


        //Loads create new card window
        private void LoadCreateCardWindow_Click(object sender, RoutedEventArgs e)
        {
            //creates NewCardWindow and passes in the kanban board
            var cardWindow = new NewCardWindow(this);
            cardWindow.Show();

        }



    }

}

