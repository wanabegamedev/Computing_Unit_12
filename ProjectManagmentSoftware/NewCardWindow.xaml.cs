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

            
        }


        


        private void CreateCardButton_Click(object sender, RoutedEventArgs e)
        {
            string tempDescription;


            //checks if description is null, if so sets tempDescription to an empty string
            if (Description_InputBox == null)
            {
                tempDescription = " ";
            }
            else
            {
                tempDescription = Description_InputBox.Text;
            }


            var newCard = new Card(TitleInputBox.Text, Convert.ToDateTime(StartDatePicker.SelectedDate), Convert.ToDateTime(EndDatePicker.SelectedDate), tempDescription);
            //adds card to project's card list
            Project.cards.Add(newCard);
            newCard.SetState(State.todo);
            currentKanbanBoard.CreateVisualCard();
            this.Close();
           

        }
    }
}
