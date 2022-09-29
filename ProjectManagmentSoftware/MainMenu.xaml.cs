using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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




            //sets project variables
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
            //selects today's date as the default date in the date pickers
            projectStartDateTextBox.SelectedDate = DateTime.Today;
            projectEndDateTextBox.SelectedDate = DateTime.Today;



        }


        //validation for the date pickers
        private void projectStartDateTextBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (projectStartDateTextBox.SelectedDate < DateTime.Today)
            {
                MessageBox.Show("Error: date can not be less then today");
                projectStartDateTextBox.SelectedDate = DateTime.Today;


            }

        }

        private void projectEndDateTextBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (projectEndDateTextBox.SelectedDate < projectStartDateTextBox.SelectedDate)
            {
                MessageBox.Show("Error: date can not be less then start date");
                projectEndDateTextBox.SelectedDate = projectStartDateTextBox.SelectedDate;
            }

        }

        private void LoadFilesGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var rowCount = 0;
            List<Button> projectButtons = new List<Button>();


            projectButtons = FileLoad.LoadProjects();

            foreach (Button button in projectButtons)
            {
                LoadFilesGrid.Children.Add(button);
                Grid.SetColumn(button, 0);
                Grid.SetRow(button, rowCount);
                LoadFilesGrid.RowDefinitions.Add(new RowDefinition());


                //sets button click event to loading the project data from the file
                button.Click += LoadSavedFile;

                rowCount++;
            }

        }

        void LoadSavedFile(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

           var projectDetails = button.Tag as List<string>;


            //set project start and end date
            Project.startDate = Convert.ToDateTime(projectDetails[0]);
            Project.endDate = Convert.ToDateTime(projectDetails[1]);

            //sets project name
            Project.projectName = projectDetails[2];

            //load kanbanboard window
            LoadProject();
        }
    }
}
