using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace ProjectManagmentSoftware
{
    /// <summary>
    /// Interaction logic for Timeline.xaml
    /// </summary>
    public partial class Timeline : Window
    {



        List<int> columnPositions = new List<int>();

        int numOfDays;

        bool isDragging;

        bool isOverMargin;

        Label currentCardBeingDragged;

        int distance;

        bool isStartDrag;


       

        int nextRowIndex = 0;
        public Timeline()
        {
            InitializeComponent();

            TimelineGrid.PreviewMouseMove += TimeLineMove;
            TimelineGrid.PreviewMouseUp += CardMouseUp;

            LoadGrid(CalculateDays());




        }


        int CalculateDays()
        {
            numOfDays = 0;
            numOfDays = (Project.endDate - Project.startDate).Days;
            numOfDays++;
            return numOfDays;
        }

        public void LoadGrid(int days)
        {
            //PrimaryScreenWidth returns the monitor width
            distance = (int)SystemParameters.PrimaryScreenWidth / days;

            //creates first line at left corner of screen
            CreateLine(0, 0);


            for (int i = 0; i < days; i++)
            {

                //gets the curren date for the column
                DateTime currentDate = Project.startDate.AddDays(i);




                var tempColum = new ColumnDefinition();
                //sets column's tag to the date being loaded
                tempColum.Tag = currentDate;
                tempColum.Width = new GridLength(distance);
                columnPositions.Add((int)(tempColum.Width.Value * (i + 1)));

                
                



                TimelineGrid.ColumnDefinitions.Add(tempColum);
                CreateLine(i, distance);
                CreateHeader(i, currentDate);

            }


            //cycles through all the cards and makes visual representations of them
            foreach (Card card in Project.cards)
            {
                CreateNewBar(card);
            }







        }


        void CreateNewBar(Card card)
        {
            //holds the column numbers for the start and end colums
            int startColumn = 0;
            int endColumn = 0;

            var tempLabel = new Label();

            tempLabel.Tag = card;
            tempLabel.Content = card.GetTitle();

            tempLabel.Background = Brushes.Teal;


            tempLabel.Height = 20;


            tempLabel.MouseMove += GetMousePos;
            tempLabel.MouseDown += CardMouseDown;
            


            //cycles through the columns and compares the start and end dates to the columns tag
            foreach (ColumnDefinition column in TimelineGrid.ColumnDefinitions)
            {


                if ((DateTime)column.Tag == card.GetStartDate())
                {
                    startColumn = TimelineGrid.ColumnDefinitions.IndexOf(column);


                }

                if ((DateTime)column.Tag == card.GetEndDate())
                {
                    endColumn = TimelineGrid.ColumnDefinitions.IndexOf(column);
                    
                }



                var days = (card.GetEndDate() - card.GetStartDate()).Days + 1;
                tempLabel.Width = column.Width.Value * days;

            }



            TimelineGrid.Children.Add(tempLabel);

            Grid.SetColumn(tempLabel, startColumn);

            //sets span of the label, this represents the days
            Grid.SetColumnSpan(tempLabel, endColumn + 1);
            Grid.SetRow(tempLabel, nextRowIndex);


            TimelineGrid.RowDefinitions.Add(new RowDefinition());

            //adds one to the nextRowIndex
            nextRowIndex++;


            RedrawGridLines();




        }


        void CreateLine(int lineCount, float lineDistance)
        {
            var tempLine = new Line();
            tempLine.StrokeThickness = 5;
            tempLine.Stroke = Brushes.Black;

            //sets x and y position of line
            tempLine.X1 = lineDistance;
            tempLine.X2 = lineDistance;
            tempLine.Y1 = 0;
            tempLine.Y2 = SystemParameters.PrimaryScreenHeight;


            Grid.SetColumn(tempLine, lineCount);


            TimelineGrid.Children.Add(tempLine);







        }

        void CreateHeader(int headerCount, DateTime date)
        {
            var tempLabel = new Label();
            tempLabel.Height = 20;
            tempLabel.Width = 25;
            tempLabel.Content = date.ToString();
            //sets tag to the date its column is
            tempLabel.FontSize = 10;
            tempLabel.Foreground = Brushes.Black;



            TimelineGrid.Children.Add(tempLabel);
            Grid.SetColumn(tempLabel, headerCount);

            //sets alignment
            tempLabel.VerticalAlignment = VerticalAlignment.Top;
            tempLabel.HorizontalAlignment = HorizontalAlignment.Center;

        }






        //not that optimized and should probably delete all lines first
        void RedrawGridLines()
        {


            int count = 0;
            for (int i = 0; i < TimelineGrid.Children.Count; i++)
            {
                if (TimelineGrid.Children[count].GetType() == typeof(Line))
                {
                    TimelineGrid.Children.Remove(TimelineGrid.Children[count]);
                }
                else
                {
                    count++;
                }
            }


            int distance = (int)SystemParameters.PrimaryScreenWidth / CalculateDays();

            //cycles through each row and column re drawing lines when a visual bar is added
            foreach (RowDefinition rowDefinition in TimelineGrid.RowDefinitions)
            {

                foreach (ColumnDefinition columnDefinition in TimelineGrid.ColumnDefinitions)
                {
                    var tempLine = new Line();
                    tempLine.StrokeThickness = 5;
                    tempLine.Stroke = Brushes.Black;

                    //sets x and y position of line
                    tempLine.X1 = distance;
                    tempLine.X2 = distance;
                    tempLine.Y1 = 0;
                    tempLine.Y2 = SystemParameters.PrimaryScreenHeight;


                    Grid.SetColumn(tempLine, TimelineGrid.ColumnDefinitions.IndexOf(columnDefinition));
                    Grid.SetRow(tempLine, TimelineGrid.RowDefinitions.IndexOf(rowDefinition));


                    TimelineGrid.Children.Add(tempLine);
                }




            }

        }


        void GetMousePos(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(sender as Label);

            CalculateDistanceToEnd(p, sender as Label);

        }


        void CalculateDistanceToEnd(Point mousePos, Label card)
        {
            double rMargin = card.Width - 200;
            double lMargin = 0 + 200;
            //Margins for dragging card

            





            //checks if the mouse is over either margin
            if (mousePos.X > rMargin)
            {
                Mouse.OverrideCursor = Cursors.Hand;
                isOverMargin = true;

                isStartDrag = false;


            }
            else if (mousePos.X < lMargin)
            {
                Mouse.OverrideCursor = Cursors.Hand;
                isOverMargin = true;
                isStartDrag = true;

            }
            else
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                isOverMargin = false;


            }



        }



        


        


        

        void CardMouseDown(object sender, MouseEventArgs e)
        {
            if (isOverMargin)
            {
                isDragging = true;
                var tempLabel = sender as Label;
               currentCardBeingDragged = tempLabel;
            }


           
            
        }


        void CardMouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            currentCardBeingDragged = null;
            
            
        }






        void TimeLineMove(object sender, MouseEventArgs e)
        {

            var mousePos = e.GetPosition(sender as UIElement);
            if (isDragging)
            {
                foreach (int column in columnPositions)
                {
                    if (mousePos.X == column)
                    {
                        UpdateCard(columnPositions.IndexOf(column) + 1);
                        
                        
                    }
                }


            }
        }


        void UpdateCard(int columnIndex, double mousePos)
        {

            
            var tempcard = (Card)currentCardBeingDragged.Tag;
            if (isStartDrag)
            {

                if (mousePos < 0)
                {
                    MessageBox.Show(mousePos.ToString());
                    tempcard.SetStartDate(tempcard.GetEndDate().AddDays(columnIndex));
                }
                else
                {
                    MessageBox.Show(mousePos.ToString());
                    tempcard.SetStartDate(tempcard.GetEndDate().AddDays(-columnIndex));
                }
                //sets start date
                tempcard.SetStartDate(tempcard.GetEndDate().AddDays(-columnIndex));
                                                                   //mini if statment to see if mousePos is less then one to know if to add or subtract a day
            }
            else
            {
                //sets new end date
                tempcard.SetEndDate(tempcard.GetStartDate().AddDays(columnIndex));
            }


            Grid.SetColumnSpan(currentCardBeingDragged, columnIndex);


            //sets card width
            var days = (tempcard.GetEndDate() - tempcard.GetStartDate()).Days + 1;

            currentCardBeingDragged.Width = distance * days;

            Grid.SetColumnSpan(currentCardBeingDragged, days);




        }

    }

    



}



