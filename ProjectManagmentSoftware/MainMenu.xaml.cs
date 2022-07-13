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
using System.ComponentModel;
using System.Windows.Forms;

namespace ProjectManagmentSoftware
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {


        // Minimize to system tray when application is closed.
        

        public MainMenu()
        {
            InitializeComponent();
            
        }
        //handles sending the application to tray
        bool exitButtonClose;
        protected override void OnClosing(CancelEventArgs e)
        {

            if (exitButtonClose)
            {

                // setting cancel to true will cancel the close request
                // so the application is not closed

                TrayHandler.CreateNotifyIcon();
                TrayHandler.GetWindow(this);
                e.Cancel = true;

                Hide();
            }
           


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            




            Project.projectName = ProjectNameTextBox.Text;
            Project.startDate = Convert.ToDateTime(projectStartDateTextBox.Text);
            Project.endDate = Convert.ToDateTime(projectEndDateTextBox.Text);


            

            exitButtonClose = false;
            LoadProject();
            CloseMainMenu();


        }



        public void LoadProject()
        {
            var kanbanBoard = new KanbanBoard();
            kanbanBoard.Show();
            kanbanBoard.LoadProjectDetails();

        }

        void CloseMainMenu()
        {
            this.Close();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            projectStartDateTextBox.SelectedDate = DateTime.Today;
            projectEndDateTextBox.SelectedDate= DateTime.Today;

            
        }

        private void projectStartDateTextBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (projectStartDateTextBox.SelectedDate < DateTime.Today)
            {
                System.Windows.MessageBox.Show("Error: date can not be less then today");
                projectStartDateTextBox.SelectedDate = DateTime.Today;

                
            }

        }

        private void projectEndDateTextBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (projectEndDateTextBox.SelectedDate < projectStartDateTextBox.SelectedDate)
            {
                System.Windows.MessageBox.Show("Error: date can not be less then start date");
                projectEndDateTextBox.SelectedDate = projectStartDateTextBox.SelectedDate;
            }
            
        }



       


        //VAR projectName
        //VAR startDate
        //VAR endDate

        //projectName = projectNameTextBox
        //startDate = projectStartDateTextBox
        //endDate = projectEndDateTextBox
        //IF startDate !< CurrentDay THEN
        //startDate = projectStartDateTextBox
        //ELSE
        //PRINT “Error Date is invalid please re enter the date”
        //RETURN
        //IF startDate !> endDate AND startDate != endDate THEN
        //startDate = projectStartDateTextBox
        //ELSE
        //PRINT “Error Date is invalid please re enter the date”
        //RETURN

        //IF endDate !< startDate AND endDate != startDate THEN
        //endDate = projectEndDateTextBox
        //ELSE
        //PRINT “Error Date is invalid please re enter the date”
        //RETURN

        //Copy name from projectName to the new projects name field
        //Copy start date from startDate to the new projects startDate field
        //Copy end date from endDate to the new projects endDate field


        //Open Kanban view window
        //Close main menu


    }
}
