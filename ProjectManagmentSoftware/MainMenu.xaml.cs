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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        
        string projectName;
        DateTime startDate;
        DateTime endDate;

    
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            projectName = ProjectNameTextBox.Text;

            startDate = Convert.ToDateTime(projectStartDateTextBox.Text);
            endDate = Convert.ToDateTime(projectEndDateTextBox.Text);


            if (startDate < DateTime.Now & startDate != endDate )
            {
                startDate = Convert.ToDateTime(projectStartDateTextBox.Text);
            }
            else
            {
                MessageBox.Show("Invalid Date: please re-enter start date");
                return;
            }

            if (endDate > DateTime.Now & endDate != startDate)
            {
                endDate = Convert.ToDateTime(projectEndDateTextBox.Text);
            }
            else
            {
                MessageBox.Show("Invalid Date: please re-enter end date");
                return;
            }


            var newProject = new Project(projectName, startDate, endDate);


            LoadProject(newProject);
            CloseMainMenu();


        }



        public void LoadProject(Project project)
        {
            var kanbanBoard = new KanbanBoard();
            kanbanBoard.Show();
            kanbanBoard.LoadProjectDetails(project);

        }

        void CloseMainMenu()
        {
            this.Close();
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
