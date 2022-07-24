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
    /// Interaction logic for Timeline.xaml
    /// </summary>
    public partial class Timeline : Window
    {
        public Timeline()
        {
            InitializeComponent();


            LoadGrid(CalculateDays());
        }


        int CalculateDays()
        {
            int numOfDays = 0;
            numOfDays = (Project.endDate - Project.startDate).Days;
            return numOfDays;
        }

        public void LoadGrid(int days)
        {

            for (int i = 0; i < days; i++)
            {

                

                int distance = (int)SystemParameters.PrimaryScreenWidth / days;



                var tempColum = new ColumnDefinition();
                tempColum.Width = new GridLength(distance);



                TimelineGrid.ColumnDefinitions.Add(tempColum);
                CreateLine(i, distance);
                CreateHeader(i);

            }



            NewButton();



        }

        void NewButton()
        {
            var tempButton = new Button();
            tempButton.Height = 20;
            tempButton.Width = 2000;
            TimelineGrid.Children.Add(tempButton);
            Grid.SetColumn(tempButton, 0);
            Grid.SetColumnSpan(tempButton, 4);
        }


        void CreateLine(int lineCount, float lineDistance)
        {
            var tempLine = new Line();
            Grid.SetColumn(tempLine, lineCount);
            tempLine.StrokeThickness = 5;
            tempLine.Stroke = Brushes.Black;


            tempLine.X1 = lineDistance;
            tempLine.X2 = lineDistance; 
            tempLine.Y1 = 0;
            tempLine.Y2 = 10000;

            TimelineGrid.Children.Add(tempLine);







        }

        void CreateHeader(int headerCount)
        {
            var tempLabel = new Label();
            tempLabel.Height = 20;
            tempLabel.Width = 20;
            tempLabel.Content = "test";
            tempLabel.FontSize = 10;
            tempLabel.Foreground = Brushes.Black;

            TimelineGrid.Children.Add(tempLabel);
            Grid.SetColumn(tempLabel, headerCount);
            tempLabel.VerticalAlignment = VerticalAlignment.Top;
            tempLabel.HorizontalAlignment = HorizontalAlignment.Center;
        }
    }
}


