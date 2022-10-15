using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading;
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


        //handles sending the application to tray
        bool exitButtonClose;
        protected override void OnClosing(CancelEventArgs e)
        {
            exitButtonClose = true;
            if (exitButtonClose)
            {

                // setting cancel to true will cancel the close request
                // so the application is not closed

                TrayHandler.CreateNotifyIcon();
                TrayHandler.GetWindow(this);
                e.Cancel = true;

                FileSave.ExitSave();

                Hide();


                //need to add exit button to timeline view
            }



        }


        public KanbanBoard()
        {
            InitializeComponent();
            Kanban_Grid.AllowDrop = true;
            var tempNotification = new Notification();
            tempNotification.NotificationLoop();
            Project.currentBoard = this;
            



        }



        public void LoadProjectDetails()
        {

            FileSave.SaveLoop();

            //added a reset to the toDoRowCount
            toDoRowCount = 0;
            //sets window name to project name
            this.Title = Project.projectName;

            //clears board for refreshing of states
            Kanban_Grid.Children.Clear();
            Kanban_Grid.RowDefinitions.Clear();
            Kanban_Grid.ColumnDefinitions.Clear();



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


                    //only used one for statment to stop duplication of cards
                    //added cards to Kanban_Grid children


                    var tempButton = CreateNewButton();
                    var inprogress = CreateNewButton();
                    var finished = CreateNewButton();


                    switch (Convert.ToInt32(Project.cards[i].GetState()))
                    {
                        //creates data cards and two empty cards depending on position

                        case 0:
                            tempButton.Tag = Project.cards[i];
                            Grid.SetColumn(tempButton, Convert.ToInt32(Project.cards[i].GetState()));
                            tempButton.Content = Project.cards[i].GetTitle();
                            Kanban_Grid.Children.Add(tempButton);
                            Grid.SetRow(tempButton, toDoRowCount);
                            inprogress.Content = "";
                            finished.Content = "";
                            Grid.SetColumn(inprogress, 1);
                            Grid.SetColumn(finished, 2);
                            Grid.SetRow(tempButton, toDoRowCount);
                            Grid.SetRow(inprogress, toDoRowCount);
                            Grid.SetRow(finished, toDoRowCount);
                            Kanban_Grid.Children.Add(inprogress);
                            Kanban_Grid.Children.Add(finished);





                            break;
                        case 1:

                            tempButton.Tag = Project.cards[i];
                            Grid.SetColumn(tempButton, Convert.ToInt32(Project.cards[i].GetState()));
                            Kanban_Grid.Children.Add(tempButton);
                            tempButton.Content = Project.cards[i].GetTitle();
                            Grid.SetRow(tempButton, toDoRowCount);
                            inprogress = CreateNewButton();
                            finished = CreateNewButton();
                            inprogress.Content = "";
                            finished.Content = "";
                            Grid.SetColumn(inprogress, 0);
                            Grid.SetColumn(finished, 2);
                            Grid.SetRow(inprogress, toDoRowCount);
                            Grid.SetRow(finished, toDoRowCount);
                            Kanban_Grid.Children.Add(inprogress);
                            Kanban_Grid.Children.Add(finished);

                            break;
                        case 2:
                            tempButton.Tag = Project.cards[i];
                            Grid.SetColumn(tempButton, Convert.ToInt32(Project.cards[i].GetState()));
                            Grid.SetRow(tempButton, toDoRowCount);
                            tempButton.Content = Project.cards[i].GetTitle();
                            Kanban_Grid.Children.Add(tempButton);
                            inprogress = CreateNewButton();
                            finished = CreateNewButton();
                            inprogress.Content = "";
                            finished.Content = "";
                            Grid.SetColumn(inprogress, 0);
                            Grid.SetColumn(finished, 1);
                            Grid.SetRow(inprogress, toDoRowCount);
                            Grid.SetRow(finished, toDoRowCount);
                            Kanban_Grid.Children.Add(inprogress);
                            Kanban_Grid.Children.Add(finished);



                            break;
                    }





                    //creates new row
                    RowDefinition newRow = new RowDefinition();
                    //adds distance between the rows
                    newRow.Height = new GridLength(70);

                    Kanban_Grid.RowDefinitions.Add(newRow);
                    toDoRowCount += 1;

                    RedrawGridLines();

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

                    //move to all buttons
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
            RedrawGridLines();


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

            //checks which column the card is in and sets the coresponding state
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


        //reloads project details on current thead
        public void ReloadProjectDetails()
        {
            Application.Current.Dispatcher.Invoke(LoadProjectDetails);

        }


        //creates and returns a new button
        Button CreateNewButton()
        {
            var newButton = new Button();
            newButton.HorizontalAlignment = HorizontalAlignment.Center;

            newButton.Width = 60;
            newButton.Height = 40;
            newButton.PreviewMouseDown += DragIt;
            newButton.Drop += DropIt;
            newButton.AllowDrop = true;

            return newButton;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadProjectDetails();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var TimelineWindow = new Timeline();
            TimelineWindow.Show();
        }


        void RedrawGridLines()
        {


            int count = 0;
            for (int i = 0; i < Kanban_Grid.Children.Count; i++)
            {
                if (Kanban_Grid.Children[count].GetType() == typeof(Line))
                {
                    Kanban_Grid.Children.Remove(Kanban_Grid.Children[count]);
                }
                else
                {
                    count++;
                }
            }



            //cycles through each row and column re drawing lines when a visual bar is added
            foreach (RowDefinition rowDefinition in Kanban_Grid.RowDefinitions)
            {

                foreach (ColumnDefinition columnDefinition in Kanban_Grid.ColumnDefinitions)
                {
                    var tempLine = new Line();
                    tempLine.StrokeThickness = 5;
                    tempLine.Stroke = Brushes.Wheat;

                    //sets x and y position of line
                    tempLine.X1 = 270;
                    tempLine.X2 = 270;
                    tempLine.Y1 = 0;
                    tempLine.Y2 = SystemParameters.PrimaryScreenHeight;


                    Grid.SetColumn(tempLine, Kanban_Grid.ColumnDefinitions.IndexOf(columnDefinition));
                    Grid.SetRow(tempLine, Kanban_Grid.RowDefinitions.IndexOf(rowDefinition));


                    Kanban_Grid.Children.Add(tempLine);
                }












            }

            

        }

        
        
    }
}




