using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for NewCardWindow.xaml
    /// </summary>
    public partial class NewCardWindow : Window
    {

        KanbanBoard currentKanbanBoard;
    
        public NewCardWindow(KanbanBoard kanbanBoard)
        {
            InitializeComponent();
            currentKanbanBoard = kanbanBoard;
        }


        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //sets start and end dates for date pickers
            StartDatePicker.DisplayDateStart = Project.startDate;
            StartDatePicker.DisplayDateEnd = Project.endDate;
            EndDatePicker.DisplayDateStart = Project.startDate;
            EndDatePicker.DisplayDateEnd = Project.endDate;

            StartDatePicker.SelectedDateChanged += StartDateValidation;
            EndDatePicker.SelectedDateChanged += EndDateValidation;





        }


      


        private void CreateCardButton_Click(object sender, RoutedEventArgs e)
        {
            string tempDescription;
            bool tempNotificationCheck;


            //checks if description is null, if so sets tempDescription to an empty string
            if (Description_InputBox == null)
            {
                tempDescription = " ";
            }
            else
            {
                tempDescription = Description_InputBox.Text;
            }

            //used to check the state of the tick box
            if (NotificationEnabledCheck.IsChecked == true)
            {
                tempNotificationCheck = true;

            }
            else
            {
                tempNotificationCheck = false;
            }


            

            var newCard = new Card(TitleInputBox.Text, Convert.ToDateTime(StartDatePicker.SelectedDate), Convert.ToDateTime(EndDatePicker.SelectedDate), tempDescription, tempNotificationCheck);
            //adds card to project's card list
            Project.cards.Add(newCard);
            newCard.SetState(State.todo);
            currentKanbanBoard.CreateVisualCard();
            this.Close();
           

        }



        //----Start of validation for NewCardWindow----
        void StartDateValidation(object sender, SelectionChangedEventArgs e)
        {
            if (StartDatePicker.SelectedDate > Project.endDate)
            {
                MessageBox.Show("Error: Date can not be more than project end date");
                StartDatePicker.SelectedDate = Project.endDate;
            }
            else if(StartDatePicker.SelectedDate < Project.startDate)
            {
                MessageBox.Show("Error: Date can not be less than project start date");
                StartDatePicker.SelectedDate = Project.startDate;
            }
            else if(StartDatePicker.SelectedDate > EndDatePicker.SelectedDate)
            {
                MessageBox.Show("Error: Date can not be more than end date");
                StartDatePicker.SelectedDate = EndDatePicker.SelectedDate;
            }
        }

        void EndDateValidation(object sender, SelectionChangedEventArgs e)
        {
            if (EndDatePicker.SelectedDate > Project.endDate)
            {
                MessageBox.Show("Error: Date can not be more than project end date");
                EndDatePicker.SelectedDate = Project.endDate;
            }
            else if (StartDatePicker.SelectedDate < Project.startDate)
            {
                MessageBox.Show("Error: Date can not be less than project start date");
                EndDatePicker.SelectedDate = Project.startDate;
            }
            else if (EndDatePicker.SelectedDate < StartDatePicker.SelectedDate)
            {
                MessageBox.Show("Error: Date can not be less then start date");
                EndDatePicker.SelectedDate = StartDatePicker.SelectedDate;
            }
        }
    }
}
