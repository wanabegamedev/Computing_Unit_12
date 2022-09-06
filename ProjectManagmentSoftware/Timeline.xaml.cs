using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ProjectManagmentSoftware
{
    /// <summary>
    /// Interaction logic for Timeline.xaml
    /// </summary>
    public partial class Timeline : Window
    {
        int nextRowIndex = 0;
        public Timeline()
        {
            InitializeComponent();


            LoadGrid(CalculateDays());
        }


        int CalculateDays()
        {
            int numOfDays = 0;
            numOfDays = (Project.endDate - Project.startDate).Days;
            numOfDays++;
            return numOfDays;
        }

        public void LoadGrid(int days)
        {
            //PrimaryScreenWidth returns the monitor width
            int distance = (int)SystemParameters.PrimaryScreenWidth / days;

         
            
           

            for (int i = 0; i < days; i++)
            {

                //gets the curren date for the column
                DateTime currentDate = Project.startDate.AddDays(i);




                var tempColum = new ColumnDefinition();
                //sets column's tag to the date being loaded
                tempColum.Tag = currentDate;
                tempColum.Width = new GridLength(distance);



                TimelineGrid.ColumnDefinitions.Add(tempColum);
                DrawGridLines();
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

            tempLabel.Width = SystemParameters.PrimaryScreenWidth;
            tempLabel.Height = 20;

           


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
            }

            TimelineGrid.Children.Add(tempLabel);
            
            Grid.SetColumn(tempLabel, startColumn);

            //sets span of the label, this represents the days
            Grid.SetColumnSpan(tempLabel, endColumn + 1);
            Grid.SetRow(tempLabel, nextRowIndex);


            TimelineGrid.RowDefinitions.Add(new RowDefinition());

            //adds one to the nextRowIndex
            nextRowIndex++;

            
            DrawGridLines();




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
        void DrawGridLines()
        {

            int distance = (int)SystemParameters.PrimaryScreenWidth / CalculateDays();

            //cycles through each row and column re drawing lines when a visual bar is added
            foreach (RowDefinition rowDefinition in TimelineGrid.RowDefinitions)
            {
                //creates the starting line for each row
                var tempStartLine = new Line();
                tempStartLine.StrokeThickness = 5;
                tempStartLine.Stroke = Brushes.Black;
                tempStartLine.X1 = 0;
                tempStartLine.X2 = 0;
                tempStartLine.Y1 = 0;
                //may not be good if timeline is used on secondary monitor with diffrent screen hight
                tempStartLine.Y2 = SystemParameters.PrimaryScreenHeight;

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




        
    }
}


